using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PerfumeFeature.Queries
{
    public class GetListPerfumeQuery : IRequest<ResponseHttp>
    {
        public class GetListPerfumeQueryHandler : IRequestHandler<GetListPerfumeQuery, ResponseHttp>
        {
            private readonly IDematContext _dematContext;

            public GetListPerfumeQueryHandler(IDematContext dematContext)
            {
                _dematContext = dematContext;
            }
            public async Task<ResponseHttp> Handle(GetListPerfumeQuery request, CancellationToken cancellationToken)
            {
                var perfumes = await _dematContext.Perfumes.Where(x => x.IsDeleted == false).ToListAsync(cancellationToken);
                return new ResponseHttp
                {
                    Status = 200,
                    Fail_Messages = "None",
                    Resultat = new
                    {
                        Perfumes = perfumes
                    }
                };
            }
        }
    }
}
