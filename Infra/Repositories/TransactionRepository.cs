using Domain.Entities;
using Infra.Context;
using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class TransactionRepository : BaseRepository<TransactionEntity>, ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
       
    }
}
