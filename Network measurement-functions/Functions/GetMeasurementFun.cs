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
using Network_measurement_database.Model;
using Network_measurement_functions.Abstracts;
using System.Linq;

namespace Network_measurement_functions.Functions
{
    public  class GetMeasurementFun:AFun
    {
        public GetMeasurementFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("GetMeasurementFun")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getmeasurement/{measurementid}")] HttpRequest req,
            int measurementid,
            ILogger log)
        {
            log.LogInformation("Get measurement function processed a request.");

            var measurement = _nMContext.Measurements.Where(x => x.MeasurementId.Equals(measurementid)).FirstOrDefault();

            return new OkObjectResult(measurement);
        }
    }
}
