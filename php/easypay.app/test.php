<!DOCTYPE html>
<html>
<body>
<h2>TESTS</h2>
<p>
    <?php

    iconv_set_encoding("internal_encoding", "UTF-8");
    iconv_set_encoding("output_encoding", "UTF-8");

    $endOfLine = "<br/>";
    ini_set('display_errors', 'On');
    error_reporting(E_ALL);

    include '../easypay.lib/Direct_payment_request.php';
    include '../easypay.lib/Checkout_page_request.php';
    include '../easypay.lib/Easypay_config.php';
    include '../easypay.lib/Easypay_request.php';
    include '../easypay.lib/Signature.php';
    use easpaylib\Checkout_page_request;
    use easpaylib\Easypay_config;
    use easpaylib\Easypay_request;
    use easpaylib\Signature;

    $paymentRequest = new Checkout_page_request();
    $paymentRequest->set_adult_content(false);
    $paymentRequest->set_amount(50);
    $paymentRequest->set_payment_info("Test");

    $config = new Easypay_config();
    $config->set_basepath("/test-service");
    $config->set_host("easypay-test.swisscom.ch");
    $config->set_merchant_id("test-merchant");
    $config->set_easypay_secret("test-secret-987654321");

    ECHO "<h3>Test Signature</h3>";

    $data = json_encode(array("key" => "test-value", "adultContent" => false, "amount" => 50.5));
    $secret = $config->get_easypay_secret();
    $signature = base64_encode(Signature::sign($data, $secret));

    $expected = "Zq3zcquJgdNNK/3HhFmVBTNX2+Y=";
    $actual = $signature;
    $assert = $expected == $actual ? "PASSED" : "FAILED";

    ECHO "expected " . $expected . $endOfLine;
    ECHO "actual " . $actual . $endOfLine;
    ECHO "Test Signature: " . $assert . $endOfLine;
    ECHO "--------------------------------------------------------------------" . $endOfLine;


    ECHO "<h3>Test HASH Signature</h3>";

    $data = Easypay_request::create_hash_string('PUT', '6KEzivnrMza/LaW7bg5n5A==', 'application/vnd.ch.swisscom.easypay.direct.payment+json', 'Tue, 19 Jan 2016 13:57:14 +0000', '/payments/F10F6A2D-CB56-4618-B57E-4A186663467D');
    $secret = "KiLgscVNTqAJQ1keGOv_hhKsuf5oftohg17VmncT";
    $signature = base64_encode(Signature::sign($data, $secret));

    $expected = "DDEVjshw5qN1Bkja9plTuc81/A0=";
    $actual = $signature;
    $assert = $expected == $actual ? "PASSED" : "FAILED";

    ECHO "expected " . $expected . $endOfLine;
    ECHO "actual " . $actual . $endOfLine;
    ECHO "Test HASH Signature: " . $assert . $endOfLine;
    ECHO "--------------------------------------------------------------------" . $endOfLine;

    ECHO "<h3>Test PaymentRequest Signature</h3>";

    $data = json_encode($paymentRequest->get_json_data());
    ECHO "data: " . $data . $endOfLine;
    $secret = $config->get_easypay_secret();
    $signature = base64_encode(Signature::sign($data, $secret));

    $expected = "gnBbvXl2PJqXEdeO09mcjMMV4FM=";
    $actual = $signature;
    $assert = $expected == $actual ? "PASSED" : "FAILED";

    ECHO "expected " . $expected . $endOfLine;
    ECHO "actual " . $actual . $endOfLine;
    ECHO "Test PaymentRequest Signature: " . $assert . $endOfLine;
    ECHO "--------------------------------------------------------------------" . $endOfLine;

    ECHO "<h3>Test GetCheckoutPageUrl</h3>";
    $url = Easypay_request::get_checkout_page_url($config, $paymentRequest);

    $expected = "http://easypay-test.swisscom.ch/test-service/authorize.jsf?signature=gnBbvXl2PJqXEdeO09mcjMMV4FM%3D&checkoutRequestItem=eyJhZHVsdENvbnRlbnQiOmZhbHNlLCJhbW91bnQiOjUwLCJvcGVyYXRpb24iOm51bGwsIm9yZGVySWQiOm51bGwsInBheW1lbnRJbmZvIjoiVGVzdCIsInJvYW1pbmciOmZhbHNlLCJ1c2VyQWdlbnRPcmlnaW4iOm51bGwsInVzZXJTb3VyY2VJUCI6bnVsbCwiY29udGVudFR5cGUiOm51bGwsInN0b3JlU291cmNlIjpudWxsLCJjYW5jZWxVcmwiOm51bGwsImNwU2VydmljZUlkIjpudWxsLCJjcFN1YnNjcmlwdGlvbklkIjpudWxsLCJkZXNjcmlwdGlvbiI6bnVsbCwiZHVyYXRpb24iOjAsImR1cmF0aW9uVW5pdCI6bnVsbCwiZXJyb3JVcmwiOm51bGwsImltYWdlVXJsIjpudWxsLCJtZXJjaGFudElkIjpudWxsLCJwcm9tb3Rpb25BbW91bnQiOm51bGwsInN1Y2Nlc3NVcmwiOm51bGwsInRpdGxlIjpudWxsfQ%3D%3D";
    $actual = $url;
    $assert = $expected == $actual ? "PASSED" : "FAILED";

    ECHO "expected " . $expected . $endOfLine;
    ECHO "actual " . $actual . $endOfLine;
    ECHO "Test GetCheckoutPageUrl: " . $assert . $endOfLine;
    ECHO "--------------------------------------------------------------------" . $endOfLine;

    ?>
</p>
</body>
</html>
