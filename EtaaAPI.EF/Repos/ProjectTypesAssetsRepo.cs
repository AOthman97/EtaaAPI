using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using Microsoft.EntityFrameworkCore;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    public class ProjectTypesAssetsRepo : BaseRepo<ProjectTypesAssets>, IProjectTypesAssetsRepo
    {
        protected new ApplicationDbContext _context;
        protected new IProjectTypesAssetsRepo _projectTypesAssetsRepo;

        public ProjectTypesAssetsRepo(ApplicationDbContext context) : base(context)
        {
        }

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public ProjectTypesAssetsRepo(ApplicationDbContext context, IProjectTypesAssetsRepo projectTypesAssetsRepo) : base(context)
        {
            _projectTypesAssetsRepo = projectTypesAssetsRepo;
        }

        public IEnumerable<ProjectTypesAssets> GetProjectTypeAssets(int ProjectTypeId)
        {
            var List = _context.ProjectTypesAssets.Where(ProjectTypeAsset => 
            ProjectTypeAsset.ProjectTypeId == ProjectTypeId).AsNoTracking().AsQueryable();
            return List;
        }
    }
}