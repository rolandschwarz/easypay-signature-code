<?php

namespace easpaylib;

class Easypay_request
{
    const newLine = "\n";

    /**
     * create data-string for signature
     *
     * @param $http_request_method string
     * @param $content_hash       string
     * @param $content_type       string
     * @param $date              string
     * @param $path              string
     * @return string
     */
    public static function create_hash_string($http_request_method, $content_hash, $content_type, $date, $path)
    {
        return $http_request_method . self::newLine . $content_hash . self::newLine . $content_type . self::newLine . $date . self::newLine . $path;
    }

    /**
     * @param $config Easypay_config
     * @param $payment_request Checkout_page_request
     * @return string
     */
    public static function get_checkout_page_url($config, $payment_request)
    {
        $data = json_encode($payment_request->get_json_data());
        $signature = Signature::sign($data, $config->get_easypay_secret());
        $path = "/authorize.jsf?signature=" . rawurlencode(base64_encode($signature)) . "&checkoutRequestItem=" . rawurlencode(base64_encode($data));
        return "http://" . $config->get_host() . $config->get_basepath() . $path;
    }

    /**
     * @param $payment_id string
     * @param $easypay_config Easypay_config
     * @param $req_id string
     * @return Payment_response
     */
    public static function commit_payment_request($payment_id, $easypay_config, $req_id)
    {
        return self::_process_request($easypay_config, json_encode(array("operation" => "COMMIT")), "PUT", "/payments/" . $payment_id, $req_id);
    }

    /**
     * @param $payment_id string
     * @param $easypay_config Easypay_config
     * @param $req_id string
     * @return Payment_response
     */
    public static function cancel_payment_request($payment_id, $easypay_config, $req_id)
    {
        return self::_process_request($easypay_config, json_encode(array("operation" => "CANCEL")), "PUT", "/payments/" . $payment_id, $req_id);
    }

    /**
     * @param $payment_id string
     * @param $easypay_config Easypay_config
     * @param $req_id string
     * @return Status_response
     */
    public static function get_payment_status($payment_id, $easypay_config, $req_id)
    {
        return self::_process_request($easypay_config, null, "GET", "/payments/" . $payment_id, $req_id);
    }

    /**
     * @param $config Easypay_config
     * @param $data string
     * @param $http_request_method string
     * @param $path string
     * @param $req_id string
     * @return mixed
     */
    private static function _process_request($config, $data, $http_request_method, $path, $req_id)
    {
        $content_type = ($http_request_method === 'GET') ? '' : 'application/vnd.ch.swisscom.easypay.direct.payment+json';
        $accept = ($http_request_method === 'GET') ? "application/vnd.ch.swisscom.easypay.direct.payment+json" : "application/vnd.ch.swisscom.easypay.message.list+json";
        $date = gmdate('D, d M Y H:i:s +0000', time());
        $url = "https://" . $config->get_host() . $config->get_basepath() . $path;

        $md5Hash = isset($data) ? base64_encode(Signature::hash($data)) : "";
        $hash_string = self::create_hash_string($http_request_method, $md5Hash, $content_type, $date, $path);
        $signature = Signature::sign($hash_string, $config->get_easypay_secret()); //Signature::Sign($hashString, base64_decode($config->getEasypaySecret()));

        $headers = array(
            "Content-Type: " . $content_type,
            "X-SCS-Date: " . $date,
            "X-Request-Id: " . $req_id,
            "X-Merchant-Id: " . $config->get_merchant_id(),
            "X-CE-Client-Specification-Version: 1.1",
            "X-SCS-Signature: " . base64_encode($signature),
            "Accept: " . $accept,
            "Date: " . $date,
        );

        if (isset($data))
            array_push($headers, "Content-MD5: " . $md5Hash);

        $options = array(
            CURLOPT_CUSTOMREQUEST => $http_request_method,
            CURLOPT_RETURNTRANSFER => 1,
            CURLOPT_HEADER => 0,
            CURLOPT_HTTPHEADER => $headers,
            CURLOPT_POSTFIELDS => $data,
            CURLOPT_SSL_VERIFYHOST => 0,
            CURLOPT_SSL_VERIFYPEER => 0
        );

        $ch = curl_init($url);
        curl_setopt_array($ch, $options);

        $response = curl_exec($ch);
        $http_code = curl_getinfo($ch)["http_code"];

        $payment_response = new Payment_response();

        echo "response: " . $response . "<br/>";

        if (curl_errno($ch) != 0 || $http_code !== 200) {
            $payment_response->setSuccess(false);

            if (curl_errno($ch) != 0) {
                $error = curl_error($ch);
                $payment_response->set_exception($error);
            } else {
                $messages = json_decode($response, true);

                foreach ($messages["messages"] as $msg) {
                    $message_obj = new Message();
                    if (array_key_exists("code", $msg))
                        $message_obj->set_code($msg["code"]);
                    if (array_key_exists("field", $msg))
                        $message_obj->set_field($msg["field"]);
                    if (array_key_exists("message", $msg))
                        $message_obj->set_message($msg["message"]);
                    if (array_key_exists("requestId", $msg))
                        $message_obj->set_request_id($msg["requestId"]);

                    $payment_response->add_message($message_obj);
                }
            }

        } else {
            $payment_response->setSuccess(true);
            $statusResponse = json_decode($response, true);

            if (isset($statusResponse)) {

                $payment_response = new Status_response();
                if (array_key_exists("orderID", $statusResponse))
                    $payment_response->set_orderID($statusResponse["orderID"]);
                if (array_key_exists("paymentInfo", $statusResponse))
                    $payment_response->set_payment_info($statusResponse["paymentInfo"]);
                if (array_key_exists("isSilentAuthenticated", $statusResponse))
                    $payment_response->set_is_silent_authenticated($statusResponse["isSilentAuthenticated"]);
                if (array_key_exists("amount", $statusResponse))
                    $payment_response->set_amount($statusResponse["amount"]);
                if (array_key_exists("msisdn", $statusResponse))
                    $payment_response->set_msisdn($statusResponse["msisdn"]);
                if (array_key_exists("status", $statusResponse))
                    $payment_response->set_status($statusResponse["status"]);
                if (array_key_exists("formattedMsisdn", $statusResponse))
                    $payment_response->set_formatted_msisdn($statusResponse["formattedMsisdn"]);
            }
        }

        curl_close($ch);

        return $payment_response;
    }
}
