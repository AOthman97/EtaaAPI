namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsAssetsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsAssetsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectsAssets()
        {
            return Ok(_unitOfWork.ProjectsAssets.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectsAssetsDto projectsAssetsDto)
        {
            try
            {
                ProjectsAssets projectsAssets = new()
                {
                    Amount = projectsAssetsDto.Amount,
                    Quantity = projectsAssetsDto.Quantity,
                    ProjectId = projectsAssetsDto.ProjectId,
                    ProjectTypesAssetsId = projectsAssetsDto.ProjectTypesAssetsId
                };
                var ProjectsAssets = _unitOfWork.ProjectsAssets.Add(projectsAssets);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectsAssets);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectsAssetsDto projectsAssetsDto)
        {
            try
            {
                ProjectsAssets projectsAssets = new()
                {
                    ProjectsAssetsId = projectsAssetsDto.ProjectsAssetsId,
                    Amount = projectsAssetsDto.Amount,
                    Quantity = projectsAssetsDto.Quantity,
                    ProjectId = projectsAssetsDto.ProjectId,
                    ProjectTypesAssetsId = projectsAssetsDto.ProjectTypesAssetsId
                };
                var ProjectsAssets = _unitOfWork.ProjectsAssets.Update(projectsAssets);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectsAssets);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectsAssetsId)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectsAssets.Delete(ProjectsAssetsId);
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