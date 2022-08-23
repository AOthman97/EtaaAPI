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
        public async Task<IActionResult> GetById(int ContributorId)
        {
            return Ok(await _unitOfWork.Contributors.GetById(ContributorId));
        }

        [HttpGet("GetByNameWithDistrict")]
        public async Task<IActionResult> GetByName(string ContributorName)
        {
            // The passed-in data here should of course come from the front-end and not static
            // After adding the new optional param that should hold the name of the model we want to include
            // in the data that is being queried, We'll include the District model here to be also present
            // in the result data
            return Ok(await _unitOfWork.Contributors.Find(C => C.NameEn.Contains(ContributorName) ||
            C.NameAr.Contains(ContributorName), new[] { "District" }));
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
        public IActionResult AddSingle(ContributorDto contributorDto)
        {
            Contributor contributor = new()
            {
                NameEn = contributorDto.NameEn,
                NameAr = contributorDto.NameAr,
                Address = contributorDto.Address,
                Email = contributorDto.Email,
                StartDate = contributorDto.StartDate,
                EndDate = contributorDto.EndDate,
                IsActive = true,
                IsCanceled = false,
                Mobile = contributorDto.Mobile,
                MonthlyShareAmount = contributorDto.MonthlyShareAmount,
                NumberOfShares = contributorDto.NumberOfShares,
                WhatsappMobile = contributorDto.WhatsappMobile,
                DistrictId = contributorDto.DistrictId
            };
            // Now we've removed the SaveChanges() from the Base repo and instead used the unit of work to save changes
            //var Contributor = _unitOfWork.Contributors.Add(new Contributor { NameAr = "تستنج 7777", NameEn = "Testing 999", MonthlyShareAmount = 5000, DistrictId = 3, IsActive = true, IsCanceled = false });
            var Contributor = _unitOfWork.Contributors.Add(contributor);
            // It's pretty useful when adding multiple entities to the DB or updating, removing them by just calling this
            // method in the action of the controller at the very end
            int NumberAffected = _unitOfWork.Complete();
            if(NumberAffected > 0)
                return Ok(Contributor);
            else
                return BadRequest();
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ContributorDto contributorDto)
        {
            Contributor contributor = new()
            {
                NameEn = contributorDto.NameEn,
                NameAr = contributorDto.NameAr,
                Address = contributorDto.Address,
                Email = contributorDto.Email,
                StartDate = contributorDto.StartDate,
                EndDate = contributorDto.EndDate,
                IsActive = true,
                IsCanceled = false,
                Mobile = contributorDto.Mobile,
                MonthlyShareAmount = contributorDto.MonthlyShareAmount,
                NumberOfShares = contributorDto.NumberOfShares,
                WhatsappMobile = contributorDto.WhatsappMobile,
                DistrictId = contributorDto.DistrictId
            };
            var Contributor = _unitOfWork.Contributors.Add(contributor);
            int NumberAffected = _unitOfWork.Complete();
            if (NumberAffected > 0)
                return Ok(Contributor);
            else
                return BadRequest();
        }
    }
}