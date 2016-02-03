package com.swisscom.easypay.clientlibrary;

import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;
import java.security.SignatureException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;

import android.util.Base64;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class EasypayRequest {
    /**
     * create data-string for signature
     *
     * @param httpRequestMethod String
     * @param contentHash       String
     * @param contentType       String
     * @param date              String
     * @param path              String
     * @return String
     */
    private static String createHashString(String httpRequestMethod, String contentHash, String contentType, String date, String path) {
        return httpRequestMethod + '\n' + contentHash + '\n' + contentType + '\n' + date + '\n' + path;
    }

    public static URL GetCheckoutPageUrl(EasypayConfig config, CheckoutPageRequest paymentRequest) {
        try {
            String data = paymentRequest.toJSONObject().toString();
            byte[] dataByteArray = data.getBytes(Signature.DEFAULT_ENCODING);
            byte[] key = config.getEasypaySecret().getBytes(Signature.DEFAULT_ENCODING);
            byte[] signature = Signature.sign(dataByteArray, key);
            String path = "/authorize.jsf?signature=" + URLEncoder.encode(Base64.encodeToString(signature, Base64.NO_WRAP), Signature.DEFAULT_ENCODING) + "&checkoutRequestItem=" + URLEncoder.encode(Base64.encodeToString(dataByteArray, Base64.NO_WRAP), Signature.DEFAULT_ENCODING);
            return new URL("http://" + config.getHost() + config.getBasepath() + path);
        } catch (MalformedURLException | SignatureException | UnsupportedEncodingException | JSONException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static PaymentResponse CommitPaymentRequest(String paymentId, EasypayConfig easypayConfig, String reqId) {
        String dataJSON = "{\"operation\":\"COMMIT\"}";
        String path = "/payments/" + paymentId;
        return ProcessPaymentRequest(dataJSON, path, easypayConfig, reqId);
    }

    public static PaymentResponse CancelPaymentRequest(String paymentId, EasypayConfig easypayConfig, String reqId) {
        String dataJSON = "{\"operation\":\"CANCEL\"}";
        String path = "/payments/" + paymentId;

        return ProcessPaymentRequest(dataJSON, path, easypayConfig, reqId);
    }

    public static StatusResponse GetPaymentStatus(String paymentId, EasypayConfig easypayConfig, String reqId) {
        Response result = ProcessRequest(easypayConfig, null, "GET", "/payments/" + paymentId, reqId);
        System.out.println("Response: " + result.getResult());

        StatusResponse statusResponse = new StatusResponse();
        try {
            JSONObject jObject = new JSONObject(result.getResult());

            if (jObject.has("amount"))
                statusResponse.setAmount(jObject.getString("amount"));
            if (jObject.has("formattedMsisdn"))
                statusResponse.setFormattedMsisdn(jObject.getString("formattedMsisdn"));
            if (jObject.has("isSilentAuthenticated"))
                statusResponse.setIsSilentAuthenticated(jObject.getBoolean("isSilentAuthenticated"));
            if (jObject.has("msisdn"))
                statusResponse.setMsisdn(jObject.getString("msisdn"));
            if (jObject.has("orderID"))
                statusResponse.setOrderID(jObject.getString("orderID"));
            if (jObject.has("paymentInfo"))
                statusResponse.setPaymentInfo(jObject.getString("paymentInfo"));
            if (jObject.has("status"))
                statusResponse.setStatus(jObject.getString("status"));
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return statusResponse;
    }

    public static PaymentResponse ProcessPaymentRequest(String dataJson, String path, EasypayConfig easypayConfig, String reqId) {
        Response result = ProcessRequest(easypayConfig, dataJson, "PUT", path, reqId);
        PaymentResponse paymentResponse = new PaymentResponse();
        System.out.println("Response: " + result.getResult());

        paymentResponse.setSuccess(result.isSuccess());

        if (!result.isSuccess() && result.getException() == null) {
            JSONObject jObject = null;
            try {
                jObject = new JSONObject(result.getResult());

                JSONArray messageArray = jObject.getJSONArray("messages");

                for (int i = 0; i < messageArray.length(); i++) {

                    Message msg = new Message();
                    JSONObject messageObject = messageArray.getJSONObject(i);
                    if (messageObject.has("code"))
                        msg.setCode(messageObject.getString("code"));
                    if (messageObject.has("field"))
                        msg.setField(messageObject.getString("field"));
                    if (messageObject.has("message"))
                        msg.setMessage(messageObject.getString("message"));
                    if (messageObject.has("requestId"))
                        msg.setRequestId(messageObject.getString("requestId"));

                    paymentResponse.getMessages().add(msg);

                }

            } catch (JSONException e) {
                e.printStackTrace();
            }
        }

        return paymentResponse;
    }

    private static Response ProcessRequest(EasypayConfig config, String data, String httpRequestMethod, String path, String reqId) {
        String contentType = httpRequestMethod.equals("GET") ? "" : "application/vnd.ch.swisscom.easypay.direct.payment+json";

        String dateFormat = "EEE, dd MMM yyyy HH:mm:ss Z";
        SimpleDateFormat df = new SimpleDateFormat(dateFormat, Locale.ENGLISH);
        String date = df.format(Calendar.getInstance().getTime());

        System.out.println("date: " + date);

        Response response = new Response();
        HttpURLConnection urlConnection = null;
        try {
            URL url = new URL("https://" + config.getHost() + config.getBasepath() + path);

            urlConnection = (HttpURLConnection) url.openConnection();

            urlConnection.setRequestMethod(httpRequestMethod);
            urlConnection.setRequestProperty("X-SCS-Date", date);
            urlConnection.setRequestProperty("X-Request-Id", reqId);
            urlConnection.setRequestProperty("X-Merchant-Id", config.getMerchantId());
            urlConnection.setRequestProperty("X-CE-Client-Specification-Version", "1.1");

            urlConnection.setRequestProperty("Accept", "application/vnd.ch.swisscom.easypay.message.list+json");
            urlConnection.setRequestProperty("Date", date);

            boolean hasData = data != null;

            byte[] dataByteArray = hasData ? data.getBytes(Signature.DEFAULT_ENCODING) : null;
            byte[] key = config.getEasypaySecret().getBytes(Signature.DEFAULT_ENCODING);
            byte[] md5Hash = hasData ? Signature.hash(dataByteArray) : new byte[0];

            String hashString = createHashString(httpRequestMethod, Base64.encodeToString(md5Hash, Base64.NO_WRAP), contentType, date, path);
            byte[] signature = Signature.sign(hashString.getBytes(Signature.DEFAULT_ENCODING), key);

            urlConnection.setRequestProperty("X-SCS-Signature", Base64.encodeToString(signature, Base64.NO_WRAP));

            urlConnection.setUseCaches(false);
            urlConnection.setDoInput(true);

            if (hasData) {
                urlConnection.setRequestProperty("Content-Type", contentType);
                urlConnection.setRequestProperty("Content-MD5", Base64.encodeToString(md5Hash, Base64.NO_WRAP));

                urlConnection.setDoOutput(true);

                OutputStream out = new BufferedOutputStream(urlConnection.getOutputStream());
                out.write(dataByteArray);
                out.close();
            }

            urlConnection.connect();

            StringBuilder sb = new StringBuilder();

            int HttpResult = urlConnection.getResponseCode();

            response.setSuccess(HttpResult == HttpURLConnection.HTTP_OK);
            BufferedReader br;

            if (HttpResult != HttpURLConnection.HTTP_OK) {
                br = new BufferedReader(new InputStreamReader(urlConnection.getErrorStream(), "utf-8"));
            } else {
                br = new BufferedReader(new InputStreamReader(urlConnection.getInputStream(), "utf-8"));
            }
            String line;
            while ((line = br.readLine()) != null) {
                sb.append(line).append("\n");
            }
            br.close();

            response.setResult(sb.toString());

        } catch (Exception e) {
            response.setException(e);
            e.printStackTrace();
        } finally {
            if (urlConnection != null)
                urlConnection.disconnect();
        }

        return response;
    }
}

class Response {
    private boolean success;
    private Exception exception;
    private String result;

    public boolean isSuccess() {
        return success;
    }

    public void setSuccess(boolean success) {
        this.success = success;
    }

    public void setException(Exception exception) {
        this.success = false;
        this.exception = exception;
    }

    public Exception getException() {
        return exception;
    }

    public void setResult(String result) {
        this.result = result;
    }

    public String getResult() {
        return result;
    }
}

