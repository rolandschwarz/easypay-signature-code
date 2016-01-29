namespace easypay.test
{
    using System.Text;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using easypay.swisscom.com;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    [TestClass]
    public class EasypayTests
    {
        readonly EasypayConfig _config = new EasypayConfig
        {
            Basepath = "/test-service",
            Host = "easypay-test.swisscom.ch",
            MerchantId = "test-merchant",
            EasypaySecret = "test-secret-987654321"
        };
        
        [TestMethod]
        public void TestSignature()
        {
            var data = JsonConvert.SerializeObject(new { Key = "test-value", AdultContent = false, Amount = 50.5 }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }); // "{ \"key\": \"test-value\" }";
            var signature = Signature.Sign(Encoding.UTF8.GetBytes(_config.EasypaySecret), Encoding.UTF8.GetBytes(data));

            Assert.AreEqual(Convert.ToBase64String(signature), "Zq3zcquJgdNNK/3HhFmVBTNX2+Y=");
        }

        [TestMethod]
        public void TestHashSignature()
        {
            var data = Signature.CreateHashString("PUT", "6KEzivnrMza/LaW7bg5n5A==", "application/vnd.ch.swisscom.easypay.direct.payment+json", "Tue, 19 Jan 2016 13:57:14 +0000", "/payments/F10F6A2D-CB56-4618-B57E-4A186663467D");
            var signature = Signature.Sign(Encoding.UTF8.GetBytes(_config.EasypaySecret), Encoding.UTF8.GetBytes(data));

            Assert.AreEqual(Convert.ToBase64String(signature), "OBprf4HGnzh1LqDFYyHx2KgSmB0=");
        }

        [TestMethod]
        public void TestPaymentRequestSignature()
        {
            var paymentRequest = new CheckoutPageRequest
            {
                AdultContent = false,
                Amount = "5.50",
                PaymentInfo = "Test"
            };

            var data = JsonConvert.SerializeObject(paymentRequest, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var signature = Signature.Sign(Encoding.UTF8.GetBytes(_config.EasypaySecret), Encoding.UTF8.GetBytes(data));
            Assert.AreEqual(Convert.ToBase64String(signature), "TCVlgDU0eX+ld3bNOQUrvnxLeEk=");
        }

        [TestMethod]
        public void TestGetCheckoutPageUrl()
        {
            var paymentRequest = new CheckoutPageRequest
            {
                AdultContent = false,
                Amount = "5.50",
                PaymentInfo = "Test"
            };

            var url = EasypayRequest.GetCheckoutPageUrl(_config, paymentRequest);

            Assert.AreEqual(url.ToString(), "http://easypay-test.swisscom.ch/test-service/authorize.jsf?signature=TCVlgDU0eX%2Bld3bNOQUrvnxLeEk%3D&checkoutRequestItem=eyJ0aXRsZSI6bnVsbCwiZGVzY3JpcHRpb24iOm51bGwsImR1cmF0aW9uIjowLCJkdXJhdGlvblVuaXQiOm51bGwsInByb21vdGlvbkFtb3VudCI6bnVsbCwibWVyY2hhbnRJZCI6bnVsbCwiY2FuY2VsVXJsIjpudWxsLCJlcnJvclVybCI6bnVsbCwic3VjY2Vzc1VybCI6bnVsbCwiY3BTZXJ2aWNlSWQiOm51bGwsImNwU3Vic2NyaXB0aW9uSWQiOm51bGwsImNwVXNlcklkIjpudWxsLCJpbWFnZVVybCI6bnVsbCwiYW1vdW50IjoiNS41MCIsInBheW1lbnRJbmZvIjoiVGVzdCIsInJvYW1pbmciOmZhbHNlLCJhZHVsdENvbnRlbnQiOmZhbHNlLCJ1c2VyQWdlbnRPcmlnaW4iOm51bGwsInVzZXJTb3VyY2VJUCI6bnVsbCwiY29udGVudFR5cGUiOm51bGwsInN0b3JlU291cmNlIjpudWxsLCJvcGVyYXRpb24iOm51bGwsIm9yZGVySWQiOm51bGx9");
        }
    }
}
