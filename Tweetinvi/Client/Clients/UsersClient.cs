﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Client.Requesters;
using Tweetinvi.Core.Models;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO.QueryDTO;
using Tweetinvi.Parameters;

// ReSharper disable MemberCanBePrivate.Global

namespace Tweetinvi.Client
{
    /// <summary>
    /// A client providing all the methods related with users
    /// </summary>
    public class UsersClient
    {
        private readonly IUsersRequester _usersRequester;

        public UsersClient(TwitterClient client)
        {
            _usersRequester = client.RequestExecutor.Users;
        }

        /// <summary>
        /// Get the authenticated user based on the TwitterClient's credentials
        /// </summary>
        /// <returns>The client's authenticated user</returns>
        public async Task<IAuthenticatedUser> GetAuthenticatedUser()
        {
            var requestResult = await _usersRequester.GetAuthenticatedUser(null);
            return requestResult?.Result;
        }

        /// <summary>
        /// Get the authenticated user based on the TwitterClient's credentials
        /// </summary>
        /// <returns>The client's authenticated user</returns>
        public async Task<IAuthenticatedUser> GetAuthenticatedUser(IGetAuthenticatedUserParameters parameters)
        {
            var requestResult = await _usersRequester.GetAuthenticatedUser(parameters);
            return requestResult?.Result;
        }

        #region GetUser

        /// <summary>
        /// Get a user
        /// </summary>
        public Task<IUser> GetUser(long? userId)
        {
            return GetUser(new UserIdentifier(userId));
        }

        /// <summary>
        /// Get a user
        /// </summary>
        public Task<IUser> GetUser(long userId)
        {
            return GetUser(new UserIdentifier(userId));
        }

        /// <summary>
        /// Get a user
        /// </summary>
        public Task<IUser> GetUser(string username)
        {
            return GetUser(new UserIdentifier(username));
        }

        /// <summary>
        /// Get a user
        /// </summary>
        public Task<IUser> GetUser(IUserIdentifier userIdentifier)
        {
            return GetUser(new GetUserParameters(userIdentifier));
        }

        /// <summary>
        /// Get a user
        /// </summary>
        public async Task<IUser> GetUser(IGetUserParameters parameters)
        {
            var requestResult = await _usersRequester.GetUser(parameters);
            return requestResult?.Result;
        }

        #endregion

        #region GetUsers

        /// <summary>
        /// Get multiple users
        /// </summary>
        public Task<IUser[]> GetUsers(IEnumerable<long> userIds)
        {
            var userIdentifiers = userIds.Select(x => new UserIdentifier(x));
            return GetUsers(userIdentifiers);
        }

        /// <summary>
        /// Get multiple users
        /// </summary>
        public Task<IUser[]> GetUsers(IEnumerable<string> usernames)
        {
            var userIdentifiers = usernames.Select(x => new UserIdentifier(x));
            return GetUsers(userIdentifiers);
        }

        /// <summary>
        /// Get multiple users
        /// </summary>
        public Task<IUser[]> GetUsers(IEnumerable<IUserIdentifier> userIdentifiers)
        {
            return GetUsers(new GetUsersParameters(userIdentifiers.ToArray()));
        }

        /// <summary>
        /// Get multiple users
        /// </summary>
        public async Task<IUser[]> GetUsers(IGetUsersParameters parameters)
        {
            var requestResult = await _usersRequester.GetUsers(parameters);
            return requestResult?.Result;
        }

        #endregion

        /// <summary>
        /// Get friend ids from a specific user
        /// </summary>
        /// <returns>A CursorResult to iterate over all the user's friends</returns>
        public async Task<ICursorResult<long>> GetFriendIds(IGetFriendIdsParameters parameters)
        {
            var twitterCursorResult = await _usersRequester.GetFriendIds(parameters);
            return new CursorResult<long, IIdsCursorQueryResultDTO>(twitterCursorResult);
        }

        /// <summary>
        /// Get friend ids from a specific user
        /// </summary>
        /// <returns>A CursorResult to iterate over all the user's friends</returns>
        public Task<ICursorResult<long>> GetFriendIds(string username)
        {
            var parameters = new GetFriendIdsParameters(username);
            return GetFriendIds(parameters);
        }

        /// <summary>
        /// Get friend ids from a specific user
        /// </summary>
        /// <returns>A CursorResult to iterate over all the user's friends</returns>
        public Task<ICursorResult<long>> GetFriendIds(long userId)
        {
            var parameters = new GetFriendIdsParameters(userId);
            return GetFriendIds(parameters);
        }

        /// <summary>
        /// Get friend ids from a specific user
        /// </summary>
        /// <returns>A CursorResult to iterate over all the user's friends</returns>
        public Task<ICursorResult<long>> GetFriendIds(IUserIdentifier userIdentifier)
        {
            var parameters = new GetFriendIdsParameters(userIdentifier);
            return GetFriendIds(parameters);
        }
    }
}
