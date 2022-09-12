using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    public class FinancialStatementsRepo : BaseRepo<FinancialStatement>, IFinancialStatementsRepo
    {
        protected new ApplicationDbContext _context;

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public FinancialStatementsRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public FinancialStatement CheckAlreadyPresent(int ProjectId) 
            => _context.FinancialStatements.Where(f => f.ProjectId == ProjectId && f.IsCanceled == false).SingleOrDefault();

        public PaymentVoucher CheckAlreadyPresentInPaymentVoucher(int ProjectId)
            => _context.PaymentVouchers.Where(f => f.ProjectId == ProjectId && f.IsCanceled == false).FirstOrDefault();
    }
}