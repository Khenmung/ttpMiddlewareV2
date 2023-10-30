using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class SchoolClassPeriod
    {
        public SchoolClassPeriod()
        {
            SchoolTimeTables = new HashSet<SchoolTimeTable>();
            TeacherPeriods = new HashSet<TeacherPeriod>();
        }

        [Key]
        public int SchoolClassPeriodId { get; set; }
        public int ClassId { get; set; }
        public int PeriodId { get; set; }
        [Required]
        [StringLength(13)]
        public string FromToTime { get; set; }
        public int PeriodTypeId { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        public byte Sequence { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }
        public int SectionId { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.SchoolClassPeriods))]
        public virtual ClassMaster Class { get; set; }
        [InverseProperty(nameof(SchoolTimeTable.SchoolClassPeriod))]
        public virtual ICollection<SchoolTimeTable> SchoolTimeTables { get; set; }
        [InverseProperty(nameof(TeacherPeriod.SchoolClassPeriod))]
        public virtual ICollection<TeacherPeriod> TeacherPeriods { get; set; }
    }
}
