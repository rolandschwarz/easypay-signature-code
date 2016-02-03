package com.swisscom.easypay.clientlibrary;

/**
 * The response message object represents a server message, which can be extracted and interpreted by the client.
 */
public class Message {

    private String _message;
    private String _field;
    private String _code;
    private String _requestId;

    /**
     * Localized message describing the nature of the problem reported
     */
    public String getMessage() {
        return _message;
    }

    /**
     * Name of the field from the request data model that this message is associated with
     */
    public String getField() {
        return _field;
    }

    /**
     * Symbolic error code identifying the type of error reported by this message
     *
     * @see Error
     */
    public String getCode() {
        return _code;
    }

    /**
     * The request id, sent within the XRequest-Id http header.
     */
    public String getRequestId() {
        return _requestId;
    }

    public void setMessage(String m) {
        _message = m;
    }

    public void setField(String f) {
        _field = f;
    }

    public void setCode(String c) {
        _code = c;
    }

    public void setRequestId(String r) {
        _requestId = r;
    }
}
