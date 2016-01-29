<?php

namespace easpaylib;

class Checkout_page_request extends Direct_payment_request
{
    private $_title;

    /**
     * Title of the content.
     * example: Online Newspaper
     */
    public final function get_title()
    {
        return $this->_title;
    }

    public final function set_title($value)
    {
        return $this->_title = $value;
    }

    private $_description;

    /**
     * Short description of the subscription/one-time charge
     * example: Read your favorite newspaper every day everywhere!
     */
    public final function get_description()
    {
        return $this->_description;
    }

    public final function set_description($value)
    {
        $this->_description = $value;
    }

    private $_duration;

    /**
     * The duration of the service subscription
     * example: 1
     */
    public final function get_duration()
    {
        return $this->_duration;
    }

    public final function set_duration($value)
    {
        $this->_duration = $value;
    }

    private $_duration_unit;

    /**
     * The duration unit of the service subscription
     * example: (WEEK|MONTH)
     */
    public final function get_duration_unit()
    {
        return $this->_duration_unit;
    }

    public final function set_duration_unit($value)
    {
        $this->_duration_unit = $value;
    }

    private $_promotion_amount;

    /**
     * The Promotion Amount of the Service.
     * must NOT be negative value, if negative value is sent it will be ignored and the amount will be charged instead.
     * If value is positive and less or equal as amount it will be charged instead of amount.
     * example: 2.25
     */
    public final function get_promotion_amount()
    {
        return $this->_promotion_amount;
    }

    public final function set_promotion_amount($value)
    {
        $this->_promotion_amount = $value;
    }

    private $_merchant_id;

    /**
     * The merchant id given by Swisscom
     * example: CH011
     */
    public final function get_merchant_id()
    {
        return $this->_merchant_id;
    }

    public final function set_merchant_id($value)
    {
        $this->_merchant_id = $value;
    }

    private $_cancel_url;

    /**
     * The cancel URL, which will be called, if the user cancels the authorization request on the COP.
     * example: http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=cancel
     */
    public final function get_cancel_url()
    {
        return $this->_cancel_url;
    }

    public final function set_cancel_url($value)
    {
        $this->_cancel_url = $value;
    }

    private $_error_url;

    /**
     * The error URL, which will be called, if some error/problem occur during the authorization process on the COP.
     * example: http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=error
     */
    public final function get_error_url()
    {
        return $this->_error_url;
    }

    public final function set_error_url($value)
    {
        $this->_error_url = $value;
    }

    private $_success_url;

    /**
     * The success back URL, which will be called, if the user proceed the authorization request on the COP with success.
     * example: http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=success
     */
    public final function get_success_url()
    {
        return $this->_success_url;
    }

    public final function set_success_url($value)
    {
        $this->_success_url = $value;
    }

    private $_cp_service_id;

    /**
     * The service Id on content partner side. This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
     * example: xys-323-gh-ff
     */
    public final function get_cp_service_id()
    {
        return $this->_cp_service_id;
    }

    public final function set_cp_service_id($value)
    {
        $this->_cp_service_id = $value;
    }

    private $_cp_subscription_id;

    /**
     * The unique subscription Id on content partner side.This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
     * example: vghv5678
     */
    public final function get_cp_subscription_id()
    {
        return $this->_cp_subscription_id;
    }

    public final function set_cp_subscription_id($value)
    {
        $this->_cp_subscription_id = $value;
    }

    private $_cp_user_id;

    /**
     * The unique user Id on content partner side. This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
     * example:user-5
     */
    public final function get_cp_user_id()
    {
        return $this->_cp_user_id;
    }

    public final function set_cp_user_id($value)
    {
        $this->_cp_user_id = $value;
    }

    private $_image_url;

    /**
     * The URL of the dynamic image which will be embedded in the COP.
     * Special feature is needed !
     * example: http://lorempixel.com/300/200
     */
    public final function get_image_url()
    {
        return $this->_image_url;
    }

    public final function set_image_url($value)
    {
        $this->_image_url = $value;
    }

    public function get_json_data()
    {
        $array = parent::get_json_data();

        $array["cancelUrl"] = $this->_cancel_url;
        $array["cpServiceId"] = $this->_cp_service_id;
        $array["cpSubscriptionId"] = $this->_cp_subscription_id;
        $array["description"] = $this->_description;
        $array["duration"] = 0 + $this->_duration;
        $array["durationUnit"] = $this->_duration_unit;
        $array["errorUrl"] = $this->_error_url;
        $array["imageUrl"] = $this->_image_url;
        $array["merchantId"] = $this->_merchant_id;
        $array["promotionAmount"] = $this->_promotion_amount;
        $array["successUrl"] = $this->_success_url;
        $array["title"] = $this->_title;

        return $array;
    }
}