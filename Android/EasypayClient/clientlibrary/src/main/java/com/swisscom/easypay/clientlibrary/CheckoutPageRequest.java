package com.swisscom.easypay.clientlibrary;

import org.json.JSONException;
import org.json.JSONObject;

public class CheckoutPageRequest extends DirectPaymentRequest {
    /**
     * Title of the content.
     * example: Online Newspaper
     */
    private String Title;

    public final String getTitle() {
        return Title;
    }

    public final void setTitle(String value) {
        Title = value;
    }

    /**
     * Short description of the subscription/one-time charge
     * example: Read your favorite newspaper every day everywhere!
     */
    private String Description;

    public final String getDescription() {
        return Description;
    }

    public final void setDescription(String value) {
        Description = value;
    }

    /**
     * The duration of the service subscription
     * example: 1
     */
    private int Duration;

    public final int getDuration() {
        return Duration;
    }

    public final void setDuration(int value) {
        Duration = value;
    }

    /**
     * The duration unit of the service subscription
     * example: (WEEK|MONTH)
     */
    private String DurationUnit;

    public final String getDurationUnit() {
        return DurationUnit;
    }

    public final void setDurationUnit(String value) {
        DurationUnit = value;
    }

    /**
     * The Promotion Amount of the Service.
     * must NOT be negative value, if negative value is sent it will be ignored and the amount will be charged instead.
     * If value is positive and less or equal as amount it will be charged instead of amount.
     * example: 2.25
     */
    private String PromotionAmount;

    public final String getPromotionAmount() {
        return PromotionAmount;
    }

    public final void setPromotionAmount(String value) {
        PromotionAmount = value;
    }

    /**
     * The merchant id given by Swisscom
     * example: CH011
     */
    private String MerchantId;

    public final String getMerchantId() {
        return MerchantId;
    }

    public final void setMerchantId(String value) {
        MerchantId = value;
    }

    /**
     * The cancel URL, which will be called, if the user cancels the authorization request on the COP.
     * example: http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=cancel
     */
    private String CancelUrl;

    public final String getCancelUrl() {
        return CancelUrl;
    }

    public final void setCancelUrl(String value) {
        CancelUrl = value;
    }

    /**
     * The error URL, which will be called, if some error/problem occur during the authorization process on the COP.
     * example: http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=error
     */
    private String ErrorUrl;

    public final String getErrorUrl() {
        return ErrorUrl;
    }

    public final void setErrorUrl(String value) {
        ErrorUrl = value;
    }

    /**
     * The success back URL, which will be called, if the user proceed the authorization request on the COP with success.
     * example: http://my.shop.com/home.jsf?uid = 43c2&sid=dc0d&purchase=success
     */
    private String SuccessUrl;

    public final String getSuccessUrl() {
        return SuccessUrl;
    }

    public final void setSuccessUrl(String value) {
        SuccessUrl = value;
    }

    /**
     * The service Id on content partner side. This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
     * example: xys-323-gh-ff
     */
    private String CpServiceId;

    public final String getCpServiceId() {
        return CpServiceId;
    }

    public final void setCpServiceId(String value) {
        CpServiceId = value;
    }

    /**
     * The unique subscription Id on content partner side.This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
     * example: vghv5678
     */
    private String CpSubscriptionId;

    public final String getCpSubscriptionId() {
        return CpSubscriptionId;
    }

    public final void setCpSubscriptionId(String value) {
        CpSubscriptionId = value;
    }

    /**
     * The unique user Id on content partner side. This is an optional parameter.It is recommended to send it anyway, as it can be very helpful for debugging.
     * example:user-5
     */
    private String CpUserId;

    public final String getCpUserId() {
        return CpUserId;
    }

    public final void setCpUserId(String value) {
        CpUserId = value;
    }

    /**
     * The URL of the dynamic image which will be embedded in the COP.
     * Special feature is needed !
     * example: http://lorempixel.com/300/200
     */
    private String ImageUrl;

    public final String getImageUrl() {
        return ImageUrl;
    }

    public final void setImageUrl(String value) {
        ImageUrl = value;
    }

    public JSONObject toJSONObject() throws JSONException {
        JSONObject jsonObj = super.toJSONObject();

        jsonObj.put("cancelUrl", CancelUrl);
        jsonObj.put("cpServiceId", CpServiceId);
        jsonObj.put("cpSubscriptionId", CpSubscriptionId);
        jsonObj.put("description", Description);
        jsonObj.put("duration",Duration);
        jsonObj.put("durationUnit", DurationUnit);
        jsonObj.put("errorUrl", ErrorUrl);
        jsonObj.put("imageUrl", ImageUrl);
        jsonObj.put("merchantId", MerchantId);
        jsonObj.put("promotionAmount", PromotionAmount);
        jsonObj.put("successUrl", SuccessUrl);
        jsonObj.put("title", Title);

        return jsonObj;
    }
}
