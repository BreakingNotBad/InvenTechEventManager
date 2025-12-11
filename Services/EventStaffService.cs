using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class EventStaffService : IEventStaffService
    {
        private readonly IRepositoryManager _repo;

        public EventStaffService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EventStaff>> GetEventStaffMembersAsync()
        {
            return await _repo.EventStaff.GetEventStaffMembersAsync();
        }

        public async Task<EventStaff?> GetEventStaffByIdAsync(int id)
        {
            var query = _repo.EventStaff.FindByCondition(
                e => e.EventStaffId == id,
                trackChanges: false
            );

            return await query.FirstOrDefaultAsync();
        }

    }
}
