using System;
using System.Net.Http;

namespace RoadHouse_Studio.Networking.Requests
{
    public sealed class DeleteRewardRequest : Request
    {
        public DeleteRewardRequest(Uri uri, string clientID, string userToken) : base(HttpMethod.Delete, uri)
        {
            AddDefaultHeaders(clientID, userToken);       
        }
    }
}
