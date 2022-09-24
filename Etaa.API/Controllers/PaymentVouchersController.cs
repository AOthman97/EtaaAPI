namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentVouchersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentVouchersRepo _paymentVouchersRepo;

        public PaymentVouchersController(IPaymentVouchersRepo paymentVouchersRepo, IUnitOfWork unitOfWork)
        {
            _paymentVouchersRepo = paymentVouchersRepo;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_paymentVouchersRepo.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int PaymentVoucherId)
        {
            return Ok(await _paymentVouchersRepo.GetById(C => C.PaymentVoucherId.Equals(PaymentVoucherId)));
        }

        [HttpPost("GetAllInstallments")]
        public IActionResult GetAllInstallments(int ProjectId)
        {
            try
            {
                List<JsonResult> data = new List<JsonResult>();
                JsonResult result;

                //int ProjectNumberOfInstallments = _unitOfWork.Projects.GetNumberOfInstallments(ProjectId);
                var Project = _unitOfWork.Projects.GetById(ProjectId);
                List<string> Installments = new List<string>();

                for (int Increment = 1; Increment <= Project.Result.NumberOfInstallments; Increment++)
                {
                    var Installment = _unitOfWork.Installments.GetById(Increment);

                    Installments.Add(Installment.Result.NameAr);

                    DateTime DueDate = DueDate = (DateTime)Project.Result.FirstInstallmentDueDate;

                    if (Increment > 1)
                    {
                        DueDate = (DateTime)Project.Result.FirstInstallmentDueDate;
                        DueDate = DueDate.AddMonths(Increment - 1);
                    }

                    Installments.Add(DueDate.ToShortDateString());

                    var MonthlyInstallmentAmount = Project.Result.MonthlyInstallmentAmount;

                    Installments.Add(MonthlyInstallmentAmount.ToString());

                    decimal SumPaidAmountForInstallmentNo = _paymentVouchersRepo.GetSumPaidAmount(ProjectId, Increment);

                    Installments.Add(SumPaidAmountForInstallmentNo.ToString());

                    var RemainAmount = MonthlyInstallmentAmount - SumPaidAmountForInstallmentNo;

                    Installments.Add(RemainAmount.ToString());

                    // Coloring for the table
                    string ColorClass = "";
                    // Firstly if the Installment due date is not due yet
                    int CompareDates = DateTime.Compare(DateTime.Now.Date, DueDate.Date);
                    if (CompareDates < 0)
                    {
                        ColorClass = "text-secondary";
                    }
                    else if (CompareDates == 0 || CompareDates > 1)
                    {
                        ColorClass = "text-danger";
                    }

                    if (RemainAmount > 0 && RemainAmount < MonthlyInstallmentAmount)
                    {
                        ColorClass = "text-warning";
                    }
                    else if (RemainAmount == 0)
                    {
                        ColorClass = "text-success";
                    }

                    result = new JsonResult(new 
                    {
                        InstallmentName = Installment.Result.NameAr,
                        dueDate = DueDate.ToShortDateString(),
                        monthlyInstallmentAmount = MonthlyInstallmentAmount.ToString(),
                        sumPaidAmountForInstallmentNo = SumPaidAmountForInstallmentNo.ToString(),
                        remainAmount = RemainAmount.ToString(),
                        colorClass = ColorClass
                    });
                    data.Add(result);
                }

                var returnObj = new
                {
                    data = data
                };

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult GetMaxInstallmentNo(int projectId)
        {
            try
            {
                return Ok(_paymentVouchersRepo.GetMaxInstallmentNo(projectId));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(PaymentVoucher paymentVoucher)
        {
            try
            {
                bool IsAdded = _paymentVouchersRepo.CreatePaymentVoucher(paymentVoucher);
                if (IsAdded)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(PaymentVoucher paymentVoucher)
        {
            try
            {
                bool IsUpdated = _paymentVouchersRepo.UpdatePaymentVoucher(paymentVoucher);
                if (!IsUpdated)
                    return BadRequest();
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int PaymentVoucherId)
        {
            try
            {
                var PaymentVoucher = _paymentVouchersRepo.GetById(PaymentVoucherId);
                if (PaymentVoucher.Result != null)
                {
                    var IsDeleted = _unitOfWork.PaymentVouchers.Delete(PaymentVoucherId);
                    if (IsDeleted)
                    {
                        int NumberAffected = _unitOfWork.Complete();
                        if (NumberAffected > 0)
                            return Ok(true);
                        else
                            return BadRequest();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}