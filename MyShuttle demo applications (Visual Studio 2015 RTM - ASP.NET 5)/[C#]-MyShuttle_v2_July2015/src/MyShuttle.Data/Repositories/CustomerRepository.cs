
namespace MyShuttle.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        MyShuttleContext context;

        public CustomerRepository(MyShuttleContext dbcontext)
	    {
            context = dbcontext;
        }
    }
}