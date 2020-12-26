using System;

namespace RoadHouse_Studio.Networking
{
    public class UriResponseBuilder
    {
        private UriBuilder uriBuilder;
        private string scheme;
        private string host;
        private string path;

        public UriResponseBuilder(string scheme, string host, string path)
        {
            this.scheme = scheme;
            this.host = host;
            this.path = path;
            uriBuilder = new UriBuilder();
        }

        public virtual void BuildUri(Query query = default)
        {
            uriBuilder.Scheme = scheme;
            uriBuilder.Host = host;
            uriBuilder.Path = path;
            if (!query.Equals(default))
                uriBuilder.Query = query.ToString();
        }

        public virtual void BuildUri(string scheme, string host, string path, Query query = default)
        {
            uriBuilder.Scheme = scheme;
            uriBuilder.Host = host;
            uriBuilder.Path = path;
            if (!query.Equals(default))
                uriBuilder.Query = query.ToString();
        }

        public override string ToString() => uriBuilder.ToString();
    }
}
