using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("OrganizationStatus")]
    public class RawOrganizationStatus
    {
        [Key]
        public short OrganizationStatusId { get; set; }
        public int? OrgStatusNameId { get; set; }
        public int? OrgStatusId { get; set; }
        public byte? Active { get; set; }
        public short? OrgId { get; set; }
    }
}
