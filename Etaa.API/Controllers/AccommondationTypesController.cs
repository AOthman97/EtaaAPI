namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommondationTypesController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        private readonly IUnitOfWork _unitOfWork;

        public AccommondationTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAccommondationTypes()
        {
            return Ok(_unitOfWork.AccommodationTypes.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(AccommodationTypeDto typeDto)
        {
            try
            {
                AccommodationType type = new()
                {
                    NameAr = typeDto.NameAr,
                    NameEn = typeDto.NameEn
                };
                var AccommodationType = _unitOfWork.AccommodationTypes.Add(type);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(AccommodationType);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(AccommodationTypeDto typeDto)
        {
            try
            {
                AccommodationType type = new()
                {
                    AccommodationTypeId = typeDto.AccommodationTypeId,
                    NameAr = typeDto.NameAr,
                    NameEn = typeDto.NameEn
                };
                var AccommodationType = _unitOfWork.AccommodationTypes.Update(type);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(AccommodationType);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int AccommodationTypeId)
        {
            try
            {
                var IsDeleted = _unitOfWork.AccommodationTypes.Delete(AccommodationTypeId);
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