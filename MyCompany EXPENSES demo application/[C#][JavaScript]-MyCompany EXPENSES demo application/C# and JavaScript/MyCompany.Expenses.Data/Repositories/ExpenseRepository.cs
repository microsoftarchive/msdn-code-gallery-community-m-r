
namespace MyCompany.Expenses.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.Model;
    using System.Data.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// The expense repository implementation
    /// </summary>
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly MyCompanyContext _context;
        private readonly MyCompany.Common.EventBus.IEventBus _eventBus = null;

        bool _eventBusEnabled = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="eventBusEnabled"></param>
        /// <param name="context">The EF context</param>
        public ExpenseRepository(MyCompany.Common.EventBus.IEventBus eventBus, bool eventBusEnabled, MyCompanyContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
            _eventBusEnabled = eventBusEnabled;
            _eventBus = eventBus;
        }


        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="expenseId"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<Expense> GetAsync(int expenseId)
        {
            return
                await _context.Expenses
                .Include(e => e.Employee)
                .SingleAsync(q => q.ExpenseId == expenseId);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="expenseId"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<Expense> GetAllUserInfoAsync(int expenseId, PictureType pictureType)
        {
            var result = await _context.Expenses
                    .Where(e => e.ExpenseId == expenseId)
                    .Select(e => new
                    {
                        Expense = e,
                        Employee = e.Employee,
                        EmployeePictures = e.Employee.EmployeePictures.Where(ep => ep.PictureType == pictureType)
                    })
                    .ToListAsync();


            return result.Select(e => BuildExpense(e.Expense, true))
            .FirstOrDefault();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses.ToListAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="userIdentity"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="expenseStatus"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<IEnumerable<Expense>> GetUserExpensesAsync(string userIdentity, int expenseStatus, int pageSize, int pageCount)
        {
            var results = await _context.Expenses
                    .Where(q => q.Employee.Email == userIdentity
                                &&
                                ((int)q.Status & expenseStatus) == (int)q.Status
                                )
                    .OrderByDescending(q => q.CreationDate)
                    .Skip(pageSize * pageCount)
                    .Take(pageSize)
                    .Select(e => new
                    {
                        Expense = e,
                        Employee = e.Employee,

                    })
                    .ToListAsync();


            return results.Select(e => BuildExpense(e.Expense, false)) // No Expense Picture!
            .ToList();
        }


        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="userIdentity"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="expenseStatus"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<int> GetUserCountAsync(string userIdentity, int expenseStatus)
        {
            return await _context.Expenses
                .Where(q => q.Employee.Email == userIdentity
                            &&
                            ((int)q.Status & expenseStatus) == (int)q.Status)
                .CountAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="expenseStatus"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<IEnumerable<Expense>> GetTeamExpensesAsync(string managerIdentity, int expenseStatus, PictureType pictureType, int pageSize, int pageCount)
        {
            var results = await _context.Expenses
                    .Where(e => e.Employee.Team.Manager.Email == managerIdentity
                                && ((int)e.Status & expenseStatus) == (int)e.Status
                     )
                    .OrderByDescending(q => q.CreationDate)
                    .Skip(pageSize * pageCount)
                    .Take(pageSize)
                    .Select(e => new
                    {
                        Expense = e,
                        Employee = e.Employee,
                        EmployeePictures = e.Employee.EmployeePictures.Where(ep => ep.PictureType == pictureType)
                    })
                    .ToListAsync();

            return results.Select(e => BuildExpense(e.Expense, false)) // No Expense Picture!
                .ToList();
        }


        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="expenseStatus"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<int> GetTeamCountAsync(string managerIdentity, int expenseStatus)
        {
            return await _context.Expenses
                    .Where(q => q.Employee.Team.Manager.Email == managerIdentity
                                &&
                                ((int)q.Status & expenseStatus) == (int)q.Status)
                    .CountAsync();
        }


        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="expense"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<int> AddAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            if (_eventBusEnabled)
            {
                var dto = Mapper.Map<ExpenseDTO>(expense);
                _eventBus.Publish<ExpenseDTO>(dto, ExpenseActions.AddExpense);
            }

            return expense.ExpenseId;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="expense"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        public async Task UpdateAsync(Expense expense)
        {
            _context.Entry<Expense>(expense)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();

            if (_eventBusEnabled)
            {
                var dto = Mapper.Map<ExpenseDTO>(expense);
                _eventBus.Publish<ExpenseDTO>(dto, ExpenseActions.UpdateExpense);
            }
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="expenseId"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        public async Task DeleteAsync(int expenseId)
        {
            var expense = _context.Expenses.Find(expenseId);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();

                if (_eventBusEnabled)
                    _eventBus.Publish<int>(expenseId, ExpenseActions.DeleteExpense);
            }
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<IEnumerable<ExpenseGrouped>> GetTeamExpensesByMemberAsync(string managerIdentity, PictureType pictureType)
        {
            var results = await _context.Employees
                    .Where(q => q.Team.Manager.Email == managerIdentity && q.Expenses.Any())
                    .Select(q => new ExpenseGrouped
                    {
                        EmployeeId = q.EmployeeId,
                        FirstName = q.FirstName,
                        LastName = q.LastName,
                        JobTitle = q.JobTitle,
                        Amount = _context.Expenses.Where(e => e.EmployeeId == q.EmployeeId).Select(e => e.Amount).Sum(),
                        Picture = _context.EmployeePictures
                            .Where(e => e.EmployeeId == q.EmployeeId && e.PictureType == pictureType).FirstOrDefault().Content,
                    })
                    .OrderByDescending(q => q.Amount)
                    .ToListAsync();

            return results;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<int> GetTeamExpensesByMemberCountAsync(string managerIdentity)
        {
            return await _context.Employees
                .Where(q => q.Team.Manager.Email == managerIdentity && q.Expenses.Any())
                .CountAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="expenseType"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public IEnumerable<ExpenseMonth> GetTeamExpensesByMonth(string managerIdentity, int expenseType)
        {
            var results = _context.Expenses
                .Where(q => q.Employee.Team.Manager.Email == managerIdentity
                        && ((int)q.ExpenseType & expenseType) == (int)q.ExpenseType
                    )
                .AsEnumerable()
                .GroupBy(q => new { q.CreationDate.Date.Year, q.CreationDate.Date.Month })
                .Select(q => new ExpenseMonth
                {
                    Date = new DateTime(q.Key.Year, q.Key.Month, 1),
                    Amount = q.Select(e => e.Amount).Sum()
                })
                .OrderByDescending(q => q.Date)
                .Take(12)
                .ToList();

            return results;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="expenseType"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public IEnumerable<ExpenseMonth> GetTeamMemberExpensesByMonth(int employeeId, int expenseType)
        {
            var results = _context.Expenses
                .Where(q => q.EmployeeId == employeeId
                            && ((int)q.ExpenseType & expenseType) == (int)q.ExpenseType
                    )
                .AsEnumerable()
                .GroupBy(q => new { q.CreationDate.Date.Year, q.CreationDate.Date.Month })
                .Select(q => new ExpenseMonth
                {
                    Date = new DateTime(q.Key.Year, q.Key.Month, 1),
                    Amount = q.Select(e => e.Amount).Sum()
                })
                .OrderByDescending(q => q.Date)
                .Take(12)
                .ToList();

            return results;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/>
        /// </summary>
        /// <param name="employeeId"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="month"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <param name="year"><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></param>
        /// <returns><see cref="MyCompany.Expenses.Data.Repositories.IExpenseRepository"/></returns>
        public async Task<IEnumerable<TeamMemberSummary>> GetTeamMemberSummaryExpensesAsync(int employeeId, int? month, int year)
        {
            var results = await _context.Expenses
                            .Where(q => q.EmployeeId == employeeId
                                    && q.CreationDate.Year == year
                                    && (!month.HasValue || q.CreationDate.Month == month.Value))
                            .GroupBy(q => q.ExpenseType)
                            .Select(q => new TeamMemberSummary
                            {
                                ExpenseType = q.Key,
                                Amount = q.Select(e => e.Amount).Sum()
                            })
                            .ToListAsync();

            return results;
        }


        private Expense BuildExpense(Expense expense, bool includeExpensePicture)
        {
            return new Expense()
            {
                ExpenseId = expense.ExpenseId,
                Name = expense.Name,
                Description = expense.Description,
                CreationDate = expense.CreationDate,
                LastModifiedDate = expense.LastModifiedDate,
                Status = expense.Status,
                Amount = expense.Amount,
                Contact = expense.Contact,
                Picture = includeExpensePicture ? expense.Picture : null,
                ExpenseType = expense.ExpenseType,
                RelatedProject = expense.RelatedProject,
                EmployeeId = expense.EmployeeId,
                Employee = new Employee()
                {
                    EmployeeId = expense.Employee.EmployeeId,
                    FirstName = expense.Employee.FirstName,
                    LastName = expense.Employee.LastName,
                    Email = expense.Employee.Email,
                    TeamId = expense.Employee.TeamId,
                    JobTitle = expense.Employee.JobTitle,
                    EmployeePictures = expense.Employee.EmployeePictures != null ?
                        expense.Employee.EmployeePictures
                        .Select(ep => new EmployeePicture()
                        {
                            Employee = null,
                            PictureType = ep.PictureType,
                            EmployeePictureId = ep.EmployeePictureId,
                            EmployeeId = ep.EmployeeId,
                            Content = ep.Content
                        }).ToList()
                        : null,
                },
            };
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
        private Expense RemoveExpensePicture(Expense expense)
        {
            expense.Picture = null;
            return expense;
        }

    }
}
