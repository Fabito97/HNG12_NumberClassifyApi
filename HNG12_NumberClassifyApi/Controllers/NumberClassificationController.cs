using HNG12_NumberClassifyApi.Model;
using HNG12_NumberClassifyApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HNG12_NumberClassifyApi.Controllers
{
    [Route("api/classify-number")]
    [ApiController]
    public class NumberClassificationController : ControllerBase
    {
        private readonly NumberClassificationService _classificationService;

        public NumberClassificationController(NumberClassificationService classificationService)
        {
            _classificationService = classificationService;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> GetNumber(string number)
        {
            if (!int.TryParse(number, out int Validnumber) || Validnumber < 1)
            {
                return BadRequest(new {number, error = true});
            }

            var response = await _classificationService.ClassifyNumberAsync(Validnumber);
           
            return Ok(response);
        }
    }
}
