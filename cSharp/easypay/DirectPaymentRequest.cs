namespace easypay.swisscom.com
{
    /// <summary>
    ///     The direct payment is a kind of one-charge-event-payment. It does not need any management on the charging-engine
    ///     side.
    /// </summary>
    public class DirectPaymentRequest : IDirectPaymentRequest
    {
        /// <summary>
        ///     The amount of the service.
        ///     <b>Note:</b> This amount must not contain any tax, such as transaction fee.
        /// </summary>
        /// <returns> string amount </returns>
        public virtual string Amount { get; set; }

        /// <summary>
        ///     The payment info of the service (also known as billing text).
        ///     <b>Note:</b> The paymentInfo will be placed on the end user invoice bill.
        /// </summary>
        /// <returns> String </returns>
        public virtual string PaymentInfo { get; set; }

        /// <summary>
        ///     Flag which marks the service as roaming (true) or none roaming (false).
        ///     This means that the customer will be checked for roaming.
        ///     If the customer has roaming and the flag was set to true, then the payment
        ///     will be blocked. If the customer has roaming and the flag was set to false,
        ///     then the payment will not be blocked.
        /// </summary>
        /// <returns> boolean isRoaming </returns>
        public virtual bool Roaming { get; set; }

        /// <summary>
        ///     Flag which marks the service as adult (true) or non adult (false).
        ///     This means that the customer will be checked, if he/she is allowed to consume adult content.
        /// </summary>
        /// <returns> boolean </returns>
        public virtual bool AdultContent { get; set; }

        /// <summary>
        ///     The UserAgent of the end customer
        ///     e.g. Mozilla/5.0 (Linux; U; Android 2.2.1; en-us; Nexus One Build/FRG83)
        /// </summary>
        /// <returns> String </returns>
        public virtual string UserAgentOrigin { get; set; }

        /// <summary>
        ///     The end user source IP
        /// </summary>
        /// <returns> String SourceIP </returns>
        public virtual string UserSourceIP { get; set; }

        /// <summary>
        ///     The content type of the provided content to end customer.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///     The source store behind the aggregator content partner.
        /// </summary>
        public string StoreSource { get; set; }

        /// <summary>
        ///     Allows to commit/ reject the payment or to refund the amount.
        ///     The amount to be refund must be the same or less as the initial payment.
        /// </summary>
        /// <returns> Operation Operation </returns>
        public virtual string Operation { get; set; }

        /// <summary>
        ///     The order id which is printed on end user invoice bill.
        /// </summary>
        /// <returns> String OrderId </returns>
        public virtual string OrderId { get; set; }
    }
}