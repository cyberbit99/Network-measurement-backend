using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Network_measurement_PDFGenerator;
using System.Linq;
using System.Net.Http;
using Network_measurement_database.Repository;
using Network_measurement_functions.Abstracts;
using QuestPDF.Fluent;
using Network_measurement_database.Model.Requests;


namespace Network_measurement_functions.Functions
{
    public class GetReportPDFFun:AFun
    {
        public GetReportPDFFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("GetReportPDFFun")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "getreportpdf")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MeasurementReportRequest data = JsonConvert.DeserializeObject<MeasurementReportRequest>(requestBody);

            //consts but not consts
            string folderPath = @"C:\Users\Admin\source\repos\Network measurement-backend\Network measurement-PDFGenerator\Reports";
            HttpResponseMessage response = new HttpResponseMessage();
            PDFGenerator Generator = new PDFGenerator();

            var list = Generator.ReadAllPDF();

            
            var pdf = list.Where(x => x == data.MeasurementReportId.ToString());

            if(pdf.Count() == 0)
            {
                MeasurementReportRequest reportRequest = new MeasurementReportRequest();
                reportRequest.MeasurementReportId = data.MeasurementReportId;
                reportRequest.UserId = data.UserId;
                reportRequest.StarDate = data.StarDate;
                reportRequest.FinishDate = data.FinishDate;
                reportRequest.Name = data.Name;
                reportRequest.Description = data.Description;

                reportRequest.Data = _nMContext.Measurements.Where(x=> x.MeasurementReportId == reportRequest.MeasurementReportId).ToList();

                string pdfDocument = Generator.Generator(reportRequest);

                byte[] pdfBytes = File.ReadAllBytes(pdfDocument);

                //adding bytes to memory stream
                var dataStream = new MemoryStream(pdfBytes);
                response.Content = new ByteArrayContent(pdfBytes);
                //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                return new FileContentResult(pdfBytes,"application/pdf");
            }
            else
            {
                byte[] pdfBytes = File.ReadAllBytes(folderPath+pdf.First());

                //adding bytes to memory stream
                var dataStream = new MemoryStream(pdfBytes);
                response.Content = new ByteArrayContent(pdfBytes);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                return new OkObjectResult(response);
            }
        }
    }
}
