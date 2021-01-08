using System;
using System.Net.Http;

namespace RoadHouse_Studio.Networking.Requests
{
    public sealed class CreateRewardRequest : Request
    {
        public CreateRewardRequest(Uri uri, string clientID, string userToken, Content content, string type) : base(HttpMethod.Post, uri)
        {
            AddDefaultHeaders(clientID, userToken);
            AddContent(content);
            AddContentHeaderType(type);
        }
    }
}
