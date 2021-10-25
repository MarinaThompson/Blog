using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace portfólio.ModelViews
{
    public class RegisterViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; } = "";

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Photo { get; set; } = "";
    }
}
