namespace easypay.swisscom.com
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class EasypayRequest
    {
        public static Uri GetCheckoutPageUrl(EasypayConfig config, CheckoutPageRequest paymentRequest)
        {
            var data = JsonConvert.SerializeObject(paymentRequest, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var dataByteArray = Encoding.UTF8.GetBytes(data);
            var signature = Signature.Sign(Encoding.UTF8.GetBytes(config.EasypaySecret), dataByteArray);
            var path = "/authorize.jsf?signature=" + Uri.EscapeDataString(Convert.ToBase64String(signature)) + "&checkoutRequestItem=" + Uri.EscapeDataString(Convert.ToBase64String(dataByteArray));
            return new Uri("http://" + config.Host + config.Basepath + path);
        }

        public static async Task<PaymentResponse> CommitPaymentRequest(string paymentId, EasypayConfig easypayConfig, string reqId)
        {
            var dataJSON = @"{""operation"":""COMMIT""}";
            var path = "/payments/" + paymentId;
            return await ProcessPaymentRequest(dataJSON, path, easypayConfig, reqId);
        }

        public static async Task<PaymentResponse> CancelPaymentRequest(string paymentId, EasypayConfig easypayConfig, string reqId)
        {
            var dataJSON = @"{""operation"":""CANCEL""}";
            var path = "/payments/" + paymentId;
            return await ProcessPaymentRequest(dataJSON, path, easypayConfig, reqId);
        }

        public static async Task<PaymentResponse> ProcessPaymentRequest(string dataJson, string path, EasypayConfig easypayConfig, string reqId)
            { 
            var resultMessage = await ProcessRequest(easypayConfig, dataJson, HttpMethod.Put, path, reqId);
            Console.WriteLine("Response: " + resultMessage.Response);

            var ret = JsonConvert.DeserializeObject<PaymentResponse>(resultMessage.Response) ?? new PaymentResponse();
            ret.Success = resultMessage.StatusCode == HttpStatusCode.OK;
            return ret;
        }

        public static async Task<StatusResponse> GetPaymentStatus(string paymentId, EasypayConfig easypayConfig, string reqId)
        {
            var resultMessage = await ProcessRequest(easypayConfig, null, HttpMethod.Get, "/payments/" + paymentId, reqId);
            Console.WriteLine("Response: " + resultMessage.Response);

            var ret = JsonConvert.DeserializeObject<StatusResponse>(resultMessage.Response) ?? new StatusResponse();
            return ret;
        }

        private static async Task<RequestResponse> ProcessRequest(EasypayConfig config, string data, HttpMethod httpMethod, string path, string reqId)
        {
            var contentType = httpMethod.Method == "GET" ? "" : "application/vnd.ch.swisscom.easypay.direct.payment+json";
            var url = new Uri("https://" + config.Host + config.Basepath + path);
            var now = DateTime.Now;
            var date = now.ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.GetCultureInfoByIetfLanguageTag("en")) + " +0000"; //Mon, 07 Dec 2015 09:01:30 +0000

            var client = new HttpClient();
            var request = new HttpRequestMessage(httpMethod, url);
            request.Headers.Add("X-SCS-Date", date);
            request.Headers.Add("X-Request-Id", reqId);
            request.Headers.Add("X-Merchant-Id", config.MerchantId);
            request.Headers.Add("X-CE-Client-Specification-Version", "1.1");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.ch.swisscom.easypay.message.list+json"));
            request.Headers.Date = now;

            var md5Hash = data != null ? Signature.HashData(Encoding.UTF8.GetBytes(data)) : null;
            var hashString = Signature.CreateHashString(httpMethod.Method, md5Hash != null ? Convert.ToBase64String(md5Hash) : "", contentType, date, path);
            var signature = Signature.Sign(Encoding.UTF8.GetBytes(config.EasypaySecret), Encoding.UTF8.GetBytes(hashString));
            
            request.Headers.Add("X-SCS-Signature", Convert.ToBase64String(signature));

            if (data != null)
            {
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                request.Content = new StringContent(data);
                request.Content.Headers.ContentMD5 = md5Hash;
            }

            var result = await client.SendAsync(request);
            var ret = new RequestResponse {Response = await result.Content.ReadAsStringAsync(), StatusCode = result.StatusCode};
            return ret;
        }

        private class RequestResponse
        {
            public string Response { get; set; }
            public HttpStatusCode StatusCode { get; set; }
        }
    }
}