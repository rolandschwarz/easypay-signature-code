namespace easypay.swisscom.com
{
    public interface IDirectPaymentRequest
    {
        bool AdultContent { get; set; }
        string Amount { get; set; }
        string Operation { get; set; }
        string OrderId { get; set; }
        string PaymentInfo { get; set; }
        bool Roaming { get; set; }
        string UserAgentOrigin { get; set; }
        string UserSourceIP { get; set; }
        string ContentType { get; set; }
        string StoreSource { get; set; }
    }
}