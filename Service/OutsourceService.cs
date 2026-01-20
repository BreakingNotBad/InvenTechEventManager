using Contracts.DTOs;
using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    public class OutsourceService : IOutsourceService
    {
        private readonly IRepositoryManager _repo;

        public OutsourceService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Outsource>> GetOutsources(string? fullName)
        {
            var outsourcesList = await _repo.Outsource.GetOutsourceAsyn();
            //  search
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                outsourcesList = outsourcesList.Where(o =>
                    o.FullName.ToLower().Contains(fullName.ToLower())
                );
            }
            return outsourcesList;
        }

        public async Task<Outsource?> GetOutsourcesByIdAsync(int id)
        {
            return await _repo.Outsource.GetOutsourceByIdAsync(id,false);
        }

        public async Task<Outsource> CreateOutsourceAsync(CreateOutsourceDto dto)
        {
            var outsource = new Outsource
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
            };

            _repo.Outsource.CreateOutsource(outsource);
            await _repo.SaveAsync();
            return outsource;
        }

        public async Task UpdateOutsourceAsync(int id, UpdateOutsourceDto dto)
        {
            var exinstingOutsource = await _repo.Outsource.GetOutsourceByIdAsync(id,true); 

            if (exinstingOutsource == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            exinstingOutsource.FullName = dto.FullName;
            exinstingOutsource.Email = dto.Email;
            exinstingOutsource.PhoneNumber = dto.PhoneNumber;
            exinstingOutsource.UpdatedAt = DateTime.UtcNow;

            if (dto.IsDeleted.HasValue)
            {
                exinstingOutsource.IsDeleted = dto.IsDeleted.Value;
            }

            _repo.Outsource.UpdateOutsource(exinstingOutsource);
            await _repo.SaveAsync();
        }

        public async Task DeleteOutsourceAsync(int id)
        {
            var exinstingOutsource = await _repo.Outsource.GetOutsourceByIdAsync(id,true);
            if (exinstingOutsource == null)
            {
                throw new ArgumentException($"Outsource with id {id} not found.");
            }
            _repo.Outsource.DeleteOutsource(exinstingOutsource);
            await _repo.SaveAsync();
        }
    }
}
