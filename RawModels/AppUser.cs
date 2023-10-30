using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawAppUser
    {
        [Key]
        public string ApplicationUserId { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [StringLength(20)]
        public string ContactNo { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public short? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public short? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidTo { get; set; }
        public byte? Active { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short? ManagerId { get; set; }
        public short? OrgId { get; set; }
        public short? DepartmentId { get; set; }
        public short? LocationId { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
