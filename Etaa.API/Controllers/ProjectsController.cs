using EtaaAPI.Core.Models;

namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectsRepo _projectsRepo;

        public ProjectsController(IProjectsRepo projectsRepo, IUnitOfWork unitOfWork)
        {
            _projectsRepo = projectsRepo;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_projectsRepo.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int ProjectId)
        {
            return Ok(await _projectsRepo.GetById(C => C.ProjectId.Equals(ProjectId)));
        }

        // In the API version instead of returning all project types and a selected one maybe try to
        // get all of them seperately and then get the selected one either with the project data itself or make
        // a call to this endpoint, the GetProjectTypes(int ProjectId)
        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] CompositeObject compositeObject)
        {
            try
            {
                bool IsCreated = _projectsRepo.CreateProject(compositeObject.projectDto,
                    compositeObject.projectsAssetsDto,
                    compositeObject.projectsSelectionReasonsDto,
                    compositeObject.projectsSocialBenefitsDto);

                if (IsCreated)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("UpdateProject")]
        public async Task<IActionResult> UpdateProject([FromBody] CompositeObject compositeObject)
        {
            try
            {
                bool IsCreated = _projectsRepo.UpdateProject(compositeObject.projectDto,
                    compositeObject.projectsAssetsDto,
                    compositeObject.projectsSelectionReasonsDto,
                    compositeObject.projectsSocialBenefitsDto);

                if (IsCreated)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(int ProjectId)
        {
            try
            {
                bool IsDeleted = _projectsRepo.DeleteProject(ProjectId);

                if (IsDeleted)
                    return Ok("Project Canceled!");
                else
                    return BadRequest("Project not Canceled!");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}