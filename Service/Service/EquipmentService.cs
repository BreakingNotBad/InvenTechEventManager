using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using FluentValidation;
using Service.Contracts.DTOs.Equipment;
using Service.Contracts.IService;

namespace Service.Service
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateEquipmentDto> _createValidator;
        private readonly IValidator<UpdateEquipmentDto> _updateValidator;

        public EquipmentService(
            IRepositoryManager repo,
            IMapper mapper,
            IValidator<CreateEquipmentDto> createValidator,
            IValidator<UpdateEquipmentDto> updateValidator)
        {
            _repo = repo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<EquipmentDto>> GetEquipmentAsync(
            string? equipmentName,
            string? category,
            bool? IsDeleted,
            DateTime CreatedAt,
            DateTime UpdatedAt
        )
        {
            var equipmentList = await _repo.Equipment.GetEquipmentAsync();

            //  search
            if (!string.IsNullOrWhiteSpace(equipmentName))
            {
                equipmentList = equipmentList.Where(e =>
                    e.EquipmentName.ToLower().Contains(equipmentName.ToLower())
                );
            }

            //  filter category
            if (!string.IsNullOrWhiteSpace(category))
            {
                equipmentList = equipmentList.Where(e =>
                e.Category.CategoryName.ToLower().Contains(category.ToLower())
                );
            }
            
            var companyResponse = _mapper.Map<IEnumerable<EquipmentDto>>(equipmentList);

            return companyResponse;
        }

        public async Task<EquipmentDto?> GetEquipmentByIdAsync(int id)
        {
            var existingEquipment = await _repo.Equipment.GetEquipmentByIdAsync(id,false);
            if (existingEquipment == null)
            {
                throw new NotFoundException(nameof(Equipment), id);
            }
            var equipmentResponse = _mapper.Map<EquipmentDto>(existingEquipment);
            return equipmentResponse;
        }

        public async Task<EquipmentDto> CreateEquipmentAsync(CreateEquipmentDto equipmentDto)
        {
            await _createValidator.ValidateAndThrowAsync(equipmentDto);

            var NewEquipment = _mapper.Map<Equipment>(equipmentDto);

            _repo.Equipment.CreateEquipment(NewEquipment);
            await _repo.SaveAsync();

            var equipmentResponse = _mapper.Map<EquipmentDto>(NewEquipment);
            return equipmentResponse;
        }


        public async Task<EquipmentDto> UpdateEquipmentAsync(int id, UpdateEquipmentDto equipmentDto)
        {
            await _updateValidator.ValidateAndThrowAsync(equipmentDto);

            var existingEquipment = await _repo.Equipment.GetEquipmentByIdAsync(id,true);
            if (existingEquipment == null)
            {
                throw new NotFoundException(nameof(Equipment), id);
            }
            // Update fields
            _mapper.Map(equipmentDto, existingEquipment);
            existingEquipment.UpdatedAt = DateTime.UtcNow;

            _repo.Equipment.UpdateEquipment(existingEquipment);
            await _repo.SaveAsync();

            var equipmentResponse = _mapper.Map<EquipmentDto>(existingEquipment);
            return equipmentResponse;
        }

        public async Task DeleteEquipment(int id)
        {
            var existingEquipment = await _repo.Equipment.GetEquipmentByIdAsync(id,true);
            if (existingEquipment == null)
            {
                throw new KeyNotFoundException($"Equipment with id {id} not found.");
            }
            _repo.Equipment.DeleteEquipment(existingEquipment);
            await _repo.SaveAsync();
        }
    }
}
