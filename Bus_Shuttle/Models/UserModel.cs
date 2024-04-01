using System.ComponentModel.DataAnnotations;

namespace Bus_Shuttle.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool IsManager { get; set; }
        public bool IsDriver { get; set; }
        public bool IsAuthorizedDriver { get; set; }
    }
}