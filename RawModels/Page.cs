using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawPage
    {
      

        [Key]
        public short PageId { get; set; }
        [Required]
        [StringLength(100)]
        public string PageTitle { get; set; }
        public short ParentId { get; set; }
        public string ParentPage { get; set; }
        public int ApplicationId { get; set; }
        [Required]
        [StringLength(100)]
        public string label { get; set; }
        [Required]
        [StringLength(200)]
        public string link { get; set; }
        public byte Active { get; set; }
        public short? OrgId { get; set; }
        [StringLength(100)]
        public string faIcon { get; set; }
        [StringLength(100)]
        public string FullPath { get; set; }
        [StringLength(250)]
        public string PhotoPath { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public byte? PlanId { get; set; }
        public byte? PlanName { get; set; }
        public byte? IsTemplate { get; set; }
        public short? DisplayOrder { get; set; }
        public byte? HasSubmenu { get; set; }
        public byte? HomePage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
