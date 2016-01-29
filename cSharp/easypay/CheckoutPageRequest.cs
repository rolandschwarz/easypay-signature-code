namespace easypay.swisscom.com
{
    /// <summary>
    ///     The CheckoutPage (Authorizepage) is used both for One-Time charge and for Subscription authorization. Once the
    ///     Subscription authorization is accepted by the customer via CheckoutPage, the content partner is allowed
    ///     to renew the Subscription.The customer can cancel the Subscription authorization at any time either on
    ///     Swisscom or on content partner side.
    /// </summary>
    public class CheckoutPageRequest : DirectPaymentRequest, ICheckoutPageRequest
    {
        /// <summary>
        ///     Title of the content.
        ///     <example>Online Newspaper</example>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Short description of the subscription/one-time charge
        ///     <example>Read your favorite newspaper every day everywhere!</example>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The duration of the service subscription
        ///     <example>1</example>
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        ///     The duration unit of the service subscription
        ///     <example>(WEEK|MONTH)</example>
        /// </summary>
        public string DurationUnit { get; set; }

        /// <summary>
        ///     The Promotion Amount of the Service.
        ///     <remarks>
        ///         must NOT be negative value, if negative value is sent it will be ignored and the amount will be charged
        ///         instead.
        ///         If value is positive and less or equal as amount it will be charged instead of amount.
        ///     </remarks>
        ///     <example>2.25</example>
        /// </summary>
        public string PromotionAmount { get; set; }

        /// <summary>
        ///     The merchant id given by Swisscom
        ///     <example>CH011</example>
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        ///     The cancel URL, which will be called, if the user cancels the authorization request on the COP.
        ///     <example>http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=cancel</example>
        /// </summary>
        public string CancelUrl { get; set; }

        /// <summary>
        ///     The error URL, which will be called, if some error/problem occur during the authorization process on the COP.
        ///     <example>http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=error</example>
        /// </summary>
        public string ErrorUrl { get; set; }

        /// <summary>
        ///     The success back URL, which will be called, if the user proceed the authorization request on the COP with success.
        ///     <example>http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=success</example>
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        ///     The service Id on content partner side. This is an optional parameter.It is recommended to send it anyway, as it
        ///     can be very helpful for debugging.
        ///     <example>xys-323-gh-ff</example>
        /// </summary>
        public string CpServiceId { get; set; }

        /// <summary>
        ///     The unique subscription Id on content partner side.This is an optional parameter.It is recommended to send it
        ///     anyway, as it can be very helpful for debugging.
        ///     <example>vghv5678</example>
        /// </summary>
        public string CpSubscriptionId { get; set; }

        /// <summary>
        ///     The unique user Id on content partner side. This is an optional parameter.It is recommended to send it anyway, as
        ///     it can be very helpful for debugging.
        ///     <example>user-5</example>
        /// </summary>
        public string CpUserId { get; set; }

        /// <summary>
        ///     The URL of the dynamic image which will be embedded in the COP.
        ///     <remarks>Special feature is needed !</remarks>
        ///     <example>http://lorempixel.com/300/200</example>
        /// </summary>
        public string ImageUrl { get; set; }
    }
}