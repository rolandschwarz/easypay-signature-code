package com.swisscom.easypay.clientlibrary;

public class StatusResponse {

    private String OrderID;
    private String PaymentInfo;
    private boolean IsSilentAuthenticated;
    private String Amount;
    private String Msisdn;
    private String Status;
    private String FormattedMsisdn;

    public final String getOrderID() {
        return OrderID;
    }

    public final void setOrderID(String value) {
        OrderID = value;
    }

    public final String getPaymentInfo() {
        return PaymentInfo;
    }

    public final void setPaymentInfo(String value) {
        PaymentInfo = value;
    }

    public final boolean getIsSilentAuthenticated() {
        return IsSilentAuthenticated;
    }

    public final void setIsSilentAuthenticated(boolean value) {
        IsSilentAuthenticated = value;
    }

    public final String getAmount() {
        return Amount;
    }

    public final void setAmount(String value) {
        Amount = value;
    }

    public final String getMsisdn() {
        return Msisdn;
    }

    public final void setMsisdn(String value) {
        Msisdn = value;
    }

    public final String getStatus() {
        return Status;
    }

    public final void setStatus(String value) {
        Status = value;
    }

    public final String getFormattedMsisdn() {
        return FormattedMsisdn;
    }

    public final void setFormattedMsisdn(String value) {
        FormattedMsisdn = value;
    }
}
