namespace easypay.swisscom.com
{
    public interface ICheckoutPageRequest : IDirectPaymentRequest
    {
        string CancelUrl { get; set; }
        string CpServiceId { get; set; }
        string CpSubscriptionId { get; set; }
        string CpUserId { get; set; }
        string Description { get; set; }
        int Duration { get; set; }
        string DurationUnit { get; set; }
        string ErrorUrl { get; set; }
        string ImageUrl { get; set; }
        string MerchantId { get; set; }
        string PromotionAmount { get; set; }
        string SuccessUrl { get; set; }
        string Title { get; set; }
    }
}