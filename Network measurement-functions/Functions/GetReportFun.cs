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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getreport/{reportid}")] HttpRequest req,
            int reportid,
            ILogger log)
        {
            log.LogInformation("Get report function processed a request.");

            var user = _nMContext.MeasurementsReport.Where(x => x.MeasurementReportId == reportid);

            return new OkObjectResult(user);
        }
    }
}
