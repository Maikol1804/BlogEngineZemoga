using BlogEngine.DataAccess.Models;
using BlogEngineAPI.DTO.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Models.Mappers
{
    public class LoggedInUserViewModelMapper : Mapper<User, LoggedInUserViewModel>
    {
        public override LoggedInUserViewModel Map(User objectToMap)
        {
            return new LoggedInUserViewModel
            {
                Id = objectToMap.Id,
                FullName = objectToMap.FullName,
                Username = objectToMap.UserName,
                RolCode = objectToMap.Rol.Code,
                RolName = objectToMap.Rol.Name
            };
        }

        public override List<LoggedInUserViewModel> ListMapView(IEnumerable<User> objectsToMap)
        {
            List<LoggedInUserViewModel> objectsList = new List<LoggedInUserViewModel>();

            foreach (User objectToMap in objectsToMap)
            {
                objectsList.Add(Map(objectToMap));
            }

            return objectsList;
        }

        public override ActionResult<IEnumerable<LoggedInUserViewModel>> ListMap(IEnumerable<User> objectsToMap)
        {
            throw new System.NotImplementedException();
        }
    }
}
