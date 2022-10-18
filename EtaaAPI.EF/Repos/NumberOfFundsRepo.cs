using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using Microsoft.EntityFrameworkCore;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    public class NumberOfFundsRepo : BaseRepo<NumberOfFunds>, INumberOfFundsRepo
    {
        protected new ApplicationDbContext _context;
        protected new INumberOfFundsRepo _numberOfFundsRepo;

        public NumberOfFundsRepo(ApplicationDbContext context) : base(context)
        {
        }

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public NumberOfFundsRepo(ApplicationDbContext context, INumberOfFundsRepo numberOfFundsRepo) : base(context)
        {
            _numberOfFundsRepo = numberOfFundsRepo;
        }

        // In the API version instead of returning all project types and a selected one maybe try to
        // get all of them seperately and then get the selected one either with the project data itself or make
        // a call to this endpoint
        public int GetNumberOfFundsId(int ProjectId)
        {
            var ProjectTypeId = _context.Projects.Where(p => p.ProjectId == ProjectId)
                .AsNoTracking().Select(p => p.NumberOfFundsId).Single();
            return ProjectTypeId;
        }
    }
}