using Application.Commands.ViewModels;
using Application.Queries.ViewModels;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.MappingProfiles
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile() 
        {
            CreateMap<Automobile, AutomobileGetDTO>()
                .ForMember(dest => dest.CategoriesName, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));

            CreateMap<AutomobileAddEditDTO, Automobile>();

            CreateMap<Category, CategoryGetDTO>();
        }
    }
}
