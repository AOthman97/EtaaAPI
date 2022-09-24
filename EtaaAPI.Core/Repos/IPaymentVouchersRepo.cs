using EtaaApi.Core.Models;

namespace EtaaAPI.Core.Repos
{
    // By inheriting from the IBaseRepo we're getting all of it's methods and here
    // we're defining some that are specific to the Projects model
    public interface IPaymentVouchersRepo : IBaseRepo<PaymentVoucher>
    {
        decimal GetSumPaidAmount(int ProjectId, int? InstallmentId);
        int GetMaxInstallmentNo(int ProjectId);
        bool CreatePaymentVoucher(PaymentVoucher paymentVoucher);
        bool UpdatePaymentVoucher(PaymentVoucher paymentVoucher);
    }
}