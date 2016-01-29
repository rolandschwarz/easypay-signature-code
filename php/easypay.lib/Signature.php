<?php

namespace easpaylib;

/**
 * Create <b>HmacSHA1</b> signature for data with key
 */
class Signature
{
    const SHA1 = "sha1";
    const MD5 = "md5";

    /**
     * Create <b>HmacSHA1</b> byte[] signature for data and key
     * @param string $data
     * @param string $key
     * @return string
     */
    public static function sign($data, $key)
    {
        return hash_hmac(self::SHA1, $data, $key, true);
    }

    /**
     * Create <b>MD5</b> byte[] hash for data
     * @param string $data
     * @return string
     */
    public static function hash($data)
    {
        return hash(self::MD5, $data, true);
    }
}
