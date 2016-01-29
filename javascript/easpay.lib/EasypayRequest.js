"use strict";
var moment = require('moment');
var https = require('https');
var Promise = require('bluebird');
var SignatureNS = require("./Signature");
var Signature = SignatureNS.easypay.Signature;
var easypay;
(function (easypay) {
    var EasypayRequest;
    (function (EasypayRequest) {
        function getCheckoutPageUrl(config, paymentRequest) {
            var data = JSON.stringify(paymentRequest);
            var encodedData = new Buffer(data).toString('base64');
            var signature = Signature.sign(config.easypaySecret, data);
            var path = "/authorize.jsf?signature=" + encodeURIComponent(signature) + "&checkoutRequestItem=" + encodeURIComponent(encodedData);
            return config.protocol + "://" + config.host + config.basepath + path;
        }
        EasypayRequest.getCheckoutPageUrl = getCheckoutPageUrl;
        function commitPaymentRequest(paymentId, easypayConfig, reqId) {
            return processRequest(easypayConfig, JSON.stringify({ operation: "COMMIT" }), "PUT", "/payments/" + paymentId, reqId);
        }
        EasypayRequest.commitPaymentRequest = commitPaymentRequest;
        function cancelPaymentRequest(paymentId, easypayConfig, reqId) {
            return processRequest(easypayConfig, JSON.stringify({ operation: "CANCEL" }), "PUT", "/payments/" + paymentId, reqId);
        }
        EasypayRequest.cancelPaymentRequest = cancelPaymentRequest;
        function getPaymentStatus(paymentId, easypayConfig, reqId) {
            return processRequest(easypayConfig, null, "GET", "/payments/" + paymentId, reqId);
        }
        EasypayRequest.getPaymentStatus = getPaymentStatus;
        function processRequest(config, data, httpMethod, path, reqId) {
            var date = moment().utc().format("ddd, DD MMM YYYY HH:mm:ss ZZ");
            var contentType = httpMethod === "GET" ? "" : "application/vnd.ch.swisscom.easypay.direct.payment+json";
            var options = {
                host: config.host,
                path: config.basepath + path,
                method: httpMethod,
                headers: {
                    "X-SCS-Date": date,
                    "X-Request-Id": reqId || new Date().getTime(),
                    "X-Merchant-Id": config.merchantId,
                    "Accept": "application/vnd.ch.swisscom.easypay.message.list+json",
                    "X-CE-Client-Specification-Version": "1.1",
                    "Date": date
                }
            };
            var md5Hash = "";
            if (data) {
                md5Hash = Signature.hashData(data);
                options.headers["Content-MD5"] = md5Hash;
            }
            var hashString = Signature.createHashString(httpMethod, md5Hash, contentType, date, path);
            var signature = Signature.sign(config.easypaySecret, hashString);
            if (contentType) {
                options.headers["Content-Type"] = contentType;
            }
            options.headers["X-SCS-Signature"] = signature;
            return request(options, data);
        }
        function request(options, requestData) {
            return new Promise(function (resolve, reject) {
                var ret = { success: false };
                try {
                    var request = https.request(options, function (result) {
                        result.setEncoding("utf8");
                        var data = "";
                        result.on("data", function (chunk) {
                            data += chunk;
                        });
                        result.on("end", function () {
                            console.log(data);
                            if (result.statusCode !== 200) {
                                try {
                                    ret.messages = JSON.parse(data)['messages'];
                                }
                                catch (e) {
                                    ret.messages = [];
                                    ret.error = data;
                                }
                                reject(ret);
                            }
                            else {
                                if (data)
                                    resolve(JSON.parse(data) || { success: true });
                                else
                                    resolve({ success: true });
                            }
                        });
                    });
                    request.on("error", function (error) {
                        ret.error = error;
                        reject(ret);
                    });
                    if (requestData) {
                        request.write(requestData);
                    }
                    request.end();
                }
                catch (e) {
                    ret.error = e;
                    reject(ret);
                }
            });
        }
    })(EasypayRequest = easypay.EasypayRequest || (easypay.EasypayRequest = {}));
})(easypay = exports.easypay || (exports.easypay = {}));
