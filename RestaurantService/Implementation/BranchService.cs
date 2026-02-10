using AutoMapper;
using DataAcess.Repositories.UnitOfWork;
using Models.DTOS.Barnch;
using Models.Entities;
using RestaurantService.Helpers;
using RestaurantService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Implementation
{
    public class BranchService : IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _iunitOfWork;

        public BranchService(IUnitOfWork unitOfWork ,IMapper mapper) {
        
            _mapper=mapper;
            _iunitOfWork = unitOfWork;
        }
        public async Task<AddBranchDTO> CreateBranchAsync(AddBranchDTO branch)
        {
            var b = new Branch
            {
                Name = branch.Name,
                Address = branch.Address,
                Phone = branch.Phone
            };
           await _iunitOfWork.Branches.AddAsync(b);
           var t =  await _iunitOfWork.SaveAsync();
            if (t) { return branch; }
            return null;

        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            await _iunitOfWork.Branches.ClosedBranchAsync(id);
            await _iunitOfWork.SaveAsync();
            return true;

        }

        public async Task<IEnumerable<ResponsAllBranchesDTO>> GetAllBranchesAsync()
        {
            var branches = await _iunitOfWork.Branches.GetAllAsync();
            
            
            return _mapper.Map<IEnumerable<ResponsAllBranchesDTO>>(branches) ;


        }

        public async Task<ResponsAllBranchesDTO> GetBranchByIdAsync(int id)
        {
            if (id <= 0) { return null; }
            
            var b = await _iunitOfWork.Branches.GetByIdAsync(id);
            if (b == null) { return null; }
            return _mapper.Map<ResponsAllBranchesDTO>(b);
        }

        public async Task<UpdateBranchDTO> UpdateBranchAsync(int id,UpdateBranchDTO branch)
        {
            if (branch == null) { return null; }
            var existingBranch = await _iunitOfWork.Branches.GetByIdAsync(id);
            if (existingBranch == null) { return null; }
            existingBranch.Name = branch.Name;
            existingBranch.Address = branch.Address;
            existingBranch.Phone = branch.Phone;
           await _iunitOfWork.Branches.SaveAsync();
            return branch;
        }
    }
}
