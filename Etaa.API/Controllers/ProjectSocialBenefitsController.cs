namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSocialBenefitsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectSocialBenefitsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectSocialBenefits()
        {
            return Ok(_unitOfWork.ProjectSocialBenefits.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectSocialBenefitsDto benefitsDto)
        {
            try
            {
                ProjectSocialBenefits benefits = new()
                {
                    NameAr = benefitsDto.NameAr,
                    NameEn = benefitsDto.NameEn
                };
                var ProjectSocialBenefits = _unitOfWork.ProjectSocialBenefits.Add(benefits);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectSocialBenefits);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectSocialBenefitsDto benefitsDto)
        {
            try
            {
                ProjectSocialBenefits benefits = new()
                {
                    ProjectSocialBenefitsId = benefitsDto.ProjectSocialBenefitsId,
                    NameAr = benefitsDto.NameAr,
                    NameEn = benefitsDto.NameEn
                };
                var ProjectSocialBenefits = _unitOfWork.ProjectSocialBenefits.Update(benefits);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectSocialBenefits);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectSocialBenefitsId)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectSocialBenefits.Delete(ProjectSocialBenefitsId);
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