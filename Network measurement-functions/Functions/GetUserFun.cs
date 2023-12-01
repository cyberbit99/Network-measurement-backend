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
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Network_measurement_functions.Functions
{
    public class GetUserFun:AFun
    {
        public GetUserFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("GetUserFun")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getuser/{username}")] HttpRequest req,
            string username,
            ILogger log)
        {
            log.LogInformation("Get user function processed a request.");

            var user = _nMContext.Users.Where(x => x.Username == username);

            return new OkObjectResult(user);
        }
    }
}
