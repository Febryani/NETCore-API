using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_tr_account_roles")]
    public class AccountRole
    {
        [Key]
        public int AccountRoleId { get; set; }
        
        [ForeignKey("NIK")]
        public string NIK { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
