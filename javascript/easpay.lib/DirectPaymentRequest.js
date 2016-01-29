"use strict";
var easypay;
(function (easypay) {
    var DirectPaymentRequest = (function () {
        function DirectPaymentRequest() {
            this.amount = null;
            this.paymentInfo = null;
            this.adultContent = null;
            this.roaming = null;
            this.contentType = null;
            this.storeSource = null;
            this.userAgentOrigin = null;
            this.userSourceIP = null;
            this.operation = null;
            this.orderId = null;
        }
        return DirectPaymentRequest;
    })();
    easypay.DirectPaymentRequest = DirectPaymentRequest;
})(easypay = exports.easypay || (exports.easypay = {}));
