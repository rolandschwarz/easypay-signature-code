<?php

namespace easpaylib;


class Status_response
{
    private $_order_ID;
    private $_payment_info;
    private $_is_silent_authenticated;
    private $_amount;
    private $_msisdn;
    private $_status;
    private $_formatted_msisdn;

    /**
     * @return string
     */
    public function get_order_ID()
    {
        return $this->_order_ID;
    }

    /**
     * @param string $_order_ID
     */
    public function set_orderID($_order_ID)
    {
        $this->_order_ID = $_order_ID;
    }

    /**
     * @return string
     */
    public function get_payment_info()
    {
        return $this->_payment_info;
    }

    /**
     * @param string $_payment_info
     */
    public function set_payment_info($_payment_info)
    {
        $this->_payment_info = $_payment_info;
    }

    /**
     * @return boolean
     */
    public function get_is_silent_authenticated()
    {
        return $this->_is_silent_authenticated;
    }

    /**
     * @param boolean $_is_silent_authenticated
     */
    public function set_is_silent_authenticated($_is_silent_authenticated)
    {
        $this->_is_silent_authenticated = $_is_silent_authenticated;
    }

    /**
     * @return float
     */
    public function get_amount()
    {
        return $this->_amount;
    }

    /**
     * @param float $_amount
     */
    public function set_amount($_amount)
    {
        $this->_amount = $_amount;
    }

    /**
     * @return string
     */
    public function get_msisdn()
    {
        return $this->_msisdn;
    }

    /**
     * @param string $_msisdn
     */
    public function set_msisdn($_msisdn)
    {
        $this->_msisdn = $_msisdn;
    }

    /**
     * @return string
     */
    public function get_status()
    {
        return $this->_status;
    }

    /**
     * @param string $_status
     */
    public function set_status($_status)
    {
        $this->_status = $_status;
    }

    /**
     * @return string
     */
    public function get_formatted_msisdn()
    {
        return $this->_formatted_msisdn;
    }

    /**
     * @param string $_formatted_msisdn
     */
    public function set_formatted_msisdn($_formatted_msisdn)
    {
        $this->_formatted_msisdn = $_formatted_msisdn;
    }
}