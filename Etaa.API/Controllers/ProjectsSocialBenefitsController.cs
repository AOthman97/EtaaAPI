namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsSocialBenefitsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsSocialBenefitsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectsSocialBenefits()
        {
            return Ok(_unitOfWork.ProjectsSocialBenefits.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectsSocialBenefitsDto socialBenefitsDto)
        {
            try
            {
                ProjectsSocialBenefits socialBenefits = new()
                {
                    ProjectId = socialBenefitsDto.ProjectId,
                    ProjectSocialBenefitsId = socialBenefitsDto.ProjectSocialBenefitsId
                };
                var ProjectsSocialBenefits = _unitOfWork.ProjectsSocialBenefits.Add(socialBenefits);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectsSocialBenefits);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectsSocialBenefitsDto socialBenefitsDto)
        {
            try
            {
                ProjectsSocialBenefits socialBenefits = new()
                {
                    ProjectsSocialBenefitsId = socialBenefitsDto.ProjectsSocialBenefitsId,
                    ProjectId = socialBenefitsDto.ProjectId,
                    ProjectSocialBenefitsId = socialBenefitsDto.ProjectSocialBenefitsId
                };
                var ProjectsSocialBenefits = _unitOfWork.ProjectsSocialBenefits.Update(socialBenefits);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectsSocialBenefits);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectsSocialBenefitsId)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectsSocialBenefits.Delete(ProjectsSocialBenefitsId);
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