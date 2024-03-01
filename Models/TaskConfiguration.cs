using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("TaskConfiguration")]
    public partial class TaskConfiguration
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string TaskName { get; set; }
        [StringLength(250)]
        public string DBConnection { get; set; }
        [StringLength(50)]
        public string TableName { get; set; }
        [StringLength(250)]
        public string ColNameNValue { get; set; }
        public short? Action { get; set; }
        public byte? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [StringLength(250)]
        public string UserEmailForAlert { get; set; }
        [StringLength(500)]
        public string AlertMessage { get; set; }
        public short? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastRun { get; set; }
        public short? ApplicationId { get; set; }
        public short? OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.TaskConfigurations))]
        public virtual Organization Org { get; set; }
    }
}
