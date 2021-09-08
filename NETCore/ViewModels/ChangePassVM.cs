using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.ViewModels
{
    public class ChangePassVM
    {
        public string NIK { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password tidak boleh kosong")]
        [MinLength(8, ErrorMessage = "Password minimal terdiri dari 8 karakter")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password tidak boleh kosong")]
        [MinLength(8, ErrorMessage = "Password minimal terdiri dari 8 karakter")]
        [Compare("NewPassword", ErrorMessage = "Password yang Anda masukan tidak sesuai")]
        public string ConfirmPassword { get; set; }
    }
}
