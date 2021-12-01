using ReportManagement.Domain.Models;
using System.Linq.Expressions;

namespace ReportManagement.Domain.Repositorys
{
    public interface IReadBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> ListAsync();
        Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
