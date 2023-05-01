using CodingTest.REST.Requests;
using CodingTest.REST.Responses;
using CodingTest.REST.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodingTest.REST.Controllers;
public class ExpenseController : ApiController
{
    private readonly IEmailExtractService _emailExtractService;

    public ExpenseController(IEmailExtractService emailExtractService)
    {
        _emailExtractService = emailExtractService;
    }

    [HttpPost]
    [Route("CreateExpense")]
    [ProducesResponseType(typeof(ExpenseResponse), (int)HttpStatusCode.OK)]
    public ActionResult<ExpenseResponse> CreateExpense([FromBody] CreateExpenseRequest request)
    {
        var response = _emailExtractService.ExtractEmailData(request?.EmailMessage);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "The request is invalid, please contact administrator.");
        }
        return Ok(response);
    }
   
}
