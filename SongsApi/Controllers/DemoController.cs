using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApi.Controllers
{
    public class DemoController : ControllerBase
    {
        private readonly IProvideServerStatus _statusService;

        public DemoController(IProvideServerStatus statusService)
        {
            _statusService = statusService;
        }

        // GET /status
        [HttpGet("/status")]
        public ActionResult<GetStatusResponse> GetTheStatus()
        {

            GetStatusResponse response = _statusService.GetMyStatus();
            return Ok(response);
        }
        // GET /products/38938983983 (Route Param)
        [HttpGet("/products/{productId:int}")] // Route Constraints.
        public ActionResult LookupProduct(int productId)
        {
            return Ok("Information about product: " + productId);
        }

        // GET /employees
        // GET /employees?department=QA
        [HttpGet("/employees")]
        public ActionResult GetEmployees([FromQuery] string department = "All")
        {
            return Ok("Getting Employees Collection (" + department + ")");
        }

        [HttpGet("/whoami")]
        public ActionResult WhoAmi([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok($"I see you are running {userAgent}");
        }

        [HttpPost("/employees")]
        public ActionResult HireAnEmployee([FromBody] PostEmployeeRequest employeeToHire)
        {
            return Ok($"Hiring {employeeToHire.LastName} in the {employeeToHire.Department} dept.");
        }
    }

    public class GetStatusResponse
    {
        public string Message { get; set; }
        public DateTime LastChecked { get; set; }
    }


    public class PostEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }

}
