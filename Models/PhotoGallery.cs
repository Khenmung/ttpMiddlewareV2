using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("PhotoGallery")]
    public partial class PhotoGallery
    {
        [Key]
        public int PhotoId { get; set; }
        [Required]
        [StringLength(250)]
        public string PhotoPath { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UploadDate { get; set; }
        public byte? Active { get; set; }
        public short? AlbumId { get; set; }
        public byte? PhotoOrFile { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty("PhotoGalleries")]
        public virtual Album Album { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.PhotoGalleries))]
        public virtual Organization Org { get; set; }
    }
}
