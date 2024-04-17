namespace Network_measurement_database.Model.Requests
{
    public class MeasurementReportRequest
    {
        public int MeasurementReportId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public List<Measurement>? Data { get; set; }
    }
}
