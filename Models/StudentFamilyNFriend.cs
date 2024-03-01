using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentFamilyNFriend")]
    public partial class StudentFamilyNFriend
    {
        [Key]
        public int StudentFamilyNFriendId { get; set; }
        public int? StudentId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int ParentStudentId { get; set; }
        [StringLength(20)]
        public string ContactNo { get; set; }
        public int? RelationshipId { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.StudentFamilyNFriends))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("StudentFamilyNFriends")]
        public virtual Student Student { get; set; }
    }
}
