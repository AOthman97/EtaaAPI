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
        // The Project model was seperated from the other models because it should have it's own unique methods/actions
        // + the standard ones from the base repo
        IProjectsRepo Projects { get; }
        // This is going to be used like the db context and it's going to return the number of rows affected
        int Complete();
    }
}