<?php

namespace easpaylib;

class Easypay_config
{
    private $_basepath;

    public final function get_basepath()
    {
        return $this->_basepath;
    }

    public final function set_basepath($value)
    {
        $this->_basepath = $value;
    }

    private $_easypay_secret;

    public final function get_easypay_secret()
    {
        return $this->_easypay_secret;
    }

    public final function set_easypay_secret($value)
    {
        $this->_easypay_secret = $value;
    }

    private $_host;

    public final function get_host()
    {
        return $this->_host;
    }

    public final function set_host($value)
    {
        $this->_host = $value;
    }

    private $_merchant_id;

    public final function get_merchant_id()
    {
        return $this->_merchant_id;
    }

    public final function set_merchant_id($value)
    {
        $this->_merchant_id = $value;
    }
}
