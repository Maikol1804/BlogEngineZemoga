using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogEngine.DataAccess.Models
{
    public class PasswordByUser : BaseModel
    {
        public User User { get; set; }
        public string Hash { get; set; }
        public string Value { get; set; }

    }
}
