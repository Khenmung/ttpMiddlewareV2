using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawAspNetRole
    {
        public RawAspNetRole()
        {
            
        }

        [Key]
        public string Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string NormalizedName { get; set; }
        [StringLength(2000)]
        public string ConcurrencyStamp { get; set; }
        public int SubOrgId { get; set; }

        
    }
}
