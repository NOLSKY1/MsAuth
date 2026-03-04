using System.ComponentModel.DataAnnotations;

namespace Ms_Auth.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password" ,ErrorMessage ="Passwords dont match" )]
        public string Passwordconfirmation {  get; set; }
    }
}
