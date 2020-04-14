using Oryx.Common.DataAccessor;
using Oryx.Common.Model.SandBox;
using Oryx.UserAccount.Model;
using Oryx.Utilities;
using Oryx.Utilities.JPush;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oryx.Common.Business
{
    public class SandBoxBusiness
    {
        public readonly SandBoxDataAccessor sandBoxDataAccessor;

        public SandBoxBusiness(SandBoxDataAccessor _sandBoxDataAccess)
        {
            sandBoxDataAccessor = _sandBoxDataAccess;
        }

        public async Task SendAlertTo(SandBoxMessage sandBoxMessage)
        {
            await sandBoxDataAccessor.Add(sandBoxMessage);
        }

        public async Task SendAlertToAll(SandBoxMessage sandBoxMessage)
        {
            var totalUserAmount = await sandBoxDataAccessor.Count<UserAccountEntry>();
            var length = Math.Round(totalUserAmount / 100.0) + 1;

            for (int step = 0; step < length; step++)
            {
                var msgBoxList = new List<SandBoxMessage>();
                var userList = await sandBoxDataAccessor.ListAsync<UserAccountEntry>(x => x.Id != sandBoxMessage.FromUserAccountId, step * 100, 100);
                foreach (var user in userList)
                {
                    msgBoxList.Add(new SandBoxMessage
                    {
                        Id = Guid.NewGuid(),
                        CreateTime = DateTime.Now,
                        Content = sandBoxMessage.Content,
                        FromUserAccountId = sandBoxMessage.FromUserAccountId,
                        MessageType = sandBoxMessage.MessageType,
                        TimeStamp = TimeStamp.Get(),
                        ToUserAccountId = user.Id
                    });
                }
                await sandBoxDataAccessor.AddRange(msgBoxList);
            }
        }

        public async Task<IList<SandBoxMessage>> GetMessageListByType(Guid userId, SandBoxMessageType? msgType = null, int skipCount = 0, int pageSize = 10)
        {
            if (msgType != null)
            {
                return await sandBoxDataAccessor.ListOrderByDescendingAsync<SandBoxMessage>(x => x.MessageType == msgType && x.ToUserAccountId == userId, skipCount, pageSize);
            }
            else
            {
                return await sandBoxDataAccessor.ListOrderByDescendingAsync<SandBoxMessage>(x => x.ToUserAccountId == userId, skipCount, pageSize);
            }
        }

        public async Task<IList<SandBoxMessage>> GetMessageList(Expression<Func<SandBoxMessage, bool>> expression, int skipCount = 0, int pageSize = 10)
        {

            return await sandBoxDataAccessor.ListOrderByDescendingAsync(expression, skipCount, pageSize);
        }

        public async Task<int> GetMessageAccount(SandBoxMessageType? msgType = null)
        {
            if (msgType != null)
            {
                return await sandBoxDataAccessor.Count<SandBoxMessage>(x => x.MessageType == msgType);
            }
            else
            {
                return await sandBoxDataAccessor.Count<SandBoxMessage>();
            }
        }

        public async Task<SandBoxMessage> GetMsgById(Guid Id)
        {
            return await sandBoxDataAccessor.OneAsync<SandBoxMessage>(x => x.Id == Id);
        }

        public string GetTypeString(SandBoxMessageType sandBoxMessageType)
        {
            var stringVal = string.Empty;
            switch (sandBoxMessageType)
            {
                case SandBoxMessageType.System:
                    stringVal = "系统通知";
                    break;
                case SandBoxMessageType.UserMessage:
                    stringVal = "用户消息";
                    break;
                case SandBoxMessageType.PostEntryComment:
                    stringVal = "发帖评论";
                    break;
                case SandBoxMessageType.PostEntryCommentSubComment:
                    stringVal = "评论回复";
                    break;
                case SandBoxMessageType.PostEntryLiked:
                    stringVal = "发帖点赞";
                    break;
                case SandBoxMessageType.PostEntryCommentLiked:
                    stringVal = "评论点赞";
                    break;
                case SandBoxMessageType.NotSave:
                default:
                    stringVal = "一般通知";
                    break;
            }
            return stringVal;
        }
    }
}
