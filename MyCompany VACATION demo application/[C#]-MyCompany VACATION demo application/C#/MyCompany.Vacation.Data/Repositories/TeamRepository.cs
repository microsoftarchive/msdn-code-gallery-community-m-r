
namespace MyCompany.Vacation.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Vacation.Model;
    using System.Data.Entity;
    using System;

    /// <summary>
    /// The team repository implementation
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="context">the context dependency</param>
        public TeamRepository(MyCompanyContext context)
        {
            if (context == null) 
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/>
        /// </summary>
        /// <param name="teamId"><see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/></returns>
        public Team Get(int teamId)
        {
            return _context.Teams.SingleOrDefault(q => q.TeamId == teamId);
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/></returns>
        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.ToList();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/>
        /// </summary>
        /// <param name="team"><see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/></param>
        /// <returns><see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/></returns>
        public int Add(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team.TeamId;
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/>
        /// </summary>
        /// <param name="team"><see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/></param>
        public void Update(Team team)
        {
            _context.Entry<Team>(team)
                    .State = EntityState.Modified;

            _context.SaveChanges();
        }

        /// <summary>
        /// <see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/>
        /// </summary>
        /// <param name="teamId"><see cref="MyCompany.Vacation.Data.Repositories.ITeamRepository"/></param>
        public void Delete(int teamId)
        {
            var team = _context.Teams
                .Find(teamId);
                
            if (team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose all resource
        /// </summary>
        /// <param name="disposing">Dispose managed resources check</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
