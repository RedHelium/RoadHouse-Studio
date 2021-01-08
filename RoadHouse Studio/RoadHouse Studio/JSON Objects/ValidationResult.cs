using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RoadHouse_Studio.JSON_Objects
{
    [Serializable]
    public sealed class ValidationResult
    {
        [JsonProperty("client_id")]
        public string client_id { get; set; }
        
        [JsonProperty("login")]
        public string login { get; set; }
        
        [JsonProperty("scopes")]
        public List<string> scopes { get; set; }
        
        [JsonProperty("user_id")]
        public string user_id { get; set; }
        
        [JsonProperty("expires_in")]
        public int expires_in { get; set; }
    }
}
