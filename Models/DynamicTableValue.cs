using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("DynamicTableValue")]
    public partial class DynamicTableValue
    {
        [Key]
        public int DynamicTableValueId { get; set; }
        public int? DynamicTableId { get; set; }
        public int? ValueReferenceId { get; set; }
        [StringLength(1000)]
        public string Value { get; set; }
        public short? OrgId { get; set; }
        public bool? Deleted { get; set; }
        public byte Active { get; set; }
        public short ApplicationId { get; set; }
        public int SubOrgId { get; set; }
    }
}
