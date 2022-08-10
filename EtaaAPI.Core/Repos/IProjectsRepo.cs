using EtaaApi.Core.Models;

namespace EtaaAPI.Core.Repos
{
    // By inheriting from the IBaseRepo we're getting all of it's methods and here
    // we're defining some that are specific to the Projects model
    public interface IProjectsRepo : IBaseRepo<Projects>
    {
        IEnumerable<Projects> ToDefineLater();
    }
}