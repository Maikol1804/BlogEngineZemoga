using BlogEngine.DataAccess.Models;
using BlogEngineAPI.DTO.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogEngine.Models.Mappers
{
    public class CommentViewModelMapper : Mapper<Comment, CommentViewModel>
    {
        public override CommentViewModel Map(Comment objectToMap)
        {
            return new CommentViewModel
            {
                Author = objectToMap.Author,
                Body = objectToMap.Body
            };
        }

        public override List<CommentViewModel> ListMapView(IEnumerable<Comment> objectsToMap)
        {
            List<CommentViewModel> objectsList = new List<CommentViewModel>();

            foreach (Comment objectToMap in objectsToMap)
            {
                objectsList.Add(Map(objectToMap));
            }

            return objectsList;
        }

        public override ActionResult<IEnumerable<CommentViewModel>> ListMap(IEnumerable<Comment> objectsToMap)
        {
            throw new System.NotImplementedException();
        }
    }
}