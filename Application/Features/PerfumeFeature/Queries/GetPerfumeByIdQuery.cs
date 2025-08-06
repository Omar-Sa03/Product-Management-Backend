using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PerfumeFeature.Queries
{
    public class GetPerfumeByIdQuery : IRequest<ResponseHttp>
    {
        public Guid Id { get; set; }

        public GetPerfumeByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetNavigatorByIdQueryHandler : IRequestHandler<GetPerfumeByIdQuery, ResponseHttp>
        {
            private readonly IDematContext _trackingContext;

            public GetNavigatorByIdQueryHandler(IDematContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetPerfumeByIdQuery request, CancellationToken cancellationToken)
            {
                var naviagtor = await _trackingContext.Perfumes
                    .Where(x => x.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);
                if (naviagtor == null)
                    return new ResponseHttp()
                    {
                        Resultat = "Not Found",
                        Status = 404,
                        Fail_Messages = "NoT Exist a Perfume with this Id"
                    };
                return new ResponseHttp()
                {
                    Resultat = naviagtor,
                    Status = 200,
                    Fail_Messages = "None"
                };
            }
        }
    }
}
