namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectAssetesProjectTypeAssetsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectAssetesProjectTypeAssetsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}