namespace com.swisscom.easypay
{



	/// <summary>
	/// Created by lambda on 11/23/2015.
	/// </summary>
	public class EasypayRequest
	{
		/// <summary>
		/// create data-string for signature
		/// </summary>
		/// <param name="httpRequestMethod"> String </param>
		/// <param name="contentHash">       String </param>
		/// <param name="contentType">       String </param>
		/// <param name="date">              String </param>
		/// <param name="path">              String </param>
		/// <returns> String </returns>
		private string createHashString(string httpRequestMethod, string contentHash, string contentType, string date, string path)
		{
			return httpRequestMethod + '\n' + contentHash + '\n' + contentType + '\n' + date + '\n' + path;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public MessageList paymentRequest(DirectPaymentRequest directPaymentRequest, EasypayConfig config) throws java.net.MalformedURLException
		public virtual MessageList paymentRequest(DirectPaymentRequest directPaymentRequest, EasypayConfig config)
		{

			URL url = new URL("https://" + config.Host);
			string httpRequestMethod = "POST";
			string contentHash = "xxx";
			string contentType = "??";
			string date = "";
			string path = "";

			HttpURLConnection urlConnection = null;
			try
			{
				urlConnection = (HttpURLConnection) url.openConnection();

				urlConnection.RequestMethod = httpRequestMethod;
				urlConnection.setRequestProperty("Content-Type", contentType);
				// urlConnection.setRequestProperty("Content-Length", "" + Integer.toString(postData.getBytes().length));

				urlConnection.UseCaches = false;
				urlConnection.DoInput = true;
				urlConnection.DoOutput = true;

				System.IO.Stream @out = new BufferedOutputStream(urlConnection.OutputStream);
				// writeStream(out);

				System.IO.Stream @in = new BufferedInputStream(urlConnection.InputStream);
				// readStream(in);


			}
			catch (IOException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			finally
			{
				if (urlConnection != null)
				{
					urlConnection.disconnect();
				}
			}
			return new MessageList();
		}
	}

}