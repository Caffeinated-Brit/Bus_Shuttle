using System.ComponentModel.DataAnnotations;

namespace Bus_Shuttle.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    public bool IsManager { get; set; }
    
    public bool IsDriver { get; set; }
    
}