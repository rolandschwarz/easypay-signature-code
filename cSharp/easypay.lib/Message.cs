namespace com.swisscom.easypay
{

	/// <summary>
	/// The response message object represents a server message, which can be extracted and interpreted by the client.
	/// </summary>
	public class Message
	{

		private string _message;
		private string _field;
		private string _code;
		private string _requestId;

		/// <summary>
		/// Localized message describing the nature of the problem reported
		/// </summary>
		public virtual string getMessage()
		{
			return _message;
		}

		/// <summary>
		/// Name of the field from the request data model that this message is associated with
		/// </summary>
		public virtual string Field
		{
			get
			{
				return _field;
			}
			set
			{
				_field = value;
			}
		}

		/// <summary>
		/// Symbolic error code identifying the type of error reported by this message
		/// </summary>
		/// <seealso cref= Error </seealso>
		public virtual string Code
		{
			get
			{
				return _code;
			}
			set
			{
				_code = value;
			}
		}

		/// <summary>
		/// The request id, sent within the XRequest-Id http header.
		/// </summary>
		public virtual string RequestId
		{
			get
			{
				return _requestId;
			}
			set
			{
				_requestId = value;
			}
		}

		public virtual void setMessage(string m)
		{
			_message = m;
		}



	}

}