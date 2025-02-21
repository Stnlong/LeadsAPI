using LeadsAPI.Models;
using LeadsAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LeadsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAngularApp")]
    public class LeadsController : Controller
    {
        private readonly ILogger<LeadsController> _logger;
        private readonly LeadsService _leadsService;

        public LeadsController(ILogger<LeadsController> logger, LeadsService leadsService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _leadsService = leadsService ?? throw new ArgumentNullException(nameof(leadsService));
        }
        // implement authorization here
        [HttpGet(Name = "GetLeads")]
        public ActionResult<IEnumerable<Leads>> Get()
        {
            try
            {
                List<Leads> leads = _leadsService.GetLeads();
                return Ok(leads);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving leads.");
                return StatusCode(500, "Internal server error");
            }
        }

        // add rate limiting, ensure https in startup/program
        // add authorization/authentication
        [HttpPost("AddLeads")]
        public async Task<IActionResult> ReceiveLeads([FromBody] List<Leads> newLeads)
        {
            try
            {
                if (newLeads == null || newLeads.Count == 0)
                {
                    return BadRequest("No leads received.");
                }
                // implement data validation

                await _leadsService.AddLeadsAsync(newLeads);
                return Ok(new { message = "Leads processed successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error processing leads");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
