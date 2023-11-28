using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Network_measurement_functions.Functions
{
    public static class STPingableFun
    {
        [FunctionName("STPingableFun")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seedtest/ping")] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("HTTP trigger ST Ping function processed a request.");

            return new OkObjectResult("Function is alive");
        }
    }
}
