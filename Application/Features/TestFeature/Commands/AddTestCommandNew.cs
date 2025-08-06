using Application.Features.TestFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TestFeature.Commands
{
    public record AddTestCommandNew(
        string name,
        string companyName, 
        string contact)
        : IRequest<ResponseHttp>
    {
        public class AddTestCommandNewHandler : IRequestHandler<AddTestCommandNew, ResponseHttp>
        {
            private readonly ITestRepository testRepository;
            private readonly IMapper _mapper;

            public AddTestCommandNewHandler(ITestRepository testRepository, IMapper mapper)
            {
                this.testRepository = testRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(AddTestCommandNew request, CancellationToken cancellationToken)
            {
                try
                {
                    var test = _mapper.Map<Test>(request);
                
                    test = await testRepository.Post(test);
                    await testRepository.SaveChange(cancellationToken);

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
