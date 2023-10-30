using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("OnlineEvaluation")]
    public class RawOnlineEvaluation
    {
        [Key]
        public short OnlineEvaluationId { get; set; }
        public int? ClassId { get; set; }
        public int? SubjectId { get; set; }
        [StringLength(250)]
        public string Topic { get; set; }
        public int? EvaluationTypeId { get; set; }
        public bool? Active { get; set; }
        public short? OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }

        //[ForeignKey(nameof(ClassId))]
        //[InverseProperty(nameof(ClassSubject.OnlineEvaluations))]
        //public class RawClassSubject Class { get; set; }
        //[ForeignKey(nameof(OrgId))]
        //[InverseProperty(nameof(Organization.OnlineEvaluations))]
        //public class RawOrganization Org { get; set; }
    }
}
