using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;

namespace EtaaAPI.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // You should do the same for all models
        IBaseRepo<Contributor> Contributors { get; }
        IBaseRepo<State> States { get; }
        IBaseRepo<City> Cities { get; }
        IBaseRepo<District> Districts { get; }
        IBaseRepo<AccommodationType> AccommodationTypes { get; }
        IBaseRepo<EducationalStatus> EducationalStatuses { get; }
        IBaseRepo<Gender> Genders { get; }
        IBaseRepo<HealthStatus> HealthStatuses { get; }
        IBaseRepo<Installments> Installments { get; }
        IBaseRepo<InvestmentType> InvestmentTypes { get; }
        IBaseRepo<Job> Jobs { get; }
        IBaseRepo<Kinship> Kinships { get; }
        IBaseRepo<MartialStatus> MartialStatuses { get; }
        IBaseRepo<NumberOfFunds> NumberOfFunds { get; }
        IBaseRepo<ProjectDomainTypes> ProjectDomainTypes { get; }
        IBaseRepo<ProjectGroup> ProjectGroups { get; }
        IBaseRepo<ProjectsAssets> ProjectsAssets { get; }
        IBaseRepo<ProjectAssetesProjectTypeAssets> ProjectAssetesProjectTypeAssets { get; }
        IBaseRepo<ProjectSelectionReasons> ProjectSelectionReasons { get; }
        IBaseRepo<ProjectSocialBenefits> ProjectSocialBenefits { get; }
        IBaseRepo<ProjectsSelectionReasons> ProjectsSelectionReasons { get; }
        IBaseRepo<ProjectsSocialBenefits> ProjectsSocialBenefits { get; }
        IBaseRepo<ProjectTypes> ProjectTypes { get; }
        IBaseRepo<ProjectTypesAssets> ProjectTypesAssets { get; }
        IBaseRepo<Religion> Religions { get; }
        IBaseRepo<Family> Families { get; }
        IBaseRepo<FamilyMember> FamilyMembers { get; }
        IBaseRepo<FinancialStatement> FinancialStatements { get; }
        
        // The Project model was seperated from the other models because it should have it's own unique methods/actions
        // + the standard ones from the base repo
        IProjectsRepo Projects { get; }
        // This is going to be used like the db context and it's going to return the number of rows affected
        int Complete();
    }
}