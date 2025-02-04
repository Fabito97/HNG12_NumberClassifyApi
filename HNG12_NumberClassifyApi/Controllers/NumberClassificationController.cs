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

            var digitSumTask = Task.Run(() => _classificationService.GetDigitSum(Validnumber));
            var isPerfectTask = Task.Run(() => _classificationService.IsPerfect(Validnumber));
            var isPrimeTask = Task.Run(() => _classificationService.IsPrime(Validnumber));
            var propertiesTask = Task.Run(() => _classificationService.GetProperties(Validnumber).ToArray());
            var funFactTask = await _classificationService.GetFunfactAsync(Validnumber);

            await Task.WhenAll(digitSumTask, isPerfectTask, isPrimeTask, propertiesTask);


            NumberClassificationResult classificationResult = new NumberClassificationResult()
            {
                number = Validnumber,
                digit_sum = digitSumTask.Result,
                is_perfect = isPerfectTask.Result,
                is_prime = isPrimeTask.Result,
                properties =propertiesTask.Result,
                fun_fact = await _classificationService.GetFunfactAsync(Validnumber)
            };                   

           
            return Ok(classificationResult);
        }
    }
}
