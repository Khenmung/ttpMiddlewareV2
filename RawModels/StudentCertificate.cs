using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentCertificate")]
    public class RawStudentCertificate
    {
        [Key]
        public int StudentCertificateId { get; set; }
        public short CertificateTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime IssuedDate { get; set; }
        public int StudentClassId { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

    }
}
