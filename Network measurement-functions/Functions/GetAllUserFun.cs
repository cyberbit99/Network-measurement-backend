using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Network_measurement_functions.Abstracts;
using Network_measurement_database.Repository;

namespace Network_measurement_functions.Functions
{
    public class GetAllUserFun:AFun
    {
        public GetAllUserFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("GetAllUserFun")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "alluser")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var allUser = _nMContext.Users;

            return new OkObjectResult(allUser);
        }
    }
}
