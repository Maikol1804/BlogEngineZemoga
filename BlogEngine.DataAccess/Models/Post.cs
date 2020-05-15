using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogEngine.DataAccess.Models
{
    public class Post : BaseModel
    {
        public User User { get; set; }
        public PostState PostState { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

    }
}
