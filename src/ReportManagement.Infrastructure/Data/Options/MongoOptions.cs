namespace ReportManagement.Infrastructure.Data.Settings
{
    public class MongoOptions
    {
        public const string Position = "Mongo";
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
