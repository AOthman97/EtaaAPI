namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClearancesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClearancesRepo _clearancesRepo;

        public ClearancesController(IClearancesRepo clearancesRepo, IUnitOfWork unitOfWork)
        {
            _clearancesRepo = clearancesRepo;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_clearancesRepo.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int ClearanceId)
        {
            return Ok(await _clearancesRepo.GetById(C => C.ClearanceId.Equals(ClearanceId)));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(Clearance clearance)
        {
            try
            {
                bool IsAdded = _clearancesRepo.CreateClearance(clearance);
                if (IsAdded)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(Clearance clearance)
        {
            try
            {
                var Clearance = _unitOfWork.Clearances.Update(clearance);
                if (Clearance == null)
                    return BadRequest();
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ClearanceId)
        {
            try
            {
                var Clearance = _clearancesRepo.GetById(ClearanceId);
                if (Clearance.Result != null)
                {
                    var IsDeleted = _unitOfWork.Clearances.Delete(ClearanceId);
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