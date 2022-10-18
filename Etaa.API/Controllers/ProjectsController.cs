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
    }
}