using DataAnnotationsExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.ViewModels
{
    public class InsertPersonVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }
        public int Salary { get; set; }

        [Email(ErrorMessage = "Email yang Anda masukan tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password tidak boleh kosong")]
        [MinLength(8, ErrorMessage = "Password minimal terdiri dari 8 karakter")]
        public string Password { get; set; }
        public int UniversityId { get; set; }
        public int RoleId { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
    }
}
