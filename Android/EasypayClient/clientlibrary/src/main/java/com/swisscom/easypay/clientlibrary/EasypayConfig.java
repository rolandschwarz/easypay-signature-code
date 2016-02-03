package com.swisscom.easypay.clientlibrary;

public class EasypayConfig {
    private String Basepath;
    public final String getBasepath()
    {
        return Basepath;
    }
    public final void setBasepath(String value)
    {
        Basepath = value;
    }
    private String EasypaySecret;
    public final String getEasypaySecret()
    {
        return EasypaySecret;
    }
    public final void setEasypaySecret(String value)
    {
        EasypaySecret = value;
    }
    private String Host;
    public final String getHost()
    {
        return Host;
    }
    public final void setHost(String value)
    {
        Host = value;
    }
    private String MerchantId;
    public final String getMerchantId()
    {
        return MerchantId;
    }
    public final void setMerchantId(String value)
    {
        MerchantId = value;
    }

}
