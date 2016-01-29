namespace com.swisscom.easypay
{

	/// <summary>
	/// The direct payment is a kind of one-charge-event-payment. It does not need any management on the charging-engine side.
	/// ObjectMapper mapper = new ObjectMapper(); // can reuse, share globally
	/// User user = mapper.readValue(new File("DirectPaymentRequest.json"), DirectPaymentRequest.class);
	/// </summary>
	public class DirectPaymentRequest
	{
		private double _amount;
		private string _paymentInfo;
		private bool _roaming;
		private bool _adultContent;
		private string _userAgentOrigin;
		private string _userSourceIP;
		private Operation _operation;
		private string _orderId;

		/// <summary>
		/// The amount of the service.
		/// <b>Note:</b> Optional for Refund (if not set full charged amount fill be refunded).
		/// </summary>
		/// <returns> double amount </returns>
		public virtual double Amount
		{
			get
			{
				return _amount;
			}
			set
			{
				_amount = value;
			}
		}

		/// <summary>
		/// The payment info of the service (also known as billing text).
		/// <b>Note:</b> The paymentInfo will be placed on the end user invoice bill.
		/// </summary>
		/// <returns> String </returns>
		public virtual string PaymentInfo
		{
			get
			{
				return _paymentInfo;
			}
			set
			{
				_paymentInfo = value;
			}
		}

		/// <summary>
		/// Flag which marks the service as roaming (true) or none roaming (false).
		/// This means that the customer will be checked for roaming.
		/// If the customer has roaming and the flag was set to true, then the payment
		/// will be blocked. If the customer has roaming and the flag was set to false,
		/// then the payment will not be blocked.
		/// </summary>
		/// <returns> boolean isRoaming </returns>
		public virtual bool Roaming
		{
			get
			{
				return _roaming;
			}
			set
			{
				_roaming = value;
			}
		}

		/// <summary>
		/// Flag which marks the service as adult (true) or non adult (false).
		/// This means that the customer will be checked, if he/she is allowed to consume adult content.
		/// </summary>
		/// <returns> boolean </returns>
		public virtual bool AdultContent
		{
			get
			{
				return _adultContent;
			}
			set
			{
				_adultContent = value;
			}
		}

		/// <summary>
		/// The UserAgent of the end customer
		/// e.g. Mozilla/5.0 (Linux; U; Android 2.2.1; en-us; Nexus One Build/FRG83)
		/// </summary>
		/// <returns> String </returns>
		public virtual string UserAgentOrigin
		{
			get
			{
				return _userAgentOrigin;
			}
			set
			{
				_userAgentOrigin = value;
			}
		}

		/// <summary>
		/// The end user source IP
		/// </summary>
		/// <returns> String SourceIP </returns>
		public virtual string UserSourceIP
		{
			get
			{
				return _userSourceIP;
			}
			set
			{
				_userSourceIP = value;
			}
		}

		/// <summary>
		/// Allows to commit/ reject the payment or to refund the amount.
		/// The amount to be refund must be the same or less as the initial payment.
		/// </summary>
		/// <returns> Operation Operation </returns>
		public virtual Operation Operation
		{
			get
			{
				return _operation;
			}
			set
			{
				_operation = value;
			}
		}

		/// <summary>
		/// The order id which is printed on end user invoice bill.
		/// </summary>
		/// <returns> String OrderId </returns>
		public virtual string OrderId
		{
			get
			{
				return _orderId;
			}
			set
			{
				_orderId = value;
			}
		}








	}
}