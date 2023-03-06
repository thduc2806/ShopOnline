using AutoMapper;
using Identity.Database;
using Identity.Database.Entities;
using Identity.Database.Enum;
using Identity.Helper;
using Identity.Services.Interface;
using Identity.ViewModel;
using Microsoft.AspNetCore.Identity;
using oShopSolution.Application.Helper;

namespace Identity.Services.Implement
{
    public class AuthenticateService : IdentityServiceBase, IAuthenticateService
    {
        private readonly IMapper _mapper;
        private readonly int TOKEN_MINUTE_TIME_LIFE = 6000000;
        private readonly IAccountService _accountService;
        public AuthenticateService(IdentityDbContext db, UserManager<Users> userManager, IMapper mapper, IAccountService accountService) : base(db, userManager)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<BaseResponse<AuthenticateViewModel>> AuthenticateByEmail(string email, string password)
        {
            var user = LoadRelated(await FindByEmail(email));

            if (user == null)
            {
                return BaseResponse<AuthenticateViewModel>.NotFound();
            }    

            if (IsActivated(user) && IsValidated(user, password))
            {
                var authResponseModel = GetAuthResponseModel(user);

                if(authResponseModel != null)
                {
                    return BaseResponse<AuthenticateViewModel>.Success(authResponseModel);
                }
            }
            return BaseResponse<AuthenticateViewModel>.BadRequest();
        }

        public async Task<BaseResponse<bool>> ValidateAccount(string email, string password)
        {
            var user = await FindByEmail(email);
            if (user == null)
                return BaseResponse<bool>.BadRequest(message: "Email not found!");
            
            if(string.IsNullOrEmpty(user.PasswordHash)) return BaseResponse<bool>.Success(true);
            var result = IsValidated(user, password);

            return result ? BaseResponse<bool>.Success(true) : BaseResponse<bool>.BadRequest(message: "Password is not correct");
        }

        private AuthenticateViewModel GetAuthResponseModel(Users user)
        {

                if (user == null)
                    return null;
                var authModel = _mapper.Map<Users, AuthenticateViewModel>(user);
                var refreshToken = UniqueIDHelper.GenarateRandomString(12);
                user.RefreshToken = refreshToken;
                var saveSucceed = _accountService.UpdateUser(user).Result.IsSuccessStatusCode;
                if (saveSucceed)
                {
                    authModel.RefreshToken = refreshToken;
                    authModel.TokenEffectiveDate = DateTime.Now.ToString();
                    authModel.TokenEffectiveTimeStick = DateTime.Now.Ticks.ToString();
                    authModel.AccountTypeId = ((int)AccountTypeEnum.Admin).ToString();
                    var result = JwtTokenHelper.GenerateJwtTokenModel(authModel, TOKEN_MINUTE_TIME_LIFE);
                    return result;
                }

                return null;
        }


    }
}
