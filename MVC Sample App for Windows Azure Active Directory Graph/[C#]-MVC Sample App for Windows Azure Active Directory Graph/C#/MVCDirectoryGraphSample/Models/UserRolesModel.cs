using Microsoft.WindowsAzure.ActiveDirectory;
using System.Collections.Generic;

namespace MvcDirectoryGraphSample.Models
{
    public class UserRolesModel
    {
        public User User { get; set; }

        public IEnumerable<Role> CurrentRoles { get; set; }

        public IEnumerable<Role> UnassignedRoles { get; set; }
    }
}
