namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FamiliesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Families.GetAll().ToList());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int FamilyId)
        {
            //return Ok(await _unitOfWork.Contributors.GetById(ContributorId));
            return Ok(await _unitOfWork.Families.GetById(F => F.FamilyId.Equals(FamilyId)));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(FamilyDto familyDto)
        {
            try
            {
                Family family = new()
                {
                    Address = familyDto.Address,
                    Age = familyDto.Age,
                    Alleyway = familyDto.Alleyway,
                    DateOfBirth = familyDto.DateOfBirth,
                    FirstPhoneNumber = familyDto.FirstPhoneNumber,
                    SecondPhoneNumber = familyDto.SecondPhoneNumber,
                    HouseNumber = familyDto.HouseNumber,
                    IsCurrentInvestmentProject = familyDto.IsCurrentInvestmentProject,
                    MonthlyIncome = familyDto.MonthlyIncome,
                    NameAr = familyDto.NameAr,
                    NameEn = familyDto.NameEn,
                    NationalNumber = familyDto.NationalNumber,
                    NumberOfIndividuals = familyDto.NumberOfIndividuals,
                    PassportNumber = familyDto.PassportNumber,
                    ResidentialSquare = familyDto.ResidentialSquare,
                    AccommodationTypeId = familyDto.AccommodationTypeId,
                    DistrictId = familyDto.DistrictId,
                    EducationalStatusId = familyDto.EducationalStatusId,
                    GenderId = familyDto.GenderId,
                    HealthStatusId = familyDto.HealthStatusId,
                    InvestmentTypeId = familyDto.InvestmentTypeId,
                    JobId = familyDto.JobId,
                    ReligionId = familyDto.ReligionId,
                    IsApprovedByManagement = true,
                    IsCanceled = false,
                    UserId = familyDto.UserId
                };
                var Family = _unitOfWork.Families.Add(family);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Family);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(FamilyDto familyDto)
        {
            try
            {
                Family family = new()
                {
                    FamilyId = familyDto.FamilyId,
                    Address = familyDto.Address,
                    Age = familyDto.Age,
                    Alleyway = familyDto.Alleyway,
                    DateOfBirth = familyDto.DateOfBirth,
                    FirstPhoneNumber = familyDto.FirstPhoneNumber,
                    SecondPhoneNumber = familyDto.SecondPhoneNumber,
                    HouseNumber = familyDto.HouseNumber,
                    IsCurrentInvestmentProject = familyDto.IsCurrentInvestmentProject,
                    MonthlyIncome = familyDto.MonthlyIncome,
                    NameAr = familyDto.NameAr,
                    NameEn = familyDto.NameEn,
                    NationalNumber = familyDto.NationalNumber,
                    NumberOfIndividuals = familyDto.NumberOfIndividuals,
                    PassportNumber = familyDto.PassportNumber,
                    ResidentialSquare = familyDto.ResidentialSquare,
                    AccommodationTypeId = familyDto.AccommodationTypeId,
                    DistrictId = familyDto.DistrictId,
                    EducationalStatusId = familyDto.EducationalStatusId,
                    GenderId = familyDto.GenderId,
                    HealthStatusId = familyDto.HealthStatusId,
                    InvestmentTypeId = familyDto.InvestmentTypeId,
                    JobId = familyDto.JobId,
                    ReligionId = familyDto.ReligionId,
                    IsApprovedByManagement = true,
                    IsCanceled = familyDto.IsCanceled
                };
                var Family = _unitOfWork.Families.Update(family);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Family);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int FamilyId)
        {
            try
            {
                var IsDeleted = _unitOfWork.Families.Delete(FamilyId);
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