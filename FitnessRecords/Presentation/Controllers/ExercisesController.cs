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

namespace FitnessRecords.Presentation.Controllers
{
    [ApiController]
    [Route("exercises")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
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
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ExerciseModel model)
        {
            if (model == null)
                return BadRequest("Введите данные");

            await _exerciseService.AddExercise(model.ToEntity());
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
