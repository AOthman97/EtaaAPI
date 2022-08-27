namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        private readonly IUnitOfWork _unitOfWork;

        public DistrictsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Districts.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int DistrictId)
        {
            return Ok(await _unitOfWork.Districts.GetById(DistrictId));
        }

        [HttpGet("GetByNameWithCity")]
        public async Task<IActionResult> GetByName(string DistrictName)
        {
            return Ok(await _unitOfWork.Districts.Find(C => C.NameEn.Contains(DistrictName) ||
            C.NameAr.Contains(DistrictName), new[] { "City" }));
        }

        [HttpGet("FindAllWithCity")]
        public IActionResult FindAll()
        {
            return Ok(_unitOfWork.Districts.FindAll(new[] { "City" }));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(DistrictDto districtDto)
        {
            try
            {
                District district = new()
                {
                    NameAr = districtDto.NameAr,
                    NameEn = districtDto.NameEn,
                    CityId = districtDto.CityId
                };
                var District = _unitOfWork.Districts.Add(district);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(District);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(DistrictDto districtDto)
        {
            try
            {
                District district = new()
                {
                    DistrictId = districtDto.DistrictId,
                    NameAr = districtDto.NameAr,
                    NameEn = districtDto.NameEn,
                    CityId = districtDto.CityId
                };
                var District = _unitOfWork.Districts.Update(district);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(District);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int DistrictId)
        {
            try
            {
                var IsDeleted = _unitOfWork.Districts.Delete(DistrictId);
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