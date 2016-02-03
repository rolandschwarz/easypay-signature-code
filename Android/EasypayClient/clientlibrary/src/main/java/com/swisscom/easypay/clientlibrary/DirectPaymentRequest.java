package com.swisscom.easypay.clientlibrary;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * The direct payment is a kind of one-charge-event-payment. It does not need any management on the charging-engine side.
 */
public class DirectPaymentRequest {
    private double amount;
    private String paymentInfo;
    private boolean roaming;
    private boolean adultContent;
    private String userAgentOrigin;
    private String userSourceIP;
    private Operation operation;
    private String orderId;
    private String contentType;
    private String storeSource;

    /**
     * The amount of the service.
     * <b>Note:</b> Optional for Refund (if not set full charged amount fill be refunded).
     *
     * @return double amount
     */
    public double getAmount() {
        return amount;
    }

    /**
     * The payment info of the service (also known as billing text).
     * <b>Note:</b> The paymentInfo will be placed on the end user invoice bill.
     *
     * @return String
     */
    public String getPaymentInfo() {
        return paymentInfo;
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
    public boolean getRoaming() {
        return roaming;
    }

    /**
     * Flag which marks the service as adult (true) or non adult (false).
     * This means that the customer will be checked, if he/she is allowed to consume adult content.
     *
     * @return boolean
     */
    public boolean getAdultContent() {
        return adultContent;
    }

    /**
     * The UserAgent of the end customer
     * e.g. Mozilla/5.0 (Linux; U; Android 2.2.1; en-us; Nexus One Build/FRG83)
     *
     * @return String
     */
    public String getUserAgentOrigin() {
        return userAgentOrigin;
    }

    /**
     * The end user source IP
     *
     * @return String SourceIP
     */
    public String getUserSourceIP() {
        return userSourceIP;
    }

    /**
     * Allows to commit/ reject the payment or to refund the amount.
     * The amount to be refund must be the same or less as the initial payment.
     *
     * @return Operation Operation
     */
    public Operation getOperation() {
        return operation;
    }

    /**
     * The order id which is printed on end user invoice bill.
     *
     * @return String OrderId
     */
    public String getOrderId() {
        return orderId;
    }

    /**
     * The amount of the service.
     * <b>Note:</b> Optional for Refund (if not set full charged amount fill be refunded).
     *
     * @param a double amount
     */
    public void setAmount(double a) {
        amount = a;
    }

    /**
     * The payment info of the service (also known as billing text).
     * <b>Note:</b> The paymentInfo will be placed on the end user invoice bill.
     *
     * @param p String paymentInfo
     */
    public void setPaymentInfo(String p) {
        paymentInfo = p;
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
    public void setRoaming(boolean r) {
        roaming = r;
    }

    /**
     * Flag which marks the service as adult (true) or non adult (false).
     * This means that the customer will be checked, if he/she is allowed to consume adult content.
     *
     * @param a boolean isAdultContent
     */
    public void setAdultContent(boolean a) {
        adultContent = a;
    }

    /**
     * The UserAgent of the end customer
     * e.g. Mozilla/5.0 (Linux; U; Android 2.2.1; en-us; Nexus One Build/FRG83)
     *
     * @param u String userAgent
     */
    public void setUserAgentOrigin(String u) {
        userAgentOrigin = u;
    }

    /**
     * The end user source IP
     *
     * @param u String SourceIP
     */
    public void setUserSourceIP(String u) {
        userSourceIP = u;
    }

    /**
     * Allows to commit/ reject the payment or to refund the amount.
     * The amount to be refund must be the same or less as the initial payment.
     *
     * @param o Operation Operation
     */
    public void setOperation(Operation o) {
        operation = o;
    }

    /**
     * The order id which is printed on end user invoice bill.
     *
     * @param o String OrderId
     */
    public void setOrderId(String o) {
        orderId = o;
    }

    public void setContentType(String contentType) {
        this.contentType = contentType;
    }

    public String getContentType() {
        return contentType;
    }

    public void setStoreSource(String storeSource) {
        this.storeSource = storeSource;
    }

    public String getStoreSource() {
        return storeSource;
    }

    public JSONObject toJSONObject() throws JSONException {
        JSONObject jsonObj = new JSONObject();

        jsonObj.put("adultContent", adultContent);
        jsonObj.put("amount", amount);
        jsonObj.put("operation", operation);
        jsonObj.put("orderId", orderId);
        jsonObj.put("paymentInfo", paymentInfo);
        jsonObj.put("roaming", roaming);
        jsonObj.put("userAgentOrigin", userAgentOrigin);
        jsonObj.put("userSourceIP", userSourceIP);
        jsonObj.put("contentType", contentType);
        jsonObj.put("storeSource", storeSource);

        return jsonObj;
    }


}