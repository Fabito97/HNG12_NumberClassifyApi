using HNG12_NumberClassifyApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace HNG12_NumberClassifyApi.Services
{
    public class NumberClassificationService
    {
        private readonly HttpClient _httpClient;

        public NumberClassificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NumberClassificationResult> ClassifyNumber(int number)
        {
            var funFactTask = GetFunfactAsync(number);

            NumberClassificationResult classificationResult = new NumberClassificationResult()
            {
                number = number,
                digit_sum = GetDigitSum(number),
                is_perfect = IsPerfect(number),
                is_prime = IsPrime(number),
                properties = GetProperties(number).ToArray(),
                fun_fact = await funFactTask
            };

            return classificationResult;
        }

        public async Task<string> GetFunfactAsync(int number)
        {
            try
            {
                string response = await _httpClient.GetStringAsync($"http://numbersapi.com/{number}/math");
                return response ?? "No fun facts available";
            }
            catch (HttpRequestException)
            {
                return $"An unexpected error occurred while retrieving fun fact for {number}";
            }
            catch (Exception)
            {
                return $"An unexpected error occurred";
            }
        }

        //public async Task<string> GetFunfactAsync(int number)
        //{
              // Determine whether the external api takes too long to fetch
        //    try
        //    {
        //        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(500)); // Set timeout to 500ms
        //        return await _httpClient.GetStringAsync($"http://numbersapi.com/{number}/math", cts.Token);
        //    }
        //    catch (TaskCanceledException)
        //    {
        //        return "Fun fact retrieval timed out.";
        //    }
        //    catch (HttpRequestException)
        //    {
        //        return "Error retrieving fun fact.";
        //    }
        //}

        public List<string> GetProperties(int number)
        {
            List<string> properties = new List<string>();

            if (IsArmstrong(number))
                properties.Add("armstrong");

            bool isEven = number % 2 == 0;

            properties.Add( isEven ? "even" : "odd");
                        
            return properties;
        }

        public bool IsArmstrong(int number)
        {
            int temp = number;
            int sum = 0;
            int digitCount = number.ToString().Length;

            while (temp > 0)
            {
                int digit = temp % 10;

                sum += (int)Math.Pow(digit, digitCount);
                temp /= 10;
            }

            return sum == number;

        }

        public bool IsPrime(int number)
        {
            if (number <= 1)            
                return false;
          
            else if (number == 2)          
                return true;
         
            else if (number % 2 == 0)            
                return false;
           

            for (int i = 3; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) 
                    return false;                
            }

            return true;
        }

        public int GetDigitSum(int number)
        {           
            int sum = 0;
            
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }

        public bool IsPerfect(int number)
        {
            if (number <= 1)
                return false;

            int sum = 1;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    sum += i;

                    if (i != number / i)
                        sum += number / i;
                }
            }
           
            return sum == number;
        }


    }
}
