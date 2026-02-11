using AutoMapper;
using DataAcess.Repositories.UnitOfWork;
using Models.DTOS.MenuItem;
using Models.Entities;
using RestaurantService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Implementation
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IUnitOfWork _unitOfWrk;
        private readonly IMapper _mapper;

        public MenuItemService(IUnitOfWork unitOfWork , IMapper mapper) {
        
            _unitOfWrk = unitOfWork;
            _mapper = mapper;

        }

        public async Task<MenuItemResponsDTO> CreateMenuItemAsync(AddMenuItemDTO dto)
        {
            var menuItem = _mapper.Map<MenuItem>(dto);

            await _unitOfWrk.MenuItems.AddAsync(menuItem);
            await _unitOfWrk.SaveAsync();

            // Upload Images
            if (dto.Images != null && dto.Images.Any())
            {
                var folderPath = Path.Combine("wwwroot", "images", "menuItems", menuItem.Id.ToString());

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                foreach (var image in dto.Images)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var fullPath = Path.Combine(folderPath, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    var menuItemImage = new MenuItemImage
                    {
                        MenuItemId = menuItem.Id,
                        ImageUrl = $"/images/menuItems/{menuItem.Id}/{fileName}"
                    };

                    await _unitOfWrk.MenuItemImages.AddAsync(menuItemImage);
                }

                await _unitOfWrk.SaveAsync();
            }

            return _mapper.Map<MenuItemResponsDTO>(menuItem);
        }


        public async Task<bool> DeleteMenuItemAsync(int menuItemId)
        {
            if (menuItemId < 0) { return false; }   
            await _unitOfWrk.MenuItems.removeMenuItemAsync(menuItemId);
            return await _unitOfWrk.SaveAsync();
            
        }

        public async Task<IEnumerable<MenuItemResponsDTO>> GetAllMenuItemsByBranchIdAsync(int branchId)
        {
            var menuItems = await _unitOfWrk.MenuItems.GetAllWithImages(branchId);
            if (menuItems == null || !menuItems.Any())
                return Enumerable.Empty<MenuItemResponsDTO>();
            return _mapper.Map<IEnumerable<MenuItemResponsDTO>>(menuItems);
        }

        public async Task<MenuItemResponsDTO> GetMenuItemByIdAsync(int menuItemId)
        {
            var menuItem = await _unitOfWrk.MenuItems.GetByIdAsync(menuItemId);
            if (menuItem==null)
            {
                return null;
            }
            return _mapper.Map<MenuItemResponsDTO>(menuItem);
        }

        public async Task<bool> UpdateMenuItem(int id, AddMenuItemDTO dto)
        {
            var existingItem = await _unitOfWrk.MenuItems.GetByIdWithImages(id);
            if (existingItem == null)
                return false;

            _mapper.Map(dto, existingItem);

            // ===== Delete Old Images =====
            if (existingItem.Images != null && existingItem.Images.Any())
            {
                foreach (var img in existingItem.Images.ToList())
                {
                    var imagePath = Path.Combine("wwwroot", img.ImageUrl.TrimStart('/'));

                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }

                    _unitOfWrk.MenuItemImages.Delete(img);
                }
            }

            // ===== Add New Images =====
            if (dto.Images != null && dto.Images.Any())
            {
                var folderPath = Path.Combine("wwwroot", "images", "menuItems", existingItem.Id.ToString());

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                foreach (var image in dto.Images)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var fullPath = Path.Combine(folderPath, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    var menuItemImage = new MenuItemImage
                    {
                        MenuItemId = existingItem.Id,
                        ImageUrl = $"/images/menuItems/{existingItem.Id}/{fileName}"
                    };

                    await _unitOfWrk.MenuItemImages.AddAsync(menuItemImage);
                }
            }

            await _unitOfWrk.SaveAsync();
            return true;
        }

    }
}
