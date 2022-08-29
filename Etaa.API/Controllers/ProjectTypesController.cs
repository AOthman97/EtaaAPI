namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTypesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectTypes()
        {
            return Ok(_unitOfWork.ProjectTypes.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectTypesDto typesDto)
        {
            try
            {
                ProjectTypes types = new()
                {
                    NameAr = typesDto.NameAr,
                    NameEn = typesDto.NameEn,
                    ProjectDomainTypeId = typesDto.ProjectDomainTypeId,
                    ProjectGroupId = typesDto.ProjectGroupId
                };
                var ProjectTypes = _unitOfWork.ProjectTypes.Add(types);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectTypes);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectTypesDto typesDto)
        {
            try
            {
                ProjectTypes types = new()
                {
                    ProjectTypeId = typesDto.ProjectTypeId,
                    NameAr = typesDto.NameAr,
                    NameEn = typesDto.NameEn,
                    ProjectDomainTypeId = typesDto.ProjectDomainTypeId,
                    ProjectGroupId = typesDto.ProjectGroupId
                };
                var ProjectTypes = _unitOfWork.ProjectTypes.Update(types);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectTypes);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectTypesId)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectTypes.Delete(ProjectTypesId);
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