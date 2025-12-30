using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;
using Service.Contract;

namespace Service
{
    public class OutsourceService : IOutsourceService
    {
        private readonly IRepositoryManager _repo;

        public OutsourceService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Outsource>> GetOutsources()
        {
            return await _repo.Outsource.GetOutsourceAsyn();
        }

        public async Task<Outsource?> GetOutsourcesByIdAsync(int id)
        {
            return await _repo.Outsource.GetOutsourceByIdAsync(id);
        }

        public async Task CreateOutsourceAsync(Outsource outsource)
        {
            _repo.Outsource.CreateOutsource(outsource);
            await _repo.SaveAsync();
        }

        public async Task UpdateOutsourceAsync(int id, Outsource outsource)
        {
            var exinstingOutsource = await _repo.Outsource.GetOutsourceByIdAsync(id);

            if (exinstingOutsource == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            exinstingOutsource.FullName = outsource.FullName;

            _repo.Outsource.UpdateOutsource(exinstingOutsource);
            await _repo.SaveAsync();
        }

        public async Task DeleteOutsourceAsync(int id)
        {
            var exinstingOutsource = await _repo.Outsource.GetOutsourceByIdAsync(id);
            if (exinstingOutsource == null)
            {
                throw new ArgumentException($"Outsource with id {id} not found.");
            }
            _repo.Outsource.DeleteOutsource(exinstingOutsource);
            await _repo.SaveAsync();
        }
    }
}
