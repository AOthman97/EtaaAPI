namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        private readonly IUnitOfWork _unitOfWork;

        public CitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Make the same controllr for the city and district and add the LoadCities and LoadDistricts in each one
        [HttpGet("GetCities")]
        public IActionResult GetCities()
        {
            return Ok(_unitOfWork.Cities.GetAll().ToList());
        }

        [HttpGet("GetCitiesByState")]
        public IActionResult GetCitiesByState(int StateId)
        {;
            return Ok(_unitOfWork.Cities.FindAll(City => City.StateId == StateId));
        }
    }
}