using Microsoft.EntityFrameworkCore;
using Oryx.Common.Model;
using Oryx.Common.Model.SandBox;
using Oryx.Content.Model;
using Oryx.Content.Model.SocialExtension;
using Oryx.DataPlatform.Model;
using Oryx.Social.Model;
using Oryx.UserAccount.Model.UserAccountExtend;
using Oryxl.DynamicPage.Model;
using System;
using System.Linq;

namespace Oryx.CommonDbOperation
{
    public class CommonDbContext : DbContext
    {
        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityMethod = typeof(ModelBuilder).GetMethods().Where(x => x.IsGenericMethodDefinition).FirstOrDefault();

            //var entityMethod = methods[0];

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Oryx.DynamicPage.Middleware")))
            {
                var entityTypes = assembly
                  .GetTypes()
                  .Where(t =>
                    t.GetCustomAttributes(typeof(PersistentAttribute), inherit: true)
                    .Any());

                foreach (var type in entityTypes)
                {
                    entityMethod.MakeGenericMethod(type)
                      .Invoke(modelBuilder, new object[] { });
                }
            }


            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=139.224.219.2;database=ShopApp;user=root;password=Linengneng123#;Character Set=utf8", opts =>
                {
                    opts.CommandTimeout(60);
                    opts.EnableRetryOnFailure(3);
                    opts.MaxBatchSize(1000);
                });
                base.OnConfiguring(optionsBuilder);
            }
        }

        public DbSet<UserAccountLoginLog> UserAccountLoginLogs { get; set; }

        public DbSet<ReponseEntry> ReponseEntry { get; set; }

        #region 

        public DbSet<CityInfo> CityInfos { get; set; }

        public DbSet<StoreMapInfo> StoreMapInfos { get; set; }

        #endregion


        #region Common

        public DbSet<SandBoxMessage> SandBoxMessage { get; set; }

        public DbSet<TipEntry> TipEntry { get; set; }

        #endregion


        #region Content 
        public DbSet<Categories> Categories { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<CategoryComment> CategoryComment { get; set; }

        public DbSet<ContentEntry> ContentEntry { get; set; }

        public DbSet<ContentPayLog> ContentPayLog { get; set; }

        public DbSet<ContentUserReadLog> ContentUserReadLog { get; set; }

        public DbSet<FileEntry> FileEntry { get; set; }

        public DbSet<Banners> Banners { get; set; }

        //CommentLikePostEntryExtension 
        public DbSet<CategoryPostEntryMapping> CategoryPostEntryMapping { get; set; }

        public DbSet<ContentPostEntryMapping> ContentPostEntryMapping { get; set; }

        public DbSet<ContentExtPostEntryTopic> ContentExtPostEntryTopic { get; set; }
        #endregion

        #region Social
        public DbSet<ChatLog> ChatLog { get; set; }

        public DbSet<ChatCollection> ChatCollection { get; set; }

        public DbSet<ChatMessage> ChatMessage { get; set; }

        public DbSet<PostEntry> PostEntry { get; set; }

        public DbSet<PostEntryComments> PostEntryComments { get; set; }

        public DbSet<PostEntryFile> PostEntryFile { get; set; }

        public DbSet<PostEntrySocialInfo> PostEntrySocialInfo { get; set; }

        public DbSet<PostEntryUserAnchor> PostEntryUserAnchor { get; set; }

        public DbSet<PostEntryTopic> PostEntryTopic { get; set; }

        public DbSet<PostEntryLikedLog> PostEntryLikedLog { get; set; }

        public DbSet<PostEntryCommentLikedLog> PostEntryCommentLikedLog { get; set; }

        public DbSet<UserSocialRelationship> UserSocialRelationship { get; set; }

        public DbSet<PostEntryTag> PostEntryTags { get; set; }
        #endregion


    }
}
