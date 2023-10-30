using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentCertificate")]
    public partial class StudentCertificate
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

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("StudentCertificates")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.StudentCertificates))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("StudentCertificates")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
