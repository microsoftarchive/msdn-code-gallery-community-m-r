using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompany.Common.EventBus
{
    /// <summary>
    /// Staff Actions
    /// </summary>
    public static class StaffActions
    {
        /// <summary>
        /// Add Employee
        /// </summary>
        public static readonly string AddEmployee = "add Employee";

        /// <summary>
        /// Update Employee
        /// </summary>
        public static readonly string UpdateEmployee = "update Employee";

        /// <summary>
        /// Delete Employee
        /// </summary>
        public static readonly string DeleteEmployee = "delete Employee";

        /// <summary>
        /// Add Employee Picture
        /// </summary>
        public static readonly string AddEmployeePicture = "add employeePicture";

        /// <summary>
        /// Update Employee Picture
        /// </summary>
        public static readonly string UpdateEmployeePicture = "update employeePicture";

        /// <summary>
        /// Delete Employee Picture
        /// </summary>
        public static readonly string DeleteEmployeePicture = "delete employeePicture";

        /// <summary>
        /// Add Team
        /// </summary>
        public static readonly string AddTeam = "add Team";

        /// <summary>
        /// Update Team
        /// </summary>
        public static readonly string UpdateTeam = "update Team";

        /// <summary>
        /// Delete Team
        /// </summary>
        public static readonly string DeleteTeam = "delete Team";

        /// <summary>
        /// Add Office
        /// </summary>
        public static readonly string AddOffice = "add Office";

        /// <summary>
        /// Update Office
        /// </summary>
        public static readonly string UpdateOffice = "update Office";

        /// <summary>
        /// Delete Office
        /// </summary>
        public static readonly string DeleteOffice = "delete Office";

        /// <summary>
        /// Add CalendarDTO
        /// </summary>
        public static readonly string AddCalendar = "add CalendarDTO";

        /// <summary>
        /// Update CalendarDTO
        /// </summary>
        public static readonly string UpdateCalendar = "update CalendarDTO";

        /// <summary>
        /// Delete CalendarDTO
        /// </summary>
        public static readonly string DeleteCalendar = "delete CalendarDTO";

        /// <summary>
        /// Add Holiday
        /// </summary>
        public static readonly string AddHoliday = "add Holiday";

        /// <summary>
        /// Update Holiday
        /// </summary>
        public static readonly string UpdateHoliday = "update Holiday";

        /// <summary>
        /// Delete Holiday
        /// </summary>
        public static readonly string DeleteHoliday = "delete Holiday";
    }

    /// <summary>
    /// Expense Actions
    /// </summary>
    public static class ExpenseActions
    {
        /// <summary>
        /// Add Expense
        /// </summary>
        public static readonly string AddExpense = "add Expense";

        /// <summary>
        /// Update Expense
        /// </summary>
        public static readonly string UpdateExpense = "update Expense";

        /// <summary>
        /// Delete Expense
        /// </summary>
        public static readonly string DeleteExpense = "delete Expense";
    }

    /// <summary>
    /// Vacation Actions
    /// </summary>
    public static class VacationActions
    {
        /// <summary>
        /// Add Vacation
        /// </summary>
        public static readonly string AddVacation = "add Vacation";

        /// <summary>
        /// Update Vacation
        /// </summary>
        public static readonly string UpdateVacation = "update Vacation";

        /// <summary>
        /// Delete Vacation
        /// </summary>
        public static readonly string DeleteVacation = "delete Vacation";
    }

}
