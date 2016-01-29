<?php

$endOfLine = "<br/>";
ini_set('display_errors', 'On');
error_reporting(E_ALL);

include '../easypay.lib/Direct_payment_request.php';
include '../easypay.lib/Checkout_page_request.php';
include '../easypay.lib/Easypay_config.php';
include '../easypay.lib/Easypay_request.php';
include '../easypay.lib/Message.php';
include '../easypay.lib/Signature.php';
include '../easypay.lib/Payment_response.php';
include '../easypay.lib/Status_response.php';
use easpaylib\Checkout_page_request;
use easpaylib\Easypay_config;
use easpaylib\Easypay_request;
use easpaylib\Message;

$MerchantId = "LTC001";
$BaseReturnUrl = "http://localhost/easypay.app/easypay_controller.php";

$paymentRequest = new Checkout_page_request();
$paymentRequest->set_adult_content(false);
$paymentRequest->set_amount(50);
$paymentRequest->set_payment_info("Test");
$paymentRequest->set_title("My new Phone");
$paymentRequest->set_description("Buy your favorite Sony Phone!");
$paymentRequest->set_adult_content(false);
$paymentRequest->set_roaming(false);
$paymentRequest->set_merchant_id($MerchantId);
$paymentRequest->set_cancel_url($BaseReturnUrl . "?uid=43c2&sid=dc0d&purchase=cancel");
$paymentRequest->set_error_url($BaseReturnUrl . "?uid=43c2&sid=dc0d&purchase=error");
$paymentRequest->set_success_url($BaseReturnUrl . "?uid=43c2&sid=dc0d&purchase=success");
$paymentRequest->set_cp_service_id("xys-323-gh-ff");
$paymentRequest->set_cp_subscription_id("23hkb379oh");
$paymentRequest->set_cp_user_id("vghv5678");
$paymentRequest->set_image_url("http://lorempixel.com/300/200");
$paymentRequest->set_content_type("App");
$paymentRequest->set_store_source("Easypay-City");

$config = new Easypay_config();
$config->set_basepath("/charging-engine-checkout");
$config->set_host("easypay-staging.swisscom.ch");
$config->set_merchant_id($MerchantId);
$config->set_easypay_secret("KiLgscVNTqAJQ1keGOv_hhKsuf5oftohg17VmncT");

ECHO json_encode($_GET) . $endOfLine;
ECHO "----------------------------------------------------------" . $endOfLine;

if (isset($_GET['buy'])) {

    ECHO "Buy Action" . "<br/>";
    if (isset($_GET['amount'])) {
        $paymentRequest->set_amount($_GET['amount']);
    }

    if (isset($_GET['paymentInfo'])) {
        $paymentRequest->set_payment_info($_GET['paymentInfo']);
    }

    $url = Easypay_request::get_checkout_page_url($config, $paymentRequest);
    ECHO "Redirect to " . $url . "<br/>";
    header("Location: " . $url);
    exit;

} elseif (isset($_GET['purchase'])) {
    $purchase = $_GET['purchase'];
    $paymentId = isset($_GET['paymentId']) ? $_GET['paymentId'] : null;

    if ($purchase == "success") {
        $reqId = "req-id-123";

        $config->set_basepath("/ce-rest-service");
        $response = Easypay_request::commit_payment_request($paymentId, $config, $reqId);

        if ($response->is_success())
            ECHO "<h2>Success</h2>";
        else {
            ECHO "<h2>ERROR</h2>";

            /** @var Message $msg */
            $messages = $response->get_messages();
            if (count($messages) > 0) {
                foreach ($messages as $msg) {
                    ECHO "<p><strong>" . $msg->get_code() . "</strong>, " . $msg->get_field() . ", " . $msg->get_message() . "</p>";
                }
            } else {
                ECHO "<h2>ERROR</h2>";
                ECHO "<strong>" . $response->get_exception() . "</strong>";
            }
        }
    } else {
        ECHO "<h2>ERROR</h2>";
        ECHO "<strong>Purchase terminated with state: '" . $purchase . "'</strong>";
    }

    if (isset($_GET['paymentId'])) {
        ECHO "<form method=\"get\" action=\"easypay_controller.php\"><input type=\"hidden\" name=\"paymentId\" value=\"" . $_GET['paymentId'] . "\" ><input type=\"submit\" name=\"status\" value=\"Get Payment Status\"/></form>";
    }
} elseif (isset($_GET['status']) && $_GET['paymentId']) {
    $paymentId = $_GET['paymentId'];
    $reqId = "req-id-123";

    $config->set_basepath("/ce-rest-service");
    $response = Easypay_request::get_payment_status($paymentId, $config, $reqId);

    ECHO "<h2>STATUS " . $response->get_status() . "</h2>";
    ECHO "<p><strong>MSISDN: " . $response->get_formatted_msisdn() . "</strong>, OrderID: " . $response->get_order_ID() . ", Payment Info: " . $response->get_payment_info() . ", CHF " . $response->get_amount() . "</p>";
}