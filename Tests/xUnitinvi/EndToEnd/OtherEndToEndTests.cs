using System;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Exceptions;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Xunit;
using Xunit.Abstractions;
using xUnitinvi.TestHelpers;

namespace xUnitinvi.EndToEnd
{
    [Collection("EndToEndTests")]
    public class OtherEndToEndTests : TweetinviTest
    {
        public OtherEndToEndTests(ITestOutputHelper logger) : base(logger)
        {
        }

        [Fact]
        public async Task ShouldProperlyHandleExceptionsAsync()
        {
            if (!EndToEndTestConfig.ShouldRunEndToEndTests)
                return;

            ITwitterException globalException = null;
            ITwitterException clientException = null;

            TweetinviEvents.OnTwitterException += (sender, exception) => { globalException = exception; };

            var invalidCredentials = new TwitterCredentials("a", "b", "c", "d");
            var client = new TwitterClient(invalidCredentials);

            TweetinviEvents.SubscribeToClientEvents(client);
            client.Events.OnTwitterException += (sender, exception) => { clientException = exception; };

            try
            {
                await client.Users.GetAuthenticatedUserAsync();
            }
            catch (TwitterException e)
            {
                Assert.Equal(401, e.StatusCode);
                Assert.Equal(globalException, e);
                Assert.Equal(clientException, e);
                return;
            }

            throw new Exception("Should have thrown a TwitterException");
        }
    }
}