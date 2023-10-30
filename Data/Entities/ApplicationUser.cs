using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ttpMiddleware.Models;

namespace ttpMiddleware.Data.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName{ get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int OrgId { get; set; }
        public int SubOrgId { get; set; }
        public int Active { get; set; }
        [Required]
        public int UserTypeId { get; set; }

    }
}
