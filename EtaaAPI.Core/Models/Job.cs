namespace EtaaApi.Core.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        [Required(ErrorMessage = "Name(Ar) is Required!")]
        [Display(Name = "Name(Ar)")]
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public bool? IsCanceled { get; set; }

        public ICollection<FamilyMember>? FamilyMembers;
        public ICollection<Family>? Families;
    }
}