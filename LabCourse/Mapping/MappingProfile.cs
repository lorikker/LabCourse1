using AutoMapper;
using LabCourse.Entities;
using LabCourse.Models;

namespace LabCourse.Mapping
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Stoku, StokuDto>();
            CreateMap<StokuDtoForUpdate, Stoku>();
        }
    }
}