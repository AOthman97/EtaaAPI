using EtaaApi.Core.Models;
using EtaaAPI.Core.Interfaces;
using EtaaAPI.Core.Repos;
using EtaaAPI.EF.Repos;
using MoviesProject.EF;

namespace EtaaAPI.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // You should do this for all models, The 'private set is because we only want to assign it values privately here'
        public IBaseRepo<Contributor> Contributors { get; private set; }
        public IBaseRepo<State> States { get; private set; }
        public IBaseRepo<City> Cities { get; private set; }
        public IBaseRepo<District> Districts { get; private set; }
        public IBaseRepo<AccommodationType> AccommodationTypes { get; private set; }
        public IBaseRepo<EducationalStatus> EducationalStatuses { get; private set; }
        public IBaseRepo<Gender> Genders { get; private set; }
        public IBaseRepo<HealthStatus> HealthStatuses { get; private set; }
        public IBaseRepo<Installments> Installments { get; private set; }
        public IBaseRepo<InvestmentType> InvestmentTypes { get; private set; }
        public IBaseRepo<Job> Jobs { get; private set; }
        public IBaseRepo<Kinship> Kinships { get; private set; }
        public IBaseRepo<MartialStatus> MartialStatuses { get; private set; }
        public IBaseRepo<NumberOfFunds> NumberOfFunds { get; private set; }
        public IBaseRepo<ProjectDomainTypes> ProjectDomainTypes { get; private set; }
        public IBaseRepo<ProjectGroup> ProjectGroups { get; private set; }
        public IBaseRepo<ProjectsAssets> ProjectsAssets { get; private set; }
        public IBaseRepo<ProjectAssetesProjectTypeAssets> ProjectAssetesProjectTypeAssets { get; private set; }
        public IBaseRepo<ProjectSelectionReasons> ProjectSelectionReasons { get; private set; }
        public IBaseRepo<ProjectSocialBenefits> ProjectSocialBenefits { get; private set; }
        public IBaseRepo<ProjectsSelectionReasons> ProjectsSelectionReasons { get; private set; }
        public IBaseRepo<ProjectsSocialBenefits> ProjectsSocialBenefits { get; private set; }
        public IBaseRepo<ProjectTypes> ProjectTypes { get; private set; }
        public IBaseRepo<ProjectTypesAssets> ProjectTypesAssets { get; private set; }
        public IBaseRepo<Religion> Religions { get; private set; }
        public IBaseRepo<Family> Families { get; private set; }
        public IBaseRepo<FamilyMember> FamilyMembers { get; private set; }
        public IBaseRepo<FinancialStatement> FinancialStatements { get; private set; }

        public IProjectsRepo Projects { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // You should do this for all models
            Contributors = new BaseRepo<Contributor>(_context);
            States = new BaseRepo<State>(_context);
            Cities = new BaseRepo<City>(_context);
            Districts = new BaseRepo<District>(_context);
            AccommodationTypes = new BaseRepo<AccommodationType>(_context);
            EducationalStatuses = new BaseRepo<EducationalStatus>(_context);
            Genders = new BaseRepo<Gender>(_context);
            HealthStatuses = new BaseRepo<HealthStatus>(_context);
            Installments = new BaseRepo<Installments>(_context);
            InvestmentTypes = new BaseRepo<InvestmentType>(_context);
            Jobs = new BaseRepo<Job>(_context);
            Kinships = new BaseRepo<Kinship>(_context);
            MartialStatuses = new BaseRepo<MartialStatus>(_context);
            NumberOfFunds = new BaseRepo<NumberOfFunds>(_context);
            ProjectDomainTypes = new BaseRepo<ProjectDomainTypes>(_context);
            ProjectGroups = new BaseRepo<ProjectGroup>(_context);
            ProjectsAssets = new BaseRepo<ProjectsAssets>(_context);
            ProjectAssetesProjectTypeAssets = new BaseRepo<ProjectAssetesProjectTypeAssets>(_context);
            ProjectSelectionReasons = new BaseRepo<ProjectSelectionReasons>(_context);
            ProjectSocialBenefits = new BaseRepo<ProjectSocialBenefits>(_context);
            ProjectsSelectionReasons = new BaseRepo<ProjectsSelectionReasons>(_context);
            ProjectsSocialBenefits = new BaseRepo<ProjectsSocialBenefits>(_context);
            ProjectTypes = new BaseRepo<ProjectTypes>(_context);
            ProjectTypesAssets = new BaseRepo<ProjectTypesAssets>(_context);
            Religions = new BaseRepo<Religion>(_context);
            Families = new BaseRepo<Family>(_context);
            FamilyMembers = new BaseRepo<FamilyMember>(_context);
            FinancialStatements = new BaseRepo<FinancialStatement>(_context);

            Projects = new ProjectsRepo(_context);
        }

        // All this what do is to return the number of rows affected
        public int Complete()
        {
            return _context.SaveChanges();
        }

        // Dispose the connection
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}