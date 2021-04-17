using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sentry;
using Serilog;

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
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://074d9f90cfc44b52a2d3c1d1d604842b@o572490.ingest.sentry.io/5721941")
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {

                logger.Error("Новый поиск упражнений");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("sentry-header", "123");
                    var url = _configuration.GetSection("FitnessRecordsUri").Value;
                    var resultMessage = await client.GetAsync($"{url}exercises");
                    resultMessage.EnsureSuccessStatusCode();
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
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
