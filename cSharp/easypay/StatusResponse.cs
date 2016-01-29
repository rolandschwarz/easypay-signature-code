namespace easypay.swisscom.com
{
    public class StatusResponse
    {
        public string OrderID { get; set; }
        public string PaymentInfo { get; set; }
        public bool IsSilentAuthenticated { get; set; }
        public string Amount { get; set; }
        public string Msisdn { get; set; }
        public string Status { get; set; }
        public string FormattedMsisdn { get; set; }
    }
}
