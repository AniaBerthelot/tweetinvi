using System.Threading.Tasks;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Parameters;

namespace Tweetinvi.Controllers.AccountSettings
{
    public interface IAccountSettingsController
    {
        Task<ITwitterResult<IAccountSettingsDTO>> GetAccountSettings(IGetAccountSettingsParameters parameters, ITwitterRequest request);
        Task<ITwitterResult<IAccountSettingsDTO>> UpdateAccountSettings(IUpdateAccountSettingsParameters parameters, ITwitterRequest request);
        Task<ITwitterResult<IUserDTO>> UpdateProfile(IUpdateProfileParameters parameters, ITwitterRequest request);
        Task<ITwitterResult<IUserDTO>> UpdateProfileImage(IUpdateProfileImageParameters parameters, ITwitterRequest request);
        Task<ITwitterResult> UpdateProfileBanner(IUpdateProfileBannerParameters parameters, ITwitterRequest request);
        Task<ITwitterResult> RemoveProfileBanner(IRemoveProfileBannerParameters parameters, ITwitterRequest request);
    }
    
    public class AccountSettingsController : IAccountSettingsController
    {
        private readonly IAccountSettingsQueryExecutor _accountSettingsQueryExecutor;

        public AccountSettingsController(IAccountSettingsQueryExecutor accountSettingsQueryExecutor)
        {
            _accountSettingsQueryExecutor = accountSettingsQueryExecutor;
        }

        public Task<ITwitterResult<IAccountSettingsDTO>> GetAccountSettings(IGetAccountSettingsParameters parameters, ITwitterRequest request)
        {
            return _accountSettingsQueryExecutor.GetAccountSettings(parameters, request);
        }
        
        public Task<ITwitterResult<IAccountSettingsDTO>> UpdateAccountSettings(IUpdateAccountSettingsParameters parameters, ITwitterRequest request)
        {
            return _accountSettingsQueryExecutor.UpdateAccountSettings(parameters, request);
        }

        public Task<ITwitterResult<IUserDTO>> UpdateProfile(IUpdateProfileParameters parameters, ITwitterRequest request)
        {
            return _accountSettingsQueryExecutor.UpdateProfile(parameters, request);
        }

        public Task<ITwitterResult<IUserDTO>> UpdateProfileImage(IUpdateProfileImageParameters parameters, ITwitterRequest request)
        {
            return _accountSettingsQueryExecutor.UpdateProfileImage(parameters, request);
        }

        public Task<ITwitterResult> UpdateProfileBanner(IUpdateProfileBannerParameters parameters, ITwitterRequest request)
        {
            return _accountSettingsQueryExecutor.UpdateProfileBanner(parameters, request);
        }

        public Task<ITwitterResult> RemoveProfileBanner(IRemoveProfileBannerParameters parameters, ITwitterRequest request)
        {
            return _accountSettingsQueryExecutor.RemoveProfileBanner(parameters, request);
        }
    }
}