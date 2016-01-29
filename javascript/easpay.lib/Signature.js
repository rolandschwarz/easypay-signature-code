"use strict";
var crypto = require('crypto');
var easypay;
(function (easypay) {
    var Signature = (function () {
        function Signature() {
        }
        Signature.createHashString = function (httpRequestMethod, contentHash, contentType, date, path) {
            return httpRequestMethod + "\n" + contentHash + "\n" + contentType + "\n" + date + "\n" + path;
        };
        Signature.hashData = function (data) {
            var md5Sum = crypto.createHash("md5");
            md5Sum.setEncoding("base64");
            md5Sum.write(data);
            md5Sum.end();
            var md5Hash = md5Sum.read();
            return md5Hash;
        };
        Signature.sign = function (key, data) {
            var algorithm = "sha1";
            var hmac = crypto.createHmac(algorithm, key);
            hmac.setEncoding("base64");
            hmac.write(data);
            hmac.end();
            var shaHash = hmac.read();
            return shaHash;
        };
        return Signature;
    })();
    easypay.Signature = Signature;
})(easypay = exports.easypay || (exports.easypay = {}));
