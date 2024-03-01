using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("SportResult")]
    public partial class SportResult
    {
        public SportResult()
        {
            GroupActivityParticipants = new HashSet<GroupActivityParticipant>();
        }

        [Key]
        public int SportResultId { get; set; }
        [Required]
        [StringLength(1000)]
        public string Achievement { get; set; }
        public int RankId { get; set; }
        public int SportsNameId { get; set; }
        public int? GroupId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? StudentClassId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int? EmployeeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AchievementDate { get; set; }
        public int SessionId { get; set; }
        public short? BatchId { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(RankId))]
        [InverseProperty(nameof(AchievementAndPoint.SportResults))]
        public virtual AchievementAndPoint Rank { get; set; }
        [InverseProperty(nameof(GroupActivityParticipant.SportResult))]
        public virtual ICollection<GroupActivityParticipant> GroupActivityParticipants { get; set; }
    }
}
