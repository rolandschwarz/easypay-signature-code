<?php

namespace easpaylib;

/**
 * The response message list object represents the server messages, which can be extracted and interpreted  by the client.
 */
class Payment_response
{
    private $_messages;
    private $_success;
    private $_exception;

    function __construct()
    {
        $this->_messages = array();
    }

    /**
     * @param $value bool
     */
    public function setSuccess($value)
    {
        $this->_success = (bool)$value;
    }

    /**
     * @return bool
     */
    public function is_success()
    {
        return $this->_success;
    }

    public function get_messages()
    {
        return $this->_messages;
    }

    /**
     * @return string
     */
    public function get_exception()
    {
        return $this->_exception;
    }

    /**
     * @param $value string
     */
    public function set_exception($value)
    {
        $this->_success = false;
        $this->_exception = $value;
    }

    /**
     * @param $message_obj Message
     */
    public function add_message($message_obj)
    {
        array_push($this->_messages, $message_obj);
    }
}
