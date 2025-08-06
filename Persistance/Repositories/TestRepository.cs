using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repositories
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(DematContext context) : base(context)
        {
        }

        public async Task<PagedList<Test>> GetAllWithTypesAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken)
        {
            var query = await _context.Tests
                                        .AsQueryable()
                                        .AsNoTracking()
                                        .Where(e => e.IsDeleted == false)
                                        .ToListAsync(cancellationToken);
            int totalRows = query.AsEnumerable().Count();
            var customer = query
           .Skip(pageNumber.HasValue ? (pageNumber.Value - 1) * pageSize.GetValueOrDefault(1) : 0)
           .Take(pageSize.GetValueOrDefault(int.MaxValue)).ToList();

            return new PagedList<Test>(customer, totalRows, pageNumber, pageSize);
        }
    }
}
