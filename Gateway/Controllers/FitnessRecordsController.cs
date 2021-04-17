using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Gateway.Controllers
{
    [Route("api/v1/fitness-records")]
    [ApiController]
    public class FitnessRecordsController : ControllerBase
    {
        private IConfiguration _configuration;

        public FitnessRecordsController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<IActionResult> GetFitnessRecords()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = _configuration.GetSection("FitnessRecordsUri").Value;
                    var resultMessage = await client.GetAsync($"{url}exercises");
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> SaveExercise(object value)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = _configuration.GetSection("FitnessRecordsUri").Value;
                    var resultMessage = await client.PutAsJsonAsync($"{url}exercises", value);
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
