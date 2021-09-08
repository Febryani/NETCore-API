using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository.Data
{
    public class PersonRepository : GeneralRepository<MyContext, Person, string>
    {
        private readonly MyContext myContext;

        public PersonRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<GetPersonVM> GetPersonVMs()
        {
            var getPersonVMs = (from p in myContext.Persons
                                join a in myContext.Accounts on p.NIK equals a.NIK
                              //  join r in myContext.Roles on a.RoleId equals r.RoleId
                                join pr in myContext.Profilings on p.NIK equals pr.NIK
                                join e in myContext.Educations on pr.EducationId equals e.EducationId
                                select new GetPersonVM
                                {
                                    NIK = p.NIK,
                                    FullName = p.Firstname + " " + p.Lastname,
                                    PhoneNumber = p.Phone,
                                    BirthDate = p.BirthDate,
                                    gender = (GetPersonVM.Gender)p.gender,
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Password = "*********",
                                    UniversityId = e.UniversityId,
                               //     RoleId = a.RoleId,
                                    Degree = e.Degree,
                                    GPA = e.GPA
                                }).ToList();

            if (getPersonVMs.Count == 0)
            {
                return null;
            }
            return getPersonVMs.ToList();
        }

        public IEnumerable<GetPersonVM> GetPersonVMs(string NIK)
        {
            if (myContext.Persons.Find(NIK) == null)
            {
                return null;
            }
            else
            {
                return (from p in myContext.Persons
                        join a in myContext.Accounts on p.NIK equals a.NIK
                    //    join r in myContext.Roles on a.RoleId equals r.RoleId
                        join pr in myContext.Profilings on a.NIK equals pr.NIK
                        join e in myContext.Educations on pr.EducationId equals e.EducationId
                       // where NIK == p.NIK
                        select new GetPersonVM
                        {
                            NIK = p.NIK,
                            FullName = p.Firstname + " " + p.Lastname,
                            PhoneNumber = p.Phone,
                            BirthDate = p.BirthDate,
                            Salary = p.Salary,
                            Email = p.Email,
                            gender = (GetPersonVM.Gender)p.gender,
                            Password = "*********",
                            UniversityId = e.UniversityId,
                       //     RoleId = a.RoleId,
                            Degree = e.Degree,
                            GPA = e.GPA
                        }).Where(p => p.NIK == NIK);
            }
        }   
            

        public int InsertPerson(InsertPersonVM insertPersonVM)
        {
            
            var insert = myContext.SaveChanges();
            Person person = new Person();
            person.NIK = insertPersonVM.NIK;
           // string[] name = getPersonVM.FullName.Split(' ');
            person.Firstname = insertPersonVM.FirstName;
            person.Lastname = insertPersonVM.LastName;
            person.Phone = insertPersonVM.PhoneNumber;
            person.BirthDate = insertPersonVM.BirthDate;
            person.Salary = insertPersonVM.Salary;
            person.Email = insertPersonVM.Email;
            person.gender = (Person.Gender)insertPersonVM.gender;
            myContext.Persons.Add(person);
            myContext.SaveChanges();

            Account account = new Account();
            account.NIK = insertPersonVM.NIK;
            string saltPassword = BCrypt.Net.BCrypt.GenerateSalt(12);
            account.Password = BCrypt.Net.BCrypt.HashPassword(insertPersonVM.Password, saltPassword);
          //  account.RoleId = insertPersonVM.RoleId;
            myContext.Accounts.Add(account);
            myContext.SaveChanges();

            Education education = new Education();
            education.Degree = insertPersonVM.Degree;
            education.GPA = insertPersonVM.GPA;
            education.UniversityId = insertPersonVM.UniversityId;
            myContext.Educations.Add(education);
            myContext.SaveChanges();

            AccountRole accountRole = new AccountRole();
            accountRole.NIK = insertPersonVM.NIK;
            accountRole.RoleId = 2;
            myContext.AccountRoles.Add(accountRole);
            myContext.SaveChanges();

            Profiling profiling = new Profiling();
            profiling.NIK = insertPersonVM.NIK;
            profiling.EducationId = education.EducationId;
            myContext.Profilings.Add(profiling);
            myContext.SaveChanges();

            return insert;
        }
        public string Checking(string NIK, string Email, string Phone)
        {
            if (myContext.Persons.Where(p => p.NIK == NIK).Count() != 0)
            {
                return "Gagal registrasi! NIK sudah terdaftar";
            }
            else if (myContext.Persons.Where(p => p.Email == Email).Count() != 0)
            {
                return "Gagal registrasi! Email sudah digunakan";
            }
            else if (myContext.Persons.Where(p => p.Phone == Phone).Count() != 0)
            {
                return "Gagal registrasi! Phone Number sudah digunakan";
            }

            return "successful";
        }
    }
}
