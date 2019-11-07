using Microsoft.WindowsAzure.ActiveDirectory;
using System.Collections.Generic;

namespace MvcDirectoryGraphSample.Models
{
    public class GroupUsersModel
    {
        public Group Group { get; set; }

        public IEnumerable<User> CurrentGroupUsers { get; set; }

        public IEnumerable<User> OtherUsers { get; set; }
    }
}
