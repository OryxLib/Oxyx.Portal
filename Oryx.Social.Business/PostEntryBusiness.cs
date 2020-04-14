using Microsoft.EntityFrameworkCore;
using Oryx.Common.Business;
using Oryx.Content.Model;
using Oryx.Social.DataAccessor;
using Oryx.Social.Model;
using Oryx.UserAccount.Model;
using Oryx.Utilities;
using Oryx.ViewModel.Social.PostEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Social.Business
{
    public class PostEntryBusiness
    {
        private readonly PostEntryDataAccessor postEntryDataAccessor;

        private readonly SandBoxBusiness sandBoxBusiness;

        private readonly UserSocialRelationshipDataAccessor userSocialInfoDataAccessor;

        public PostEntryBusiness(
            PostEntryDataAccessor _postEntryDataAccessor,
            SandBoxBusiness _sandBoxBusiness,
            UserSocialRelationshipDataAccessor _userSocialRelationshipDataAccessor)
        {
            postEntryDataAccessor = _postEntryDataAccessor;
            sandBoxBusiness = _sandBoxBusiness;
            userSocialInfoDataAccessor = _userSocialRelationshipDataAccessor;
        }

        public async Task CreatePostEntry(PostEntry model)
        {
            if (!string.IsNullOrEmpty(model.PostEntryTopic))
            {
                var oriModel = await postEntryDataAccessor.OneAsync<PostEntryTopic>(x => x.Text == model.PostEntryTopic);
                if (oriModel == null)
                {
                    await postEntryDataAccessor.Add(new PostEntryTopic
                    {
                        Id = Guid.NewGuid(),
                        CreateTime = DateTime.Now,
                        Text = model.PostEntryTopic,
                        PosterId = model.UserId
                    });
                }
            }
            await postEntryDataAccessor.Add(model);
        }

        public async Task<PostEntry> GetHotPostEntry()
        {
            return await postEntryDataAccessor.All<PostEntry>().Where(x => x.PostEntryFileList.Count > 0).OrderByDescending(x => x.CreateTime).Include(x => x.PostEntryFileList).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取无筛选postEntry
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IList<GetPostEntryListOutput>> GetPostEntryList(Guid? userId, int skipCount = 0, int pageSize = 10)
        {
            var postEntryQuerable = postEntryDataAccessor.All<PostEntry>().OrderByDescending(x => x.TimeStamp);
            return await GetPostEntryCore(userId, postEntryQuerable, skipCount, pageSize);
        }

        /// <summary>
        /// 获取时间戳筛选的PostEntry
        /// </summary>
        /// <param name="startTimeStamp"></param>
        /// <param name="endTimeStamp"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IList<GetPostEntryListOutput>> GetPostEntryList(Guid? userId, long startTimeStamp, long endTimeStamp, int skipCount, int pageSize)
        {
            var postEntryQuerable = postEntryDataAccessor.All<PostEntry>()
               .Where(x => x.TimeStamp <= startTimeStamp && x.TimeStamp >= endTimeStamp)
               .OrderByDescending(x => x.TimeStamp);

            return await GetPostEntryCore(userId, postEntryQuerable, skipCount, pageSize);
        }

        /// <summary>
        /// 根据UserId获取PostEntry
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IList<GetPostEntryListOutput>> GetPostEntryListByUserId(Guid userId, int skipCount, int pageSize)
        {
            var postEntryQuerable = postEntryDataAccessor.All<PostEntry>()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.TimeStamp);

            return await GetPostEntryCore(userId, postEntryQuerable, skipCount, pageSize);
        }
        /// <summary>
        /// 查询postentry的公共方法
        /// 经由特殊的组装, 将postentry的相关信息进行打包
        /// </summary>
        /// <param name="userId">传null 未登录状态下查看 不匹配是否关注</param>
        /// <param name="postEntryQuerable"></param>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<IList<GetPostEntryListOutput>> GetPostEntryCore(Guid? userId, IQueryable<PostEntry> postEntryQuerable, int skipCount, int pageSize)
        {
            var postEntryOutput = await postEntryQuerable
                       .Skip(skipCount)
                       .Take(pageSize)
                       .Include("PostEntryFileList")
                       .Include("PostEntryCommentList")
                       .Select(x => new
                       {
                           PostEntryId = x.Id,
                           PostEntryTopic = x.PostEntryTopic,
                           Content = x.TextContent,
                           PostEntryFileList = x.PostEntryFileList,
                           TimeStamp = x.TimeStamp,
                           LikeNum = x.LikeSum,
                           UserInfoId = x.UserId,
                           CommentNum = x.PostEntryCommentList.Count()
                       })
                       .ToListAsync();

            var userList = postEntryOutput.Select(x => x.UserInfoId).ToList();

            var userQueryList = await postEntryDataAccessor.ListAsync<UserAccountEntry>(x => userList.Contains(x.Id));

            var subscribList = await userSocialInfoDataAccessor.GetRelationshipByUserList(userId, userQueryList);

            return postEntryOutput.Select(x => new GetPostEntryListOutput
            {
                Id = x.PostEntryId,
                PostEntryTopic = x.PostEntryTopic,
                Content = x.Content,
                Voice = x.PostEntryFileList?.FirstOrDefault(c => c.Type == PostEntryFileType.Voice),
                ImageList = x.PostEntryFileList?.Where(c => c.Type == PostEntryFileType.Image || c.Type == PostEntryFileType.Video).ToList(),
                TimeStamp = x.TimeStamp,
                UserInfo = userQueryList.FirstOrDefault(c => c.Id == x.UserInfoId),
                LikeNum = x.LikeNum,
                CommentNum = x.CommentNum,
                IsSubscribed = subscribList[x.UserInfoId]
            }).ToList();
        }

        public async Task<PostEntryTopicInfoViewModel> GetTopicInfo(Guid? userId, string topicName, int skipCount, int pageSize)
        {
            var topicInfo = await postEntryDataAccessor.OneAsync<PostEntryTopic>(x => x.Text == topicName);
            var category = await postEntryDataAccessor.OneAsync<Categories>(x => x.Name == topicName);
            var postetnryCount = await postEntryDataAccessor.Count<PostEntry>(x => x.PostEntryTopic == topicName);
            var postEntryQuerable = postEntryDataAccessor.All<PostEntry>()
                 .Where(x => x.PostEntryTopic == topicName)
                 .OrderByDescending(x => x.TimeStamp);
            var postentryList = await GetPostEntryCore(userId, postEntryQuerable, skipCount, pageSize);
            var newsCount = await postEntryDataAccessor.Count<ContentEntry>(x => x.Tags.Any(c => c.Name == "资讯"));
            var newsList = await postEntryDataAccessor.ListOrderByDescendingAsync<ContentEntry>(x => x.Tags.Any(c => c.Name == topicName), skipCount, pageSize);

            var topicViewModel = new PostEntryTopicInfoViewModel
            {
                TopicPosterId = topicInfo.PosterId,
                TopicPosterName = topicInfo.PosterAccount?.NickName ?? "未知",
                TopicPosterAvatar = topicInfo.PosterAccount?.Avatar ?? "",
                LinkedCartoonId = category?.Id,
                LinkedCartoonName = category?.Name,
                NewsCount = newsCount,
                NewsList = newsList?.ToList(),
                PostEntryCount = postetnryCount,
                PostEntryList = postentryList
            };

            return topicViewModel;
        }

        public async Task<PostEntryDetailViewModel> GetPostEntryDetail(Guid Id)
        {
            var queryResult = await postEntryDataAccessor.All<PostEntry>()
                .Include(x => x.PostEntryCommentList)
                .ThenInclude(x => x.UserAccount)
                 .Include(x => x.PostEntryCommentList)
                .ThenInclude(x => x.ParentComment)
                .ThenInclude(x => x.UserAccount)
                .Where(x => x.Id == Id).Select(x => new
                {
                    x.Id,
                    x.TextContent,
                    x.UserAccountEntry,
                    x.PostEntryTopic,
                    PostEntryCommentList = x.PostEntryCommentList
                                                          .OrderByDescending(c => c.CreateTime)
                                                          .Take(5)
                                                          .Select(c => new
                                                          {
                                                              c.TimeStamp,
                                                              c.UserAccount,
                                                              c.ParentComment,
                                                              ParentPackage = new
                                                              {
                                                                  c.ParentComment.UserAccount
                                                              },
                                                              c.LikeNum,
                                                              c.Content,
                                                              c.Id
                                                          })
                                                          .ToList(),
                    PostEntryCommentListNum = x.PostEntryCommentList.Count(),
                    Voice = x.PostEntryFileList.FirstOrDefault(c => c.Type == PostEntryFileType.Voice),
                    ImageList = x.PostEntryFileList.Where(c => c.Type == PostEntryFileType.Image || c.Type == PostEntryFileType.Video).ToList(),
                    x.TimeStamp,
                    x.LikeSum,
                    x.Order
                }).FirstOrDefaultAsync();

            return new PostEntryDetailViewModel
            {
                Id = queryResult.Id,
                LikeSum = queryResult.LikeSum,
                Order = queryResult.Order,
                ImageList = queryResult.ImageList,
                Video = queryResult.Voice,
                PostEntryCommentList = queryResult.PostEntryCommentList.Select(c => new PostEntryComments
                {
                    Id = c.Id,
                    Content = c.Content,
                    LikeNum = c.LikeNum,
                    ParentComment =
                    c.ParentComment != null ?
                        new PostEntryComments
                        {
                            Id = c.ParentComment.Id,
                            UserAccount = c.ParentPackage.UserAccount,
                            TimeStamp = c.ParentComment.TimeStamp,
                            Content = c.ParentComment.Content,
                            UserAccountId = c.ParentPackage.UserAccount.Id
                        } :
                         null,
                    UserAccount = c.UserAccount,
                    TimeStamp = c.TimeStamp
                }).ToList(),
                PostEntryCommentListNum = queryResult.PostEntryCommentListNum,
                PostEntryTopic = queryResult.PostEntryTopic,
                TextContent = queryResult.TextContent,
                TimeStamp = queryResult.TimeStamp,
                UserInfo = queryResult.UserAccountEntry
            };
        }

        public async Task RemovePostEntry(Guid postId)
        {
            await postEntryDataAccessor.Delete<PostEntry>(x => x.Id == postId);
        }

        public async Task<IList<PostEntryComments>> GetPostEntryComments(Guid postEntryId, int skipCount = 0, int pageSize = 10)
        {
            return await postEntryDataAccessor.ListOrderByDescendingAsync<PostEntryComments>(x => x.PostEntry.Id == postEntryId, skipCount, pageSize, "UserAccount");
        }

        public async Task<IList<PostEntryComments>> GetPostEntryCommentsByUserId(Guid userId
            , int skipCount = 0, int pageSize = 10)
        {
            return await postEntryDataAccessor.ListOrderByDescendingAsync<PostEntryComments>(x => x.UserAccountId == userId, skipCount, pageSize, "UserAccount");
        }

        /// <summary>
        /// 帖子点赞
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="postId"></param>
        /// <param name="likeType"></param>
        /// <returns></returns>
        public async Task SetLiked(Guid userId, Guid postId)
        {
            var postEntry = await postEntryDataAccessor.OneAsync<PostEntry>(x => x.Id == postId);
            var hasLiked = await postEntryDataAccessor.Any<PostEntryLikedLog>(x => x.UserAccountEntryId == userId && x.PostEntryId == postId);
            //1: add new postentrylikelog
            if (!hasLiked)
            {
                postEntry.LikeSum += 1;
                await postEntryDataAccessor.Add(new PostEntryLikedLog
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    PostEntryId = postId,
                    UserAccountEntryId = userId
                });
            }
            else
            {
                postEntry.LikeSum -= 1;
                await postEntryDataAccessor.Delete<PostEntryLikedLog>(x => x.UserAccountEntryId == userId && x.PostEntryId == postId);
            }
        }

        public async Task SetPostEntryComments(Guid userId, Guid? postId, string text, Guid? ParentCommentId)
        {
            await postEntryDataAccessor.Add(new PostEntryComments
            {
                Id = Guid.NewGuid(),
                UserAccountId = userId,
                Content = text,
                CreateTime = DateTime.Now,
                TimeStamp = TimeStamp.Get(),
                PostEntryId = postId,
                ParentCommentId = ParentCommentId
            });
        }

        public async Task RemovePostEntryComment(Guid commentId)
        {
            await postEntryDataAccessor.Delete<PostEntryComments>(x => x.Id == commentId);
        }

        public async Task SetPostEntryCommentLiked(Guid userId, Guid commentId, Guid postEntryId)
        {
            var postEntryCommentModel = await postEntryDataAccessor.OneAsync<PostEntryComments>(x => x.Id == commentId);
            var hasLiked = await postEntryDataAccessor.Any<PostEntryCommentLikedLog>(x => x.UserAccountEntryId == userId && x.PostEntryCommentId == commentId);
            if (!hasLiked)
            {
                postEntryCommentModel.LikeNum += 1;
                await postEntryDataAccessor.Add(new PostEntryCommentLikedLog
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    PostEntryCommentId = commentId,
                    UserAccountEntryId = userId,
                    PostEntryId = postEntryId
                });
            }
            else
            {
                postEntryCommentModel.LikeNum -= 1;
                await postEntryDataAccessor.Delete<PostEntryCommentLikedLog>(x => x.UserAccountEntryId == userId && x.PostEntryCommentId == commentId);
            }
        }
    }
}
