using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogEngineAPI.DTO.Mappers
{
    public abstract class Mapper<O, D> : IMapper<O, D>
    {
        public abstract D Map(O objectToMap);
        public abstract ActionResult<IEnumerable<D>> ListMap(IEnumerable<O> objectsToMap);
        public abstract List<D> ListMapView(IEnumerable<O> objectsToMap);
    }
}
