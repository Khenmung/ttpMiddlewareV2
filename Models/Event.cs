using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("Event")]
    public partial class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        [StringLength(100)]
        public string EventName { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventEndDate { get; set; }
        [Required]
        [StringLength(256)]
        public string Venue { get; set; }
        [StringLength(256)]
        public string Participants { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        public byte Active { get; set; }
        public byte? Broadcasted { get; set; }
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
        public int EventTypeId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("Events")]
        public virtual Batch Batch { get; set; }
    }
}
