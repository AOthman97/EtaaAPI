namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KinshipsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public KinshipsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetKinships()
        {
            return Ok(_unitOfWork.Kinships.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(KinshipDto kinshipDto)
        {
            try
            {
                Kinship kinship = new()
                {
                    NameAr = kinshipDto.NameAr,
                    NameEn = kinshipDto.NameEn
                };
                var Kinship = _unitOfWork.Kinships.Add(kinship);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Kinship);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(KinshipDto kinshipDto)
        {
            try
            {
                Kinship kinship = new()
                {
                    KinshipId = kinshipDto.KinshipId,
                    NameAr = kinshipDto.NameAr,
                    NameEn = kinshipDto.NameEn
                };
                var Kinship = _unitOfWork.Kinships.Update(kinship);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Kinship);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int KinshipId)
        {
            try
            {
                var IsDeleted = _unitOfWork.Kinships.Delete(KinshipId);
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