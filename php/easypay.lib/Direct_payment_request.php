<?php

namespace easpaylib;

/**
 * The direct payment is a kind of one-charge-event-payment. It does not need any management on the charging-engine side.
 */
class Direct_payment_request
{
    private $_amount;
    private $_payment_info;
    private $_roaming;
    private $_adult_content;
    private $_user_agent_origin;
    private $_user_source_IP;
    private $_operation;
    private $_order_id;
    private $_content_type;
    private $_store_source;

    /**
     * The amount of the service.
     * <b>Note:</b> Optional for Refund (if not set full charged amount fill be refunded).
     *
     * @return double amount
     */
    public function get_amount()
    {
        return $this->_amount;
    }

    /**
     * The payment info of the service (also known as billing text).
     * <b>Note:</b> The paymentInfo will be placed on the end user invoice bill.
     *
     * @return String
     */
    public function get_payment_info()
    {
        return $this->_payment_info;
    }

    /**
     * Flag which marks the service as roaming (true) or none roaming (false).
     * This means that the customer will be checked for roaming.
     * If the customer has roaming and the flag was set to true, then the payment
     * will be blocked. If the customer has roaming and the flag was set to false,
     * then the payment will not be blocked.
     *
     * @return boolean isRoaming
     */
    public function get_roaming()
    {
        return $this->_roaming;
    }

    /**
     * Flag which marks the service as adult (true) or non adult (false).
     * This means that the customer will be checked, if he/she is allowed to consume adult content.
     *
     * @return boolean
     */
    public function get_adultcontent()
    {
        return $this->_adult_content;
    }

    /**
     * The UserAgent of the end customer
     * e.g. Mozilla/5.0 (Linux; U; Android 2.2.1; en-us; Nexus One Build/FRG83)
     *
     * @return String
     */
    public function get_user_agent_origin()
    {
        return $this->_user_agent_origin;
    }

    /**
     * The end user source IP
     *
     * @return String SourceIP
     */
    public function get_user_source_ip()
    {
        return $this->_user_source_IP;
    }

    /**
     * Allows to commit/ reject the payment or to refund the amount.
     * The amount to be refund must be the same or less as the initial payment.
     *
     * @return Operation Operation
     */
    public function get_operation()
    {
        return $this->_operation;
    }

    /**
     * The order id which is printed on end user invoice bill.
     *
     * @return String OrderId
     */
    public function get_order_id()
    {
        return $this->_order_id;
    }

    /**
     * The amount of the service.
     * <b>Note:</b> Optional for Refund (if not set full charged amount fill be refunded).
     *
     * @param a double amount
     */
    public function set_amount($value)
    {
        $this->_amount = $value;
    }

    /**
     * The payment info of the service (also known as billing text).
     * <b>Note:</b> The paymentInfo will be placed on the end user invoice bill.
     *
     * @param p String paymentInfo
     */
    public function set_payment_info($value)
    {
        $this->_payment_info = $value;
    }

    /**
     * Flag which marks the service as roaming (true) or none roaming (false).
     * This means that the customer will be checked for roaming.
     * If the customer has roaming and the flag was set to true, then the payment
     * will be blocked. If the customer has roaming and the flag was set to false,
     * then the payment will not be blocked.
     *
     * @param r boolean isRoaming
     */
    public function set_roaming($value)
    {
        $this->_roaming = $value;
    }

    /**
     * Flag which marks the service as adult (true) or non adult (false).
     * This means that the customer will be checked, if he/she is allowed to consume adult content.
     *
     * @param a boolean isAdultContent
     */
    public function set_adult_content($value)
    {
        $this->_adult_content = $value;
    }

    /**
     * The UserAgent of the end customer
     * e.g. Mozilla/5.0 (Linux; U; Android 2.2.1; en-us; Nexus One Build/FRG83)
     *
     * @param u String userAgent
     */
    public function set_user_agent_origin($value)
    {
        $this->_user_agent_origin = $value;
    }

    /**
     * The end user source IP
     *
     * @param u String SourceIP
     */
    public function set_user_source_ip($value)
    {
        $this->_user_source_IP = $value;
    }

    /**
     * Allows to commit/ reject the payment or to refund the amount.
     * The amount to be refund must be the same or less as the initial payment.
     *
     * @param o Operation Operation
     */
    public function set_operation($value)
    {
        $this->_operation = $value;
    }

    /**
     * The order id which is printed on end user invoice bill.
     *
     * @param o String OrderId
     */
    public function set_order_id($value)
    {
        $this->_order_id = $value;
    }

    public function set_content_type($value)
    {
        $this->_content_type = $value;
    }

    public function get_content_type()
    {
        return $this->_content_type;
    }

    public function set_store_source($value)
    {
        $this->_store_source = $value;
    }

    public function get_store_source()
    {
        return $this->_store_source;
    }

    public function get_json_data()
    {
        $array = array();
        $array["adultContent"] = (bool)$this->_adult_content;
        $array["amount"] = 0.0 + $this->_amount;
        $array["operation"] = $this->_operation;
        $array["orderId"] = $this->_order_id;
        $array["paymentInfo"] = $this->_payment_info;
        $array["roaming"] = (bool)$this->_roaming;
        $array["userAgentOrigin"] = $this->_user_agent_origin;
        $array["userSourceIP"] = $this->_user_source_IP;
        $array["contentType"] = $this->_content_type;
        $array["storeSource"] = $this->_store_source;

        return $array;
    }
}
