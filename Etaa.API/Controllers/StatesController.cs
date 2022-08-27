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

        [HttpGet("GetStates")]
        public IActionResult GetStates()
        {
            return Ok(_unitOfWork.States.GetAll().ToList());
        }

        [HttpGet("GetStateByCity")]
        public IActionResult GetCitiesByState(int StateId)
        {
            return Ok(_unitOfWork.States.FindAll(State => State.StateId == StateId));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(StateDto stateDto)
        {
            try
            {
                State state = new()
                {
                    NameAr = stateDto.NameAr,
                    NameEn = stateDto.NameEn
                };
                var State = _unitOfWork.States.Add(state);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(State);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(StateDto stateDto)
        {
            try
            {
                State state = new()
                {
                    StateId = stateDto.StateId,
                    NameAr = stateDto.NameAr,
                    NameEn = stateDto.NameEn
                };
                var State = _unitOfWork.States.Update(state);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(State);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int StateId)
        {
            try
            {
                var IsDeleted = _unitOfWork.States.Delete(StateId);
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