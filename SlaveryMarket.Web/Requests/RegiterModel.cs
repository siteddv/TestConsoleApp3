using System.ComponentModel.DataAnnotations;

namespace SlaveryMarket.Web.Requests
{
    public class RegiterModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Password name is required")]
        public string Email { get; set; }
    }
}
