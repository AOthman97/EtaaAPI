namespace Etaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReligionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReligionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetReligions()
        {
            return Ok(_unitOfWork.Religions.GetAll().ToList());
        }

        [HttpPost("AddSingle")]
        public IActionResult AddSingle(ReligionDto religionDto)
        {
            try
            {
                Religion religion = new()
                {
                    NameAr = religionDto.NameAr,
                    NameEn = religionDto.NameEn
                };
                var Religion = _unitOfWork.Religions.Add(religion);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Religion);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateSingle")]
        public IActionResult UpdateSingle(ReligionDto religionDto)
        {
            try
            {
                Religion religion = new()
                {
                    ReligionId = religionDto.ReligionId,
                    NameAr = religionDto.NameAr,
                    NameEn = religionDto.NameEn
                };
                var Religion = _unitOfWork.Religions.Update(religion);
                int NumberAffected = _unitOfWork.Complete();
                if (NumberAffected > 0)
                    return Ok(Religion);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("DeleteSingle")]
        //public IActionResult DeleteSingle(int ReligionId)
        //{
        //    try
        //    {
        //        var IsDeleted = _unitOfWork.Religions.Delete(ReligionId);
        //        if (IsDeleted)
        //        {
        //            int NumberAffected = _unitOfWork.Complete();
        //            if (NumberAffected > 0)
        //                return Ok(true);
        //            else
        //                return BadRequest();
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}