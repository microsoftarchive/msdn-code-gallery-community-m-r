namespace MyShuttle.Data
{
    using System.Threading.Tasks;
    using Model;

    public interface IEmployeeRepository
    {
        Task<Employee> GetAsync(int employeeId);
    }
}