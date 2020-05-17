using BlogEngine.DataAccess.Models;

namespace BlogEngineAPI.DTO.Mappers
{
    public class MappersFactory
    {
        public static IMapper<Post, PostDTO> PostDTO()
        {
            return new PostMapper();
        }
    }
}
