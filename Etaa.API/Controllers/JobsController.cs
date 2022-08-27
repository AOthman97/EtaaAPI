namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetJobs()
        {
            return Ok(_unitOfWork.Jobs.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(JobDto jobDto)
        {
            try
            {
                Job job = new()
                {
                    NameAr = jobDto.NameAr,
                    NameEn = jobDto.NameEn
                };
                var Job = _unitOfWork.Jobs.Add(job);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Job);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(JobDto jobDto)
        {
            try
            {
                Job job = new()
                {
                    JobId = jobDto.JobId,
                    NameAr = jobDto.NameAr,
                    NameEn = jobDto.NameEn
                };
                var Job = _unitOfWork.Jobs.Update(job);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Job);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int JobIdId)
        {
            try
            {
                var IsDeleted = _unitOfWork.Jobs.Delete(JobIdId);
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