namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentTypesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvestmentTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetInvestmentTypes()
        {
            return Ok(_unitOfWork.InvestmentTypes.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(InvestmentTypeDto typeDto)
        {
            try
            {
                InvestmentType investmentType = new()
                {
                    NameAr = typeDto.NameAr,
                    NameEn = typeDto.NameEn
                };
                var InvestmentTypes = _unitOfWork.InvestmentTypes.Add(investmentType);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(InvestmentTypes);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(InvestmentTypeDto typeDto)
        {
            try
            {
                InvestmentType type = new()
                {
                    InvestmentTypeId = typeDto.InvestmentTypeId,
                    NameAr = typeDto.NameAr,
                    NameEn = typeDto.NameEn
                };
                var InvestmentType = _unitOfWork.InvestmentTypes.Update(type);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(InvestmentType);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int InvestmentTypeId)
        {
            try
            {
                var IsDeleted = _unitOfWork.InvestmentTypes.Delete(InvestmentTypeId);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}