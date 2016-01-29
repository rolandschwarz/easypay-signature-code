export module easypay {

    /**
	* The direct payment is a kind of one-charge-event-payment. It does not need any management on the charging-engine side.
    **/
    export class DirectPaymentRequest {

        /**
        * The amount of the service.
        * <b>Note:</b> This amount must not contain any tax, such as transaction fee.
		* @returns {string} amount
        **/
        public amount: string;

        /**
        * The payment info of the service (also known as billing text).
        * <b>Note:</b> The paymentInfo will be placed on the end user invoice bill.
		* @returns {string}
        **/
        public paymentInfo: string;

        /**
        * Flag which marks the service as roaming (true) or none roaming (false).
        * This means that the customer will be checked for roaming.
        * If the customer has roaming and the flag was set to true, then the payment
        * will be blocked. If the customer has roaming and the flag was set to false,
        * then the payment will not be blocked.
		* @returns {boolean} isRoaming
        **/

        public roaming: boolean;

        /**
        * Flag which marks the service as adult (true) or non adult (false).
        * This means that the customer will be checked, if he/she is allowed to consume adult content.
		* @returns {boolean}
        **/
        public adultContent: boolean;

        /**
        * The UserAgent of the end customer
        * e.g. Mozilla/5.0 (Linux; U; Android 2.2.1; en-us; Nexus One Build/FRG83)
		* @returns {string}
        **/
        public userAgentOrigin: string;

        /**
        * The end user source IP
		* @returns {string}
        **/
        public userSourceIP: string;

        /**
        * The content type of the provided content to end customer.
		* @returns {string}
        **/
        public contentType: string;

        /**
        * The source store behind the aggregator content partner.
		* @returns {string}
        **/
        public storeSource: string;

        /**
        * Allows to commit/ reject the payment or to refund the amount.
        * The amount to be refund must be the same or less as the initial payment.
		* @returns {string}
        **/
        public operation: string;

        /**
        * The order id which is printed on end user invoice bill.
		* @returns {string}
        **/
        public orderId: string;

        constructor() {
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
    }
}
