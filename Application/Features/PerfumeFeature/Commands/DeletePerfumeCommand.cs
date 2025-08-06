using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.PerfumeFeature.Commands
{  
    public record DeletePerfumeCommand(
        Guid PerfumeId
        )
        : IRequest<ResponseHttp>
        {

        public class DeletePerfumeCommandHandler : IRequestHandler<DeletePerfumeCommand, ResponseHttp>
        {
            private readonly IPerfumeRepository perfumeRepository;
            public DeletePerfumeCommandHandler(IPerfumeRepository perfumeRepository)
            {
                this.perfumeRepository = perfumeRepository;
            }

            public async Task<ResponseHttp> Handle(DeletePerfumeCommand request, CancellationToken cancellationToken)
            {
                var perfume = await perfumeRepository.GetById(request.PerfumeId);

                if (perfume == null)
                {
                    return new ResponseHttp
                    {
                        Fail_Messages = "No perfume found",
                        Status = StatusCodes.Status400BadRequest,
                    };
                }

                await perfumeRepository.SoftDelete(request.PerfumeId);
                await perfumeRepository.SaveChange(cancellationToken);

                return new ResponseHttp
                {
                    Status = StatusCodes.Status200OK,
                };
            }
        }
    }
}
