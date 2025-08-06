using Application.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        public readonly DematContext _context;

        public GenericRepository(DematContext context)
            => _context = context;
        public async Task<List<T>> PostRange(List<T> entity)
        {
            foreach(var item in entity)
            await _context.Set<T>().AddAsync(item);
          //  await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Post(T entity)
        {
            _context.Set<T>().Add(entity);

            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            List<T> entities = await _context.Set<T>().ToListAsync();

            return entities;
        }

        public async Task<T?> GetById(Guid id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);

            return entity ?? null;
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task Delete(Guid id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return;
            }
            else
            {
                _context.Set<T>().Remove(entity);
            }
        }
        public async Task<bool> SoftDelete(Guid id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return false; // The entity doesn't exist; perhaps handle this in a specific way.
            }

            // Mark the entity as deleted (soft deletion).
            if (entity is ISoftDeleteable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true; // Make sure your entity has an IsDeleted property.
                softDeletableEntity.DeletedDate = DateTime.UtcNow;
            }
            else
            {
                // Handle the case where the entity does not support soft deletion.
                return false;
            }

            // Save the changes to the database context.
            _context.Entry(entity).State = EntityState.Modified;

            return true;
        }

        public async Task<bool> Exists(Guid id)
        {
            T? output = await _context.Set<T>().FindAsync(id);

            return output != null;
        }

        public async Task SaveChange(CancellationToken cancellationToken)
        {
           await  _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            T? entity = await _context.Set<T>().FirstOrDefaultAsync(e => !e.IsDeleted && e.Id == id, cancellationToken);

            return entity ?? null;
        }
    }
}
