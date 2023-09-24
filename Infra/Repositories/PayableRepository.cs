using Domain.Entities;
using Infra.Context;
using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class PayableRepository : BaseRepository<PayableEntity>, IPayableRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PayableRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<PayableEntity>> GetAllPayable(string availability)
        {
            return await _dbContext.Payable
                            .Where(s => s.Availability == availability)                            
                            .ToListAsync();
        }
    }
}