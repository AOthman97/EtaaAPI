namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalStatusesController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        private readonly IUnitOfWork _unitOfWork;

        public EducationalStatusesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetEducationalStatuses()
        {
            return Ok(_unitOfWork.EducationalStatuses.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(EducationalStatusDto statusDto)
        {
            try
            {
                EducationalStatus status = new()
                {
                    NameAr = statusDto.NameAr,
                    NameEn = statusDto.NameEn
                };
                var EducationalStatus = _unitOfWork.EducationalStatuses.Add(status);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(EducationalStatus);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(EducationalStatusDto statusDto)
        {
            try
            {
                EducationalStatus status = new()
                {
                    EducationalStatusId = statusDto.EducationalStatusId,
                    NameAr = statusDto.NameAr,
                    NameEn = statusDto.NameEn
                };
                var EducationalStatus = _unitOfWork.EducationalStatuses.Update(status);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(EducationalStatus);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int EducationalStatusId)
        {
            try
            {
                var IsDeleted = _unitOfWork.EducationalStatuses.Delete(EducationalStatusId);
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