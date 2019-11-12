using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MyCompany.Travel.Model.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void Employee_Manager_IsManager_Returns_True()
        {
            Employee employee = new Employee()
            {
                EmployeeId = 1,
                ManagedTeams = new List<Team>()
                {
                    new Team()
                }
            };

            Assert.IsTrue(employee.IsManager);
        }

        [TestMethod]
        public void Employee_NotManager_IsManager_Returns_False()
        {
            Employee employee = new Employee()
            {
                EmployeeId = 1,
                ManagedTeams = new List<Team>()
                {
                    
                }
            };

            Assert.IsFalse(employee.IsManager);

            employee = new Employee()
            {
                EmployeeId = 1,
                ManagedTeams = null
            };

            Assert.IsFalse(employee.IsManager);
        }
    }
}
