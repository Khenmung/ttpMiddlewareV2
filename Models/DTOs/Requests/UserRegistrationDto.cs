using System;
using System.ComponentModel.DataAnnotations;

namespace ttpMiddleware.Models.DTOs.Requests
{
    public class UserRegistrationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string OrganizationName { get; set; }
        public string ContactNo { get; set; }
        public string UserId { get; set; }

    }
    public class UserUpdate
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public DateTime ValidTo { get; set; }
        public short Active { get; set; }


    }
    public class ChangePassword
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
    public class ResetPassword
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }

    }
}
