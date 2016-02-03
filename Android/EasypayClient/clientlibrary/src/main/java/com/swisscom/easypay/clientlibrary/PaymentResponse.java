package com.swisscom.easypay.clientlibrary;

import java.util.ArrayList;

/**
 * The response message list object represents the server messages, which can be extracted and interpreted  by the client.
 */
public class PaymentResponse {
    private ArrayList<Message> messages;
    private boolean success;
    private Exception exception;

    public PaymentResponse(){
        messages = new ArrayList<>();
    }

    public void setSuccess(boolean success) {
        this.success = success;
    }

    public boolean isSuccess() {
        return success;
    }

    public ArrayList<Message> getMessages() {
        return messages;
    }

    public void setException(Exception exception) {
        this.success = false;
        this.exception = exception;
    }

    public Exception getException() {
        return exception;
    }
}
