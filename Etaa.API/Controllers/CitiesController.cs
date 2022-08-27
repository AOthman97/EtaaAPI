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
        {
            return Ok(_unitOfWork.Cities.FindAll(City => City.StateId == StateId));
        }

        [HttpGet("GetByNameWithstate")]
        public async Task<IActionResult> GetByName(string CityName)
        {
            return Ok(await _unitOfWork.Cities.Find(C => C.NameEn.Contains(CityName) ||
            C.NameAr.Contains(CityName), new[] { "State" }));
        }

        [HttpGet("FindAllWithState")]
        public IActionResult FindAll()
        {
            return Ok(_unitOfWork.Cities.FindAll(new[] { "State" }));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(CityDto cityDto)
        {
            try
            {
                City city = new ()
                {
                    NameAr = cityDto.NameAr,
                    NameEn = cityDto.NameEn,
                    StateId = cityDto.StateId
                };
                var City = _unitOfWork.Cities.Add(city);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(City);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(CityDto cityDto)
        {
            try
            {
                City city = new()
                {
                    CityId = cityDto.CityId,
                    NameAr = cityDto.NameAr,
                    NameEn = cityDto.NameEn,
                    StateId = cityDto.StateId
                };
                var City = _unitOfWork.Cities.Update(city);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(City);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int CityId)
        {
            try
            {
                var IsDeleted = _unitOfWork.Cities.Delete(CityId);
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