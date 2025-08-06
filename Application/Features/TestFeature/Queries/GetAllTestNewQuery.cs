using Application.Features.TestFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TestFeature.Queries
{
    public record GetAllTestNewQuery(int? pageNumber, int? pageSize) : IRequest<ResponseHttp>
    {
        public class GetAllTestNewQueryHandler : IRequestHandler<GetAllTestNewQuery, ResponseHttp>
        {
            private readonly ITestRepository testRepository;
            private readonly IMapper _mapper;

            public GetAllTestNewQueryHandler(ITestRepository testRepository, IMapper mapper)
            {
                this.testRepository = testRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(GetAllTestNewQuery request, CancellationToken cancellationToken)
            {
                var test = await testRepository.GetAllWithTypesAsync(request.pageNumber, request.pageSize, cancellationToken);

                if (test == null)
                    return new ResponseHttp
                    {
                        Fail_Messages = "No test found !",
                        Status = StatusCodes.Status400BadRequest,
                    };

                var customersToReturn = _mapper.Map<PagedList<TestDTO>>(test);
                return new ResponseHttp
                {
                    Status = 200,
                    Resultat = customersToReturn
                };
            }
        }
    }
}
