package com.swisscom.easypay.easypayclient;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.webkit.WebView;
import android.webkit.WebViewClient;

import com.swisscom.easypay.clientlibrary.CheckoutPageRequest;
import com.swisscom.easypay.clientlibrary.EasypayConfig;
import com.swisscom.easypay.clientlibrary.EasypayRequest;
import com.swisscom.easypay.clientlibrary.PaymentResponse;

import java.net.URL;
import java.util.UUID;
import java.util.concurrent.ExecutionException;

public class DemoActivity extends Activity {
    WebView webview;

    public static EasypayConfig getEasypayConfig() {
        EasypayConfig config = new EasypayConfig();
        config.setBasepath("/charging-engine-checkout");
        config.setHost("easypay-staging.swisscom.ch");
        config.setMerchantId("LTC001");
        config.setEasypaySecret("KiLgscVNTqAJQ1keGOv_hhKsuf5oftohg17VmncT");
        return config;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_demo);

        EasypayConfig config = getEasypayConfig();

        String baseReturnUrl = "http://www.mocky.io/v2/566aec731000007826990c05";

        Intent intent = getIntent();
        String paymentInfo = intent.getStringExtra(MainActivity.PAYMENT_INFO);
        Double amount = intent.getDoubleExtra(MainActivity.AMOUNT, 0);

        CheckoutPageRequest paymentRequest = new CheckoutPageRequest();
        paymentRequest.setAdultContent(false);
        paymentRequest.setAmount(amount);
        paymentRequest.setPaymentInfo(paymentInfo);
        paymentRequest.setTitle("My new Phone");
        paymentRequest.setDescription("Buy your favorite Sony Phone!");
        paymentRequest.setAdultContent(false);
        paymentRequest.setRoaming(false);
        paymentRequest.setMerchantId(config.getMerchantId());
        paymentRequest.setCancelUrl(baseReturnUrl + "?uid=43c2&sid=dc0d&purchase=cancel");
        paymentRequest.setErrorUrl(baseReturnUrl + "?uid=43c2&sid=dc0d&purchase=error");
        paymentRequest.setSuccessUrl(baseReturnUrl + "?uid=43c2&sid=dc0d&purchase=success");
        paymentRequest.setCpServiceId("xys-323-gh-ff");
        paymentRequest.setCpSubscriptionId("23hkb379oh");
        paymentRequest.setCpUserId("vghv5678");
        paymentRequest.setImageUrl("http://lorempixel.com/300/200");
        paymentRequest.setContentType("App");
        paymentRequest.setStoreSource("Easypay-City");

        URL url = EasypayRequest.GetCheckoutPageUrl(config, paymentRequest);

        webview = (WebView) findViewById(R.id.webView);
        webview.getSettings().setJavaScriptEnabled(true);
        assert url != null;
        webview.loadUrl(url.toString());
        webview.setWebViewClient(new EasypayWebViewClient(baseReturnUrl, DemoActivity.this));
    }
}

class EasypayWebViewClient extends WebViewClient {
    private final Activity parentActivity;
    private final String baseReturnUrl;

    public EasypayWebViewClient(String baseReturnUrl, Activity parentActivity) {

        this.baseReturnUrl = baseReturnUrl;
        this.parentActivity = parentActivity;
    }

    @Override
    public void onPageFinished(WebView view, String url) {
        super.onPageFinished(view, url);
        if (url.contains(baseReturnUrl)) {
            // commit or reject payment
            Uri uri = Uri.parse(url);
            String paymentId = uri.getQueryParameter("paymentId");
            String reqId = UUID.randomUUID().toString();

            CommitPaymentRequestTask task = new CommitPaymentRequestTask();
            AsyncTask<String, Void, PaymentResponse> execute = task.execute(paymentId, reqId);

            PaymentResponse response = new PaymentResponse();
            try {
                response = execute.get();

                if (response == null) {
                    response = new PaymentResponse();
                    response.setException(task.exception);
                }
            } catch (InterruptedException | ExecutionException e) {
                response.setException(e);
                e.printStackTrace();
            }

            String title = "Error";
            String message = "Technical Error";

            if (response.isSuccess()) {
                title = "Success";
                message = "Payment succeeded";
            } else {
                if (response.getException() != null) {
                    message = response.getException().getMessage();
                } else if (!response.getMessages().isEmpty()) {
                    message = response.getMessages().get(0).getCode() + "; " + response.getMessages().get(0).getField() + "; " + response.getMessages().get(0).getMessage();
                }
            }

            Intent intent = new Intent(this.parentActivity, ResultActivity.class);
            intent.putExtra(ResultActivity.TITLE, title);
            intent.putExtra(ResultActivity.MESSAGE, message);
            intent.putExtra(ResultActivity.PAYMENT_ID, paymentId);
            intent.putExtra(ResultActivity.REQ_ID, reqId);
            this.parentActivity.startActivity(intent);
        }
    }

    class CommitPaymentRequestTask extends AsyncTask<String, Void, PaymentResponse> {

        private Exception exception;

        protected PaymentResponse doInBackground(String... args) {
            try {
                EasypayConfig config = DemoActivity.getEasypayConfig();
                config.setBasepath("/ce-rest-service");

                String paymentId = args[0];
                String reqId = args[1];

                return EasypayRequest.CommitPaymentRequest(paymentId, config, reqId);
            } catch (Exception e) {
                this.exception = e;
                return null;
            }
        }
    }
}
