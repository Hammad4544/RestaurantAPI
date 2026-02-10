using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Models.DTOS.Barnch;
using Models.DTOS.Category;
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
        }
    }
}
