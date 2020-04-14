using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Oryx.UserAccount.DataAccessor;
using Oryx.UserAccount.Model;
using Oryx.UserAccount.Model.UserAccountExtend;
using Oryx.UserAccount.Model.UserBusinessExtend;
using Oryx.Utilities;
using Oryx.ViewModel.CommonAccountUser;
using Oryx.ViewModel.UserInfo;
using Oryxl.Wx.WebApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.UserAccount.Business
{
    public class UserAccountBusiness
    {
        public const string UserAccountSessionkey = "UserAccount";

        public const string AdminUserRoleKey = "Admin";
        public const string SpecialUserRoleKey = "Special";
        public const string GeneralUserRoleKey = "General";
        public UserAccountDataAccessor userAccountAccessor { get; set; }
        public IConfiguration Configuration { get; set; }
        public UserAccountBusiness(UserAccountDataAccessor _userAccountAccessor, IConfiguration _configuration)
        {
            userAccountAccessor = _userAccountAccessor;
            Configuration = _configuration;
        }

        public async Task<UserInfoOutputViewModel> GetUserInfo(Guid userId)
        {
            var userAccountEntry = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == userId, "Profile");
            var result = new UserInfoOutputViewModel
            {
                userId = userAccountEntry.Id,
                avatar = userAccountEntry.Avatar,
                nickName = userAccountEntry.NickName,
                birthDay = userAccountEntry.Profile?.BirthDay ?? default(DateTime),
                location = userAccountEntry.Profile?.Location ?? string.Empty
            };
            return result;
        }
        public async Task<List<string>> GetRoles(string userId)
        {
            Guid _userId;
            if (Guid.TryParse(userId, out _userId))
            {
                var roleEntry = await userAccountAccessor.ListAsync<UserAccountBusinessRole>(x => x.UserAccountId == _userId);
                return roleEntry?.Select(x => x.RoleName).ToList();
            }
            else
            {
                return default(List<string>);
            }
        }

        public async Task<List<string>> GetRoles(Guid userId)
        {
            var roleEntry = await userAccountAccessor.ListAsync<UserAccountBusinessRole>(x => x.UserAccountId == userId);
            return roleEntry?.Select(x => x.RoleName).ToList();
        }

        public async Task UpdateUserInfoNickName(Guid userId, UpdateUserInfoData updateModel)
        {
            var userInfo = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == userId, "Phone");

            userInfo.NickName = updateModel.NickName;
            if (userInfo.Phone == null)
            {
                userInfo.Phone = new UserAccountPhone
                {
                    Phone = updateModel.Phone,
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                };
                await userAccountAccessor.Add(userInfo.Phone);
            }
            else
            {
                userInfo.Phone = new UserAccountPhone
                {
                    Phone = updateModel.Phone,
                };
                await userAccountAccessor.Update(userInfo.Phone);
            }

            userInfo.NickName = updateModel.NickName;
            await userAccountAccessor.Update(userInfo);
        }

        public async Task SetPhone(Guid userId, string phone)
        {
            var userAccountEntry = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == userId, "Phone");
            if (userAccountEntry.Phone == null)
            {
                userAccountEntry.Phone = new UserAccountPhone
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UserAccountId = userId,
                    Phone = phone
                };
            }
            else
            {
                userAccountEntry.Phone.Phone = phone;
            }
            await userAccountAccessor.Update(userAccountEntry);
        }

        public async Task DeleteUser(Guid id)
        {
            await userAccountAccessor.Delete<UserAccountEntry>(x => x.Id == id);
        }

        public async Task AddOrUpdateUser(UserAccountEntry userAccountEntry, string roleName)
        {
            await userAccountAccessor.AddOrUpdate(userAccountEntry);
        }

        public async Task<UserAccountEntry> GetUserAccountEntry(Guid Id)
        {
            return await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == Id, "Phone", "UserNamePwd", "Roles");
        }

        public async Task ChangeRole(Guid id, string roleName)
        {
            var roles = await userAccountAccessor.ListAsync<UserAccountBusinessRole>(x => x.UserAccountId == id);
            if (roles != null)
            {
                foreach (var item in roles)
                {
                    await userAccountAccessor.Delete(item);
                }
            }

            var role = new UserAccountBusinessRole
            {
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                RoleName = roleName,
                UserAccountId = id
            };
            await userAccountAccessor.AddOrUpdate(role);
        }

        public async Task DeleteRole(Guid roleId)
        {
            await userAccountAccessor.Delete<UserAccountBusinessRole>(x => x.Id == roleId);
        }

        public async Task SetTrueName(Guid userId, string name)
        {
            var userAccountEntry = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == userId, "Profile");
            if (userAccountEntry.Profile == null)
            {
                userAccountEntry.Profile = new UserAccountProfile
                {
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    UserAccountId = userId,
                    TrueName = name
                };
            }
            else
            {
                userAccountEntry.Profile.TrueName = name;
            }
            await userAccountAccessor.Update(userAccountEntry);
        }

        public async Task<UserAccountEntry> GetUserAccount(Guid userId, params string[] includeProperty)
        {
            return await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == userId, includeProperty);
        }

        public async Task SetRole(Guid userId, string UserRole)
        {
            var userAccount = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == userId, "Roles");

            if (userAccount.Roles == null)
            {
                var hasRole = userAccount.Roles.Any(x => x.RoleName == UserRole);
                if (!hasRole)
                {
                    userAccount.Roles.Add(new UserAccountBusinessRole
                    {
                        Id = Guid.NewGuid(),
                        RoleName = UserRole,
                        UserAccountId = userId
                    });
                }
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns>注册结果(bool) , 注册信息, 用户ID(string)</returns>
        public async Task<Tuple<bool, string, string>> RegisterUserNamePwd(CaRegisterModel registerModel, string roleType = "")
        {
            if (registerModel == null)
            {
                return Tuple.Create(false, "注册信息为空", string.Empty);
            }

            if (string.IsNullOrEmpty(registerModel.UserName) || string.IsNullOrEmpty(registerModel.Password))
            {
                return Tuple.Create(false, "用户名或密码不能为空", string.Empty);
            }

            var userEntity = await userAccountAccessor.OneAsync<UserAccountUserNamePwd>(x => x.UserName == registerModel.UserName);

            if (userEntity != null)
            {
                return Tuple.Create(false, "用户名已经存在", string.Empty);
            }

            string sha256String = EncryptPwd(registerModel.Password);

            var userAccountEntry = new UserAccountEntry
            {
                Id = Guid.NewGuid(),
                NickName = !string.IsNullOrEmpty(registerModel.NickName) ? registerModel.NickName : RandomName(),
                Avatar = "/images/avarta.png", //RandomAvatar(),
                UserNamePwd = new UserAccountUserNamePwd
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Password = sha256String,
                    UserName = registerModel.UserName
                },
                InviteOrigin = new UserAccountInviteOrign
                {
                    Id = Guid.NewGuid(),
                    InviteKey = registerModel.InviteOrigin
                },
                Phone = new UserAccountPhone
                {
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Phone = registerModel.Mobile
                },
                Roles = new List<UserAccountBusinessRole>
                 {
                       new UserAccountBusinessRole{
                            Id = Guid.NewGuid(),
                            CreateTime = DateTime.Now,
                            RoleName =  string.IsNullOrEmpty(roleType)? GeneralUserRoleKey:roleType
                       }
                 },
                CreateTime = DateTime.Now
            };

            try
            {
                await userAccountAccessor.Add(userAccountEntry);
            }
            catch (Exception exc)
            {
                return Tuple.Create(false, "异常: " + exc.Message, string.Empty);
            }
            return Tuple.Create(true, "注册成功", userAccountEntry.Id.ToString());
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns>注册结果(bool) , 注册信息, 用户ID(string)</returns>
        public async Task<Tuple<bool, string, string>> RegisterUserAdminNamePwd(CaRegisterModel registerModel)
        {
            if (registerModel == null)
            {
                return Tuple.Create(false, "注册信息为空", string.Empty);
            }

            if (string.IsNullOrEmpty(registerModel.UserName) || string.IsNullOrEmpty(registerModel.Password))
            {
                return Tuple.Create(false, "用户名或密码不能为空", string.Empty);
            }

            var userEntity = await userAccountAccessor.OneAsync<UserAccountUserNamePwd>(x => x.UserName == registerModel.UserName);

            if (userEntity != null)
            {
                return Tuple.Create(false, "用户名已经存在", string.Empty);
            }

            string sha256String = EncryptPwd(registerModel.Password);

            var userAccountEntry = new UserAccountEntry
            {
                Id = Guid.NewGuid(),
                NickName = string.IsNullOrEmpty(registerModel.NickName) ? registerModel.NickName : RandomName(),
                Avatar = RandomAvatar(),
                UserNamePwd = new UserAccountUserNamePwd
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Password = sha256String,
                    UserName = registerModel.UserName
                },
                InviteOrigin = new UserAccountInviteOrign
                {
                    Id = Guid.NewGuid(),
                    InviteKey = registerModel.InviteOrigin
                },
                Phone = new UserAccountPhone
                {
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Phone = registerModel.Mobile
                },
                Roles = new List<UserAccountBusinessRole>
                 {
                       new UserAccountBusinessRole{
                            Id = Guid.NewGuid(),
                            CreateTime = DateTime.Now,
                            RoleName = AdminUserRoleKey
                       }
                 },
                CreateTime = DateTime.Now
            };

            try
            {
                await userAccountAccessor.Add(userAccountEntry);
            }
            catch (Exception exc)
            {
                return Tuple.Create(false, "异常: " + exc.Message, string.Empty);
            }
            return Tuple.Create(true, "注册成功", userAccountEntry.Id.ToString());
        }

        public async Task UpdateUserInfo(Guid userId, UpdateUserInfoViewModel updateModel)
        {
            var userInfoEntry = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == userId);
            /*remove old img from qiniu */
            if (!string.IsNullOrEmpty(userInfoEntry.Avatar))
            {
                var avatarUri = new Uri(userInfoEntry.Avatar);
                var saveKey = avatarUri.AbsolutePath.Remove(0);
                await QiniuTool.DeleteImage(new string[] { saveKey });
            }
            /*end */
            userInfoEntry.NickName = updateModel.NickName;
            userInfoEntry.Avatar = updateModel.Avatar;
            if (userInfoEntry.Profile == null)
            {
                userInfoEntry.Profile = new UserAccountProfile { };
            }

            userInfoEntry.Profile.BirthDay = updateModel.BirthDay;
            userInfoEntry.Profile.Location = updateModel.Location;
            await userAccountAccessor.Update(userInfoEntry);
        }

        public async Task UpdateUserAvarta(Guid userId, string imgUrl)
        {
            var userInfoEntry = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.Id == userId);
            userInfoEntry.Avatar = imgUrl;
            await userAccountAccessor.Update(userInfoEntry);
        }

        public async Task UpdateUser(CaRegisterModel model)
        {
            var userAccount = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.UserNamePwd.UserName == model.UserName, "Phone", "UserNamePwd");
            userAccount.NickName = model.NickName;
            userAccount.Phone.Phone = model.Mobile;
            userAccount.UserNamePwd.UserName = model.UserName;

            if (!string.IsNullOrEmpty(model.Password))
            {
                string sha256String = EncryptPwd(model.Password);
                userAccount.UserNamePwd.Password = sha256String;
            }

            await userAccountAccessor.Update(userAccount);
        }
        private string RandomName()
        {
            var nameFile = AppDomain.CurrentDomain.BaseDirectory + "Resource/names.txt";
            var NameList = File.ReadAllLines(nameFile);
            var nameIndex = new Random().Next(NameList.Length);
            return NameList[nameIndex];
        }
        private string RandomAvatar()
        {
            var avatarIndex = new Random().Next(45);
            return $"/assets/imgs/avatar/avatar_{avatarIndex}.jpg";
        }

        public async Task<bool> CheckOldPassword(string userId, string oldPassword)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var _userId = Guid.Parse(userId);

            string sha256String = EncryptPwd(oldPassword);

            return await userAccountAccessor.Any<UserAccountEntry>(x => x.Id == _userId && x.UserNamePwd.Password == oldPassword);
        }

        public async Task<bool> CheckAdminAccount()
        {
            return await userAccountAccessor.Any<UserAccountEntry>(x => x.Roles.Any(c => c.RoleName == AdminUserRoleKey));
        }


        public async Task<bool> ChangePassword(string userId, string newPassword)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var _userId = Guid.Parse(userId);
            string sha256String = EncryptPwd(newPassword);
            var userModel = await userAccountAccessor.OneAsync<UserAccountUserNamePwd>(x => x.UserAccount.Id == _userId);
            userModel.Password = sha256String;
            await userAccountAccessor.Update(userModel);
            return true;
        }

        public async Task<IList<UserAccountEntry>> QueryUserByKey(string queryKey)
        {
            return await userAccountAccessor.ListAsync<UserAccountEntry>(x => x.NickName == queryKey
            || x.UserNamePwd.UserName == queryKey);
        }

        public async Task<Tuple<bool, string, string>> LoginUserNamePwd(CaUserLoginModel loginModel)
        {
            if (loginModel == null)
            {
                return Tuple.Create(false, "注册信息为空", string.Empty);
            }

            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return Tuple.Create(false, "用户名或密码不能为空", string.Empty);
            }

            var userEntity = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.UserNamePwd.UserName == loginModel.UserName, "UserNamePwd");

            if (userEntity == null)
            {
                return Tuple.Create(false, "用户名不存在", string.Empty);
            }

            string sha256String = EncryptPwd(loginModel.Password);

            if (userEntity.UserNamePwd.Password != sha256String)
            {
                return Tuple.Create(false, "密码错误", string.Empty);
            }
            if (!string.IsNullOrEmpty(loginModel.IP))
            {
                var loginLog = new UserAccountLoginLog
                {
                    UserAccountEntryId = userEntity.Id,
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IP = loginModel.IP
                };
                await userAccountAccessor.Add(loginLog);
            }


            return Tuple.Create(true, "登录成功", userEntity.Id.ToString());
        }

        private static string EncryptPwd(string password)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] textBytes = encoding.GetBytes(password);
            Byte[] keyBytes = encoding.GetBytes("LinengnengDadi");

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
        }

        public async Task<bool> HasLogin(HttpContext httpContext)
        {
            try
            {
                //await httpContext.Session?.LoadAsync();
                byte[] storeData;
                if (httpContext.Session.TryGetValue(UserAccountBusiness.UserAccountSessionkey, out storeData))
                {
                    var strValue = Encoding.UTF8.GetString(storeData);
                    if (!string.IsNullOrEmpty(strValue))
                    {
                        return true;
                    }
                    else
                    {
                        return false;//httpContext.Response.Redirect("/Account/Login?return_url=" + httpContext.Request.Path);
                    }
                }
                else
                {
                    return false; //httpContext.Response.Redirect("/Account/Login?return_url=" + httpContext.Request.Path);
                }
            }
            catch (Exception exc)
            {
                return false;
            }
        }
        /// <summary>
        /// 微信网页登录
        /// 兼容服务端渲染与前端渲染页面
        /// </summary>
        /// <returns>用户Id</returns>
        public async Task<string> WxWebLoin(WebAppAuthUserInfo userInfo)
        {
            var targetUserAccount = await userAccountAccessor.OneAsync<UserAccountEntry>(x => x.WeChat.OpenIdMapping
                                .Any(c => c.Source == userInfo.AppId && c.OpenId == userInfo.OpenId));
            if (targetUserAccount == null)
            {
                targetUserAccount = new UserAccountEntry
                {
                    WeChat = new UserAccountWeChat
                    {
                        OpenIdMapping = new List<WeChatAccountOpenIdMapping> {
                              new WeChatAccountOpenIdMapping
                              {
                                    Source =userInfo.AppId,
                                    OpenId =  userInfo.OpenId,
                                    Type   =userInfo .WxLoginType,
                                    UnionId = userInfo.UnionId,
                                    Id = Guid.NewGuid()
                              }
                         },
                        Id = Guid.NewGuid(),
                        UnionId = userInfo.UnionId
                    },
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now
                };
                await userAccountAccessor.Add(targetUserAccount);
            }

            targetUserAccount.Avatar = userInfo.HeadImgUrl;
            targetUserAccount.NickName = userInfo.NickName;
            if (targetUserAccount.Profile == null)
            {
                targetUserAccount.Profile = new UserAccountProfile();
            }
            targetUserAccount.Profile.Location = userInfo.Country + "," + userInfo.Country + "," + userInfo.Province;
            await userAccountAccessor.Update(targetUserAccount);
            return targetUserAccount.Id.ToString();
        }
        public async Task<bool> HasLoginRole(HttpContext httpContext, params string[] targetRoleList)
        {
            try
            {
                //await httpContext.Session?.LoadAsync();
                byte[] storeData;
                if (httpContext.Session.TryGetValue(UserAccountBusiness.UserAccountSessionkey, out storeData))
                {
                    var strValue = Encoding.UTF8.GetString(storeData);
                    if (!string.IsNullOrEmpty(strValue))
                    {
                        var roles = await GetRoles(strValue);
                        if (roles.Any(x => targetRoleList.Any(c => c == x)))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;//httpContext.Response.Redirect("/Account/Login?return_url=" + httpContext.Request.Path);
                    }
                }
                else
                {
                    return false; //httpContext.Response.Redirect("/Account/Login?return_url=" + httpContext.Request.Path);
                }
            }
            catch (Exception exc)
            {
                return false;
            }
        }
    }
}