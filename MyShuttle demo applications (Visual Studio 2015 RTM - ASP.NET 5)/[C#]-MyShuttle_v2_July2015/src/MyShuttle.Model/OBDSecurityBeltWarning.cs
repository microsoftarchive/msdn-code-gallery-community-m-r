namespace MyShuttle.Model
   {
       using System;

       public class OBDSecurityBeltWarning
       {
           public int OBDSecurityBeltWarningId { get; set; }

           public string WinStartTime { get; set; }

           public string WinEndTime { get; set; }

           public int DriverId { get; set; }

           public Driver Driver { get; set; }

           public string LicensePlate { get; set; }

           public string Name { get; set; }

           public int NumWarnings { get; set; }

       }
   }