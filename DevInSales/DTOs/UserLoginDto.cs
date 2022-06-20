using System.ComponentModel.DataAnnotations;

namespace DEVinSalesTest.DTOs
{
    public class UserLoginDto
    {       
        
        [Display(Name = "email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
