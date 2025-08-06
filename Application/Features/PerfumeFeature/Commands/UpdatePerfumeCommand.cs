using Application.Features.PerfumeFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Application.Features.PerfumeFeature.Commands
{
    public record UpdatePerfumeCommand(
    Guid perfumeId,
    string Name,
    string Brand,
    string Description,
    decimal Price,
    float Size,
    int Stock,
    string Image
    ) : IRequest<ResponseHttp>
    {
        public class UpdatePerfumeCommandHandler : IRequestHandler<UpdatePerfumeCommand, ResponseHttp>
        {
            private readonly IPerfumeRepository perfumeRepository;
            private readonly IMapper _mapper;

            public UpdatePerfumeCommandHandler(IPerfumeRepository perfumeRepository, IMapper mapper)
            {
                this.perfumeRepository = perfumeRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(UpdatePerfumeCommand request, CancellationToken cancellationToken)
            {
                Perfume? perfume = await perfumeRepository.GetById(request.perfumeId);

                if (perfume == null)
                {
                    return new ResponseHttp
                    {
                        Resultat = this._mapper.Map<PerfumeDTO>(perfume),
                        Fail_Messages = "Perfume with this Id not found.",
                        Status = StatusCodes.Status400BadRequest,
                    };
                }
                else
                {
                    perfume.Id = request.perfumeId;
                    perfume.Name = request.Name;
                    perfume.Brand = request.Brand;
                    perfume.Description = request.Description;
                    perfume.Price = request.Price;
                    perfume.Size = request.Size;
                    perfume.Stock = request.Stock;
                    perfume.Image = request.Image;
                    await perfumeRepository.Update(perfume);
                    await perfumeRepository.SaveChange(cancellationToken);

                    var perfumeToReturn = _mapper.Map<PerfumeDTO>(perfume);
                    return new ResponseHttp
                    {
                        Resultat = perfumeToReturn,
                        Status = StatusCodes.Status200OK,
                    };

                }

            }
        }
    }
}
