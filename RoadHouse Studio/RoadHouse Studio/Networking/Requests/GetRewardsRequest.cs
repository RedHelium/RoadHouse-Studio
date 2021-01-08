using System;
using System.Net.Http;


namespace RoadHouse_Studio.Networking.Requests
{
    public sealed class GetRewardsRequest : Request
    {
        public GetRewardsRequest(Uri uri, string clientID, string userToken) : base(HttpMethod.Get, uri)
        {
            AddDefaultHeaders(clientID, userToken);
        }
    }
}
