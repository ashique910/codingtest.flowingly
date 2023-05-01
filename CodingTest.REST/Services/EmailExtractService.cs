using CodingTest.REST.Responses;
using System.Globalization;
using System.Xml.Serialization;

namespace CodingTest.REST.Services;

public class EmailExtractService : IEmailExtractService
{
    private readonly ILogger<EmailExtractService> _logger;

    public EmailExtractService(ILogger<EmailExtractService> logger)
    {
        _logger = logger;
    }

    public ExpenseResponse? ExtractEmailData(string? emailMessage)
    {
        if (string.IsNullOrEmpty(emailMessage))
        {
            _logger.LogError("The input message is empty");
            return null;
        }

        object? response = DeserializeMessage(emailMessage);

        // If the Desirialize went wrong response will be null
        if (response is null)
        {
            return null;
        }

        if (((ExpenseResponse)response).Expense?.Total is null)
        {
            // Log error, Total is missing
            _logger.LogError("Total in the input data is missing");
            return null;
        }

        return (ExpenseResponse)response;
    }

    private object? DeserializeMessage(string? emailMessage)
    {
        object? response = null;
        XmlSerializer serializer = new XmlSerializer(typeof(ExpenseResponse));
        serializer.UnknownElement += Serializer_UnknownElement;

        try
        {
            response = serializer.Deserialize(new StringReader("<ExpenseResponse>" + emailMessage + "</ExpenseResponse>"));
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, ex.Message);
        }

        return response;
    }

    private void Serializer_UnknownElement(object? sender, XmlElementEventArgs e)
    {
        if (e.Element.Name == "total")
        {
            var total = decimal.Parse(string.IsNullOrEmpty(e.Element.InnerText) ? "0" : e.Element.InnerText, CultureInfo.InvariantCulture);
            var expense = (Expense)e.ObjectBeingDeserialized;
            expense.Total = total;
        }
    }
}
