namespace ReportManagement.Application.Common
{
    public interface ICreateReportCommand
    {
        public string Name { get; set; }
    }

    public interface ICreateReportStatus
    {
        Guid ReportId { get; }
    }
}
