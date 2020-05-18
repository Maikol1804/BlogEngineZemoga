using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace BlogEngineAPI.DTO.Mappers
{
    public interface IMapper<O, D>
    {
        D Map(O objectToMap);
        ActionResult<IEnumerable<D>> ListMap(IEnumerable<O> objectsToMap);
        List<D> ListMapView(IEnumerable<O> objectsToMap);
    }
}
