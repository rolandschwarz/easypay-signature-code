namespace easypay.web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using System.Web.UI.WebControls;
    using easypay.swisscom.com;
    using easypay.web.Models;

    public class EasypayController : Controller
    {
        private const string BaseReturnUrl = "http://localhost:6392/Easypay/Callback";
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

        // GET: Easypay
        public ActionResult Index()
        {
            return View("BuyPage");
        }

        // GET: Easypay/Callback?paymentId=xyz&purchase=success
        public async Task<ActionResult> Callback(string paymentId, string purchase)
        {
            var msg = new Response { Title = "Technical Problem", Message = "", PaymentId = paymentId};

            if (purchase == "success")
            {
                var reqId = msg.ReqId = Guid.NewGuid().ToString();

                var easypayConfig = GetEasypayConfig();
                easypayConfig.Basepath = "/ce-rest-service";

                try
                {
                    var message = await EasypayRequest.CommitPaymentRequest(paymentId, easypayConfig, reqId);

                    if (message.Success)
                        msg.Title = "Success";
                    else
                        msg.Message = message.Messages.Select(x => string.Join(", ", x.Code, x.Field, x.RequestId)).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    msg.Message = ex.Message;

                }
            }
            else
            {
                msg.Title = "Purchase terminated with state: '" + purchase + "'";
            }

            return View("ResponsePage", msg);
        }

        // GET: Easypay
        public async Task<ActionResult> Status(string paymentId, string reqId)
        {
            var easypayConfig = GetEasypayConfig();
            easypayConfig.Basepath = "/ce-rest-service";

            var message = await EasypayRequest.GetPaymentStatus(paymentId, easypayConfig, reqId);
            var msg = new Response { Title = "STATUS: " + message.Status, Message = "Msisdn: " + message.FormattedMsisdn + ", OrderID: " + message.OrderID + ", Payment Info: " + message.PaymentInfo + ", CHF " + message.Amount, PaymentId = paymentId, ReqId = reqId };

            return View("ResponsePage", msg);
        }

        [HttpPost]
        public ActionResult Buy()
        {
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
            return Redirect(url.ToString());
        }
    }
}