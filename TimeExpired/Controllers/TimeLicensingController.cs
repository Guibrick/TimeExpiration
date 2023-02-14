using Microsoft.AspNetCore.Mvc;
using TimeExpired.Models;
using TimeExpired.Services;

namespace TimeExpired.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeLicensingController : ControllerBase
    {
        private readonly IDateFileClient _dateFileClient;
        private readonly ILogger<TimeLicensingController> _logger;

        public TimeLicensingController(ILogger<TimeLicensingController> logger, IDateFileClient dateFileClient)
        {
            _logger = logger;
            _dateFileClient = dateFileClient;
        }

        [Route("getexp")]
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetExpirationDate(int id)
        {
            try
            {
                var date = await _dateFileClient.GetDateInput(id);

                return date;
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddDate(int dateId, DateTime date)
        {
            try
            {
                var dateValue = await _dateFileClient.AddDate(dateId, date);
                var response = new DateInput
                {
                    Id = dateId,
                    Date = date,
                };

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }
    }
}




