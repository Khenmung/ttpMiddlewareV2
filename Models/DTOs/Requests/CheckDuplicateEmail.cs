using System.ComponentModel.DataAnnotations;

namespace ttpMiddleware.Models.DTOs.Requests
{
    public class CheckEmailDuplicate
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Id { get; set; }
    }
}