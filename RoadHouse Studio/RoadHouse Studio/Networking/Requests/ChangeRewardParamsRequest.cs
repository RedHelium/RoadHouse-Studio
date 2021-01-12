using System;
using System.Net.Http;

namespace RoadHouse_Studio.Networking.Requests
{
    public sealed class ChangeRewardParamsRequest : Request
    {
        public ChangeRewardParamsRequest(Uri uri, string clientID, string userToken, Content content, string contentType) 
        : base(new HttpMethod("PATCH"), uri)
        {
            AddDefaultHeaders(clientID, userToken);
            AddContent(content);
            AddContentHeaderType(contentType);
        }
    }
}
