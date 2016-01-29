using System;

namespace com.swisscom.easypay
{

	using Algorithm = com.sun.org.apache.xml.@internal.security.algorithms.Algorithm;



	/// <summary>
	/// Create <b>HmacSHA1</b> signature for data with key
	/// </summary>
	public class Signature
	{
		private const string DEFAULT_ENCODING = "UTF-8";
		private const string HmacSHA1 = "HmacSHA1";

		/// <summary>
		/// Create <b>HmacSHA1</b> string signature for data and key
		/// </summary>
		/// <param name="data"> String </param>
		/// <param name="key">  String </param>
		/// <returns> signature String </returns>
		/// <exception cref="SignatureException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String signString(String data, String key) throws java.security.SignatureException
		public virtual string signString(string data, string key)
		{
			try
			{
				sbyte[] dataBytes = data.GetBytes(DEFAULT_ENCODING);
				sbyte[] keyBytes = key.GetBytes(DEFAULT_ENCODING);
				sbyte[] signature = sign(dataBytes, keyBytes);

				return new string(Base64.Encoder.encode(signature));
			}
			catch (UnsupportedEncodingException e)
			{
				throw new SignatureException("Unable to calculate a request signature: " + e.Message, e);
			}
		}

		/// <summary>
		/// Create <b>HmacSHA1</b> byte[] signature for data and key
		/// </summary>
		/// <param name="data"> byte[] </param>
		/// <param name="key">  byte[] </param>
		/// <returns> signature byte[] </returns>
		/// <exception cref="SignatureException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public byte[] sign(byte[] data, byte[] key) throws java.security.SignatureException
		public virtual sbyte[] sign(sbyte[] data, sbyte[] key)
		{
			try
			{
				Mac mac = Mac.getInstance(HmacSHA1);
				mac.init(new SecretKeySpec(key, HmacSHA1));
				return mac.doFinal(data);
			}
			catch (Exception e)
			{
				throw new SignatureException("Unable to calculate a request signature: " + e.Message, e);
			}
		}
	}

}