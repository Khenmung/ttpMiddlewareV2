using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("SchoolTimeTable")]
    public partial class SchoolTimeTable
    {
        [Key]
        public int TimeTableId { get; set; }
        public int DayId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SchoolClassPeriodId { get; set; }
        public int? TeacherSubjectId { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("SchoolTimeTables")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.SchoolTimeTables))]
        public virtual ClassMaster Class { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.SchoolTimeTables))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(SchoolClassPeriodId))]
        [InverseProperty("SchoolTimeTables")]
        public virtual SchoolClassPeriod SchoolClassPeriod { get; set; }
        [ForeignKey(nameof(TeacherSubjectId))]
        [InverseProperty("SchoolTimeTables")]
        public virtual TeacherSubject TeacherSubject { get; set; }
    }
}
