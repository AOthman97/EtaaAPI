namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialStatementsController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IFinancialStatementsRepo _financialStatementsRepo;

        public FinancialStatementsController(IUnitOfWork unitOfWork, IFinancialStatementsRepo financialStatementsRepo)
        {
            //_unitOfWork = unitOfWork;
            _financialStatementsRepo = financialStatementsRepo;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_financialStatementsRepo.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int FinancialStatementId)
        {
            return Ok(await _financialStatementsRepo.GetById(C => C.FinancialStatementId.Equals(FinancialStatementId)));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(FinancialStatementDto financialStatementDto)
        {
            try
            {
                var IsPresent = _financialStatementsRepo.CheckAlreadyPresent(financialStatementDto.ProjectId);

                if(IsPresent != null)
                {
                    return BadRequest("Finanical Statement for this Project is already Present!");
                }
                else
                {
                    FinancialStatement financialStatement = new()
                    {
                        DocumentPath = financialStatementDto.DocumentPath,
                        IsApprovedByManagement = true,
                        IsCanceled = false,
                        ProjectId = financialStatementDto.ProjectId
                    };
                    var FinancialStatement = _financialStatementsRepo.Add(financialStatement);
                    if (FinancialStatement != null)
                        return Ok(FinancialStatement);
                    else
                        return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(FinancialStatementDto financialStatementDto)
        {
            try
            {
                FinancialStatement financialStatement = new()
                {
                    FinancialStatementId = financialStatementDto.FinancialStatementId,
                    DocumentPath = financialStatementDto.DocumentPath,
                    IsApprovedByManagement = true,
                    IsCanceled = false,
                    ProjectId = financialStatementDto.ProjectId
                };
                var FinancialStatement = _financialStatementsRepo.Update(financialStatement);
                if (FinancialStatement != null)
                    return Ok(FinancialStatement);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int FinancialStatementId)
        {
            try
            {
                var FinancialStatement = _financialStatementsRepo.GetById(FinancialStatementId);
                if(FinancialStatement.Result != null)
                {
                    var IsPresent = _financialStatementsRepo.CheckAlreadyPresentInPaymentVoucher(FinancialStatement.Result.ProjectId);

                    if (IsPresent != null)
                    {
                        return BadRequest("There are current payment/s for this Project!");
                    }
                    else
                    {
                        var IsDeleted = _financialStatementsRepo.Delete(FinancialStatementId);
                        if (IsDeleted)
                        {
                            return Ok(true);
                        }
                        else
                        {
                            return BadRequest("Not Deleted!");
                        }
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