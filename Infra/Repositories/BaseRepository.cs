
using Application.Errors;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infra.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (PostgresException ex)
            {
                throw new DatabaseException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            try
            {
                return await _dbContext.Set<TEntity>().
                    OrderByDescending(p => p.Id).
                    ToListAsync();
            }
            catch (PostgresException ex)
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
