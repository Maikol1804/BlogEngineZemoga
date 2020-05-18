using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.DataAccess.Models
{
    public class Comment : BaseModel
    {
        public string Author { get; set; }
        public string Body { get; set; }
    }
}
