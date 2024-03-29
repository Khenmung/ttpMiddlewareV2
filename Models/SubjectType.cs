﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), Name = "subjecttypeindx")]
    public partial class SubjectType
    {
        public SubjectType()
        {
            ClassSubjects = new HashSet<ClassSubject>();
        }

        [Key]
        public short SubjectTypeId { get; set; }
        [Required]
        [StringLength(100)]
        public string SubjectTypeName { get; set; }
        public short SelectHowMany { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [InverseProperty(nameof(ClassSubject.SubjectType))]
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
    }
}
