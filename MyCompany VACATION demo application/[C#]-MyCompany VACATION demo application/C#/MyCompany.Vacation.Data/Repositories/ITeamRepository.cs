
namespace MyCompany.Vacation.Data.Repositories
{
    using System.Collections.Generic;
    using MyCompany.Vacation.Model;
    using System;

    /// <summary>
    /// Base contract for team repository
    /// </summary>
    public interface ITeamRepository
        : IDisposable
    {
        /// <summary>
        /// Get team by Id
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Team Get(int teamId);

        /// <summary>
        /// Get All teams
        /// </summary>
        /// <returns>List of teams</returns>
        IEnumerable<Team> GetAll();

        /// <summary>
        /// Add new team
        /// </summary>
        /// <param name="team">team information</param>
        /// <returns>teamId</returns>
        int Add(Team team);

        /// <summary>
        /// Update team
        /// </summary>
        /// <param name="team">team information</param>
        void Update(Team team);

        /// <summary>
        /// Delete team
        /// </summary>
        /// <param name="teamId">team to delete</param>
        void Delete(int teamId);
    }
}
