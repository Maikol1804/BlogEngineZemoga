using static BlogEngine.Transverse.Enumerator.Enumerator;

namespace BlogEngine.Transverse.Entities
{
    public class Response
    {
        public long EntityId { get; set; }
        public State State { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }

    }
}
