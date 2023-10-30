using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Keyless]
    [Table("AssetVendor")]
    public partial class AssetVendor
    {
        public int? AssetVendorId { get; set; }
        [StringLength(50)]
        public string VendorName { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(10)]
        public string zipcode { get; set; }
        public int? State { get; set; }
        public int? CountryId { get; set; }
        [StringLength(15)]
        public string GSTNumber { get; set; }
        [StringLength(30)]
        public string RegistrationNo { get; set; }
        [StringLength(30)]
        public string ContactPerson { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(21)]
        public string ContactNo { get; set; }
        public bool? Active { get; set; }
        public short? OrgId { get; set; }
        public bool? Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
    }
}
