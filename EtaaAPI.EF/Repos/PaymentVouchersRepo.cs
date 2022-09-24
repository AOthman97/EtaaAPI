using EtaaApi.Core.Models;
using EtaaAPI.Core.Repos;
using MoviesProject.EF;

namespace EtaaAPI.EF.Repos
{
    public class PaymentVouchersRepo : BaseRepo<PaymentVoucher>, IPaymentVouchersRepo
    {
        protected new ApplicationDbContext _context;

        // We don't have a "_context = context" here because it's already defined in the BaseRepo, the "base" below refers
        // to the BaseRepo above that we're inheriting from
        public PaymentVouchersRepo(ApplicationDbContext context) : base(context)
        {
        }

        public bool CreatePaymentVoucher(PaymentVoucher paymentVoucher)
        {
            var userId = "1";
            var filePath = string.Empty;
            paymentVoucher.PaymentDocumentPath = filePath;
            paymentVoucher.UserId = userId;

            int ProjectId = paymentVoucher.ProjectId;

            var IsFinancialStatementConfirmed = _context.FinancialStatements.Single(f => f.ProjectId == ProjectId);

            if (!IsFinancialStatementConfirmed.Equals(null))
            {
                var Project = _context.Projects.Single(p => p.ProjectId == ProjectId);
                int NumberOfInstallments = (int)Project.NumberOfInstallments;

                decimal MonthlyInstallmentAmount = (decimal)Project.MonthlyInstallmentAmount;

                decimal PaymentAmount = paymentVoucher.PaymentAmount;

                List<PaymentVoucher> paymentVouchers = new List<PaymentVoucher>();

                if (paymentVoucher.InstallmentsId <= NumberOfInstallments)
                {
                    for (int Increment = paymentVoucher.InstallmentsId; Increment <= NumberOfInstallments; Increment++)
                    {
                        if (PaymentAmount > 0)
                        {
                            decimal SumPaidAmountForInstallmentNo = GetSumPaidAmount(ProjectId, Increment);

                            if (SumPaidAmountForInstallmentNo.Equals(0) || SumPaidAmountForInstallmentNo.Equals(null) || SumPaidAmountForInstallmentNo == 0)
                            {
                                if (PaymentAmount <= MonthlyInstallmentAmount)
                                {
                                    paymentVoucher.PaymentAmount = PaymentAmount;
                                    paymentVouchers.Add(new PaymentVoucher { UserId = userId, ManagementUserId = null, PaymentDocumentPath = filePath, ProjectId = ProjectId, InstallmentsId = Increment, PaymentAmount = PaymentAmount, PaymentDate = paymentVoucher.PaymentDate });
                                    PaymentAmount = 0;
                                }
                                else if (PaymentAmount > MonthlyInstallmentAmount)
                                {
                                    paymentVoucher.PaymentAmount = MonthlyInstallmentAmount;
                                    paymentVouchers.Add(new PaymentVoucher { UserId = userId, ManagementUserId = null, PaymentDocumentPath = filePath, ProjectId = ProjectId, InstallmentsId = Increment, PaymentAmount = MonthlyInstallmentAmount, PaymentDate = paymentVoucher.PaymentDate });
                                    PaymentAmount -= MonthlyInstallmentAmount;
                                    paymentVoucher.InstallmentsId += 1;
                                }
                            }
                            else if (SumPaidAmountForInstallmentNo > 0 && SumPaidAmountForInstallmentNo < MonthlyInstallmentAmount)
                            {
                                if ((PaymentAmount + SumPaidAmountForInstallmentNo) <= MonthlyInstallmentAmount)
                                {
                                    paymentVoucher.PaymentAmount = PaymentAmount;
                                    paymentVouchers.Add(new PaymentVoucher { UserId = userId, ManagementUserId = null, PaymentDocumentPath = filePath, ProjectId = ProjectId, InstallmentsId = Increment, PaymentAmount = PaymentAmount, PaymentDate = paymentVoucher.PaymentDate });
                                    PaymentAmount = 0;
                                }
                                else if ((PaymentAmount + SumPaidAmountForInstallmentNo) > MonthlyInstallmentAmount)
                                {
                                    paymentVoucher.PaymentAmount = (MonthlyInstallmentAmount - SumPaidAmountForInstallmentNo);
                                    paymentVouchers.Add(new PaymentVoucher { UserId = userId, ManagementUserId = null, PaymentDocumentPath = filePath, ProjectId = ProjectId, InstallmentsId = Increment, PaymentAmount = (MonthlyInstallmentAmount - SumPaidAmountForInstallmentNo), PaymentDate = paymentVoucher.PaymentDate });
                                    PaymentAmount -= (MonthlyInstallmentAmount - SumPaidAmountForInstallmentNo);
                                    paymentVoucher.InstallmentsId += 1;
                                }
                            }
                            else if (SumPaidAmountForInstallmentNo >= MonthlyInstallmentAmount)
                            {
                                paymentVoucher.InstallmentsId += 1;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    _context.Set<PaymentVoucher>().AddRange(paymentVouchers);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int GetMaxInstallmentNo(int ProjectId)
        {
            var InstallmentsNo = (from paymentVoucher in _context.PaymentVouchers
                                  where paymentVoucher.ProjectId == ProjectId &&
                                  paymentVoucher.IsCanceled == false
                                  select (int?)paymentVoucher.InstallmentsId).Max();

            if (InstallmentsNo.Equals(null))
            {
                InstallmentsNo = 1;
            }
            else
            {
                decimal MonthlyInstallmentAmount = (from project in _context.Projects
                                                    where project.ProjectId == ProjectId
                                                    select (decimal)project.MonthlyInstallmentAmount).Single();

                decimal SumPaidAmountForInstallmentNo = (from paymentVoucher in _context.PaymentVouchers
                                                         where paymentVoucher.ProjectId == ProjectId &&
                                                         paymentVoucher.InstallmentsId == InstallmentsNo
                                                         select (decimal)paymentVoucher.PaymentAmount).Sum();

                if (MonthlyInstallmentAmount == SumPaidAmountForInstallmentNo)
                {
                    InstallmentsNo++;
                }
            }
            return (int)InstallmentsNo;
        }

        public decimal GetSumPaidAmount(int ProjectId, int? InstallmentId)
        {
            if(InstallmentId != null)
                return _context.PaymentVouchers.Where(p => p.ProjectId == ProjectId && p.InstallmentsId == InstallmentId && p.IsCanceled == false).Select(p => p.PaymentAmount).Sum();
            else
                return _context.PaymentVouchers.Where(p => p.ProjectId == ProjectId && p.IsCanceled == false).Select(p => p.PaymentAmount).Sum();
        }

        public bool UpdatePaymentVoucher(PaymentVoucher paymentVoucher)
        {
            // Firstly check if this payment voucher is canceled then it can't be modeified again
            var canceled = (from paymentVouchers in _context.PaymentVouchers
                            where paymentVouchers.PaymentVoucherId == paymentVoucher.PaymentVoucherId
                            select (bool)paymentVouchers.IsCanceled).Single();
            if (!canceled)
            {
                // Then check if the paid amount is greater than the monthly installment amount then it's not allowed
                decimal MonthlyInstallmentAmount = (from project in _context.Projects
                                                    where project.ProjectId == paymentVoucher.ProjectId
                                                    select (decimal)project.MonthlyInstallmentAmount).Single();
                if (paymentVoucher.PaymentAmount > MonthlyInstallmentAmount)
                {
                    return false;
                }
                else
                {
                    // If the session value exists then the user has added a file to update instead of the existing file
                    //if (NewFilePath != null)
                    //{
                    //    var OldFilePath = "";
                    //    OldFilePath = _context.PaymentVouchers.Where(f => f.PaymentVoucherId == paymentVoucher.PaymentVoucherId).Select(f => f.PaymentDocumentPath).Single();
                    //    if (OldFilePath != null && !string.IsNullOrEmpty(OldFilePath))
                    //    {
                    //        FileInfo file = new FileInfo(OldFilePath);
                    //        if (file.Exists)
                    //        {
                    //            file.Delete();
                    //        }
                    //    }
                    //    paymentVoucher.PaymentDocumentPath = NewFilePath;
                    //}

                    _context.Update(paymentVoucher);
                    _context.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}