namespace MyCompany.Visitors.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Visitors.Data.Repositories;
    using MyCompany.Visitors.Model;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class TeamRepositoryTests
    {
        [TestMethod]
        public async Task TeamRepository_GetTeam_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int teamId = context.Teams.FirstOrDefault().TeamId;

            var target = new TeamRepository(context);
            var result = await target.GetAsync(teamId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TeamId == teamId);
        }

        [TestMethod]
        public async Task TeamRepository_GetAllTeams_Call_GetResults_Test()
        {
            var context = new MyCompanyContext();
            int expectedCount = context.Teams.Count();

            var target = new TeamRepository(context);
            var results = await target.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public async Task TeamRepository_AddTeam_Added_NotFail_Test()
        {
            var context = new MyCompanyContext();
            var employeeId = context.Employees
                .FirstOrDefault()
                .EmployeeId;

            int expected = context.Teams.Count() + 1;


            var target = new TeamRepository(context);
            
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
            var target = new TeamRepository(context);

            var context2 = new MyCompanyContext();
            team.ManagerId = context2.Employees.FirstOrDefault(e => e.EmployeeId != team.ManagerId).EmployeeId; ;
            await target.UpdateAsync(team);

            var actual = await target.GetAsync(team.TeamId);

            Assert.AreEqual(team.ManagerId, actual.ManagerId);
        }

        [TestMethod]
        public async Task TeamRepository_DeleteTeam_Deleted_NotFail_Test()
        {
            var context = new MyCompanyContext();
            ITeamRepository target = new TeamRepository(context);

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

            ITeamRepository target = new TeamRepository(context);
            await target.DeleteAsync(-1);

            int actual = context.Teams.Count();

            Assert.AreEqual(expected, actual);
        }

    }
}
