import DirectPaymentRequestNS = require("./DirectPaymentRequest");

var DirectPaymentRequest = DirectPaymentRequestNS.easypay.DirectPaymentRequest;

export module easypay {
    /**
    * The CheckoutPage (Authorizepage) is used both for One-Time charge and for Subscription authorization. Once the
    * Subscription authorization is accepted by the customer via CheckoutPage, the content partner is allowed
    * to renew the Subscription.The customer can cancel the Subscription authorization at any time either on
    * Swisscom or on content partner side.
    **/
    export class CheckoutPageRequest extends DirectPaymentRequest {
        /**
        * Title of the content.
        * @example Online Newspaper
		* @returns {string}
        **/
        public title: string;

        /**
        * Short description of the subscription/one-time charge
        * @example Read your favorite newspaper every day everywhere!
		* @returns {string}
        **/
        public description: string;

        /**
        * The duration of the service subscription
        * @example 1
		* @returns {number}
        **/
        public duration: number;

        /**
        * The duration unit of the service subscription
        * @example (WEEK|MONTH)
		* @returns {string}
        **/
        public durationUnit: string;

        /**
        * The Promotion Amount of the Service.
        * <remarks>must NOT be negative value, if negative value is sent it will be ignored and the amount will be charged instead.
        * If value is positive and less or equal as amount it will be charged instead of amount.</remarks>
        * @example 2.25
		* @returns {number}
        **/
        public promotionAmount: number;

        /**
        * The merchant id given by Swisscom
        * @example CH011
        **/
        public merchantId: string;

        /**
        * The cancel URL, which will be called, if the user cancels the authorization request on the COP.
        * @example http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=cancel
		* @returns {string}
        **/
        public cancelUrl: string;

        /**
        * The error URL, which will be called, if some error/problem occur during the authorization process on the COP.
        * @example http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=error
		* @returns {string}
        **/
        public errorUrl: string;

        /**
        * The success back URL, which will be called, if the user proceed the authorization request on the COP with success.
        * @example http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=success
		* @returns {string}
        **/
        public successUrl: string;

        /**
        * The service Id on content partner side. This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
        * @example xys-323-gh-ff
		* @returns {string}
        **/
        public cpServiceId: string;

        /**
        * The unique subscription Id on content partner side.This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
        * @example vghv5678
		* @returns {string}
        **/
        public cpSubscriptionId: string;

        /**
        * The unique user Id on content partner side. This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
        * @example user-5
		* @returns {string}
        **/
        public cpUserId: string;

        /**
        * The URL of the dynamic image which will be embedded in the COP.
        * <remarks>Special feature is needed !</remarks>
        * @example http://lorempixel.com/300/200
		* @returns {string}
        **/
        public imageUrl: string;

        constructor() {
            super();
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
    }
}
