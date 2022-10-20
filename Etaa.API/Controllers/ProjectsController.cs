using EtaaAPI.Core.Models;
using System.Collections;

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
        /*Projects project, [FromBody] List<ProjectsAssets> projectsAssets,
                [FromBody] List<ProjectsSelectionReasons> projectsSelectionReasons,
            [FromBody] List<ProjectsSocialBenefits> projectsSocialBenefits*/
        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] CompositeObject compositeObject)
        {
            try
            {
                ArrayList sf = new();
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
    }
}