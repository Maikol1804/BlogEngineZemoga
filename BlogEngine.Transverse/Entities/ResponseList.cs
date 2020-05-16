using System.Collections.Generic;

namespace BlogEngine.Transverse.Entities
{
    public class ResponseList<T> : Response where T : class
    {
        public IEnumerable<T> List { get; set; }
    }
}
