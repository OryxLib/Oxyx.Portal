using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oryx.UserAccount.Business;
using Oryx.ViewModel;
using Oryx.ViewModel.CommonAccountUser;

namespace Oryx.Content.Portal.Controllers
{
    public class PortalUserController : Controller
    {
        private readonly UserAccountBusiness userAccountBusiness;

        public PortalUserController(UserAccountBusiness _userAccountBusiness)
        {
            userAccountBusiness = _userAccountBusiness;
        }

        public async Task<IActionResult> Login(CaUserLoginModel loginModel)
        {
            var result = new ApiMessage();
            var loginResult = await userAccountBusiness.LoginUserNamePwd(loginModel);
            if (loginResult.Item1)
            {
                var str = JsonConvert.SerializeObject(loginResult.Item3);
                HttpContext.Session.SetString(UserAccountBusiness.UserAccountSessionkey, str);
                await HttpContext.Session.CommitAsync();
                result.Success = true;
                result.Message = "登录成功";
            }
            else
            {
                result.Success = false;
                result.Message = loginResult.Item2;
            }
            return Json(result);
        }

        public async Task<IActionResult> Register(CaRegisterModel registerModel)
        {
            var result = new ApiMessage();

            var registerResult = await userAccountBusiness.RegisterUserNamePwd(registerModel);

            result.Success = registerResult.Item1;
            result.Message = registerResult.Item2;

            return Json(result);
        }
    }
}