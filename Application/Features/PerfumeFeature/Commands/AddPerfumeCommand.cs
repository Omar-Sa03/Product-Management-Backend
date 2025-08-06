using Application.Features.PerfumeFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Application.Features.PerfumeFeature.Commands
{
    public record AddPerfumeCommand(
    string Name,
    string Brand,
    string Description,
    decimal Price,
    float Size,
    int Stock,
    string Image
    ) : IRequest<ResponseHttp>
    {
        public class AddPerfumeCommandHandler : IRequestHandler<AddPerfumeCommand, ResponseHttp>
        {
            private readonly IPerfumeRepository perfumeRepository;
            private readonly IMapper _mapper;

            public AddPerfumeCommandHandler(IPerfumeRepository perfumeRepository, IMapper mapper)
            {
                this.perfumeRepository = perfumeRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(AddPerfumeCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var perfume = _mapper.Map<Perfume>(request);

                    perfume = await this.perfumeRepository.Post(perfume);
                    await this.perfumeRepository.SaveChange(cancellationToken);

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
