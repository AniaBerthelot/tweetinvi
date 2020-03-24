﻿using System.Threading.Tasks;
using Tweetinvi.Client.Tools;
using Tweetinvi.Core.Helpers;
using Tweetinvi.Core.Streaming;
using Tweetinvi.Core.Wrappers;
using Tweetinvi.Parameters;
using Tweetinvi.Streaming;
using Tweetinvi.Streams.Properties;

namespace Tweetinvi.Streams
{
    public class SampleStream : TweetStream, ISampleStream
    {
        public SampleStream(
            ITwitterClient twitterClient,
            IStreamResultGenerator streamResultGenerator,
            IJsonObjectConverter jsonObjectConverter,
            IJObjectStaticWrapper jObjectStaticWrapper,
            ITwitterClientFactories factories,
            ICreateSampleStreamParameters createSampleStreamParameters)
            : base(
                twitterClient,
                streamResultGenerator,
                jsonObjectConverter,
                jObjectStaticWrapper,
                factories,
                createSampleStreamParameters)
        {
        }

        public async Task StartStream()
        {
            await StartStream(Resources.Stream_Sample).ConfigureAwait(false);
        }
    }
}