import crypto = require('crypto');

export module easypay
{
    /**
    * Create <b>HmacSHA1</b> signature for data with key
    **/
    export class Signature
    {
        /**
        * create data-string for signature
        **/
        public static createHashString( httpRequestMethod : string,  contentHash : string,  contentType : string,  date: string,  path: string) : string
        {
            return httpRequestMethod + "\n" + contentHash + "\n" + contentType + "\n" + date + "\n" + path;
        }

		/**
		* Create <b>MD5</b> hash for data
		* @param {string} data
		* @returns {string} hash base64 encoded
		**/
        public static hashData(data: string) : string
        {
			var md5Sum = <any>crypto.createHash("md5");
			md5Sum.setEncoding("base64");
			md5Sum.write(data);
			md5Sum.end();
			var md5Hash = md5Sum.read();
			return md5Hash;
        }

        /**
        * Create <b>HmacSHA1</b> signature for data and key
		* @param {string} key
		* @param {string} data
		* @returns {string | Buffer} signature base64 encoded
        **/
        public static sign(key: string,  data: string) : string
        {
			var algorithm = "sha1";
            var hmac = crypto.createHmac(algorithm, key);
            hmac.setEncoding("base64");
            hmac.write(data);
            hmac.end();
            var shaHash = hmac.read();
			return <string>shaHash;
        }
    }
}
