﻿using Newtonsoft.Json;
using Tweetinvi.Core.JsonConverters;
using Tweetinvi.Models.DTO;

namespace Tweetinvi.Core.DTO
{
    public class MessageCreateDTO : IMessageCreateDTO
    {
        // Twitter fields
        [JsonProperty("target")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public IMessageCreateTargetDTO Target { get; set; }

        [JsonProperty("sender_id")]
        public long SenderId { get; set; }

        [JsonProperty("source_app_id")]
        public long? SourceAppId { get; set; }

        [JsonProperty("message_data")]
        [JsonConverter(typeof(JsonPropertyConverterRepository))]
        public IMessageDataDTO MessageData { get; set; }
    }
}
