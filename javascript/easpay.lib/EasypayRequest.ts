import moment = require('moment');
import https = require('https');
import Promise = require('bluebird');

import SignatureNS = require("./Signature");
import PaymentResponseNS = require("./IPaymentResponse");
import StatusResponseNS = require("./IStatusResponse");
var Signature = SignatureNS.easypay.Signature;

export module easypay {
    export module EasypayRequest {

        export function getCheckoutPageUrl(config: IEasypayConfig, paymentRequest: any): string {
            var data = JSON.stringify(paymentRequest);
            var encodedData = new Buffer(data).toString('base64');
            var signature = Signature.sign(config.easypaySecret, data);
            var path = "/authorize.jsf?signature=" + encodeURIComponent(signature) + "&checkoutRequestItem=" + encodeURIComponent(encodedData);
            return `${config.protocol}://${config.host}${config.basepath}${path}`;
        }

        export function commitPaymentRequest(paymentId: string, easypayConfig: IEasypayConfig, reqId: string): Promise<PaymentResponseNS.IPaymentResponse> {
            return processRequest(easypayConfig, JSON.stringify({ operation: "COMMIT" }), "PUT", `/payments/${paymentId}`, reqId);
        }

        export function cancelPaymentRequest(paymentId: string, easypayConfig: IEasypayConfig, reqId: string): Promise<PaymentResponseNS.IPaymentResponse> {
            return processRequest(easypayConfig, JSON.stringify({ operation: "CANCEL" }), "PUT", `/payments/${paymentId}`, reqId);
        }

        export function getPaymentStatus(paymentId, easypayConfig, reqId) :Promise<StatusResponseNS.IStatusResponse> {
            return processRequest(easypayConfig, null, "GET", "/payments/" + paymentId, reqId);
        }

        function processRequest(config: IEasypayConfig, data: string, httpMethod: string, path: string, reqId: string): any {
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

        function request(options, requestData): Promise<PaymentResponseNS.IPaymentResponse>{

            return new Promise<PaymentResponseNS.IPaymentResponse>((resolve, reject) => {
                var ret = <PaymentResponseNS.IPaymentResponse>{ success: false };

                try {
                    var request = https.request(options, (result) => {
                        result.setEncoding("utf8");
                        var data = "";

                        result.on("data", (chunk) => {
                            data += chunk;
                        });

                        result.on("end", () => {
                            console.log(data);
                            if (result.statusCode !== 200) {
                                try {
                                    ret.messages = JSON.parse(data)['messages'];
                                } catch (e) {
                                    ret.messages = [];
                                    ret.error = data;
                                }
                                reject(ret);
                            } else {
                                if (data)
                                    resolve(JSON.parse(data) || { success: true });
                                else
                                    resolve(<PaymentResponseNS.IPaymentResponse>{ success: true });
                            }
                        });
                    });

                    // Handle errors
                    request.on("error", (error) => {
                        ret.error = error;
                        reject(ret);
                    });

                    // send the data
                    if (requestData) {
                        request.write(requestData);
                    }

                    request.end();
                } catch (e) {
                    ret.error = e;
                    reject(ret);
                }
            });
        }
    }
}
