using MongoDB.Driver;

namespace ReportManagement.Data.Configurations
{
    public interface IApplicationMongoDbContext : IDisposable
    {
        IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : class;
        void Add(Func<Task> func);
        Task<int> SaveChanges();
    }
}
