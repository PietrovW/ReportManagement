using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReportManagement.Domain.Models;
using ReportManagement.Domain.Repositorys;
using ReportManagement.Infrastructure.Data;

namespace ReportManagement.Infrastructure.Repositorys
{
    public  class WriteBaseRepository<TEntity> : IWriteBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        public WriteBaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Guid Insert(TEntity entity)
        {
            EntityEntry<TEntity>  model = _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return model.Entity.Id;
        }
        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
