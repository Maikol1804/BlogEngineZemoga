using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Models
{
    public class LoggedInUserViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string RolCode { get; set; }
        public string RolName { get; set; }
    }
}
