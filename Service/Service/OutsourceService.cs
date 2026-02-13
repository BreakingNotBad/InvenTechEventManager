using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using FluentValidation;
using Service.Contracts.DTOs.Outsource;
using Service.Contracts.IService;
using Shared.RequestFeatures.Parameters;

namespace Service.Service
{
    public class OutsourceService : IOutsourceService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOutsourceDto> _createValidator;
        private readonly IValidator<UpdateOutsourceDto> _updateValidator;

        public OutsourceService(
            IRepositoryManager repo,
            IMapper mapper,
            IValidator<CreateOutsourceDto> createValidator,
            IValidator<UpdateOutsourceDto> updateValidator)
        {
            _repo = repo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<OutsourceDto>> GetOutsources(OutsourceParameter outsourceParameter)
        {
            var outsourcesList =
                await _repo.Outsource.GetOutsourceAsyn(outsourceParameter, false);

            var outsourceResponse = _mapper.Map<IEnumerable<OutsourceDto>>(outsourcesList);

            // ถ้ามีการส่ง Date/Period มา → ต้องคำนวณ Status
            if (outsourceParameter.Date.HasValue && outsourceParameter.Period.HasValue)
            {
                var checkPeriod = outsourceParameter.Period.Value;

                foreach (var outsource in outsourceResponse)
                {
                    // หา Entity
                    var outsourceEntity = outsourcesList.First(o => o.OutsourceId == outsource.OutsourceId);

                    // ดึง Event ที่ outsource คนนี้ทำงานอยู่
                    var workingEvents = outsourceEntity.EventOutsources?.Select(eo => eo.Event).ToList() ?? [];

                    bool isUnavailable = false;
                    bool hasJobToday = workingEvents.Count > 0;

                    if (hasJobToday)
                    {
                        isUnavailable =
                            workingEvents.Any(e => e.Period == checkPeriod);
                    }

                    if (isUnavailable)
                    {
                        outsource.Status = "Unavailable";
                    }
                    else if (hasJobToday)
                    {
                        outsource.Status = "WorkingToday";
                    }
                    else
                    {
                        outsource.Status = "Available";
                    }
                }
            }

            return outsourceResponse;
        }


        public async Task<OutsourceDto?> GetOutsourcesByIdAsync(int id)
        {
            var ExistingOutsource = await _repo.Outsource.GetOutsourceByIdAsync(id,false);
            if (ExistingOutsource == null)
            {
                throw new NotFoundException(nameof(Outsource), id);
            }
            var outsourceResponse = _mapper.Map<OutsourceDto>(ExistingOutsource);
            return outsourceResponse;
        }

        public async Task<OutsourceDto> CreateOutsourceAsync(CreateOutsourceDto OutsourceDto)
        {
            await _createValidator.ValidateAndThrowAsync(OutsourceDto);

            var NewOutsource = _mapper.Map<Outsource>(OutsourceDto);

            _repo.Outsource.CreateOutsource(NewOutsource);
            await _repo.SaveAsync();

            var outsourceResponse = _mapper.Map<OutsourceDto>(NewOutsource);
            return outsourceResponse;
        }

        public async Task<OutsourceDto> UpdateOutsourceAsync(int id, UpdateOutsourceDto OutsourceDto)
        {
            await _updateValidator.ValidateAndThrowAsync(OutsourceDto);
            var exinstingOutsource = await _repo.Outsource.GetOutsourceByIdAsync(id,true); 

            if (exinstingOutsource == null)
            {
                throw new NotFoundException(nameof(Outsource), id);
            }

            _mapper.Map(OutsourceDto, exinstingOutsource);
            exinstingOutsource.UpdatedAt = DateTime.UtcNow;

            if (OutsourceDto.IsDeleted.HasValue)
            {
                exinstingOutsource.IsDeleted = OutsourceDto.IsDeleted.Value;
            }

            _repo.Outsource.UpdateOutsource(exinstingOutsource);
            await _repo.SaveAsync();

            var outsourceResponse = _mapper.Map<OutsourceDto>(exinstingOutsource);
            return outsourceResponse;
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
