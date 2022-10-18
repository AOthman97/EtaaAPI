using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using Microsoft.EntityFrameworkCore;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    public class ProjectsAssetsRepo : BaseRepo<ProjectsAssets>, IProjectsAssetsRepo
    {
        protected new ApplicationDbContext _context;
        protected new IProjectsAssetsRepo _projectsAssetsRepo;

        public ProjectsAssetsRepo(ApplicationDbContext context) : base(context)
        {
        }

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public ProjectsAssetsRepo(ApplicationDbContext context, IProjectsAssetsRepo projectsAssetsRepo) : base(context)
        {
            _projectsAssetsRepo = projectsAssetsRepo;
        }

        public IEnumerable<ProjectsAssets> GetProjectTypeAssetsForEdit(int ProjectId)
        {
            var List = _context.ProjectsAssets.Where(ProjectsAssets => 
            ProjectsAssets.ProjectId == ProjectId).AsNoTracking().AsQueryable();
            return List;
        }
    }
}