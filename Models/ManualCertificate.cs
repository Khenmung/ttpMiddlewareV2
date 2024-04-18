﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ManualCertificate")]
    public partial class ManualCertificate
    {
        [Key]
        public int ManualCertificateId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(30)]
        public string Batch { get; set; }
        [StringLength(20)]
        public string PassingYear { get; set; }
        [StringLength(50)]
        public string FatherName { get; set; }
        [StringLength(50)]
        public string MotherName { get; set; }
        [StringLength(30)]
        public string ExamName { get; set; }
        [StringLength(10)]
        public string Rank { get; set; }
        [StringLength(20)]
        public string Division { get; set; }
        [StringLength(15)]
        public string RollNo { get; set; }
        [StringLength(15)]
        public string Percentage { get; set; }
        [StringLength(30)]
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BOD { get; set; }
        public short? OrgId { get; set; }
        public int? SubOrgId { get; set; }
        public bool? Deleted { get; set; }
        public bool? Active { get; set; }
        public bool? Issued { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public Guid? SyncId { get; set; }
    }
}
