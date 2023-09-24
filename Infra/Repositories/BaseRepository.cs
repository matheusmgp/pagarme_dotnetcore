
using Infra.Context;
using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

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
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().OrderByDescending(p => p.Id).ToListAsync();
        }
    }
}
