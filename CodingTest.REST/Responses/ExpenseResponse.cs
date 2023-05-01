using System.Xml.Linq;
using System.Xml.Serialization;

namespace CodingTest.REST.Responses;

[Serializable()]
public class ExpenseResponse
{
    [System.Xml.Serialization.XmlElement("expense")]
    public Expense Expense { get; set; }

    [System.Xml.Serialization.XmlElement("vendor")]
    public string Vendor { get; set; }

    [System.Xml.Serialization.XmlElement("description")]
    public string Description { get; set; }

    [System.Xml.Serialization.XmlElement("date")]
    public string Date { get; set; }
}

[Serializable()]
public class Expense
{
    private string _costCentre;
    [System.Xml.Serialization.XmlElement("cost_centre")]
    public string CostCentre {
        get { return string.IsNullOrEmpty(_costCentre)? "UNKNOWN": _costCentre; }
        set { _costCentre = value; }
    }

    [System.Xml.Serialization.XmlElement("total")]
    [XmlIgnore]
    public decimal? Total { get; set; }

    [System.Xml.Serialization.XmlElement("payment_method")]
    public string PaymentMethod { get; set; }
}
