using System;
using System.Collections.Generic;
using MyCompany.Visitors.Client;
using MyCompany.Visitors.Client.DocumentResponse;

namespace MyCompany.Visitors.Client.WindowsStore.Services.SampleData
{

    /// <summary>
    /// Sample data contract implementations.
    /// </summary>
    public class SampleDataService : ISampleDataService
    {
        /// <summary>
        /// Get sample visits.
        /// </summary>
        /// <returns>Visits list</returns>
        public List<Visit> GetVisits(int n)
        {
            List<Visit> visits = new List<Visit>();

            for (int i = 0; i < n; i++)
            {
                visits.Add(new Visit
                               {
                                   Comments = "Visit comments",
                                   Visitor  = new Visitor
                                                  {
                                                      VisitorId = i,
                                                      Company = "MyCompany",
                                                      FirstName = "David",
                                                      LastName = "Hamilton",
                                                      CreatedDateTime = DateTime.Now,
                                                      LastModifiedDateTime = DateTime.Now,
                                                      PersonalId = "123456789A",
                                                      Email = "email@domain.com",
                                                      LastVisit = null,
                                                      Position = "Position",
                                                      VisitorPictures = null,
                                                      Visits = null
                                                  },
                                    Employee = new Employee
                                                   {
                                                       EmployeeId = i,
                                                       FirstName = "Adam",
                                                       LastName = "Barr",
                                                       JobTitle = "Developer",
                                                       Email = "email@domain.com",
                                                       EmployeePictures = null,
                                                       ManagedTeams = null,
                                                       Team = null,
                                                       TeamId = i,
                                                       Visits = null
                                                   },
                                    VisitDateTime = DateTime.Now.AddHours(i),
                                    CreatedDateTime = DateTime.Now,
                                    EmployeeId = i,
                                    VisitorId = i,
                                    HasCar = false,
                                    VisitId = i,
                                    Status = VisitStatus.Pending,
                                    Plate = "XYZK69"
                               });
            }
            return visits;
        }

        /// <summary>
        /// Get sample employees.
        /// </summary>
        /// <returns>Employees list</returns>
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee;
            for (int i = 0; i < 10; i++)
            {
                employee = new Employee
                {
                    EmployeeId = i,
                    Email = "aaaa@domain.com",
                    JobTitle = "Developer",
                    FirstName = "Adrew",
                    LastName = "Davis"
                };
                employees.Add(employee);
            }
            return employees;
        }

        /// <summary>
        /// Sample visitors.
        /// </summary>
        /// <returns>Visitors list</returns>
        public List<Visitor> GetVisitors()
        {
            List<Visitor> visitors = new List<Visitor>();
            Visitor visitor;
            for(int i = 0; i < 10; i++)
            {
                visitor = new Visitor
                              {
                                  VisitorId = i,
                                  FirstName = "Adam",
                                  LastName = "Barr",
                                  Company = "MyCompany",
                                  LastVisit = new Visit
                                                  {
                                                      VisitDateTime = DateTime.UtcNow
                                                  }
                              };
                visitors.Add(visitor);
            }
            return visitors;
        } 
    }
}
