using CodingTest.REST.Controllers;
using CodingTest.REST.Responses;

namespace CodingTest.REST.Services;

public interface IEmailExtractService
{
    ExpenseResponse? ExtractEmailData(string? emailMessage);
}
