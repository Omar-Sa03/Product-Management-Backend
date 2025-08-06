using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITestRepository : IGenericRepository<Test>
    {
        Task<PagedList<Test>> GetAllWithTypesAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken);
    }
}
