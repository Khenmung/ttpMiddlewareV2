using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class TaskAssignment
    {
        public TaskAssignment()
        {
            TaskAssignmentComments = new HashSet<TaskAssignmentComment>();
        }

        [Key]
        public int AssignmentId { get; set; }
        [Required]
        [StringLength(50)]
        public string AssignmentName { get; set; }
        [Required]
        [StringLength(2000)]
        public string Description { get; set; }
        public int? AssignedToClassId { get; set; }
        public int? AssignedToSectionId { get; set; }
        public int? AssignedToEmployeeId { get; set; }
        public int AssignedByEmployeeId { get; set; }
        public int AssignmentStatusId { get; set; }
        public int? ParentId { get; set; }
        public byte? Score { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AssignmentDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpectedCompletion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActualCompletion { get; set; }
        [StringLength(256)]
        public string Remarks { get; set; }
        public int BatchId { get; set; }
        public short OrgId { get; set; }
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

        [ForeignKey(nameof(AssignedByEmployeeId))]
        [InverseProperty(nameof(EmpEmployee.TaskAssignmentAssignedByEmployees))]
        public virtual EmpEmployee AssignedByEmployee { get; set; }
        [ForeignKey(nameof(AssignedToClassId))]
        [InverseProperty(nameof(StudentClass.TaskAssignments))]
        public virtual StudentClass AssignedToClass { get; set; }
        [ForeignKey(nameof(AssignedToEmployeeId))]
        [InverseProperty(nameof(EmpEmployee.TaskAssignmentAssignedToEmployees))]
        public virtual EmpEmployee AssignedToEmployee { get; set; }
        [ForeignKey(nameof(AssignmentStatusId))]
        [InverseProperty(nameof(MasterItem.TaskAssignments))]
        public virtual MasterItem AssignmentStatus { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.TaskAssignments))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(TaskAssignmentComment.TaskAssignment))]
        public virtual ICollection<TaskAssignmentComment> TaskAssignmentComments { get; set; }
    }
}
