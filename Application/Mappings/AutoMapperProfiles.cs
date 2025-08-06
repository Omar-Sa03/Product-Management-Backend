using Application.Features.TestFeature.Commands;
using Application.Features.TestFeature.Dtos;
using Application.Features.PerfumeFeature.Commands;
using Application.Features.PerfumeFeature.Dtos;
using AutoMapper;
using Domain.Common;
using Domain.Entities;

namespace Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Commands
            CreateMap<AddTestCommandNew, Test>();
            CreateMap<PagedList<Test>, PagedList<TestDTO>>().ReverseMap();

            //Dto
            CreateMap<Test, TestDTO>().ReverseMap();

            //Partie Perfume 

            // Commands
            CreateMap<AddPerfumeCommand, Perfume>();
            CreateMap<PagedList<Perfume>, PagedList<PerfumeDTO>>().ReverseMap();

            //Dto
            CreateMap<Perfume, PerfumeDTO>().ReverseMap();
        }
    }
}
