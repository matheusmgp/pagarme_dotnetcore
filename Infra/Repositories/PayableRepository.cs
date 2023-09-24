using Application.Errors;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;

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
            try
            {
               
                return await _dbContext.Payable
               .Where(s => s.Availability == availability)
               .ToListAsync();
            }
            catch(PostgresException ex)
            {   
                throw new DatabaseException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }
    }
}
