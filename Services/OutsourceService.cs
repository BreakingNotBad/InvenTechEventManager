using Contract.Interfaces.IRepository;
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

        public async Task<IEnumerable<Outsources>> GetOutsources()
        {
            return await _repo.Outsource.GetOutsourceAsyn();
        }

        public async Task<Outsources?> GetOutsourcesByIdAsync(int id)
        {
            return await _repo.Outsource.GetOutsourceByIdAsync(id);
        }
        public async Task CreateOutsourceAsync(Outsources outsource)
        {
            _repo.Outsource.Createoutsource(outsource);
            await _repo.SaveAsync();
        }
        public async Task<IEnumerable<Outsources>> GetOutsourceActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        )
        {
            return await _repo.Outsource.GetOutsourceActiveAsync(
                search,
                date,
                time_period,
                filter_available
            );
        }
        public async Task UpdateOutsourceAsync(int id, Outsources outsource)
        {
            var exinstingOutsource = await _repo.Outsource.GetOutsourceByIdAsync(id);

            if (exinstingOutsource == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            _repo.Outsource.Updateoutsource(exinstingOutsource);
            await _repo.SaveAsync();
        }
        public async Task DeleteOutsource(int id)
        {
            var exinstingOutsource = await _repo.Outsource.GetOutsourceByIdAsync(id);
            if (exinstingOutsource == null)
            {
                throw new ArgumentException($"Outsource with id {id} not found.");
            }
            _repo.Outsource.Deleteoutsource(exinstingOutsource);
            await _repo.SaveAsync();
        }

    }
}
