using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogEngine.DataAccess.Models
{
    public class Post : BaseModel
    {

        public Post() {
            Comments = new List<Comment>();        
        }

        public User User { get; set; }
        public string PostStateCode { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime ApprovalDate { get; set; }

    }
}
