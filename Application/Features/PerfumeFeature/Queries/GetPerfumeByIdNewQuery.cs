using Application.Features.PerfumeFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.PerfumeFeature.Queries
{
    public record GetPerfumeByIdNewQuery(
        Guid PerfumeId
        ) : IRequest<ResponseHttp>
    {
        public class GetPerfumeByIdNewQueryHandler : IRequestHandler<GetPerfumeByIdNewQuery, ResponseHttp>
        {
            private readonly IPerfumeRepository perfumeRepository;
            private readonly IMapper _mapper;

            public GetPerfumeByIdNewQueryHandler(IPerfumeRepository perfumeRepository, IMapper mapper)
            {
                this.perfumeRepository = perfumeRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(GetPerfumeByIdNewQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var perfume = await perfumeRepository.GetByIdAsync(request.PerfumeId, cancellationToken);

                    if (perfume == null)
                        return new ResponseHttp()
                        {
                            Status = 404,
                            Fail_Messages = "perfume not found !"
                        };

                    return new ResponseHttp()
                    {

                        Resultat = _mapper.Map<PerfumeDTO>(perfume),
                        Status = 200
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseHttp
                    {
                        Fail_Messages = ex.Message,
                        Status = StatusCodes.Status400BadRequest,
                    };

                }

            }
        }
    }
}
