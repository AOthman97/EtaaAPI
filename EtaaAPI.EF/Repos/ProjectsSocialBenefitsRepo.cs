using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    public class ProjectsSocialBenefitsRepo : BaseRepo<ProjectsSocialBenefits>, IProjectsSocialBenefitsRepo
    {
        protected new ApplicationDbContext _context;
        protected new IProjectsSocialBenefitsRepo _projectsSocialBenefitsRepo;

        public ProjectsSocialBenefitsRepo(ApplicationDbContext context) : base(context)
        {
        }

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public ProjectsSocialBenefitsRepo(ApplicationDbContext context, IProjectsSocialBenefitsRepo projectsSocialBenefitsRepo) : base(context)
        {
            _projectsSocialBenefitsRepo = projectsSocialBenefitsRepo;
        }

        public IEnumerable<ProjectsSocialBenefits> GetProjectsSocialBenefits(int ProjectId)
        {
            var SocialBenefits = _context.ProjectsSocialBenefits.Where(p => p.ProjectId == ProjectId).AsQueryable();
            return SocialBenefits;
        }
    }
}