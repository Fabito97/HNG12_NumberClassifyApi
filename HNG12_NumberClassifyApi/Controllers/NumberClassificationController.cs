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

        [HttpGet]
        public async Task<IActionResult> GetNumber([FromQuery]string number)
        {
            if (!int.TryParse(number, out int Validnumber))
            {
                return BadRequest(new {number, error = true});
            }

            NumberClassificationResult classificationResult = new NumberClassificationResult()
            {
                number = Validnumber,
                digit_sum = _classificationService.GetDigitSum(Validnumber),
                is_perfect = _classificationService.IsPerfect(Validnumber),
                is_prime = _classificationService.IsPrime(Validnumber),
                properties = _classificationService.GetProperties(Validnumber).ToArray(),
                fun_fact = await _classificationService.GetFunfactAsync(Validnumber)
            };                   

           
            return Ok(classificationResult);
        }
    }
}
