using BlogEngine.DataAccess.Models;
using BlogEngine.Models;
using BlogEngine.Models.Mappers;

namespace BlogEngineAPI.DTO.Mappers
{
    public class MappersFactory
    {
        public static IMapper<Post, PostDTO> PostDTO()
        {
            return new PostDTOMapper();
        }

        public static IMapper<User, LoggedInUserViewModel> LoggedInUserViewModel()
        {
            return new LoggedInUserViewModelMapper();
        }

        public static IMapper<Comment, CommentViewModel> CommentViewModel()
        {
            return new CommentViewModelMapper();
        }

        public static IMapper<Post, PostViewModel> PostViewModel()
        {
            return new PostViewModelMapper();
        }

    }
}
