using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentStatus")]
    public partial class StudentStatus
    {
        [Key]
        public int StudentStatusId { get; set; }
        public int StudentClassId { get; set; }
        public int StatusId { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public Int32 BatchId { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public int ClassId { get; set; }

        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("StudentStatuses")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
