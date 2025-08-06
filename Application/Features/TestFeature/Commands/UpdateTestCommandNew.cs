using Application.Features.TestFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TestFeature.Commands
{
    public record UpdateTestCommandNew(
       Guid testId,
       string name,
       string companyName, 
       string contact)
        : IRequest<ResponseHttp>

    {
        public class UpdateTestCommandNewHandler : IRequestHandler<UpdateTestCommandNew, ResponseHttp>
        {
            private readonly ITestRepository testRepository;
            private readonly IMapper _mapper;

            public UpdateTestCommandNewHandler(ITestRepository testRepository, IMapper mapper)
            {
                this.testRepository = testRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(UpdateTestCommandNew request, CancellationToken cancellationToken)
            {
                Test? test = await testRepository.GetById(request.testId);

                if (test == null)
                {
                    return new ResponseHttp
                    {
                        Resultat = this._mapper.Map<TestDTO>(test),
                        Fail_Messages = "Customer with this Id not found.",
                        Status = StatusCodes.Status400BadRequest,
                    };
                }
                else
                {
                    test.Id = request.testId;
                    test.Contact = request.contact;
                    test.CompanyName = request.companyName;
                    test.Name = request.name;
                    await testRepository.Update(test);
                    await testRepository.SaveChange(cancellationToken);

                    var customerToReturn = _mapper.Map<TestDTO>(test);
                    return new ResponseHttp
                    {
                        Resultat = customerToReturn,
                        Status = StatusCodes.Status200OK,
                    };

                }

            }
        }
    }
}
