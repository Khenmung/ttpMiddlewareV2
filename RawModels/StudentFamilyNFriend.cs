using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentFamilyNFriend")]
    public class RawStudentFamilyNFriend
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

       
    }
}
