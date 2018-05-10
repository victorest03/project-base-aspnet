using System.Collections.Generic;

namespace Common.Model
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<Error> Errors { get; set; }
    }
}
