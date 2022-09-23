using Microsoft.AspNetCore.Mvc.Rendering;

namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstallmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetInstallments()
        {
            return Ok(_unitOfWork.Installments.GetAll().ToList());
        }

        [HttpGet("GetInstallmentForPaymentVoucher")]
        public IActionResult GetInstallmentForPaymentVoucher(int PaymentVoucherId)
        {
            var PaymentVoucher = _unitOfWork.PaymentVouchers.GetById(p => p.PaymentVoucherId == PaymentVoucherId);
            return Ok(_unitOfWork.Installments.GetById(i => i.InstallmentsId == PaymentVoucher.Result.InstallmentsId));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(InstallmentDto installmentDto)
        {
            try
            {
                Installments installments = new()
                {
                    InstallmentNumber = installmentDto.InstallmentNumber,
                    NameAr = installmentDto.NameAr,
                    NameEn = installmentDto.NameEn
                };
                var Installments = _unitOfWork.Installments.Add(installments);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Installments);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(InstallmentDto installmentDto)
        {
            try
            {
                Installments installments = new()
                {
                    InstallmentsId = installmentDto.InstallmentsId,
                    InstallmentNumber = installmentDto.InstallmentNumber,
                    NameAr = installmentDto.NameAr,
                    NameEn = installmentDto.NameEn
                };
                var Installments = _unitOfWork.Installments.Update(installments);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Installments);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("DeleteSingle")]
        //public IActionResult DeleteSingle(int EducationalStatusId)
        //{
        //    try
        //    {
        //        var IsDeleted = _unitOfWork.EducationalStatuses.Delete(EducationalStatusId);
        //        if (IsDeleted)
        //        {
        //            int NumberAffected = _unitOfWork.Complete();
        //            if (NumberAffected > 0)
        //                return Ok(true);
        //            else
        //                return BadRequest();
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}