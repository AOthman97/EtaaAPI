namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GendersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetGenders()
        {
            return Ok(_unitOfWork.Genders.GetAll().ToList());
        }
    }
}