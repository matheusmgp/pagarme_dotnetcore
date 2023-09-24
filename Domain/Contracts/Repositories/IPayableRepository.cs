

using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IPayableRepository : IBaseRepository<PayableEntity>
    {
        Task<ICollection<PayableEntity>> GetAllPayable(string availability);
    }
}
