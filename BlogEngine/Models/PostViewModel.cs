using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Models
{
    public class PostViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CreatedDate { get; set; }
        public string CreatorFullName { get; set; }
    }
}
