using EtaaApi.Core.Models;

namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorsController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        private readonly IBaseRepo<Contributor> _ContributorRepo;

        public ContributorsController(IBaseRepo<Contributor> contributor)
        {
            _ContributorRepo = contributor;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_ContributorRepo.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            return Ok(await _ContributorRepo.GetById(1));
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName()
        {
            // The passed-in data here should of course come from the front-end and not static
            // After adding the new optional param that should hold the name of the model we want to include
            // in the data that is being queried, We'll include the District model here to be also present
            // in the result data
            return Ok(await _ContributorRepo.Find(C => C.NameEn.Contains("Qut"), new[] { "District" }));
        }

        [HttpGet("FindAll")]
        public IActionResult FindAll()
        {
            // After adding the new optional param that should hold the name of the model we want to include
            // in the data that is being queried, We'll include the District model here to be also present
            // in the result data
            return Ok(_ContributorRepo.FindAll(C => C.NameEn.Contains("Qut"), new[] { "District" }));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle()
        {
            return Ok(_ContributorRepo.Add(new Contributor { NameAr = "تستنج", NameEn = "Testing", MonthlyShareAmount = 5000, DistrictId = 3}));
        }
    }
}