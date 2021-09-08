using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key] //anotasi primari key
        public int EducationId { get; set; }

        public string Degree { get; set; }

        public string GPA { get; set; }

        [ForeignKey("UniversityId")]
        public int UniversityId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }

        [JsonIgnore]
        public virtual University University { get; set; }
    }
}
