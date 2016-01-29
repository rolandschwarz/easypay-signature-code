namespace easypay.swisscom.com
{
    using System.Security.Cryptography;

    /// <summary>
    ///     Create <b>HmacSHA1</b> signature for data with key
    /// </summary>
    public class Signature
    {
        /// <summary>
        ///     create data-string for signature
        /// </summary>
        /// <param name="httpRequestMethod"> String </param>
        /// <param name="contentHash">       String </param>
        /// <param name="contentType">       String </param>
        /// <param name="date">              String </param>
        /// <param name="path">              String </param>
        /// <returns> String </returns>
        public static string CreateHashString(string httpRequestMethod, string contentHash, string contentType, string date, string path)
        {
            return httpRequestMethod + '\n' + contentHash + '\n' + contentType + '\n' + date + '\n' + path;
        }

        public static byte[] HashData(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }

        /// <summary>
        ///     Create <b>HmacSHA1</b> byte[] signature for data and key
        /// </summary>
        /// <param name="key">  byte[] </param>
        /// <param name="data"> byte[] </param>
        /// <returns> signature byte[] </returns>
        public static byte[] Sign(byte[] key, byte[] data)
        {
            using (var hmac = new HMACSHA1(key))
            {
                // Compute the hash of hashString.
                return hmac.ComputeHash(data);
            }
        }
    }
}