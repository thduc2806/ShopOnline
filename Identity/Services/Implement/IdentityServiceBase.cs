using Identity.Database;
using Identity.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services.Implement
{
    public class IdentityServiceBase
    {
        protected readonly IdentityDbContext _db;
        protected UserManager<Users> _userManager;

        public IdentityServiceBase(IdentityDbContext db, UserManager<Users> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        #region Admin Method
        protected virtual bool IsActivated(Users user) => user.IsActivated;
        protected virtual bool IsValidated(Users user, string password) => !string.IsNullOrEmpty(password) && _userManager.CheckPasswordAsync(user, password).Result;
        protected virtual async Task<Users> FindByEmail(string email) => await _userManager.Users.Where(k => k.Email == email.ToLower()).FirstOrDefaultAsync();
        protected virtual async Task<Users> Find(string key) => await FindByEmail(key) ?? FindByUserId(key);
        protected virtual async Task<Users> FindUser(string key) => FindByUserId(key);
        protected virtual async Task<Users> FindRelated(string key) => LoadRelated(await Find(key));
        protected virtual Users FindById(string id) => _userManager.Users.FirstOrDefault(k => k.Id.ToString().ToLower() == id.ToLower());
        protected virtual Users FindByUserId(string userId) => _userManager.Users.FirstOrDefault(k => k.UserId.ToString().ToLower() == userId.ToLower());
        protected virtual Users LoadRelated(Users user)
        {
            try
            {
                if (user != null)
                {
                    _db.UserRoles
                       .Include(k => k.Role)
                       .Where(k => k.UserId == user.Id)
                       .Load();
                }

                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }
        protected bool UpdateEffectiveDateToken(Users user, DateTime date)
        {
            if (user != null)
            {
                var now = DateTime.Now;
                var entity = _db.Users.Find(user.Id);
                entity.TokenEffectiveTimeStick = now.Ticks;
                entity.TokenEffectiveDate = now;
                _db.Update(user);
                var result = _db.SaveChanges();
                return result == 1;
            }
            return false;
        }
        protected ulong ToMilliseconds(DateTime dateTime)
        {
            var doubleNum = dateTime.Subtract(DateTime.MinValue).TotalMilliseconds;
            var longNum = Convert.ToUInt64(doubleNum);
            return longNum;
        }
        protected bool UpdateRefreshToken(Users user, string refreshToken)
        {
            if (user != null)
            {
                var entity = _db.Users.Find(user.Id);
                entity.RefreshToken = refreshToken;
                _db.Update(user);
                var result = _db.SaveChanges();
                return result == 1;
            }
            return false;
        }
        //[Obsolete("please use method SyncUserProfileIfNotExisted")]
        //protected bool SyncFacebookUserProfileIfNotExisted(FacebookAuthenticateModel facebookUser)
        //{
        //    try
        //    {
        //        var user = Find(facebookUser.Email);
        //        if (user != null)
        //            return true;
        //        user = new Users(facebookUser.Email, facebookUser.FirstName, facebookUser.LastName);
        //        _db.Users.Add(user);
        //        var result = _db.SaveChanges();
        //        return result == 1;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        protected async Task<bool> SyncUserProfileIfNotExisted(string email, string firstName, string lastName)
        {
            try
            {
                var user = await Find(email);
                if (user != null)
                    return true;
                user = new Users(email, firstName, lastName);
                _db.Users.Add(user);
                var result = _db.SaveChanges();
                return result == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion Base Method
    }
}
