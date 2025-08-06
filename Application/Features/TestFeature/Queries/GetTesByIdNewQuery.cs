using Application.Features.TestFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TestFeature.Queries
{
    public record GetTesByIdNewQuery(
        Guid TestId
        ) : IRequest<ResponseHttp>
    {
        public class GetTesByIdNewQueryHandler : IRequestHandler<GetTesByIdNewQuery, ResponseHttp>
        {
            private readonly ITestRepository testRepository;
            private readonly IMapper _mapper;

            public GetTesByIdNewQueryHandler(ITestRepository testRepository, IMapper mapper)
            {
                this.testRepository = testRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(GetTesByIdNewQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var test = await testRepository.GetByIdAsync(request.TestId, cancellationToken);

                    if (test == null)
                        return new ResponseHttp()
                        {
                            Status = 404,
                            Fail_Messages = "Test not found !"
                        };

                    return new ResponseHttp()
                    {

                        Resultat = _mapper.Map<TestDTO>(test),
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
