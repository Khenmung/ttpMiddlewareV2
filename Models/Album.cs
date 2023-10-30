using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class Album
    {
        public Album()
        {
            PhotoGalleries = new HashSet<PhotoGallery>();
        }

        [Key]
        public short AlbumId { get; set; }
        [StringLength(250)]
        public string AlbumName { get; set; }
        [StringLength(250)]
        public string UpdatableName { get; set; }
        public byte? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        [InverseProperty(nameof(PhotoGallery.Album))]
        public virtual ICollection<PhotoGallery> PhotoGalleries { get; set; }
    }
}
