using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcercisesCompletion.Presentation
{
    [Route("api/v1/completion")]
    [ApiController]
    public class CompletionController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
