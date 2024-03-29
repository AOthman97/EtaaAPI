﻿namespace Etaa.API.Controllers
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
            return Ok(await _unitOfWork.Contributors.GetById(C => C.ContributorId.Equals(ContributorId), new[] { "District" }));
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
        public IActionResult FindAll(string Prefix)
        {
            // After adding the new optional param that should hold the name of the model we want to include
            // in the data that is being queried, We'll include the District model here to be also present
            // in the result data
            return Ok(_unitOfWork.Contributors.FindAll(C => C.NameEn.Contains(Prefix), new[] { "District" }));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ContributorDto contributorDto)
        {
            try
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
                if (NumberAffected > 0)
                    return Ok(Contributor);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ContributorDto ContributorDto)
        {
            try
            {
                Contributor contributor = new()
                {
                    ContributorId = ContributorDto.ContributorId,
                    NameEn = ContributorDto.NameEn,
                    NameAr = ContributorDto.NameAr,
                    Address = ContributorDto.Address,
                    Email = ContributorDto.Email,
                    StartDate = ContributorDto.StartDate,
                    EndDate = ContributorDto.EndDate,
                    IsActive = true,
                    IsCanceled = false,
                    Mobile = ContributorDto.Mobile,
                    MonthlyShareAmount = ContributorDto.MonthlyShareAmount,
                    NumberOfShares = ContributorDto.NumberOfShares,
                    WhatsappMobile = ContributorDto.WhatsappMobile,
                    DistrictId = ContributorDto.DistrictId
                };
                var Contributor = _unitOfWork.Contributors.Update(contributor);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Contributor);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int ContributorId)
        {
            try
            {
                var IsDeleted = _unitOfWork.Contributors.Delete(ContributorId);
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