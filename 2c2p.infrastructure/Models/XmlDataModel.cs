using System.Collections.Generic;
using System.Xml.Serialization;

namespace _2c2p.infrastructure.Models
{
    [XmlRoot("Transactions")]
	public class XmlDataModel
	{
		[XmlElement("Transaction")]
		public List<TransactionData> Transaction { get; set; }
	}

	[XmlRoot("Transaction")]
	public class TransactionData
	{
		[XmlElement("TransactionDate")]
		public string TransactionDate { get; set; }

		[XmlElement("PaymentDetails")]
		public PaymentDetails PaymentDetails { get; set; }

		[XmlElement(ElementName = "Status")]
		public string Status { get; set; }

		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
	}

	[XmlRoot("PaymentDetails")]
	public class PaymentDetails
	{
		[XmlElement("Amount")]
		public decimal Amount { get; set; }

		[XmlElement("CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

}
