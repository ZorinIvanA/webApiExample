using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FitnessRecords.Domain.Interfaces;
using FitnessRecords.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sentry;
using Serilog;

namespace FitnessRecords.Presentation.Controllers
{
    [ApiController]
    [Route("exercises")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        private readonly ILogger<ExercisesController> _logger;

        public ExercisesController(IExerciseService exerciseService, ILogger<ExercisesController> logger)
        {
            _exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok((await _exerciseService.GetExercises())
                    .Select(exercise => new ExerciseModel(exercise)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ExerciseModel model)
        {
            try
            {
                Console.WriteLine("Getting excercises");

                if (model == null)
                {
                    Console.WriteLine("bad request");
                    return BadRequest("Введите данные");
                }

                await _exerciseService.AddExercise(model.ToEntity());
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }

        [HttpPut("file")]
        public async Task<IActionResult> PutFile()
        {
            try
            {
                var files = Request.Form.Files[0];
                var description = Request.Form["Description"];

                byte[] resultBytes = new byte[0];
                using (var st = files.OpenReadStream())
                {
                    st.Read(resultBytes);
                    await _exerciseService.AddFile(description, resultBytes);
                }



                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка, обратитесь в службу поддержки!");
            }
        }
    }
}
