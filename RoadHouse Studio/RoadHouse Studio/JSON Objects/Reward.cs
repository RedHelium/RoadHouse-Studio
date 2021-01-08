using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RoadHouse_Studio.JSON_Objects
{
    [Serializable]
    public sealed class Reward
    {
        [JsonProperty("data")]
        public List<Datum> data { get; set; } = new List<Datum>();
    }

    [Serializable]
    public sealed class Datum
    {
        [JsonProperty("broadcaster_name")]
        public string broadcaster_name { get; set; }

        [JsonProperty("broadcaster_id")]
        public string broadcaster_id { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("image")]
        public object image { get; set; }

        [JsonProperty("background_color")]
        public string background_color { get; set; }

        [JsonProperty("is_enabled")]
        public bool is_enabled { get; set; }

        [JsonProperty("cost")]
        public int cost { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("prompt")]
        public string prompt { get; set; }

        [JsonProperty("is_user_input_required")]
        public bool is_user_input_required { get; set; }

        [JsonProperty("max_per_stream_setting")]
        public MaxPerStreamSetting max_per_stream_setting { get; set; }

        [JsonProperty("max_per_user_per_stream_setting")]
        public MaxPerUserPerStreamSetting max_per_user_per_stream_setting { get; set; }

        [JsonProperty("global_cooldown_setting")]
        public GlobalCooldownSetting global_cooldown_setting { get; set; }

        [JsonProperty("is_paused")]
        public bool is_paused { get; set; }

        [JsonProperty("is_in_stock")]
        public bool is_in_stock { get; set; }

        [JsonProperty("default_image")]
        public DefaultImage default_image { get; set; }

        [JsonProperty("should_redemptions_skip_request_queue")]
        public bool should_redemptions_skip_request_queue { get; set; }

        [JsonProperty("redemptions_redeemed_current_stream")]
        public object redemptions_redeemed_current_stream { get; set; }

        [JsonProperty("cooldown_expires_at")]
        public object cooldown_expires_at { get; set; }
    }

    [Serializable]
    public sealed class MaxPerStreamSetting
    {
        [JsonProperty("is_enabled")]
        public bool is_enabled { get; set; }

        [JsonProperty("max_per_stream")]
        public int max_per_stream { get; set; }
    }

    [Serializable]
    public sealed class MaxPerUserPerStreamSetting
    {
        [JsonProperty("is_enabled")]
        public bool is_enabled { get; set; }

        [JsonProperty("max_per_user_per_stream")]
        public int max_per_user_per_stream { get; set; }
    }

    [Serializable]
    public sealed class GlobalCooldownSetting
    {
        [JsonProperty("is_enabled")]
        public bool is_enabled { get; set; }

        [JsonProperty("global_cooldown_seconds")]
        public int global_cooldown_seconds { get; set; }
    }

    [Serializable]
    public sealed class DefaultImage
    {
        [JsonProperty("url_1x")]
        public string url_1x { get; set; }

        [JsonProperty("url_2x")]
        public string url_2x { get; set; }

        [JsonProperty("url_4x")]
        public string url_4x { get; set; }
    }
}
