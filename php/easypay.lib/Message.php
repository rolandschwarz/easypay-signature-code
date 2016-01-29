<?php

namespace easpaylib;
/**
 * The response message object represents a server message, which can be extracted and interpreted by the client.
 */
class Message
{
    private $_message;
    private $_field;
    private $_code;
    private $_request_id;

    /**
     * Localized message describing the nature of the problem reported
     */
    public function get_message()
    {
        return $this->_message;
    }

    public function set_message($value)
    {
        $this->_message = $value;
    }

    /**
     * Name of the field from the request data model that this message is associated with
     */
    public function get_field()
    {
        return $this->_field;
    }

    public function set_field($value)
    {
        $this->_field = $value;
    }

    /**
     * Symbolic error code identifying the type of error reported by this message
     *
     * @see Error
     */
    public function get_code()
    {
        return $this->_code;
    }

    public function set_code($value)
    {
        $this->_code = $value;
    }

    /**
     * The request id, sent within the XRequest-Id http header.
     */
    public function get_request_id()
    {
        return $this->_request_id;
    }

    public function set_request_id($value)
    {
        $this->_request_id = $value;
    }
}
