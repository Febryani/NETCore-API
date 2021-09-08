using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_persons")]
    public class Person
    {
        [Key] //anotasi primari key
        public string NIK { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public int Salary { get; set; }

        public string Email { get; set; }

        public enum Gender 
        {
            Male,
            Female
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
    }

}
