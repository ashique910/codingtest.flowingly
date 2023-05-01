using CodingTest.REST.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CodingTest.REST.Tests.Services;

public class EmailExtractServiceTests
{
    public Mock<ILogger<EmailExtractService>> mockLogger = new Mock<ILogger<EmailExtractService>>();

    [Fact]
    public void ExtractEmailData_ShouldReturnValidResponseIfRequestIsValid()
    {
        var service = new EmailExtractService(mockLogger.Object);
        string emailMessage = "Hi Patricia,Please create an expense claim for the below. Relevant details are marked up as requested...<expense><cost_centre>DEV632</cost_centre><total>35,000</total><payment_method>personalcard</payment_method></expense>From: William SteeleSent: Friday, 16 June 2022 10:32 AMTo: Maria WashingtonSubject: testHi Maria,Please create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our<description>development team’s project endcelebration</description> on <date>27 April2022</date> at 7.30pm.Regards,William";

        var result = service.ExtractEmailData(emailMessage);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("<expense><cost_centre></cost_centre><total>35,000</total><payment_method>personalcard</payment_method></expense>From: William SteeleSent: Friday, 16 June 2022 10:32 AMTo: Maria WashingtonSubject: testHi Maria,Please create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our<description>development team’s project endcelebration</description> on <date>27 April2022</date> at 7.30pm.Regards,William")]
    [InlineData("<expense><total>35,000</total><payment_method>personalcard</payment_method></expense>From: William SteeleSent: Friday, 16 June 2022 10:32 AMTo: Maria WashingtonSubject: testHi Maria,Please create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our<description>development team’s project endcelebration</description> on <date>27 April2022</date> at 7.30pm.Regards,William")]
    public void ExtractEmailData_ShouldReturnUnknownCostCentreInResponseIfCostCentreIsMissing(string emailMessage)
    {
        var service = new EmailExtractService(mockLogger.Object);

        var result = service.ExtractEmailData(emailMessage);

        Assert.Equal(result.Expense.CostCentre, "UNKNOWN");
    }

    [Theory]
    // Missing total
    [InlineData("<expense><cost_centre>DEV632</cost_centre><payment_method>personalcard</payment_method></expense>From: William SteeleSent: Friday, 16 June 2022 10:32 AMTo: Maria WashingtonSubject: testHi Maria,Please create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our<description>development team’s project endcelebration</description> on <date>27 April2022</date> at 7.30pm.Regards,William")]
    
    // Opening tags have no closing tag
    [InlineData("<expense><cost_centre>DEV632<payment_method>personalcard</payment_method></expense>From: William SteeleSent: Friday, 16 June 2022 10:32 AMTo: Maria WashingtonSubject: testHi Maria,Please create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our<description>development team’s project endcelebration</description> on <date>27 April2022</date> at 7.30pm.Regards,William")]
    public void ExtractEmailData_ShouldReturnNullResponseIfRequestIsInvalid(string emailMessage)
    {
        var service = new EmailExtractService(mockLogger.Object);

        var result = service.ExtractEmailData(emailMessage);

        Assert.Null(result);
    }
}
