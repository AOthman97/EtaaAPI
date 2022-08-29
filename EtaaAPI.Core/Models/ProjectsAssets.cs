﻿namespace EtaaApi.Core.Models
{
    public class ProjectsAssets
    {
        [Key]
        public int ProjectsAssetsId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        // Relationship between the project type and project domain
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Projects? Projects { get; set; }

        // Relationship between the project type and project group
        public int ProjectTypesAssetsId { get; set; }
        [ForeignKey("ProjectTypesAssetsId")]
        public ProjectTypesAssets? ProjectTypesAssets { get; set; }
    }
}