namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MartialStatusesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MartialStatusesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetMartialStatuses()
        {
            return Ok(_unitOfWork.MartialStatuses.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(MartialStatusDto statusDto)
        {
            try
            {
                MartialStatus status = new()
                {
                    NameAr = statusDto.NameAr,
                    NameEn = statusDto.NameEn
                };
                var MartialStatus = _unitOfWork.MartialStatuses.Add(status);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(MartialStatus);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(MartialStatusDto statusDto)
        {
            try
            {
                MartialStatus status = new()
                {
                    MartialStatusId = statusDto.MartialStatusId,
                    NameAr = statusDto.NameAr,
                    NameEn = statusDto.NameEn
                };
                var MartialStatus = _unitOfWork.MartialStatuses.Update(status);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(MartialStatus);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int MartialStatusId)
        {
            try
            {
                var IsDeleted = _unitOfWork.MartialStatuses.Delete(MartialStatusId);
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