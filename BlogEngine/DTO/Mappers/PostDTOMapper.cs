using BlogEngine.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;

namespace BlogEngineAPI.DTO.Mappers
{
    public class PostDTOMapper : Mapper<Post, PostDTO>
    {
        public override PostDTO Map(Post objectToMap)
        {
            return new PostDTO
            {
                Id = objectToMap.Id,
                Title = objectToMap.Title,
                Body = objectToMap.Body,
                CreatedDate = objectToMap.CreatedDate.ToString("dddd, dd MMMM yyyy HH:mm", new CultureInfo("en-US")),
                CreatorFullName = objectToMap.User.FullName
            };
        }

        public override ActionResult<IEnumerable<PostDTO>> ListMap(IEnumerable<Post> objectsToMap)
        {
            List<PostDTO> objectsList = new List<PostDTO>();

            foreach (Post objectToMap in objectsToMap)
            {
                objectsList.Add(Map(objectToMap));
            }

            return objectsList;
        }

        public override List<PostDTO> ListMapView(IEnumerable<Post> objectsToMap)
        {
            throw new System.NotImplementedException();
        }
    }
}
