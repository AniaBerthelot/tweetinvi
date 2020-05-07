﻿using System;
using System.Threading.Tasks;
using FakeItEasy;
using Tweetinvi.Controllers.Tweet;
using Tweetinvi.Core.Iterators;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Models.DTO.QueryDTO;
using Tweetinvi.Parameters;
using Xunit;
using xUnitinvi.TestHelpers;

namespace xUnitinvi.ClientActions.TweetsClient
{
    public class TweetControllerTests
    {
        public TweetControllerTests()
        {
            _fakeBuilder = new FakeClassBuilder<TweetController>();
            _fakeTweetQueryExecutor = _fakeBuilder.GetFake<ITweetQueryExecutor>().FakedObject;
            _fakePageCursorIteratorFactories = _fakeBuilder.GetFake<IPageCursorIteratorFactories>().FakedObject;
        }

        private readonly FakeClassBuilder<TweetController> _fakeBuilder;
        private readonly ITweetQueryExecutor _fakeTweetQueryExecutor;
        private readonly IPageCursorIteratorFactories _fakePageCursorIteratorFactories;

        private TweetController CreateTweetController()
        {
            return _fakeBuilder.GenerateClass();
        }

        [Fact]
        public async Task GetTweet_ReturnsQueryExecutorResultAsync()
        {
            // Arrange
            var controller = CreateTweetController();
            var parameters = new GetTweetParameters(42);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<ITweetDTO>>();

            A.CallTo(() => _fakeTweetQueryExecutor.GetTweetAsync(parameters, request)).Returns(expectedResult);

            // Act
            var result = await controller.GetTweetAsync(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async Task PublishTweet_ReturnsQueryExecutorResultAsync()
        {
            // Arrange
            var controller = CreateTweetController();
            var parameters = new PublishTweetParameters("hello");
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<ITweetDTO>>();

            A.CallTo(() => _fakeTweetQueryExecutor.PublishTweetAsync(parameters, request)).Returns(expectedResult);

            // Act
            var result = await controller.PublishTweetAsync(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetFavoriteTweets_ReturnsFromPageCursorIteratorFactories()
        {
            // arrange
            var parameters = new GetUserFavoriteTweetsParameters("username") { PageSize = 2 };
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterPageIterator<ITwitterResult<ITweetDTO[]>, long?>>();

            A.CallTo(() => _fakePageCursorIteratorFactories.Create(parameters, It.IsAny<Func<long?, Task<ITwitterResult<ITweetDTO[]>>>>()))
                .Returns(expectedResult);

            var controller = CreateTweetController();
            var friendIdsIterator = controller.GetFavoriteTweetsIterator(parameters, request);

            // assert
            Assert.Equal(friendIdsIterator, expectedResult);
        }

        [Fact]
        public async Task GetRetweets_ReturnsQueryExecutorResultAsync()
        {
            // Arrange
            var controller = CreateTweetController();
            var parameters = new GetRetweetsParameters(42);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<ITweetDTO[]>>();

            A.CallTo(() => _fakeTweetQueryExecutor.GetRetweetsAsync(parameters, request)).Returns(expectedResult);

            // Act
            var result = await controller.GetRetweetsAsync(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async Task PublishRetweet_ReturnsQueryExecutorResultAsync()
        {
            // Arrange
            var controller = CreateTweetController();
            var parameters = new PublishRetweetParameters(42);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<ITweetDTO>>();

            A.CallTo(() => _fakeTweetQueryExecutor.PublishRetweetAsync(parameters, request)).Returns(expectedResult);

            // Act
            var result = await controller.PublishRetweetAsync(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async Task DestroyRetweet_ReturnsQueryExecutorResultAsync()
        {
            // Arrange
            var controller = CreateTweetController();
            var parameters = new DestroyRetweetParameters(42);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<ITweetDTO>>();

            A.CallTo(() => _fakeTweetQueryExecutor.DestroyRetweetAsync(parameters, request)).Returns(expectedResult);

            // Act
            var result = await controller.DestroyRetweetAsync(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetRetweeterIds_ReturnsFromPageCursorIteratorFactories()
        {
            // arrange
            var parameters = new GetRetweeterIdsParameters(42);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterPageIterator<ITwitterResult<IIdsCursorQueryResultDTO>>>();

            A.CallTo(() => _fakePageCursorIteratorFactories.Create(parameters, It.IsAny<Func<string, Task<ITwitterResult<IIdsCursorQueryResultDTO>>>>()))
                .Returns(expectedResult);

            var controller = CreateTweetController();
            var iterator = controller.GetRetweeterIdsIterator(parameters, request);

            // assert
            Assert.Equal(iterator, expectedResult);
        }
    }
}