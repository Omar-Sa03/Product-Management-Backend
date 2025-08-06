using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TestFeature.Commands
{
    public record DeleteTestCommandNew(
        Guid TestId
        )
        : IRequest<ResponseHttp>
    {

        public class DeleteTestCommandNewHandler : IRequestHandler<DeleteTestCommandNew, ResponseHttp>
        {
            private readonly ITestRepository testRepository;
            public DeleteTestCommandNewHandler(ITestRepository testRepository)
            {
                this.testRepository = testRepository;
            }

            public async Task<ResponseHttp> Handle(DeleteTestCommandNew request, CancellationToken cancellationToken)
            {
                var test = await testRepository.GetById(request.TestId);

                if (test == null)
                {
                    return new ResponseHttp
                    {
                        Fail_Messages = "No test found",
                        Status = StatusCodes.Status400BadRequest,
                    };
                }

                await testRepository.SoftDelete(request.TestId);
                await testRepository.SaveChange(cancellationToken);

                return new ResponseHttp
                {
                    Status = StatusCodes.Status200OK,
                };
            }
        }
    }
}
