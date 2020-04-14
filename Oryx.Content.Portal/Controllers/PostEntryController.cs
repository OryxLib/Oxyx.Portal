using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Content.Portal.Filters;
using Oryx.Social.Business;
using Oryx.Social.Model;
using Oryx.Utilities;
using Oryx.ViewModel;
using Oryx.ViewModel.Social.PostEntry;

namespace Oryx.Content.Portal.Controllers
{
    public class PostEntryController : Controller
    {
        private readonly PostEntryBusiness postEntryBusiness;
        public PostEntryController(PostEntryBusiness _postEntryBusiness)
        {
            postEntryBusiness = _postEntryBusiness;
        }
        /// <summary>
        /// 发帖
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> CreatEntry([FromBody]PostEntry model)
        {
            var userIdStr = User.Claims.FirstOrDefault(x => x.Type == "OryxUser").Value;
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Content("Empty User");
            }
            model.UserId = Guid.Parse(userIdStr);
            if (model != null)
            {
                model.CreateTime = DateTime.Now;
                model.TimeStamp = TimeStamp.Get();
            }
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await postEntryBusiness.CreatePostEntry(model);
            });

            return Json(apiMsg);
        }

        /// <summary>
        /// 帖子列表
        /// </summary>
        /// <param name="startTimeStamp"></param>
        /// <param name="endTimeStamp"></param>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> PostEntryList(long startTimeStamp, long endTimeStamp, int skipCount, int pageSize = 10)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            Guid? userId;
            if (!string.IsNullOrEmpty(userIdStr))
            {
                userId = Guid.Parse(userIdStr);
            }
            else
            {
                userId = null;
            }
            startTimeStamp = TimeStamp.Get();
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await postEntryBusiness.GetPostEntryList(userId, startTimeStamp, endTimeStamp, skipCount, pageSize);
            });
            return Json(apiMsg);
        }

        /// <summary>
        /// 帖子详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> PostEntryDetail(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
             {
                 return await postEntryBusiness.GetPostEntryDetail(Id);
             });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetPostEntryComments(Guid postEntryId, int skipCount = 0, int pageSize = 0)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await postEntryBusiness.GetPostEntryComments(postEntryId, skipCount, pageSize);
            });
            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> GetPostEntryCommentsByUser(Guid? userId, int skipCount = 0, int pageSize = 10)
        {
            if (userId == null)
            {
                var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
                userId = Guid.Parse(userIdStr);
            }

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await postEntryBusiness.GetPostEntryCommentsByUserId(userId.Value, skipCount, pageSize);
            });
            return Json(apiMsg);
        }

        /// <summary>
        /// 用户的帖子
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PostEntryListByUserId(Guid? userId, int skipCount = 0, int pageSize = 10)
        {
            if (userId == null)
            {
                var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
                userId = Guid.Parse(userIdStr);
            }

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await postEntryBusiness.GetPostEntryListByUserId(userId.Value, skipCount, pageSize);
            });
            return Json(apiMsg);
        }

        /// <summary>
        /// 发帖的点赞
        /// </summary>
        /// <param name="postEntryId"></param>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PostEntryLiked(Guid postEntryId)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            var request = new StreamReader(HttpContext.Request.Body).ReadToEnd();

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await postEntryBusiness.SetLiked(userId, postEntryId);
            });

            return Json(apiMsg);
        }

        /// <summary>
        /// 评论的点赞
        /// </summary>
        /// <param name="postCommentId"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PostEntryCommentLiked(Guid postCommentId, Guid postId)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await postEntryBusiness.SetPostEntryCommentLiked(userId, postCommentId, postId);
            });

            return Json(apiMsg);
        }

        [TypeFilter(typeof(APIAuthenticationFilter))]
        [Obsolete]
        public async Task<IActionResult> AddPostEntryComment(Guid postId, string Content, Guid parentCommentId)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await postEntryBusiness.SetPostEntryComments(userId, postId, Content, parentCommentId);
            });

            return Json(apiMsg);
        }


        /// <summary>
        /// 发评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PostEntryComment([FromBody]PostEntryCommentViewModel comment)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);
            //comment.UserAccountId = userId;
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await postEntryBusiness.SetPostEntryComments(userId, comment.PostId, comment.Content, comment.ParentCommentId);
            });
            return Json(apiMsg);
        }
        /// <summary>
        /// 实际为 社区发帖的评论
        /// </summary>
        /// <returns></returns>
        [TypeFilter(typeof(APIAuthenticationFilter))]
        public async Task<IActionResult> PostEntryCommentSubComment([FromBody]PostEntryCommentViewModel comments)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            var userId = Guid.Parse(userIdStr);

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await postEntryBusiness.SetPostEntryComments(userId, comments.PostId, comments.Content, comments.ParentCommentId);
            });

            return Json(apiMsg);
        }



        [TypeFilter(typeof(APIAuthenticationWithoutDeniedFilter))]
        public async Task<IActionResult> TopicInfo(string topicName, int skipCount = 0, int pageSize = 10)
        {
            var userIdStr = User.Claims?.FirstOrDefault(x => x.Type == "OryxUser")?.Value;
            Guid? userId;
            if (!string.IsNullOrEmpty(userIdStr))
            {
                userId = Guid.Parse(userIdStr);
            }
            else
            {
                userId = null;
            }

            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await postEntryBusiness.GetTopicInfo(userId, topicName, skipCount, pageSize);
            });

            return Json(apiMsg);
        }
    }
}