package com.swisscom.easypay.clientlibrary;

import java.security.NoSuchAlgorithmException;
import java.security.SignatureException;

import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;

/**
 * Create <b>HmacSHA1</b> signature for data with key
 */
public class Signature {
    public static final String DEFAULT_ENCODING = "UTF-8";

    private static final String HmacSHA1 = "HmacSHA1";
    private static final String MD5 = "MD5";

    /**
     * Create <b>HmacSHA1</b> byte[] signature for data and key
     *
     * @param data byte[]
     * @param key  byte[]
     * @return signature byte[]
     * @throws SignatureException
     */
    public static byte[] sign(byte[] data, byte[] key) throws SignatureException {
        try {
            Mac mac = Mac.getInstance(HmacSHA1);
            mac.init(new SecretKeySpec(key, HmacSHA1));
            return mac.doFinal(data);
        } catch (Exception e) {
            throw new SignatureException("Unable to calculate a request signature: " + e.getMessage(), e);
        }
    }

    /**
     * Create <b>MD5</b> byte[] hash for data
     *
     * @param data byte[]
     * @return hash byte[]
     */
    public static byte[] hash(byte[] data) {

        java.security.MessageDigest md = null;
        try {
            md = java.security.MessageDigest.getInstance(MD5);
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        byte[] array = new byte[0];
        if (md != null) {
            array = md.digest(data);
        }
        return array;

    }
}
