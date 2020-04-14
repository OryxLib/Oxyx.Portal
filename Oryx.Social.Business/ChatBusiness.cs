using Oryx.Social.DataAccessor;
using Oryx.Social.Model;
using Oryx.Utilities;
using Oryx.ViewModel.Social.Chat;
using Social.WebSocket;
using System;
using Oryx.CommonDbOperation;
using System.Threading.Tasks;
using Oryx.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Oryx.UserAccount.Model;
using Newtonsoft.Json;

namespace Oryx.Social.Business
{
    public class ChatBusiness
    {
        private readonly ChatLogDataAccessor chatLogDataAccessor;
        private readonly ChatMessageDataAccessor chatMessageDataAccessor;
        private readonly SocialMsgManager socialMsgManager;
        public ChatBusiness(ChatLogDataAccessor _chatLogDataAccessor,
            ChatMessageDataAccessor _chatMessageDataAccessor,
            SocialMsgManager _socialMsgManager)
        {
            chatLogDataAccessor = _chatLogDataAccessor;
            chatMessageDataAccessor = _chatMessageDataAccessor;
            socialMsgManager = _socialMsgManager;
        }

        public async Task<List<ChatMessageOutputData>> GetChatList(Guid userId)
        {
            var resultData = await chatLogDataAccessor.Query<ChatLog>(x => x.ChatCollection.Any(c => c.UserId == userId))
                .Include("ChatMessageList.FromUser").Select(x => new
                {
                    ChatLogId = x.Id,
                    UserCollection = x.ChatCollection.Select(c => c.UserAccountEntry).ToList(),
                    LastMessage = x.ChatMessageList.OrderByDescending(c => c.CreateTime).FirstOrDefault()
                }).ToListAsync();

            var chLst = new List<ChatMessageOutputData>();
            resultData.ForEach(item =>
            {
                chLst.Add(new ChatMessageOutputData
                {
                    ChatLogId = item.ChatLogId,
                    ToUserId = item.UserCollection.Find(x => x.Id != userId)?.Id,
                    ToUserInfo = item.UserCollection.FirstOrDefault(x => x.Id != userId),
                    ChatMessage = item.LastMessage,
                    UserAccountCollectoin = item.UserCollection
                });
            });
            return chLst;
        }


        public async Task<IList<ChatMessage>> GetChatHistory(Guid toUserId, Guid fromUserId, int skipCount, int pageSize)
        {
            return await chatLogDataAccessor.ListAsync<ChatMessage>(x =>
            x.ChageLog.ChatCollection.Any(c => c.UserId == toUserId) &&
            x.ChageLog.ChatCollection.Any(c => c.UserId == fromUserId), skipCount, pageSize
            );
        }

        /// <summary>
        /// 发送消息逻辑, 如果用户不在线就记录未读
        /// </summary>
        /// <param name="msg"></param>
        public async Task SendMessage(TextChatMessageViewModel msg)
        {
            var userOnline = socialMsgManager.HasOnline(msg.ToId.ToString());

            var chatRelationShip = await chatLogDataAccessor.OneAsync<ChatLog>(x =>
             x.ChatCollection.Any(c => c.UserId == msg.FromId)
             && x.ChatCollection.Any(c => c.UserId == msg.ToId), "ChatCollection", "ChatMessageList");

            if (chatRelationShip == null)
            {
                chatRelationShip = new ChatLog
                {
                    Id = Guid.NewGuid(),
                    ChatCollection = new List<ChatCollection>
                    {
                        new ChatCollection{
                            Id= Guid.NewGuid(),
                            CreateTime =DateTime.Now,
                            UserId = msg.FromId
                        },
                         new ChatCollection{
                             Id= Guid.NewGuid(),
                             CreateTime = DateTime.Now,
                            UserId = msg.ToId
                         }
                    }
                };

                await chatLogDataAccessor.Add(chatRelationShip);
            }

            var msgModel = new ChatMessage();
            msgModel.MessageContent = msg.Content;
            msgModel.TimeStamp = TimeStamp.Get();
            msgModel.MsgType = MessageType.Text;
            msgModel.ChageLog = chatRelationShip;
            msgModel.FromUserId = msg.FromId;
            msgModel.ToUserId = msg.ToId;
            msgModel.CreateTime = DateTime.Now;
            if (userOnline)
            {
                msgModel.IsReaded = true;
            }
            else
            {
                msgModel.IsReaded = false;
            }
            await chatLogDataAccessor.Add(msgModel);

            //当前
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            var contentJson = JsonConvert.SerializeObject(msg, jsonSetting);
            socialMsgManager.SendMssage(msg.ToId.ToString(), msg.Content);
        }

        public async Task<int> GetUnReadMsgNum(Guid userId)
        {
            return await chatMessageDataAccessor.All<ChatMessage>().Where(x => !x.IsReaded && x.ToUserId == userId).CountAsync();
        }
    }
}
