

namespace MyShuttle.WebJob.Services
{
    using System;

    public class Invoice
    {
        public int RideId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string StartAddress { get; set; }

        public string EndAddress { get; set; }

        public double Distance { get; set; }

        public int Duration { get; set; }

        public double Cost { get; set; }

        public string EmployeeName { get; set; }

        public byte[] Signature { get; set; }
    }
}
