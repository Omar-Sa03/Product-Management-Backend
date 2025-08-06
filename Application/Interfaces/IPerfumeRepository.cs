using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPerfumeRepository : IGenericRepository<Perfume>
    {
        Task<PagedList<Perfume>> GetAllWithTypesAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken);
    }
}
