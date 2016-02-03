package com.swisscom.easypay.clientlibrary;

import android.app.Application;
import android.test.ApplicationTestCase;
import android.util.Base64;

import junit.framework.Assert;

import java.io.UnsupportedEncodingException;
import java.net.URL;
import java.security.SignatureException;

/**
 * <a href="http://d.android.com/tools/testing/testing_android.html">Testing Fundamentals</a>
 */
public class ApplicationTest extends ApplicationTestCase<Application> {
    public ApplicationTest() {
        super(Application.class);
    }

    public void testSignature() throws UnsupportedEncodingException, SignatureException {
        String data = "{ \"key\": \"test-value\" }";
        String secret = "test-secret-987654321";
        byte[] dataByteArray = data.getBytes(Signature.DEFAULT_ENCODING);
        byte[] signature = Signature.sign(dataByteArray, secret.getBytes(Signature.DEFAULT_ENCODING));

        Assert.assertEquals(Base64.encodeToString(signature, Base64.NO_WRAP), "CM1j1puWcSUVbZHeqDYTiWXaQ/s=");
    }

    public void testGetCheckoutPageUrl() {
        CheckoutPageRequest paymentRequest = new CheckoutPageRequest();
        paymentRequest.setAdultContent(false);
        paymentRequest.setAmount(50);
        paymentRequest.setPaymentInfo("Test");

        EasypayConfig config = new EasypayConfig();
        config.setBasepath("/test-service");
        config.setHost("easypay-test.swisscom.ch");
        config.setMerchantId("test-merchant");
        config.setEasypaySecret("test-secret-987654321");

        URL url = EasypayRequest.GetCheckoutPageUrl(config, paymentRequest);

        Assert.assertEquals(url.toString(), "http://easypay-test.swisscom.ch/test-service/authorize.jsf?signature=bRiX9DvG9cQ39k5d6AqlVML0WOU%3D&checkoutRequestItem=eyJhZHVsdENvbnRlbnQiOmZhbHNlLCJhbW91bnQiOjUwLCJwYXltZW50SW5mbyI6IlRlc3QiLCJyb2FtaW5nIjpmYWxzZSwiZHVyYXRpb24iOjB9");
    }
}