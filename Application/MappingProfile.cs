using Application.Features.Request.Commands.Create;
using Application.Features.Request.Commands.Update;
using Application.Features.Request.Queries.GetAll;
using Application.Features.Request.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Request, CreateRequestDto>().ReverseMap();
            CreateMap<Request, UpdateRequestDto>().ReverseMap();

            CreateMap<Request, RequestListDto>().ReverseMap();
            CreateMap<Request, RequestByIdVm>().ReverseMap();
        }
    }
}
