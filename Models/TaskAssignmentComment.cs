using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class TaskAssignmentComment
    {
        [Key]
        public int AssignmentCommentId { get; set; }
        public int TaskAssignmentId { get; set; }
        [Required]
        [StringLength(256)]
        public string Comments { get; set; }
        public short CommentedBy { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.TaskAssignmentComments))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(TaskAssignmentId))]
        [InverseProperty("TaskAssignmentComments")]
        public virtual TaskAssignment TaskAssignment { get; set; }
    }
}
