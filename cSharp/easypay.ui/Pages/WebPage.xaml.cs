namespace easypay.ui.Pages
{
    using System.Diagnostics;
    using easypay.swisscom.com;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    /// <summary>
    /// Interaction logic for WebPage.xaml
    /// </summary>
    public partial class WebPage : Page
    {
        private const string BaseReturnUrl = "http://www.mocky.io/v2/566aec731000007826990c05";
        private const string MerchantId = "LTC001";

        private static EasypayConfig GetEasypayConfig()
        {
            return new EasypayConfig
            {
                Host = "easypay-staging.swisscom.ch",
                MerchantId = MerchantId,
                EasypaySecret = "KiLgscVNTqAJQ1keGOv_hhKsuf5oftohg17VmncT"
            };
        }

        public WebPage()
        {
            InitializeComponent();

            Trace.WriteLine("Webpage loaded");

            var paymentRequest = new CheckoutPageRequest();
            paymentRequest.AdultContent = false;
            paymentRequest.Amount = "5.50";
            paymentRequest.PaymentInfo = "Test";
            paymentRequest.Title = "My new Phone";
            paymentRequest.Description = "Buy your favorite Sony Phone!";
            paymentRequest.AdultContent = false;
            paymentRequest.Roaming = false;
            paymentRequest.MerchantId = MerchantId;
            paymentRequest.CancelUrl = BaseReturnUrl + "?uid=43c2&sid=dc0d&purchase=cancel";
            paymentRequest.ErrorUrl = BaseReturnUrl + "?uid=43c2&sid=dc0d&purchase=error";
            paymentRequest.SuccessUrl = BaseReturnUrl + "?uid=43c2&sid=dc0d&purchase=success";
            paymentRequest.CpServiceId = "xys-323-gh-ff";
            paymentRequest.CpSubscriptionId = "23hkb379oh";
            paymentRequest.CpUserId = "vghv5678";
            paymentRequest.ImageUrl = "http://lorempixel.com/300/200";
            paymentRequest.ContentType = "App";
            paymentRequest.StoreSource = "Easypay-City";

            var easypayConfig = GetEasypayConfig();
            easypayConfig.Basepath = "/charging-engine-checkout";
            var url = EasypayRequest.GetCheckoutPageUrl(easypayConfig, paymentRequest);
            checkoutView.Navigate(url);

            checkoutView.Navigated += CheckoutView_Navigated;
        }

        private void CheckoutView_Navigated(object sender, NavigationEventArgs e)
        {
            if (checkoutView.Source != null && checkoutView.Source.AbsoluteUri.StartsWith(BaseReturnUrl))
            {
                var parameters = QueryParse(checkoutView.Source.Query);
                var paymentId = parameters["paymentId"];
                var purchase = parameters["purchase"];

                if (purchase == "success")
                {
                    var reqId = Guid.NewGuid().ToString();

                    var easypayConfig = GetEasypayConfig();
                    easypayConfig.Basepath = "/ce-rest-service";

                    EasypayRequest.CommitPaymentRequest(paymentId, easypayConfig, reqId).ContinueWith((message) =>
                    {
                        if (message.Result.Success)
                            MessageBox.Show("Success");
                        else
                            MessageBox.Show(message.Result.Messages.Select(x => string.Join(", ", x.Code, x.Field, x.RequestId)).FirstOrDefault());
                    });
                }
                else if (purchase == "cancel")
                {
                    MessageBox.Show("User cancelled purchase");
                }
                else
                {
                    MessageBox.Show("Error in purchase");
                }
            }
        }

        private static Dictionary<string, string> QueryParse(string url)
        {
            return url.Substring(url.IndexOf('?') + 1).Split('&').Select(qPair => qPair.Split('=')).ToDictionary(qVal => qVal[0], qVal => Uri.UnescapeDataString(qVal[1]));
        }
    }
}
