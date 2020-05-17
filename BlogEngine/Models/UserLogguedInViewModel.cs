using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Models
{
    public class UserLogguedInViewModel
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        public string RolCode { get; set; }

        public string RolName { get; set; }
    }
}
