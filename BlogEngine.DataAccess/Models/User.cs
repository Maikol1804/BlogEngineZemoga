
namespace BlogEngine.DataAccess.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Rol Rol { get; set; }
    }

}
