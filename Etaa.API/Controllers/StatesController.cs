namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        private readonly IUnitOfWork _unitOfWork;

        public StatesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Make the same controllr for the city and district and add the LoadCities and LoadDistricts in each one
        [HttpGet("GetStates")]
        public IActionResult GetStates()
        {
            return Ok(_unitOfWork.States.GetAll().ToList());
        }
    }
}