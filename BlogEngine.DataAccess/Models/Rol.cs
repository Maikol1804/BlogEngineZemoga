using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.DataAccess.Models
{
    public class Rol : BaseModel
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
