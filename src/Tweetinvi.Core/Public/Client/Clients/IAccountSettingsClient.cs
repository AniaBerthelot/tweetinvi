using System.Threading.Tasks;
using Tweetinvi.Core.Client.Validators;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Tweetinvi.Client
{
    /// <summary>
    /// A client providing all the actions relative to the account settings
    /// </summary>
    public interface IAccountSettingsClient
    {
        /// <summary>
        /// Validate all the AccountSettingsClient parameters
        /// </summary>
        IAccountSettingsClientParametersValidator ParametersValidator { get; }

        /// <inheritdoc cref="GetAccountSettings(IGetAccountSettingsParameters)" />
        Task<IAccountSettings> GetAccountSettings();

        /// <summary>
        /// Get the client's account settings
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/manage-account-settings/api-reference/get-account-settings </para>
        /// </summary>
        /// <returns>Account settings</returns>
        Task<IAccountSettings> GetAccountSettings(IGetAccountSettingsParameters parameters);

        /// <summary>
        /// Update the client's account settings
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/manage-account-settings/api-reference/post-account-settings </para>
        /// </summary>
        /// <returns>Updated account settings</returns>
        Task<IAccountSettings> UpdateAccountSettings(IUpdateAccountSettingsParameters parameters);

        /// <summary>
        /// Update the client's account profile
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/manage-account-settings/api-reference/post-account-update_profile </para>
        /// </summary>
        /// <returns>Updated profile</returns>
        Task<IAuthenticatedUser> UpdateProfile(IUpdateProfileParameters parameters);

        /// <inheritdoc cref="UpdateProfileImage(IUpdateProfileImageParameters)" />
        Task UpdateProfileImage(byte[] binary);

        /// <summary>
        /// Update the profile image of the account
        /// <para>Read more : https://dev.twitter.com/rest/reference/post/account/update_profile_image</para>
        /// </summary>
        Task UpdateProfileImage(IUpdateProfileImageParameters parameters);

        /// <inheritdoc cref="UpdateProfileBanner(IUpdateProfileBannerParameters)" />
        Task UpdateProfileBanner(byte[] binary);

        /// <summary>
        /// Update the profile banner of the account
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/manage-account-settings/api-reference/post-account-update_profile_banner </para>
        /// </summary>
        Task UpdateProfileBanner(IUpdateProfileBannerParameters parameters);

        /// <inheritdoc cref="RemoveProfileBanner(IRemoveProfileBannerParameters)" />
        Task RemoveProfileBanner();

        /// <summary>
        /// Remove the profile banner of the account
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/manage-account-settings/api-reference/post-account-remove_profile_banner </para>
        /// </summary>
        Task RemoveProfileBanner(IRemoveProfileBannerParameters parameters);
    }
}