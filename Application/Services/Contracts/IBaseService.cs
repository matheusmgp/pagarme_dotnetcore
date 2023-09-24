


namespace Application.Services.Contracts
{
    public interface IBaseService<TEntity>
    {
        Task<ResultService<TEntity>> CreateAsync(TEntity entity);
        Task<ResultService<ICollection<TEntity>>> GetAll();
    }
}
