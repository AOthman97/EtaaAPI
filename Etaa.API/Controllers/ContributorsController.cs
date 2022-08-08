using EtaaApi.Core.Models;
using EtaaAPI.Core.Interfaces;
using MoviesProject.Dtos;

namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorsController : ControllerBase
    {
        // Firstly Inject the IBaseRepo
        private readonly IUnitOfWork _unitOfWork;

        public ContributorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Contributors.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            return Ok(await _unitOfWork.Contributors.GetById(1));
        }

        [HttpGet("GetByNameWithDistrict")]
        public async Task<IActionResult> GetByName()
        {
            // The passed-in data here should of course come from the front-end and not static
            // After adding the new optional param that should hold the name of the model we want to include
            // in the data that is being queried, We'll include the District model here to be also present
            // in the result data
            return Ok(await _unitOfWork.Contributors.Find(C => C.NameEn.Contains("Qut"), new[] { "District" }));
        }

        [HttpGet("FindAllWithDistrict")]
        public IActionResult FindAll()
        {
            // After adding the new optional param that should hold the name of the model we want to include
            // in the data that is being queried, We'll include the District model here to be also present
            // in the result data
            return Ok(_unitOfWork.Contributors.FindAll(C => C.NameEn.Contains("Qut"), new[] { "District" }));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(GenreDto genre)
        {
            //Contributor contributor = new() { NameEn = genre.Name };
            return Ok(_unitOfWork.Contributors.Add(new Contributor { NameAr = "تستنج", NameEn = "Testing", MonthlyShareAmount = 5000, DistrictId = 3}));
        }
    }
}