namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectGroupsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectGroupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectGroups()
        {
            return Ok(_unitOfWork.ProjectGroups.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectGroupDto groupDto)
        {
            try
            {
                ProjectGroup projectGroup = new()
                {
                    NameAr = groupDto.NameAr,
                    NameEn = groupDto.NameEn
                };
                var ProjectGroup = _unitOfWork.ProjectGroups.Add(projectGroup);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectGroup);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectGroupDto groupDto)
        {
            try
            {
                ProjectGroup projectGroup = new()
                {
                    ProjectGroupId = groupDto.ProjectGroupId,
                    NameAr = groupDto.NameAr,
                    NameEn = groupDto.NameEn
                };
                var ProjectGroup = _unitOfWork.ProjectGroups.Update(projectGroup);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectGroup);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectGroupId)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectGroups.Delete(ProjectGroupId);
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