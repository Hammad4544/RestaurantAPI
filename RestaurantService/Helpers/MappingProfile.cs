using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Models.DTOS.Barnch;
using Models.DTOS.Category;
using Models.DTOS.MenuItem;
using Models.Entities;

namespace RestaurantService.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //BRNCHES
            CreateMap<AddBranchDTO, Branch>();
            CreateMap<Branch, ResponsAllBranchesDTO>();
            CreateMap<ResponsAllBranchesDTO, Branch>();
            CreateMap<UpdateBranchDTO, Branch>();
            CreateMap<Branch, UpdateBranchDTO>();

            //CATEGORIES
             CreateMap<AddCategoryDTO, Category>();
             CreateMap<Category, ResponsCategoryDTO>();
             CreateMap<ResponsCategoryDTO, Category>();
             CreateMap<UpdateCategoryDTO, Category>();
             CreateMap<Category, UpdateCategoryDTO>();

            // MENUITEMS

            CreateMap<AddMenuItemDTO, MenuItem>()
    .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<MenuItem, MenuItemResponsDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.Images.Select(i => i.ImageUrl)));

            CreateMap<MenuItemResponsDTO, MenuItem>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());


        }
    }
}
