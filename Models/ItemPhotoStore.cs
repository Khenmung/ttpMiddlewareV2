using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ItemPhotoStore")]
    public partial class ItemPhotoStore
    {
        [Key]
        public int ItemStoreId { get; set; }
        public int ItemId { get; set; }
        [Required]
        [StringLength(50)]
        public string PhotoName { get; set; }
        [Required]
        [StringLength(150)]
        public string PhotoPath { get; set; }
        [Required]
        [StringLength(50)]
        public string QrcodeName { get; set; }
        [Required]
        [StringLength(150)]
        public string QrCodePath { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }
    }
}
