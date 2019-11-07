
namespace MyCompany.Expenses.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Expenses.Model;
    using System.Data.Entity;
    using System;
using System.Threading.Tasks;

    /// <summary>
    /// The expense repository implementation
    /// </summary>
    public class ExpenseTravelRepository : IExpenseTravelRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Creates a new instance of TeamRepository class
        /// </summary>
        /// <param name="context"></param>
        public ExpenseTravelRepository(MyCompanyContext context)
        {
            if ( context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;

        }
        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/>
        /// </summary>
        /// <param name="expenseId"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/></returns>
        public async Task<ExpenseTravel> GetAsync(int expenseId)
        {
            return await _context.ExpenseTravels.FindAsync(expenseId);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/></returns>
        public async Task<IEnumerable<ExpenseTravel>> GetAllAsync()
        {
                return await _context.ExpenseTravels.ToListAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/>
        /// </summary>
        /// <param name="expenseTravel"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/></returns>
        public async Task<int> AddAsync(ExpenseTravel expenseTravel)
        {
                _context.ExpenseTravels.Add(expenseTravel);
                await _context.SaveChangesAsync();
                return expenseTravel.ExpenseId;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/>
        /// </summary>
        /// <param name="expenseTravel"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/></param>
        public async Task UpdateAsync(ExpenseTravel expenseTravel)
        {
                _context.Entry<ExpenseTravel>(expenseTravel)
                    .State = EntityState.Modified;

                await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/>
        /// </summary>
        /// <param name="expenseId"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseTravelRepository"/></param>
        public async Task DeleteAsync(int expenseId)
        {
                var expenseTravel = _context.ExpenseTravels.Find(expenseId);
                if (expenseTravel != null)
                {
                    _context.ExpenseTravels.Remove(expenseTravel);
                    await _context.SaveChangesAsync();
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