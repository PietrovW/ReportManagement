using ReportManagement.Domain.Models;

namespace ReportManagement.Domain.Repositorys
{
    public interface IWriteBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Guid Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
