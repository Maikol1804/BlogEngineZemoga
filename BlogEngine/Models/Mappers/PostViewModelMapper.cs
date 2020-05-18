using BlogEngine.DataAccess.Models;
using BlogEngineAPI.DTO.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Models.Mappers
{
    public class PostViewModelMapper : Mapper<Post, PostViewModel>
    {
        public override PostViewModel Map(Post objectToMap)
        {
            return new PostViewModel
            {
                Id = objectToMap.Id,
                Title = objectToMap.Title,
                Body = objectToMap.Body,
                CreatedDate = objectToMap.CreatedDate.ToString("dddd, dd MMMM yyyy HH:mm", new CultureInfo("en-US")),
                ApprovalDate = objectToMap.ApprovalDate.ToString("dddd, dd MMMM yyyy HH:mm", new CultureInfo("en-US")),
                CreatorFullName = objectToMap.User.FullName,
                Comments = MappersFactory.CommentViewModel().ListMapView(objectToMap.Comments)
            };
        }

        public override List<PostViewModel> ListMapView(IEnumerable<Post> objectsToMap)
        {
            List<PostViewModel> objectsList = new List<PostViewModel>();

            foreach (Post objectToMap in objectsToMap)
            {
                objectsList.Add(Map(objectToMap));
            }

            return objectsList;
        }

        public override ActionResult<IEnumerable<PostViewModel>> ListMap(IEnumerable<Post> objectsToMap)
        {
            throw new System.NotImplementedException();
        }
    }
}
