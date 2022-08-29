namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTypesAssetsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTypesAssetsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProjectTypesAssets()
        {
            return Ok(_unitOfWork.ProjectTypesAssets.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ProjectTypesAssetsDto assetsDto)
        {
            try
            {
                ProjectTypesAssets assets = new()
                {
                    ProjectTypeId = assetsDto.ProjectTypeId,
                    NameAr = assetsDto.NameAr,
                    NameEn = assetsDto.NameEn
                };
                var ProjectTypesAssets = _unitOfWork.ProjectTypesAssets.Add(assets);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectTypesAssets);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ProjectTypesAssetsDto assetsDto)
        {
            try
            {
                ProjectTypesAssets assets = new()
                {
                    ProjectTypesAssetsId = assetsDto.ProjectTypesAssetsId,
                    ProjectTypeId = assetsDto.ProjectTypeId,
                    NameAr = assetsDto.NameAr,
                    NameEn = assetsDto.NameEn
                };
                var ProjectTypesAssets = _unitOfWork.ProjectTypesAssets.Update(assets);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(ProjectTypesAssets);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ProjectTypesAssetsId)
        {
            try
            {
                var IsDeleted = _unitOfWork.ProjectTypesAssets.Delete(ProjectTypesAssetsId);
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