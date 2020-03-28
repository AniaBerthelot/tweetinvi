﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examplinvi.AccountActivityEvents;
using Examplinvi.AccountActivityEvents.Controllers;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO.Webhooks;

namespace Examplinvi.AccountActivity.ASP.NETCore.Controllers
{
    [Route("tweetinvi/")]
    public class TweetinviWebhookController : Controller
    {
        private readonly AccountActivityWebhooksController _accountActivityWebhooksController;
        private readonly AccountActivitySubscriptionsController _accountActivitySubscriptionsController;
        private readonly AccountActivityEventsController _accountActivityEventsController;

        public TweetinviWebhookController()
        {
            _accountActivityWebhooksController = new AccountActivityWebhooksController(Startup.WebhookClient);
            _accountActivitySubscriptionsController = new AccountActivitySubscriptionsController(Startup.WebhookClient);
            _accountActivityEventsController = new AccountActivityEventsController(Startup.AccountActivityRequestHandler);
        }

        // WEBHOOK - Prepare and configure webhook
        [HttpPost("SetUserCredentials")]
        public async Task<string> SetUserCredentials([FromBody]TwitterCredentials credentials)
        {
            var client = new TwitterClient(credentials);
            var user = await client.Users.GetAuthenticatedUser();

            await AccountActivityCredentialsRetriever.SetUserCredentials(user.Id, credentials);
            return $"User {user.Id} registered!";
        }


        [HttpPost("TriggerAccountActivityWebhookCRC")]
        public async Task<bool> TriggerAccountActivityWebhookCRC(string environment, string webhookId)
        {
            return await _accountActivityWebhooksController.TriggerAccountActivityWebhookCRC(environment, webhookId);
        }

        [HttpPost("RegisterWebhook")]
        public async Task<bool> RegisterWebhook(string environment, string url)
        {
            return await _accountActivityWebhooksController.CreateAccountActivityWebhook(environment, url);
        }

        [HttpDelete("DeleteWebhook")]
        public async Task<bool> DeleteWebhook(string environment, string webhookId)
        {
            return await _accountActivityWebhooksController.DeleteAccountActivityWebhook(environment, webhookId);
        }

        [HttpGet("GetWebhookEnvironments")]
        public async Task<IEnumerable<IWebhookEnvironmentDTO>> GetWebhookEnvironments()
        {
            return (await _accountActivityWebhooksController.GetAccountActivityWebhookEnvironments()).Select(x => x.WebhookEnvironmentDTO);
        }

        [HttpGet("CountAccountActivitySubscriptions")]
        public async Task<string> CountAccountActivitySubscriptions()
        {
            return await _accountActivityWebhooksController.CountAccountActivitySubscriptions();
        }

        // SUBSCRIPTIONS - Subscribe / Unsubscribe user from webhook

        [HttpGet("GetWebhookSubscriptions")]
        public async Task<IEnumerable<IWebhookSubscription>> GetWebhookSubscriptions(string environment)
        {
            return await _accountActivitySubscriptionsController.GetWebhookSubscriptions(environment);
        }

        [HttpPost("SubscribeToAccountActivity")]
        public async Task<bool> SubscribeToAccountActivity(string environment, long userId)
        {
            return await _accountActivitySubscriptionsController.SubscribeToAccountActivity(environment, userId);
        }

        [HttpPost("UnsubscribeFromAccountActivity")]
        public async Task<bool> UnsubscribeFromAccountActivity(string environment, long userId)
        {
            return await _accountActivitySubscriptionsController.UnsubscribeFromAccountActivity(environment, userId);
        }

        // ACCOUNT ACTIVITY EVENTS
        [HttpPost("SubscribeToEvents")]
        public async Task<string> SubscribeToEvents(string environment, long userId)
        {
            return await _accountActivityEventsController.SubscribeToEvents(environment, userId);
        }

        [HttpPost("UnsubscribeFromEvents")]
        public async Task<string> UnsubscribeFromEvents(string environment, long userId)
        {
            return await _accountActivityEventsController.UnsubscribeFromEvents(environment, userId);
        }
    }
}
