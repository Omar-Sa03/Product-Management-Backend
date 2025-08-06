using Application.Features.PerfumeFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.PerfumeFeature.Queries
{
    public record GetAllPerfumeQuery(int? pageNumber, int? pageSize) : IRequest<ResponseHttp>
    {
        public class GetAllPerfumeQueryHandler : IRequestHandler<GetAllPerfumeQuery, ResponseHttp>
        {
            private readonly IPerfumeRepository perfumeRepository;
            private readonly IMapper _mapper;

            public GetAllPerfumeQueryHandler(IPerfumeRepository perfumeRepository, IMapper mapper)
            {
                this.perfumeRepository = perfumeRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(GetAllPerfumeQuery request, CancellationToken cancellationToken)
            {
                var perfume = await perfumeRepository.GetAllWithTypesAsync(request.pageNumber, request.pageSize, cancellationToken);

                if (perfume == null)
                    return new ResponseHttp
                    {
                        Fail_Messages = "No test found !",
                        Status = StatusCodes.Status400BadRequest,
                    };

                var perfumesToReturn = _mapper.Map<PagedList<PerfumeDTO>>(perfume);
                return new ResponseHttp
                {
                    Status = 200,
                    Resultat = perfumesToReturn
                };
            }
        }
    }
}
