using System.ComponentModel.DataAnnotations;

namespace IsProje.Models.ViewModels
{
    public class LoginModel
    {
        [Required] 
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }

    }
}   
