using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;


namespace Persistance.Repositories
{
    public class PerfumeRepository : GenericRepository<Perfume>, IPerfumeRepository
    {
        public PerfumeRepository(DematContext context) : base(context)
        {
        }
        public async Task<PagedList<Perfume>> GetAllWithTypesAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken)
        {
            var query = await _context.Perfumes
                                        .AsQueryable()
                                        .AsNoTracking()
                                        .Where(e => e.IsDeleted == false)
                                        .ToListAsync(cancellationToken);
            int totalRows = query.AsEnumerable().Count();
            var customer = query
           .Skip(pageNumber.HasValue ? (pageNumber.Value - 1) * pageSize.GetValueOrDefault(1) : 0)
           .Take(pageSize.GetValueOrDefault(int.MaxValue)).ToList();

            return new PagedList<Perfume>(customer, totalRows, pageNumber, pageSize);
        }
    }
}
