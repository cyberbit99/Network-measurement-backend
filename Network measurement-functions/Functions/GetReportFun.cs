using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Network_measurement_database.Repository;
using Network_measurement_functions.Abstracts;
using System.Linq;

namespace Network_measurement_functions.Functions
{
    public  class GetReportFun:AFun
    {
        public GetReportFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("GetReportFun")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getreport/{userid}")] HttpRequest req,
            int userid,
            ILogger log)
        {
            log.LogInformation("Get report function processed a request.");

            var report = _nMContext.Measurement_Reports.Where(x => x.UserId == userid).ToList();
            if (!report.Any())
            {
                return new OkObjectResult(null);
            }
            else
            {
                return new OkObjectResult(report);
            }
            
        }
    }
}
