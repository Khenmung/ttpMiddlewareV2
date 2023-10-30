using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("Organization")]
    public class RawOrganization
    {
        

        [Key]
        public short OrganizationId { get; set; }
        [Required]
        [StringLength(50)]
        public string OrganizationName { get; set; }
        [StringLength(100)]
        public string LogoPath { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public short? CityId { get; set; }
        public short? StateId { get; set; }
        public short? CountryId { get; set; }
        [StringLength(50)]
        public string Contact { get; set; }
        [StringLength(50)]
        public string RegistrationNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidTo { get; set; }
        [StringLength(254)]
        public string WebSite { get; set; }
        public byte? Active { get; set; }
        public short? ParentId { get; set; }
        public short? MainOrgId { get; set; }
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
