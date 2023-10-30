using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawEmployeeGeneratedCertificate
    {
        [Key]
        public int EmployeeGeneratedCertificateId { get; set; }
        public int EmployeeId { get; set; }
        public int? GroupId { get; set; }
        public int? ActivityId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? SessionId { get; set; }
        public int CertificateTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IssuedDate { get; set; }
        public short? OrgId { get; set; }
        public bool? Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? Deleted { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
