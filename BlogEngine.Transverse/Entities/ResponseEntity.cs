
namespace BlogEngine.Transverse.Entities
{
    public class ResponseEntity<T> : Response where T : class
    {
        public T Entity { get; set; }
    }
}
