using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("PhotoGallery")]
    public class RawPhotoGallery
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

       
    }
}
