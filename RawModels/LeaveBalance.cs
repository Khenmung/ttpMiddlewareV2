﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawLeaveBalance
    {
        [Key]
        public int LeaveBalanceId { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int LeavePolicyId { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal OB { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal Adjusted { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal CB { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

     
    }
}
