using System.ComponentModel.DataAnnotations;

namespace Ms_Auth.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }    

    }
}
