
namespace MyShuttle.MobileServices.DataObjects
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Employee : CustomDataEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int CustomerId { get; set; }

        public byte[] Picture { get; set; }

    }
}