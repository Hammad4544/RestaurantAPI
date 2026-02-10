using AutoMapper;
using DataAcess.Repositories.UnitOfWork;
using Models.DTOS.Category;
using Models.Entities;
using RestaurantService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mappar;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork , IMapper mapper) {
            _mappar = mapper;

            _unitOfWork = unitOfWork;
        }
        public async Task<ResponsCategoryDTO> CreateCategoryAsync(AddCategoryDTO category)
        {
           var newCategory = new Category
           {
                Name = category.Name,
                BranchId = category.BranchId
           };
           await _unitOfWork.Categories.AddAsync(newCategory);
           await _unitOfWork.SaveAsync();
            var responsCategoryDTO = _mappar.Map<ResponsCategoryDTO>(newCategory);
            return responsCategoryDTO ;

        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var closed = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (closed == null) { return false; }
            closed.IsActive = false;
            return true;
        }

        public async Task<IEnumerable<ResponsCategoryDTO>> GetAllCategories()
        {
            var AllCategory = await _unitOfWork.Categories.GetAllAsync();
            if (AllCategory == null) { return null; }
            return _mappar.Map<IEnumerable<ResponsCategoryDTO>>(AllCategory);
        }

        public async Task<IEnumerable<ResponsCategoryDTO>> GetAllCategoriesByBranchIdAsync(int branchId)
        {
            var b = await _unitOfWork.Categories.GetAllCategoriesByBranchIdAsync(branchId);
            if(b == null) { return null; }
            return _mappar.Map<IEnumerable<ResponsCategoryDTO>>(b);
        }

        public async Task<ResponsCategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null) { return null; }
            return _mappar.Map<ResponsCategoryDTO>(category);
        }

        public async Task<ResponsCategoryDTO> UpdateCategoryAsync(int categoryId, UpdateCategoryDTO category)
        {
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (existingCategory == null) { return null; }
            existingCategory.Name = category.Name;
            existingCategory.IsActive = category.IsActive;
            existingCategory.BranchId = category.BranchId;
           await _unitOfWork.SaveAsync();
            return _mappar.Map<ResponsCategoryDTO>(existingCategory);
        }
    }
}
