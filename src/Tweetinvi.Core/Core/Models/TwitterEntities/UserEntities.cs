﻿using Newtonsoft.Json;
using Tweetinvi.Core.JsonConverters;
using Tweetinvi.Models.Entities;

namespace Tweetinvi.Core.Models.TwitterEntities
{
    public class UserEntities : IUserEntities
    {
        [JsonProperty("url")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public IWebsiteEntity Website { get; set; }

        [JsonProperty("description")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public IDescriptionEntity Description { get; set; }
    }
}