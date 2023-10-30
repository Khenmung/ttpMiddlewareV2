﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmployeeGroupActivityParticipant")]
    public partial class EmployeeGroupActivityParticipant
    {
        [Key]
        public int GroupActivityParticipantId { get; set; }
        public int EmployeeActivityId { get; set; }
        public int EmployeeId { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public short OrgId { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public short? BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(EmployeeActivityId))]
        [InverseProperty("EmployeeGroupActivityParticipants")]
        public virtual EmployeeActivity EmployeeActivity { get; set; }
    }
}
