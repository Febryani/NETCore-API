using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository
{
    public class OldPersonRepository : IOldPersonRepository
    {
        private readonly MyContext myContext;
        public OldPersonRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(string NIK)
        {
            //  throw new NotImplementedException();
            var wantDelete = myContext.Persons.Find(NIK);
            if(wantDelete.NIK == null)
            {
                throw new ArgumentNullException();
            }
            myContext.Persons.Remove(wantDelete);
            var deleted = myContext.SaveChanges();
            return deleted;
        }

        public IEnumerable<Person> Get()
        {
            // throw new NotImplementedException();
            return myContext.Persons.ToList();
        }

        public Person Get(string NIK)
        {
            //throw new NotImplementedException();
            return myContext.Persons.Find(NIK);
        }

        public int Insert(Person person)
        {
            var insert = 0;
            myContext.Persons.Add(person);
            if (person.NIK != "")
            {
                insert = myContext.SaveChanges();

            }
            return insert;
        }

        public int Update(Person person)
        {
            // throw new NotImplementedException();
            myContext.Entry(person).State = EntityState.Modified;
            var update = myContext.SaveChanges();
            return update;
        }
    }
}
