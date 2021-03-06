using System.Collections.Generic;
using System.Net;

namespace Game.Common.RefitModels
{
    public class ResponseData<T> : ResponseData
    {
        public T Data { get; set; }
    }
    public class ResponseData
    {
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>> { };
    }
}
