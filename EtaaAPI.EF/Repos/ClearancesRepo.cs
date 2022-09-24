using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    public class ClearancesRepo : BaseRepo<Clearance>, IClearancesRepo
    {
        protected new ApplicationDbContext _context;
        protected new IPaymentVouchersRepo _paymentVouchersRepo;

        public ClearancesRepo(ApplicationDbContext context) : base(context)
        {
        }

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public ClearancesRepo(ApplicationDbContext context, IPaymentVouchersRepo paymentVouchersRepo) : base(context)
        {
            _paymentVouchersRepo = paymentVouchersRepo;
        }

        public bool CreateClearance(Clearance clearance)
        {
            var userId = "1";
            var filePath = string.Empty;
            clearance.ClearanceDocumentPath = filePath;
            clearance.UserId = userId;

            // Firstly before saving the clearance we need to check if this project is fully paid
            int ProjectId = clearance.ProjectId;
            decimal Capital = (decimal)_context.Projects.Where(p => p.ProjectId == ProjectId).Select(p => p.Capital).First();

            decimal SumPaidAmount = _paymentVouchersRepo.GetSumPaidAmount(ProjectId, null);

            var Project = _context.Clearances.Where(c => c.ProjectId == clearance.ProjectId).Select(c => c.ProjectId);
            if (Project.Any() == false)
            {
                if (SumPaidAmount >= Capital)
                {
                    _context.Add(clearance);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public decimal GetRemainAmount(int ProjectId)
        {
            decimal SumPaidAmount = _paymentVouchersRepo.GetSumPaidAmount(ProjectId, null);
            decimal Capital = (decimal)_context.Projects.Where(p => p.ProjectId == ProjectId).Select(p => p.Capital).First();
            return (Capital - SumPaidAmount);
        }
    }
}