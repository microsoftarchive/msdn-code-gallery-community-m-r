
namespace MyCompany.Vacation.Data.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Data.Repositories;

    [TestClass]
    public class TeamRepositoryTests
    {
        [TestMethod]
        public void TeamRepository_GetTeam_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int teamId = context.Teams.FirstOrDefault().TeamId;

            var target = new TeamRepository(context);
            var result = target.Get(teamId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TeamId == teamId);
        }

        [TestMethod]
        public void TeamRepository_GetAllTeams_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Teams.Count();

            var target = new TeamRepository(context);
            var results = target.GetAll().ToList();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void TeamRepository_AddTeam_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Teams.Count() + 1;

            var target = new TeamRepository(context);
            var officeId = context.Offices.FirstOrDefault().OfficeId;
            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var teamId = context.Teams.Select(e => e.TeamId).Max() + 1;
            var Team = new Team()
            {
                TeamId = teamId,
                ManagerId = employeeId,
                OfficeId = officeId,
            };
            target.Add(Team);

            int actual = context.Teams.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TeamRepository_UpdateTeam_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var team = context.Teams.FirstOrDefault();
            var target = new TeamRepository(context);

            var context2 = new MyCompanyContext();
            team.ManagerId = context2.Employees.FirstOrDefault(e => e.EmployeeId != team.ManagerId).EmployeeId; ;
            target.Update(team);

            var actual = target.Get(team.TeamId);

            Assert.AreEqual(team.ManagerId, actual.ManagerId);
        }

        [TestMethod]
        public void TeamRepository_DeleteTeam_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            ITeamRepository target = new TeamRepository(context);
            
            var officeId = context.Offices.FirstOrDefault().OfficeId;
            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var teamId = context.Teams.Select(e => e.TeamId).Max() + 1;
            var newTeam = new Team()
            {
                TeamId = teamId,
                ManagerId = employeeId,
                OfficeId = officeId,
            };

            target.Add(newTeam);

            int expected = context.Teams.Count() - 1;


            target.Delete(teamId);

            int actual = context.Teams.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TeamRepository_DeleteTeam_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Teams.Count();

            ITeamRepository target = new TeamRepository(context);
            target.Delete(-1);

            int actual = context.Teams.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
