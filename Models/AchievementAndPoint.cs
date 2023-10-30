using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class AchievementAndPoint
    {
        public AchievementAndPoint()
        {
            SportResults = new HashSet<SportResult>();
        }

        [Key]
        public int AchievementAndPointId { get; set; }
        [Required]
        [StringLength(30)]
        public string Rank { get; set; }
        public int CategoryId { get; set; }
        public short Points { get; set; }
        public bool Active { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }

        [InverseProperty(nameof(SportResult.Rank))]
        public virtual ICollection<SportResult> SportResults { get; set; }
    }
}
