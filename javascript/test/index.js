"use strict";
var assert = require('assert');
var SignatureNS = require("../easpay.lib/Signature");
var CheckoutPageRequestNS = require("../easpay.lib/CheckoutPageRequest");
var EasypayRequestNS = require("../easpay.lib/EasypayRequest");
var Signature = SignatureNS.easypay.Signature;
var EasypayRequest = EasypayRequestNS.easypay.EasypayRequest;
var CheckoutPageRequest = CheckoutPageRequestNS.easypay.CheckoutPageRequest;
var config = {
    basepath: "/test-service",
    host: "easypay-test.swisscom.ch",
    merchantId: "test-merchant",
    protocol: "http",
    easypaySecret: "test-secret-987654321"
};
describe('Easypay', function () {
    describe('Signature', function () {
        it('should produce constant hash for predefined hashString', function () {
            var data = Signature.createHashString("PUT", "6KEzivnrMza/LaW7bg5n5A==", "application/vnd.ch.swisscom.easypay.direct.payment+json", "Tue, 19 Jan 2016 13:57:14 +0000", "/payments/F10F6A2D-CB56-4618-B57E-4A186663467D");
            var secret = "KiLgscVNTqAJQ1keGOv_hhKsuf5oftohg17VmncT";
            var signature = Signature.sign(secret, data);
            assert.equal(signature, "DDEVjshw5qN1Bkja9plTuc81/A0=");
        });
        it('should produce constant hash for predefined json', function () {
            var data = JSON.stringify({ key: "test-value", adultContent: false, amount: 50.5 });
            var signature = Signature.sign(config.easypaySecret, data);
            assert.equal(signature, "Zq3zcquJgdNNK/3HhFmVBTNX2+Y=");
        });
    });
    describe('EasypayRequest', function () {
        it('should produce constant base64 string for predefined data', function () {
            var data = '{"title":"My new Phone","description":"Buy your favorite Sony Phone!","duration":0,"durationUnit":null,"promotionAmount":null,"merchantId":"APT01","cancelUrl":"http://localhost:6392/Easypay/Callback?uid=43c2&sid=dc0d&purchase=cancel","errorUrl":"http://localhost:6392/Easypay/Callback?uid=43c2&sid=dc0d&purchase=error","successUrl":"http://localhost:6392/Easypay/Callback?uid=43c2&sid=dc0d&purchase=success","cpServiceId":"xys-323-gh-ff","cpSubscriptionId":"23hkb379oh","cpUserId":"vghv5678","imageUrl":"http://lorempixel.com/300/200","amount":50.5,"paymentInfo":"Test","roaming":false,"adultContent":false,"userAgentOrigin":null,"userSourceIP":null,"contentType":"App","storeSource":"Easypay-City","operation":0,"orderId":null}';
            var encodedData = new Buffer(data).toString('base64');
            assert.equal(encodedData, "eyJ0aXRsZSI6Ik15IG5ldyBQaG9uZSIsImRlc2NyaXB0aW9uIjoiQnV5IHlvdXIgZmF2b3JpdGUgU29ueSBQaG9uZSEiLCJkdXJhdGlvbiI6MCwiZHVyYXRpb25Vbml0IjpudWxsLCJwcm9tb3Rpb25BbW91bnQiOm51bGwsIm1lcmNoYW50SWQiOiJBUFQwMSIsImNhbmNlbFVybCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjM5Mi9FYXN5cGF5L0NhbGxiYWNrP3VpZD00M2MyJnNpZD1kYzBkJnB1cmNoYXNlPWNhbmNlbCIsImVycm9yVXJsIjoiaHR0cDovL2xvY2FsaG9zdDo2MzkyL0Vhc3lwYXkvQ2FsbGJhY2s/dWlkPTQzYzImc2lkPWRjMGQmcHVyY2hhc2U9ZXJyb3IiLCJzdWNjZXNzVXJsIjoiaHR0cDovL2xvY2FsaG9zdDo2MzkyL0Vhc3lwYXkvQ2FsbGJhY2s/dWlkPTQzYzImc2lkPWRjMGQmcHVyY2hhc2U9c3VjY2VzcyIsImNwU2VydmljZUlkIjoieHlzLTMyMy1naC1mZiIsImNwU3Vic2NyaXB0aW9uSWQiOiIyM2hrYjM3OW9oIiwiY3BVc2VySWQiOiJ2Z2h2NTY3OCIsImltYWdlVXJsIjoiaHR0cDovL2xvcmVtcGl4ZWwuY29tLzMwMC8yMDAiLCJhbW91bnQiOjUwLjUsInBheW1lbnRJbmZvIjoiVGVzdCIsInJvYW1pbmciOmZhbHNlLCJhZHVsdENvbnRlbnQiOmZhbHNlLCJ1c2VyQWdlbnRPcmlnaW4iOm51bGwsInVzZXJTb3VyY2VJUCI6bnVsbCwiY29udGVudFR5cGUiOiJBcHAiLCJzdG9yZVNvdXJjZSI6IkVhc3lwYXktQ2l0eSIsIm9wZXJhdGlvbiI6MCwib3JkZXJJZCI6bnVsbH0=");
        });
        it('should produce constant url for predefined paymentRequest', function () {
            var paymentRequest = new CheckoutPageRequest();
            paymentRequest.adultContent = false;
            paymentRequest.amount = "50.55";
            paymentRequest.paymentInfo = "Test";
            var url = EasypayRequest.getCheckoutPageUrl(config, paymentRequest);
            assert.equal(url, "http://easypay-test.swisscom.ch/test-service/authorize.jsf?signature=5eISni20vgCs0SUrIeTs9lLwIgo%3D&checkoutRequestItem=eyJhbW91bnQiOiI1MC41NSIsInBheW1lbnRJbmZvIjoiVGVzdCIsImFkdWx0Q29udGVudCI6ZmFsc2UsInJvYW1pbmciOm51bGwsImNvbnRlbnRUeXBlIjpudWxsLCJzdG9yZVNvdXJjZSI6bnVsbCwidXNlckFnZW50T3JpZ2luIjpudWxsLCJ1c2VyU291cmNlSVAiOm51bGwsIm9wZXJhdGlvbiI6bnVsbCwib3JkZXJJZCI6bnVsbCwidGl0bGUiOm51bGwsImRlc2NyaXB0aW9uIjpudWxsLCJtZXJjaGFudElkIjpudWxsLCJjYW5jZWxVcmwiOm51bGwsImVycm9yVXJsIjpudWxsLCJzdWNjZXNzVXJsIjpudWxsLCJjcFNlcnZpY2VJZCI6bnVsbCwiY3BTdWJzY3JpcHRpb25JZCI6bnVsbCwiY3BVc2VySWQiOm51bGwsImltYWdlVXJsIjpudWxsLCJkdXJhdGlvbiI6bnVsbCwiZHVyYXRpb25Vbml0IjpudWxsLCJwcm9tb3Rpb25BbW91bnQiOm51bGx9");
        });
    });
});
