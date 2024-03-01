using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StorageFnP")]
    [Index(nameof(StudentId), nameof(Deleted), Name = "NonClusteredIndex-20230904-200451")]
    public partial class StorageFnP
    {
        [Key]
        public int FileId { get; set; }
        [StringLength(250)]
        public string FileName { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(250)]
        public string UpdatedFileFolderName { get; set; }
        public int? ParentId { get; set; }
        public byte? FileOrFolder { get; set; }
        public byte? FileOrPhoto { get; set; }
        public byte? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UploadDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int? EmployeeId { get; set; }
        public int? StudentClassId { get; set; }
        public int? StudentId { get; set; }
        public int? DocTypeId { get; set; }
        public int? PageId { get; set; }
        public int? QuestionId { get; set; }
        [StringLength(250)]
        public string Parent { get; set; }
        public short? BatchId { get; set; }
        public short? OrgId { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int CategoryId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(DocTypeId))]
        [InverseProperty(nameof(MasterItem.StorageFnPs))]
        public virtual MasterItem DocType { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.StorageFnPs))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.StorageFnPs))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(QuestionId))]
        [InverseProperty(nameof(QuestionBank.StorageFnPs))]
        public virtual QuestionBank Question { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("StorageFnPs")]
        public virtual Student Student { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("StorageFnPs")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
