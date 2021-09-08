using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.ViewModels
{
    public class LoginVM
    {
        public string NIK { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong")]
        public string Email { get; set; }

      //  [Required(ErrorMessage = "Password tidak boleh kosong")]
        public string Password { get; set; }
        public string Phone { get; set; }

        //public string RoleName { get; set; }

        //[Required(ErrorMessage = "Password tidak boleh kosong")]
        //[MinLength(8, ErrorMessage = "Password minimal terdiri dari 8 karakter")]
        //public string NewPassword { get; set; }

        //[Required(ErrorMessage = "Password tidak boleh kosong")]
        //[MinLength(8, ErrorMessage = "Password minimal terdiri dari 8 karakter")]
        //public string ConfirmPassword { get; set; }
    }
}
