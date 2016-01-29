namespace easypay.swisscom.com
{
    using System.Collections.Generic;

    /// <summary>
    ///     The response message list object represents the server messages, which can be extracted and interpreted  by the
    ///     client.
    /// </summary>
    public class PaymentResponse
    {
        public List<MessageItem> Messages { get; set; }
        public bool Success { get; set; }
    }
}