using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.DTOs.Role;
using Service.Contracts.IService;
using Shared.RequestFeatures.Parameters;

namespace Service.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;

        public RoleService(IRepositoryManager repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetRoleByAsync(RoleParameter roleParameter)
        {
            // ดึงข้อมูล Entity
            var roles = await _repo.Role.GetAllRoleAsync(roleParameter, false);

            // 4. แปลงจาก Entity เป็น DTO
            var roleDto = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return roleDto;
        }
    }
}
