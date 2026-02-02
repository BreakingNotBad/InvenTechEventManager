using AutoMapper;
using Contracts.IRepository.BaseManager;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.Contracts.DTOs.Company;
using Service.Contracts.DTOs.Equipment;
using Service.Contracts.DTOs.Outsource;
using Service.Contracts.DTOs.Package;
using Service.Contracts.DTOs.Staff;
using Service.Contracts.IService;
using Service.Contracts.Manager;
using Service.Service;

namespace Service.Manager
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEventService> _eventService;
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IStaffService> _staffService;
        private readonly Lazy<IOutsourceService> _outsourceService;
        private readonly Lazy<IEquipmentService> _equipmentService;
        private readonly Lazy<IPackageService> _packageService;
        private readonly Lazy<IRoleService> _roleService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IFileService> _fileService;

        public ServiceManager(
            IRepositoryManager repo,
            IFileService fileService,
            IMapper mapper,
            IServiceProvider serviceProvider
        )
        {
            _eventService = new Lazy<IEventService>(() => new EventService(repo,mapper,fileService));
            _companyService = new Lazy<ICompanyService>(() =>
            {
                var createValidator = serviceProvider.GetRequiredService<
                    IValidator<CreateCompanyDto>
                >();
                var updateValidator = serviceProvider.GetRequiredService<
                    IValidator<UpdateCompanyDto>
                >();
                return new CompanyService(repo, mapper, createValidator, updateValidator);
            });
            _staffService = new Lazy<IStaffService>(() =>
            {
                var createValidator = serviceProvider.GetRequiredService<
                    IValidator<CreateStaffDto>
                >();
                var updateValidator = serviceProvider.GetRequiredService<
                    IValidator<UpdateStaffDto>
                >();
                return new StaffService(
                    repo,
                    fileService,
                    mapper,
                    createValidator,
                    updateValidator
                );
            });
            _outsourceService = new Lazy<IOutsourceService>(() =>
            {
                var createValidator = serviceProvider.GetRequiredService<
                    IValidator<CreateOutsourceDto>
                >();
                var updateValidator = serviceProvider.GetRequiredService<
                    IValidator<UpdateOutsourceDto>
                >();
                return new OutsourceService(
                    repo,
                    mapper,
                    createValidator,
                    updateValidator
                );
            });
            _equipmentService = new Lazy<IEquipmentService>(() =>
            {
                var createValidator = serviceProvider.GetRequiredService<
                    IValidator<CreateEquipmentDto>
                >();
                var updateValidator = serviceProvider.GetRequiredService<
                    IValidator<UpdateEquipmentDto>
                >();
                return new EquipmentService(
                    repo,
                    mapper,
                    createValidator,
                    updateValidator
                );
            });
            _packageService = new Lazy<IPackageService>(() =>
            {
                var createValidator = serviceProvider.GetRequiredService<
                    IValidator<CreatePackageDto>
                >();
                var updateValidator = serviceProvider.GetRequiredService<
                    IValidator<UpdatePackageDto>
                >();
                return new PackageService(
                    repo,
                    mapper,
                    createValidator,
                    updateValidator
                );
            });
            _roleService = new Lazy<IRoleService>(() => new RoleService(repo,mapper));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repo, mapper));
            _fileService = new Lazy<IFileService>(() => fileService);
        }

        public IEventService Event => _eventService.Value;
        public ICompanyService Company => _companyService.Value;
        public IStaffService Staff => _staffService.Value;
        public IOutsourceService Outsource => _outsourceService.Value;
        public IEquipmentService Equipment => _equipmentService.Value;
        public IPackageService Package => _packageService.Value;
        public IRoleService Role => _roleService.Value;
        public ICategoryService Category => _categoryService.Value;
        public IFileService File => _fileService.Value;
    }
}
