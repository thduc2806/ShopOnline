using AutoMapper;
using Identity.Database;
using Identity.Database.Entities;
using Identity.Helper;
using Identity.Services.Interface;
using Identity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using oShopSolution.Application.Helper;

namespace Identity.Services.Implement
{
    public class AccountService : IdentityServiceBase, IAccountService
    {
        private readonly IMapper _mapper;
        public AccountService(IdentityDbContext db, UserManager<Users> userManager, IMapper mapper) : base(db, userManager)
        {
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> UpdateUser(Users user)
        {
            try
            {
                if (user == null)
                    return BaseResponse<bool>.BadRequest();
                var userDb = await Find(user.Email);
                userDb.InjectFrom(user);
                _db.Update(userDb);
                var saveSucceed = await _db.SaveChangesAsync();
                if (saveSucceed == 1)
                    return BaseResponse<bool>.Success(true);
                return BaseResponse<bool>.NotFound();
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.InternalServerError(message: ex.Message, fullMsg: ex.StackTrace);
            }
        }

        public BaseResponse<RegisterResultModel> Register(RegisterModel model)
        {
            if (model == null)
            {
                return BaseResponse<RegisterResultModel>.BadRequest(new RegisterResultModel
                {
                    ConfirmEmailToken = string.Empty,
                    IsSuccess = false,
                    UserId = string.Empty,
                });
            }
            var user = new Users(model.Email, model.FirstName ?? string.Empty, model.LastName ?? string.Empty)
            {
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            var createResult = _userManager.CreateAsync(user, model.Password).Result;

            if (createResult.Succeeded)
            {
                var RoleId = model.RoleId.ToInt32();
                if (RoleId > 0)
                {
                    GrantRolesToUser(user.Email, RoleId);
                }

                return BaseResponse<RegisterResultModel>.Success(new RegisterResultModel
                {
                    ConfirmEmailToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result,
                    IsSuccess = true,
                    UserId = user.UserId.ToString()
                });
            }

            string msesage = string.Join(";", createResult.Errors.Select(k => k.Description).ToList());
            return BaseResponse<RegisterResultModel>.BadRequest(message: msesage);
        }

        public BaseResponse<bool> GrantRolesToUser(string email, int roleId)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            var role = _db.Roles.AsNoTracking().FirstOrDefault(k => k.Id == roleId) ?? new Roles();
            var roles = new List<Roles>() {
                    role
                };
            var actionResult = GrantRolesToUser(user, roles);
            return actionResult;
        }

        public BaseResponse<bool> GrantRolesToUser(Users user, List<Roles> roles)
        {
            if (user == null || roles == null || roles.Count == 0)
                return BaseResponse<bool>.BadRequest();
            var userRoles = new List<UserRoles>();
            foreach (var r in roles)
            {
                userRoles.Add(new UserRoles()
                {
                    UserId = user.Id,
                    RoleId = r.Id
                });
            }
            _db.UserRoles.AddRange(userRoles);
            var actionResult = _db.SaveChanges();
            if (actionResult == 1)
            {
                return BaseResponse<bool>.Success(true);
            }
            return BaseResponse<bool>.BadRequest();
        }

        public BaseResponse<bool> CheckEmailExist(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                return BaseResponse<bool>.Success(true, message: "Email can be use");
            }
            else
                return BaseResponse<bool>.BadRequest(message: "Email already exists");
        }

        //public async Task<BaseResponse<AuthenViewModel>> GetUserProfile(string key)
        //{
        //    Users user = await Find(key);
        //    if(user == null)
        //    {
        //        return BaseResponse<AuthenViewModel>.NotFound();
        //    }
        //    LoadRelated(user);
        //    var model = _mapper.Map<Users, AuthenViewModel>(user);
        //    return BaseResponse<AuthenViewModel>.Success(model);
        //}
    }
}
