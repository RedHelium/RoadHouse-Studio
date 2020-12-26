using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RoadHouse_Studio.Networking
{
    public struct Content
    {
        private string content;

        public void Start() => content = "{";

        public void End() => content += "\n}";

        public void Append(KeyValuePair<string, string> value, string delimiter = ",")
        => content += string.Concat('\n', "\u005c", "\u0022" + value.Key + "\u005c" + "\u0022",
        ':', "\u005c" + "\u0022" + value.Value + "\u005c" + "\u0022", delimiter);

        public override string ToString() => content;
    }

    public class Request : IDisposable
    {
        protected HttpRequestMessage request;

        public Request(HttpMethod method, Uri uri)
        {
            request = new HttpRequestMessage(method, uri);
        }

        public void AddHeader(string key, string value) 
        => request.Headers.TryAddWithoutValidation(key, value);

        public void AddContent(Content content) 
        => request.Content = new StringContent(content.ToString());

        public void AddContentHeaderType(string type)
        => request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(type);

        public void Dispose() => request.Dispose();
        
    }
}
