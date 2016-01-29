"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var DirectPaymentRequestNS = require("./DirectPaymentRequest");
var DirectPaymentRequest = DirectPaymentRequestNS.easypay.DirectPaymentRequest;
var easypay;
(function (easypay) {
    var CheckoutPageRequest = (function (_super) {
        __extends(CheckoutPageRequest, _super);
        function CheckoutPageRequest() {
            _super.call(this);
            this.amount = null;
            this.paymentInfo = null;
            this.title = null;
            this.description = null;
            this.adultContent = null;
            this.roaming = null;
            this.merchantId = null;
            this.cancelUrl = null;
            this.errorUrl = null;
            this.successUrl = null;
            this.cpServiceId = null;
            this.cpSubscriptionId = null;
            this.cpUserId = null;
            this.imageUrl = null;
            this.contentType = null;
            this.storeSource = null;
            this.duration = null;
            this.durationUnit = null;
            this.promotionAmount = null;
            this.userAgentOrigin = null;
            this.userSourceIP = null;
            this.operation = null;
            this.orderId = null;
        }
        return CheckoutPageRequest;
    })(DirectPaymentRequest);
    easypay.CheckoutPageRequest = CheckoutPageRequest;
})(easypay = exports.easypay || (exports.easypay = {}));
