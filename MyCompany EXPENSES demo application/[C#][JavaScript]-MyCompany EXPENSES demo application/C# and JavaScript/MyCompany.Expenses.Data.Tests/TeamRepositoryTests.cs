
namespace MyCompany.Expenses.Data.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;

    [TestClass]
    public class TeamRepositoryTests
    {
        ITeamRepository target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = new TeamRepository(new MyCompanyContext());
        }

        [TestMethod]
        public async Task TeamRepository_GetTeam_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int teamId = context.Teams.FirstOrDefault().TeamId;

            var result = await target.GetAsync(teamId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TeamId == teamId);
        }

        [TestMethod]
        public async Task TeamRepository_GetAllTeams_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Teams.Count();

            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task TeamRepository_AddTeam_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Teams.Count() + 1;

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var teamId = context.Teams.Select(e => e.TeamId).Max() + 1;
            var Team = new Team()
            {
                TeamId = teamId,
                ManagerId = employeeId, 
            };
            await target.AddAsync(Team);

            int actual = context.Teams.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TeamRepository_UpdateTeam_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var team = context.Teams.FirstOrDefault();

            team.ManagerId = context.Employees.FirstOrDefault(e => e.EmployeeId != team.ManagerId).EmployeeId;;
            await target.UpdateAsync(team);

            var actual = await target.GetAsync(team.TeamId);

            Assert.AreEqual(team.ManagerId, actual.ManagerId);
        }

        [TestMethod]
        public async Task TeamRepository_DeleteTeam_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();

            var employeeId = context.Employees.FirstOrDefault().EmployeeId;
            var teamId = context.Teams.Select(e => e.TeamId).Max() + 1;
            var newTeam = new Team()
            {
                TeamId = teamId,
                ManagerId = employeeId,
            };

            await target.AddAsync(newTeam);

            int expected = context.Teams.Count() - 1;

            await target.DeleteAsync(teamId);

            int actual = context.Teams.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TeamRepository_DeleteTeam_NoExists_NotFail_Test()
        {
            var context = new MyCompanyContext();
            int expected = context.Teams.Count();

            await target.DeleteAsync(-1);

            int actual = context.Teams.Count();

            Assert.AreEqual(expected, actual);
        }
    }
}