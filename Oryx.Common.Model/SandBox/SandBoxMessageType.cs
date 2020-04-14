using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Common.Model.SandBox
{
    public enum SandBoxMessageType
    {
        //系统消息
        System,
        //用户消息
        UserMessage, 
        //发帖被评论
        PostEntryComment,
        //发帖评论被评论
        PostEntryCommentSubComment,
        //发帖被点赞
        PostEntryLiked,
        //发帖评论被点赞
        PostEntryCommentLiked,
        //不存储在消息
        NotSave
    }
}
