namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthStatusesController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        private readonly IUnitOfWork _unitOfWork;

        public HealthStatusesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetHealthStatuses()
        {
            return Ok(_unitOfWork.HealthStatuses.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(HealthStatusDto statusDto)
        {
            try
            {
                HealthStatus status = new()
                {
                    NameAr = statusDto.NameAr,
                    NameEn = statusDto.NameEn
                };
                var HealthStatus = _unitOfWork.HealthStatuses.Add(status);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(HealthStatus);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(HealthStatusDto statusDto)
        {
            try
            {
                HealthStatus status = new()
                {
                    HealthStatusId = statusDto.HealthStatusId,
                    NameAr = statusDto.NameAr,
                    NameEn = statusDto.NameEn
                };
                var HealthStatus = _unitOfWork.HealthStatuses.Update(status);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(HealthStatus);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int HealthStatusId)
        {
            try
            {
                var IsDeleted = _unitOfWork.HealthStatuses.Delete(HealthStatusId);
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