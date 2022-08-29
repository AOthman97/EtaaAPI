namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDomainTypesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectDomainTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectDomainTypes()
        {
            return Ok(_unitOfWork.ProjectDomainTypes.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectDomainTypesDto domainDto)
        {
            try
            {
                ProjectDomainTypes projectDomainTypes = new()
                {
                    NameAr = domainDto.NameAr,
                    NameEn = domainDto.NameEn
                };
                var ProjectDomainTypes = _unitOfWork.ProjectDomainTypes.Add(projectDomainTypes);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectDomainTypes);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectDomainTypesDto domainDto)
        {
            try
            {
                ProjectDomainTypes projectDomainTypes = new()
                {
                    ProjectDomainTypeId = domainDto.ProjectDomainTypeId,
                    NameAr = domainDto.NameAr,
                    NameEn = domainDto.NameEn
                };
                var ProjectDomainTypes = _unitOfWork.ProjectDomainTypes.Update(projectDomainTypes);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectDomainTypes);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectDomainTypesId)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectDomainTypes.Delete(ProjectDomainTypesId);
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