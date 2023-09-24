

namespace Domain.Contracts.Repositories
{
    public interface IBaseRepository <TEntity> where TEntity : class   
    {
        Task<TEntity> Create(TEntity entity);
        Task<ICollection<TEntity>> GetAll();
    }
}
