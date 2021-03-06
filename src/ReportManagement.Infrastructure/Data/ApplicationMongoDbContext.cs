using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReportManagement.Data.Configurations;
using ReportManagement.Infrastructure.Data.Settings;

namespace ReportManagement.Infrastructure.Data
{
    public class ApplicationMongoDbContext : IApplicationMongoDbContext
    {
        private IMongoDatabase _database { get; set; } = null!;
        public IClientSessionHandle Session { get; set; } = null!;
        public MongoClient MongoClient { get; set; } = null!;
        private MongoOptions mongoSettings { get; set; } = null!;


        private readonly List<Func<Task>> _commands = new List<Func<Task>>();
        protected ApplicationMongoDbContext()
            :base()
        {

        }
        public ApplicationMongoDbContext(IOptions<MongoOptions> settings)
        {
            mongoSettings = settings.Value;
            _commands = new List<Func<Task>>();
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : class
        {
            ConfigureMongo();
            return _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public void Add(Func<Task> func)
        {
            _commands.Add(func);
        }
        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);
            return _commands.Count;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(mongoSettings.ConnectionString);
            _database = MongoClient.GetDatabase(mongoSettings.DatabaseName);
        }
    }
}
