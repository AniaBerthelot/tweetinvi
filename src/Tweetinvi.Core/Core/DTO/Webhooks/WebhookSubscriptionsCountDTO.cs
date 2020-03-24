﻿using Newtonsoft.Json;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO.Webhooks;

namespace Tweetinvi.Core.DTO.Webhooks
{
    public class WebhookSubscriptionsCountDTO : IWebhookSubscriptionsCount
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }
        [JsonProperty("subscriptions_count")]
        public string SubscriptionsCount { get; set; }
        [JsonProperty("provisioned_count")]
        public string ProvisionedCount { get; set; }
    }
}


