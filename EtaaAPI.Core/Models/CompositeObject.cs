using EtaaApi.Core.Models;
using MoviesProject.Dtos;

namespace EtaaAPI.Core.Models
{
    public class CompositeObject
    {
        public ProjectDto projectDto { get; set; }
        public List<ProjectsAssetsDto> projectsAssetsDto { get; set; }
        public List<ProjectsSelectionReasonsDto> projectsSelectionReasonsDto { get; set; }
        public List<ProjectsSocialBenefitsDto> projectsSocialBenefitsDto { get; set; }
    }
}