namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsSelectionReasonsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectsSelectionReasonsRepo _projectsSelectionReasons;

        public ProjectsSelectionReasonsController(IProjectsSelectionReasonsRepo projectsSelectionReasons,
            IUnitOfWork unitOfWork)
        {
            _projectsSelectionReasons = projectsSelectionReasons;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectsSelectionReasons()
        {
            return Ok(_projectsSelectionReasons.GetAll().ToList());
        }

        [HttpGet("GetProjectSelectionReasons")]
        public IActionResult GetProjectSelectionReasons(int ProjectId)
        {
            return Ok(_projectsSelectionReasons.GetProjectsSelectionReasons(ProjectId));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectsSelectionReasonsDto selectionReasonsDto)
        {
            try
            {
                ProjectsSelectionReasons selectionReasons = new()
                {
                    ProjectId = selectionReasonsDto.ProjectId,
                    ProjectSelectionReasonsId = selectionReasonsDto.ProjectSelectionReasonsId
                };
                var ProjectsSelectionReasons = _projectsSelectionReasons.Add(selectionReasons);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectsSelectionReasons);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectsSelectionReasonsDto selectionReasonsDto)
        {
            try
            {
                ProjectsSelectionReasons selectionReasons = new()
                {
                    ProjectsSelectionReasonsId = selectionReasonsDto.ProjectsSelectionReasonsId,
                    ProjectId = selectionReasonsDto.ProjectId,
                    ProjectSelectionReasonsId = selectionReasonsDto.ProjectSelectionReasonsId
                };
                var ProjectsSelectionReasons = _projectsSelectionReasons.Update(selectionReasons);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectsSelectionReasons);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectsSelectionReasonsId)
        {
            try
            {
                var IsDeleted = _projectsSelectionReasons.Delete(ProjectsSelectionReasonsId);
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