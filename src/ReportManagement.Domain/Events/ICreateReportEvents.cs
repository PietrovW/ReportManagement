namespace ReportManagement.Application.Events
{
    public interface ICreateReportEvents
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
         
        public DateTime CreatedDate { get; set; }// = DateTime.Now;
    }
}
