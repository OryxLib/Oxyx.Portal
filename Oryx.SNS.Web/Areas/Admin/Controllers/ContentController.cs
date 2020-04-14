using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.SNS.Web.Filters;
using Oryx.Social.Business;
using Oryx.ViewModel;
using Oryx.ViewModel.Social.PostEntry;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        private readonly ContentBusiness contentBusiness;
        private readonly BannersBusienss bannersBusienss;
        private readonly PostEntryBusiness postEntryBusiness;
        public ContentController(ContentBusiness _contentBusiness,
            BannersBusienss _bannersBusienss,
            PostEntryBusiness _postEntryBusiness)
        {
            contentBusiness = _contentBusiness;
            bannersBusienss = _bannersBusienss;
            postEntryBusiness = _postEntryBusiness;
        }

        public async Task<IActionResult> GetTags()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                //contentBusiness.GetCartoonTags();
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetBanners()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
           {
               var topNews = await contentBusiness.GetLatestNews();
               var topPostEntry = await postEntryBusiness.GetHotPostEntry();
               var bannerList = await bannersBusienss.GetList() ?? new List<Banners>();
               if (topNews != null)
               {
                   bannerList.Add(new Banners
                   {
                       CreateTime = topNews.CreateTime,
                       Image = topNews.MediaResource?[0]?.ActualPath,
                       Link = "news:" + topNews.Id,
                       Title = topNews.Title
                   });
               }
               if (topPostEntry != null)
               {
                   bannerList.Add(new Banners
                   {
                       CreateTime = topPostEntry.CreateTime,
                       Image = topPostEntry.PostEntryFileList?[0]?.ActualPath,
                       Link = "postentry:" + topPostEntry.Id,
                       Title = topPostEntry.TextContent
                   });
               }
               return bannerList;
           });
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetNewsest()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetLatestNews();
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetHotpot()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetHotpotContent();
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetRecommand()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetRecommandContent();
            });
            return Json(apiMsg);
        }

        /// <summary>
        /// 获取默认的列表 及 分类
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetTopList()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetTopList();
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetCategories()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetTopCategory(string.Empty);
            });
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> GetCategoryByCateId(Guid cid)
        {
            var userIdStr = User.Claims.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            ApiMessage apiMsg;
            if (string.IsNullOrEmpty(userIdStr))
            {
                apiMsg = await ApiMessage.Wrap(async () =>
                {
                    return await contentBusiness.GetCategoryById(cid);
                });
            }
            else
            {
                var userId = Guid.Parse(userIdStr);
                apiMsg = await ApiMessage.Wrap(async () =>
                {
                    return await contentBusiness.GetCategoryByIdWithUserReadLog(cid, userId);
                });
            }
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetCategoryByTagName(string tagName, int pageIndex, int pageSize)
        {
            var apiMsg = await ApiMessage.Wrap(async () => await contentBusiness.GetCategoryByTagName(tagName, pageIndex * pageSize, pageSize));
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetListByCatId(Guid cid)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetContentListByCategoryId(cid);
            });
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> GetContentDetail(Guid Id)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            ContentEntry apiMsg;
            if (!string.IsNullOrEmpty(userIdStr))
            {
                apiMsg = await contentBusiness.GetContentByIdSetLog(Id, userIdStr);
            }
            else
            {
                apiMsg = await contentBusiness.GetContentById(Id);
            }


            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> GetContentUserReadLog(Guid? userId, int skipCount = 0, int pageSize = 10)
        {
            if (userId == null)
            {
                var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
                userId = Guid.Parse(userIdStr);
            }
            var apiMsg = await contentBusiness.GetContentUserReadLog(userId.Value, skipCount, pageSize);
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> RecentCategory()
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetContentUserReadLog(userId, 0, 3);
            });
            return Json(apiMsg);
        }

        #region 变更漫画评论与章节评论到 社区内容

        /// <summary>
        /// 漫画总评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PostCategoryComment([FromBody]CategoryComment comment)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            comment.UserAccountId = userId;
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await contentBusiness.CreateCategoryCommentExtPostEntry(comment);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetCategoryComment(Guid cateId, int skipCount, int pageSize)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetCategoryCommentExtPostEntry(cateId, skipCount, pageSize);
            });

            return Json(apiMsg);
        }

        /// <summary>
        /// 实际为 社区发帖的评论
        /// </summary>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PostCategoryCommentSubComment([FromBody]PostEntryCommentViewModel comments)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await postEntryBusiness.SetPostEntryComments(userId, comments.PostId, comments.Content, comments.ParentCommentId);
            });

            return Json(apiMsg);
        }

        /// <summary>
        /// 漫画章节评论
        /// </summary>
        /// <param name="_commentModel"></param>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PostComments(Comment _commentModel)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await contentBusiness.CreateComment(_commentModel);
            });
            return Json(apiMsg);
        }

        public async Task<IActionResult> GetCommentsList(Guid Id, int skipCount = 0, int pageSize = 10)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetComments(Id, skipCount, pageSize);
            });
            return Json(apiMsg);
        }

        #endregion

        public async Task<IActionResult> GetNews(int skipCount, int pageSize)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetNews(skipCount, pageSize);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetNewsDetail(Guid id)
        {

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await contentBusiness.GetNewsDetail(id);
            });

            return Json(apiMsg);
        }
    }
}