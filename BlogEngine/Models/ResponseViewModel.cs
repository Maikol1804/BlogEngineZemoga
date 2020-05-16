using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Models
{
    public class ResponseViewModel
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
