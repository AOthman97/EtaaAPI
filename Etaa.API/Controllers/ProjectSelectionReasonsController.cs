namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSelectionReasonsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectSelectionReasonsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectSelectionReasons()
        {
            return Ok(_unitOfWork.ProjectSelectionReasons.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectSelectionReasonsDto reasonsDto)
        {
            try
            {
                ProjectSelectionReasons reasons = new()
                {
                    NameAr = reasonsDto.NameAr,
                    NameEn = reasonsDto.NameEn
                };
                var ProjectSelectionReasons = _unitOfWork.ProjectSelectionReasons.Add(reasons);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectSelectionReasons);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectSelectionReasonsDto reasonsDto)
        {
            try
            {
                ProjectSelectionReasons reasons = new()
                {
                    ProjectSelectionReasonsId = reasonsDto.ProjectSelectionReasonsId,
                    NameAr = reasonsDto.NameAr,
                    NameEn = reasonsDto.NameEn
                };
                var ProjectSelectionReasons = _unitOfWork.ProjectSelectionReasons.Update(reasons);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectSelectionReasons);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectSelectionReasonsId)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectSelectionReasons.Delete(ProjectSelectionReasonsId);
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