using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Xunit;
using Xunit.Abstractions;
using xUnitinvi.TestHelpers;
#pragma warning disable 618

namespace xUnitinvi.EndToEnd
{
    [Collection("EndToEndTests")]
    public class AccountSettingsEndToEndTests : TweetinviTest
    {
        public AccountSettingsEndToEndTests(ITestOutputHelper logger) : base(logger)
        {
        }

        [Fact]
        public async Task ChangeImagesTestsAsync()
        {
            if (!EndToEndTestConfig.ShouldRunEndToEndTests)
                return;

            // act
            var authenticatedUser = await _protectedClient.Users.GetAuthenticatedUserAsync();
            var profile = File.ReadAllBytes("./tweetinvi-logo-purple.png");
            var banner = File.ReadAllBytes("./banner.jpg");
            await _protectedClient.AccountSettings.UpdateProfileImageAsync(profile);
            await _protectedClient.AccountSettings.UpdateProfileBannerAsync(banner);
            var userAfterAddingBanner = await _protectedClient.Users.GetUserAsync(authenticatedUser);

            await _protectedClient.AccountSettings.RemoveProfileBannerAsync();
            var userAfterRemovingBanner = await _protectedClient.Users.GetUserAsync(authenticatedUser);

            // assert
            Assert.NotEqual(authenticatedUser.ProfileImageUrl, userAfterAddingBanner.ProfileImageUrl);
            Assert.NotEqual(authenticatedUser.ProfileBannerURL, userAfterAddingBanner.ProfileBannerURL);
            Assert.Null(userAfterRemovingBanner.ProfileBannerURL);
        }

        [Fact]
        public async Task AccountProfileTestsAsync()
        {
            if (!EndToEndTestConfig.ShouldRunEndToEndTests)
                return;

            var initialProfile = await _protectedClient.Users.GetAuthenticatedUserAsync();

            // act
            var updatedProfileParameters = new UpdateProfileParameters
            {
                Name = $"{initialProfile.Name}_42",
                Description = "new_desc",
                Location = "new_loc",
                WebsiteUrl = "https://www.twitter.com/artwolkt",
                ProfileLinkColor = "F542B9"
            };

            var newProfile = await _protectedClient.AccountSettings.UpdateProfileAsync(updatedProfileParameters);

            var restoredProfileParameters = new UpdateProfileParameters
            {
                Name = initialProfile.Name,
                Description = initialProfile.Description,
                Location = initialProfile.Location,
                WebsiteUrl = initialProfile.Url,
                ProfileLinkColor = initialProfile.ProfileLinkColor
            };

            var restoredProfile = await _protectedClient.AccountSettings.UpdateProfileAsync(restoredProfileParameters);

            // assert
            Assert.Equal($"{initialProfile.Name}_42", newProfile.Name);
            Assert.NotEqual(initialProfile.Name, newProfile.Name);
            Assert.Equal(initialProfile.Name, restoredProfile.Name);

            Assert.Equal("new_desc", newProfile.Description);
            Assert.NotEqual(initialProfile.Description, updatedProfileParameters.Description);
            Assert.Equal(initialProfile.Description, restoredProfile.Description);

            Assert.Equal("new_loc", newProfile.Location);
            Assert.NotEqual(initialProfile.Location, newProfile.Location);
            Assert.Equal(initialProfile.Location, restoredProfile.Location);

            // cannot test url equality as twitter uses tiny url
            Assert.NotEqual(initialProfile.Url, newProfile.Url);
            Assert.Equal(initialProfile.Url, restoredProfile.Url);

            Assert.Equal("F542B9", newProfile.ProfileLinkColor);
            Assert.NotEqual(initialProfile.ProfileLinkColor, newProfile.ProfileLinkColor);
            Assert.Equal(initialProfile.ProfileLinkColor, restoredProfile.ProfileLinkColor);
        }

        [Fact]
        public async Task AccountSettingsTestsAsync()
        {
            if (!EndToEndTestConfig.ShouldRunEndToEndTests)
                return;

            var initialSettings = await _protectedClient.AccountSettings.GetAccountSettingsAsync();

            var newSettings = new UpdateAccountSettingsParameters
            {
                DisplayLanguage = DisplayLanguages.Spanish,
                SleepTimeEnabled = !initialSettings.SleepTimeEnabled,
                StartSleepHour = 23,
                EndSleepHour = 7,
                TimeZone = TimeZoneFromTwitter.Bangkok.ToTZinfo(),
                TrendLocationWoeid = 580778
            };

            var updatedSettings = await _protectedClient.AccountSettings.UpdateAccountSettingsAsync(newSettings);

            var recoveredSettings = await _protectedClient.AccountSettings.UpdateAccountSettingsAsync(new UpdateAccountSettingsParameters
            {
                DisplayLanguage = initialSettings.Language,
                TimeZone = initialSettings.TimeZone.TzinfoName,
                SleepTimeEnabled = initialSettings.SleepTimeEnabled,
                StartSleepHour = initialSettings.StartSleepHour,
                EndSleepHour = initialSettings.EndSleepHour,
                TrendLocationWoeid = initialSettings.TrendLocations.FirstOrDefault()?.WoeId ?? 1
            });

            // assert
            Assert.Equal(Language.Spanish, updatedSettings.Language);
            Assert.NotEqual(initialSettings.Language, updatedSettings.Language);
            Assert.Equal(initialSettings.Language, recoveredSettings.Language);

            Assert.Equal(TimeZoneFromTwitter.Bangkok.ToTZinfo(), updatedSettings.TimeZone.Name);
            Assert.NotEqual(initialSettings.TimeZone.Name, updatedSettings.TimeZone.Name);
            Assert.Equal(initialSettings.TimeZone.Name, recoveredSettings.TimeZone.Name);

            Assert.Equal(updatedSettings.SleepTimeEnabled, !initialSettings.SleepTimeEnabled);
            Assert.Equal(initialSettings.SleepTimeEnabled, recoveredSettings.SleepTimeEnabled);

            Assert.Equal(23, updatedSettings.StartSleepHour);
            Assert.NotEqual(initialSettings.StartSleepHour, updatedSettings.StartSleepHour);
            Assert.Equal(initialSettings.StartSleepHour, recoveredSettings.StartSleepHour);

            Assert.Equal(7, updatedSettings.EndSleepHour);
            Assert.NotEqual(initialSettings.StartSleepHour, updatedSettings.StartSleepHour);
            Assert.Equal(initialSettings.EndSleepHour, recoveredSettings.EndSleepHour);

            Assert.Equal(580778, updatedSettings.TrendLocations[0].WoeId);
            Assert.NotEqual(initialSettings.TrendLocations?.FirstOrDefault()?.WoeId, updatedSettings.TrendLocations[0].WoeId);

            if (initialSettings.TrendLocations != null)
            {
                Assert.Equal(initialSettings.TrendLocations[0].WoeId, recoveredSettings.TrendLocations[0].WoeId);
            }
        }
    }
}