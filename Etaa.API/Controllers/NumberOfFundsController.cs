namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberOfFundsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public NumberOfFundsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetNumberOfFunds()
        {
            return Ok(_unitOfWork.NumberOfFunds.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(NumberOfFundsDto fundsDto)
        {
            try
            {
                NumberOfFunds numberOfFunds = new()
                {
                    NameAr = fundsDto.NameAr,
                    NameEn = fundsDto.NameEn,
                    Description = fundsDto.Description,
                    MaxAmount = fundsDto.MaxAmount,
                    MinAmount = fundsDto.MinAmount,
                    Order = fundsDto.Order
                };
                var NumberOfFunds = _unitOfWork.NumberOfFunds.Add(numberOfFunds);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(NumberOfFunds);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(NumberOfFundsDto fundsDto)
        {
            try
            {
                NumberOfFunds numberOfFunds = new()
                {
                    NumberOfFundsId = fundsDto.NumberOfFundsId,
                    NameAr = fundsDto.NameAr,
                    NameEn = fundsDto.NameEn,
                    Description = fundsDto.Description,
                    MaxAmount = fundsDto.MaxAmount,
                    MinAmount = fundsDto.MinAmount,
                    Order = fundsDto.Order
                };
                var NumberOfFunds = _unitOfWork.NumberOfFunds.Update(numberOfFunds);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(NumberOfFunds);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int NumberOfFundsId)
        {
            try
            {
                var IsDeleted = _unitOfWork.NumberOfFunds.Delete(NumberOfFundsId);
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