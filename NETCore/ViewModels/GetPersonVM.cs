using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.ViewModels
{
    public class GetPersonVM
    {
        public string NIK { get; set; }
     //   public string FirstName { get; set; }
     //   public string LastName { get; set; }
        public string FullName { get; set; }
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
        public string Email { get; set; }
        public string Password { get; set; }
        public int UniversityId { get; set; }
   //     public int RoleId { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
    }
}
