namespace MyShuttle.Client.Core.DocumentResponse
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] Picture { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string Id { get; set; }
    }
}