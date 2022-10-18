using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    public class ProjectsSelectionReasonsRepo : BaseRepo<ProjectsSelectionReasons>, IProjectsSelectionReasonsRepo
    {
        protected new ApplicationDbContext _context;
        protected new IProjectsSelectionReasonsRepo _projectsSelectionReasonsRepo;

        public ProjectsSelectionReasonsRepo(ApplicationDbContext context) : base(context)
        {
        }

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public ProjectsSelectionReasonsRepo(ApplicationDbContext context, IProjectsSelectionReasonsRepo projectsSelectionReasons) : base(context)
        {
            _projectsSelectionReasonsRepo = projectsSelectionReasons;
        }
        public IEnumerable<ProjectsSelectionReasons> GetProjectsSelectionReasons(int ProjectId)
        {
            var Reasons = _context.ProjectsSelectionReasons.Where(p => p.ProjectId == ProjectId).AsQueryable();
            return Reasons;
        }
    }
}