﻿using Newtonsoft.Json;
using Tweetinvi.Models;

namespace Tweetinvi.Core.Models.Properties
{
    public class QuickReplyOption : IQuickReplyOption
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }
    }
}
