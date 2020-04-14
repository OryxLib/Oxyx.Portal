using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.PayLog.Model;
using Oryx.UserAccount.Business;

namespace Oryx.Content.Portal.Pages.Contents
{
    public class ContentDetailModel : PageModel
    {
        UserAccountBusiness userAccountBusiness { get; set; }
        ContentBusiness contentBusiness;
        public ContentEntry contentEntry { get; set; }
        public string NextCid { get; set; }
        public string PrevCid { get; set; }

        public bool NeedPay { get; set; }

        public ContentDetailModel(ContentBusiness _contentBusiness, UserAccountBusiness _userAccountBusiness)
        {
            contentBusiness = _contentBusiness;
            userAccountBusiness = _userAccountBusiness;
        }

        public async Task OnGet(Guid? cid)
        {
            await HttpContext.Session.LoadAsync();
            var userId = HttpContext.Session.GetString(UserAccountBusiness.UserAccountSessionkey);
            if (cid != null)
            {
                contentEntry = await contentBusiness.GetContentById(cid.Value);
                if (contentEntry.Order > 1)
                {
                    if (string.IsNullOrEmpty(userId))
                    {
                        NeedPay = true;
                    }
                    else
                    {
                        var _userId = Guid.Parse(userId);
                        var payapiLog = userAccountBusiness.userAccountAccessor.All<PayAPILog>().FirstOrDefault(x => x.UserAccountId == _userId);
                        if (payapiLog == null)
                        {
                            NeedPay = true;
                        }
                        else
                        {
                            if (payapiLog.EndTime == null || payapiLog.EndTime < DateTime.Now)
                            {
                                NeedPay = true;
                            }
                        }
                    }
                }
                NextCid = await contentBusiness.GetNextContentId(contentEntry);
                PrevCid = await contentBusiness.GetPrevContentId(contentEntry);
            }
            else
            {
                NotFound();
            }
        }
    }
}