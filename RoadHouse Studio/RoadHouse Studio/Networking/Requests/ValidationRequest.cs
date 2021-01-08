using System;
using System.Net.Http;

namespace RoadHouse_Studio.Networking.Requests
{
    public sealed class ValidationRequest : Request
    {
        public ValidationRequest(Uri uri, string accessToken) : base(HttpMethod.Get, uri)
        {
            request.Headers.TryAddWithoutValidation("Authorization", "OAuth " + accessToken);
        }
    }
}
