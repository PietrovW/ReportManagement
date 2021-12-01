using System.Linq.Expressions;
using MongoDB.Driver;
using ReportManagement.Data.Configurations;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;

namespace ReportManagement.Infrastructure.Repositorys
{
    public class ReadBaseRepository<TEntity> : IReadBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private IApplicationMongoDbContext _dbContext;
        protected IMongoCollection<TEntity> DbSet;
        public ReadBaseRepository(IApplicationMongoDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.GetCollection<TEntity>();
        }
        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            IEnumerable<TEntity> date = await ListAsync(x => x.Id == id);
            if(date.Any())
            {
                return date.First();
            }
            return null;
        }
        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }
        public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var data = await DbSet.FindAsync(predicate);
            return data.ToList();
        }
    }
}
