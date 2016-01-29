namespace easypay.swisscom.com
{
    /// <summary>
    ///     The response message object represents a server message, which can be extracted and interpreted by the client.
    /// </summary>
    public class MessageItem
    {
        /// <summary>
        ///     Name of the field from the request data model that this message is associated with
        /// </summary>
        public virtual string Field { get; set; }

        /// <summary>
        ///     Symbolic error code identifying the type of error reported by this message
        /// </summary>
        /// <seealso cref="Error" />
        public virtual string Code { get; set; }

        /// <summary>
        ///     The request id, sent within the XRequest-Id http header.
        /// </summary>
        public virtual string RequestId { get; set; }

        /// <summary>
        ///     Localized message describing the nature of the problem reported
        /// </summary>
        public virtual string Message { get; set; }
    }
}