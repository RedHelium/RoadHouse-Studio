using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RoadHouse_Studio.Networking
{

    public struct Content
    {
        private string content;

        public void Start() => content = "{";

        public void End() => content += "\n}";

        public void Append(string key, string value, string delimiter = ",")
        => content += string.Concat('\n', '\t',  "\u0022" + key + "\u0022",
        ':', "\u0022" + value + "\u0022", delimiter);    

        public override string ToString() => content;
    }

    public class Request : IDisposable
    {
        public HttpRequestMessage request { get; protected set; }

        public Request(HttpMethod method, Uri uri)
        {
            request = new HttpRequestMessage(method, uri);
        }

        public void AddDefaultHeaders(string clientID, string userToken)
        {
            request.Headers.TryAddWithoutValidation("client-id", clientID);
            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + userToken);
        }

        public void AddHeader(string key, string value) 
        => request.Headers.TryAddWithoutValidation(key, value);

        public void AddContent(Content content) 
        => request.Content = new StringContent(content.ToString(), Encoding.UTF8);

        public void AddContentHeaderType(string type)
        => request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(type);

        public void Dispose() => request.Dispose();
        
    }
}
