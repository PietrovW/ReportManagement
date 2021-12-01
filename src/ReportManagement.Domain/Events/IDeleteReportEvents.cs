namespace ReportManagement.Application.Events
{
    public interface IDeleteReportEvents
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } 
    }
}
