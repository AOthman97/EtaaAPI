namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMembersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FamilyMembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.FamilyMembers.GetAll().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int FamilyMemberId)
        {
            return Ok(await _unitOfWork.FamilyMembers.GetById(familyMember => familyMember.FamilyMemberId.Equals(FamilyMemberId), new[] { "Family" }));
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(FamilyMemberDto familyMemberDto)
        {
            try
            {
                FamilyMember familyMember = new()
                {
                    NameAr = familyMemberDto.NameAr,
                    NameEn = familyMemberDto.NameEn,
                    Note = familyMemberDto.Note,
                    Age = familyMemberDto.Age,
                    EducationalStatusId = familyMemberDto.EducationalStatusId,
                    FamilyId = familyMemberDto.FamilyId,
                    GenderId = familyMemberDto.GenderId,
                    JobId = familyMemberDto.JobId,
                    KinshipId = familyMemberDto.KinshipId,
                    IsCanceled = false
                };
                var FamilyMember = _unitOfWork.FamilyMembers.Add(familyMember);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(FamilyMember);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(FamilyMemberDto familyMemberDto)
        {
            try
            {
                FamilyMember familyMember = new()
                {
                    FamilyMemberId = familyMemberDto.FamilyMemberId,
                    NameAr = familyMemberDto.NameAr,
                    NameEn = familyMemberDto.NameEn,
                    Note = familyMemberDto.Note,
                    Age = familyMemberDto.Age,
                    EducationalStatusId = familyMemberDto.EducationalStatusId,
                    FamilyId = familyMemberDto.FamilyId,
                    GenderId = familyMemberDto.GenderId,
                    JobId = familyMemberDto.JobId,
                    KinshipId = familyMemberDto.KinshipId,
                    IsCanceled = familyMemberDto.IsCanceled
                };
                var FamilyMember = _unitOfWork.FamilyMembers.Update(familyMember);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(FamilyMember);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSingle")]
        public IActionResult DeleteSingle(int FamilyMemberId)
        {
            try
            {
                var IsDeleted = _unitOfWork.FamilyMembers.Delete(FamilyMemberId);
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