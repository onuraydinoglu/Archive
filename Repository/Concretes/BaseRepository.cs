using ArchiveApp.Repository.Abstracts;
using ArchiveApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ArchiveApp.Repository.Concretes
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() // => await _dbSet.ToListAsync();
        // {
        //     var entity = await _dbSet.ToListAsync();
        //     return entity;
        // }
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
            {
                throw new Exception($"Id: {id} Bulunamdı.");
            }
            return entity;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            SaveAsync();
        }

        public async Task UpdateAsync(int id, TEntity entity)
        {
            var entityUpdate = await GetByIdAsync(id);
            _dbSet.Update(entityUpdate);
            SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entityDelete = await GetByIdAsync(id);
            _dbSet.Remove(entityDelete);
            SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
