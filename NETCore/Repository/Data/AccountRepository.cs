using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace NETCore.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        //show list of register account 
        public IEnumerable<LoginVM> DataLogin()
        {
            var dataAccount =(from p in myContext.Persons
                            join a in myContext.Accounts on p.NIK equals a.NIK
                            join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                            join r in myContext.Roles on ar.RoleId equals r.RoleId
                            select new LoginVM
                            {
                                NIK = p.NIK,
                                Email = p.Email,
                                Password = a.Password,
                               // RoleName = r.RoleName,
                                Phone = p.Phone
                            }).ToList();
           if (dataAccount.Count == 0)
                return null;
           return dataAccount.ToList();        
        }

        public string[] GetRoles(string NIK)
        {
            
            var getRoles = (from a in myContext.Accounts
                           join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                           join r in myContext.Roles on ar.RoleId equals r.RoleId
                           where NIK == ar.NIK
                           select new Role
                           {
                               RoleName = r.RoleName
                           }).ToList();
            string[] GetRoles = new string[getRoles.Count];
            for (int i = 0; i < getRoles.Count; i++)
            {
                GetRoles[i] = getRoles[i].RoleName;
            }
            //string[] getRoles = Array.ConvertAll((getRole), Convert.ToString);

            return GetRoles;
        }

        public string GetRole(string NIK)
        {
            string roles = "";
            var getRole = (from a in myContext.Accounts
                           join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                           join r in myContext.Roles on ar.RoleId equals r.RoleId
                           where NIK == ar.NIK
                           select new Role
                           {
                               RoleName = r.RoleName
                           }).ToArray();

            for (int i = 0; i < getRole.Length; i++)
            {
                  roles += getRole[i].RoleName;
               if (i<getRole.Length - 1)
                {
                    roles += ", ";
                }
            }
            return roles;
        }

        public string CheckLoginEmail(string Email)
        {
            var checkLoginemail = (from p in myContext.Persons where p.Email == Email select new LoginVM { NIK = p.NIK }).ToList();
            if (checkLoginemail.Count == 0)
                return null;
            return checkLoginemail[0].NIK;
        }

        public bool CheckLoginPassword(string NIK, string password)
        {
            var checkLoginemail = (from p in myContext.Persons
                                   join a in myContext.Accounts on p.NIK equals a.NIK
                                   where p.NIK == NIK
                                   select new LoginVM
                                   {
                                       NIK = p.NIK,
                                       Password = a.Password
                                   }).ToList();
            if (BCrypt.Net.BCrypt.Verify(password, checkLoginemail[0].Password))
                return true;           
            return false;
        }

        public bool CheckLoginPhone(string NIK, string phone)
        {
            var checkLoginPhone = (from p in myContext.Persons
                                   where p.NIK == NIK 
                                   select new LoginVM
                                   {
                                       NIK = p.NIK,
                                       Phone = p.Phone
                                   }).ToList();
            if (checkLoginPhone.Count() != 0)
                return true;            
            return false;
        }

        public bool CheckPhone(string NIK, string Phone)
        {
            if (myContext.Persons.Where(p => p.Phone == Phone && p.NIK == NIK).Count() != 0)
            {
                return true;
            }
            return false;
        }

        //generate reset password
        public string GeneratePass()
        {
            Guid guid = Guid.NewGuid();
            string newPass = guid.ToString();
            return newPass;
        }

        public void SendEmail(string Email, string newPass)
        {
            try
            {
                string timeStamp = DateTime.Now.ToString("g");
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("febyaniwaranti@gmail.com");
                message.To.Add(new MailAddress($"{Email}"));
                message.Subject = $"RESET PASSWORD - NETCore / {timeStamp}";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = $"Reset Password NETCore berhasil. Silahkan login dengan email : {Email} | password : {newPass}";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("febyaniwaranti@gmail.com", "febryanisriwaranti12");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e) 
            {
               //e.Message;
            }
        }

        internal void UpdateAccount(string password)
        {
           // throw new NotImplementedException();
        }
    }
}
