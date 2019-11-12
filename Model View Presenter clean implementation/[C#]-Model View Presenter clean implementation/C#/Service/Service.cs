using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EModel;

namespace Service
{
    public static class AdministrationService
    {
        static List<Person> persons = new List<Person>();

        static AdministrationService()
        {
            persons.Add(new Person() { Name = "Bhawesh", Age = "33", Dept = "IT" });
            persons.Add(new Person() { Name = "Jagadeesan", Age = "32", Dept = "HR" });
            persons.Add(new Person() { Name = "Nishant", Age = "30", Dept = "Admin" });
            persons.Add(new Person() { Name = "Ravikant", Age = "19", Dept = "IT" });
            persons.Add(new Person() { Name = "Pankaj", Age = "22", Dept = "IT Infra" });
            persons.Add(new Person() { Name = "Raj", Age = "34", Dept = "PMG" });
            persons.Add(new Person() { Name = "Jay", Age = "37", Dept = "IT" });
            persons.Add(new Person() { Name = "Robert", Age = "35", Dept = "SQA" });
            persons.Add(new Person() { Name = "Prabha", Age = "31", Dept = "SQA" });
            persons.Add(new Person() { Name = "Brikesh", Age = "31", Dept = "SQA" });
        }


        /// <summary>
        /// Business logic call goes from here
        /// </summary>
        /// <param name="a_Name"></param>
        /// <param name="a_Age"></param>
        /// <param name="a_Dept"></param>
        /// <returns></returns>
        public static List<Person> GetPersons(string a_Name, string a_Age, string a_Dept)
        {
            IEnumerable<Person> lstPersons = new List<Person>();

            lstPersons = persons;

            if (!string.IsNullOrEmpty(a_Name))
                lstPersons = (from p in persons where (p.Name.ToLower().Contains(a_Name.ToLower())) select p);

            if (!string.IsNullOrEmpty(a_Age))
                lstPersons = lstPersons.Where(p => p.Age == a_Age);

            if(!string.IsNullOrEmpty(a_Dept))
                lstPersons = lstPersons.Where(p => p.Dept.ToLower().Contains(a_Dept.ToLower()));

            return lstPersons.ToList();
        }
    }
}
