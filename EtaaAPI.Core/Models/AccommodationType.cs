namespace EtaaApi.Core.Models
{
    public class AccommodationType
    {
        [Key]
        public int AccommodationTypeId { get; set; }
        [Required(ErrorMessage = "Name(Ar) is Required!")]
        [Display(Name = "Name(Ar)")]
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public bool? IsCanceled { get; set; }
        public ICollection<FamilyMember>? FamilyMembers;
    }
}