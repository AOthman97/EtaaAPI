using EtaaApi.Core.Models;
using MoviesProject.Dtos;

namespace EtaaAPI.Core.Repos
{
    // By inheriting from the IBaseRepo we're getting all of it's methods and here
    // we're defining some that are specific to the Projects model
    public interface IProjectsRepo : IBaseRepo<Projects>
    {
        int GetNumberOfInstallments(int ProjectId);
        bool CreateProject(ProjectDto projectDto, List<ProjectsAssetsDto> projectsAssetsDto, 
                List<ProjectsSelectionReasonsDto> projectsSelectionReasonsDto, 
            List<ProjectsSocialBenefitsDto> projectsSocialBenefitsDto);
    }
}