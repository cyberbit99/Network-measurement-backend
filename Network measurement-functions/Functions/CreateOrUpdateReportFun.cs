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
    public  class CreateOrUpdateReportFun:AFun
    {
        public CreateOrUpdateReportFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("CreateOrUpdateReportFun")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "cou/report")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create or update report function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MeasurementReport data = JsonConvert.DeserializeObject<MeasurementReport>(requestBody);

            var report = _nMContext.MeasurementsReport.Where(x => x.MeasurementReportId.Equals(data.MeasurementReportId)).FirstOrDefault();
            if (report != null)
            {
                report.MeasurementReportId = data.MeasurementReportId;
                report.UserId = data.UserId;
                report.StarDate = data.StarDate;
                report.FinishDate = data.FinishDate;
            }
            else
            {
                MeasurementReport newitem = new MeasurementReport();
                newitem.MeasurementReportId = data.MeasurementReportId;
                newitem.UserId = data.UserId;
                newitem.StarDate = data.StarDate;
                newitem.FinishDate = data.FinishDate;


                _nMContext.MeasurementsReport.Add(newitem);

            }
            _nMContext.SaveChanges();

            return new OkObjectResult("OK");
        }
    }
}
