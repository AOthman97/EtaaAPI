﻿namespace EtaaApi.Core.Models
{
    public class ProjectTypesAssets
    {
        [Key]
        public int ProjectTypesAssetsId { get; set; }
        [Required(ErrorMessage = "!حقل الإسم عربي مطلوب")]
        [Display(Name = "حقل الإسم عربي")]
        public string NameAr { get; set; }
        [Required(ErrorMessage = "!حقل الإسم إنجليزي مطلوب")]
        [Display(Name = "حقل الإسم إنجليزي")]
        public string NameEn { get; set; }
        [Display(Name = "هل تم الحذف")]
        public bool IsCanceled { get; set; }

        // Relationship between the project type assets and project type, the relationship is represented in the main table
        [Display(Name = "نوع المشروع")]
        public int ProjectTypeId { get; set; }
    }
}