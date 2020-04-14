using JiebaNet.Segmenter;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Oryx.JiebaLucene.NetCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace Oryx.LuncTest
{

    class Program
    {
        static void Main(string[] args)
        {
            //var analyzer = new JieBaAnalyzer(TokenizerMode.Default);
            var IndexWriterConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, new JieBaAnalyzer(TokenizerMode.Default));

            var directory = FSDirectory.Open(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Lucene"));
            var indexWriter = new IndexWriter(directory, IndexWriterConfig);
            var document = new Document();


            var fieldList = new List<Field>();
            //var test = new StringField("id", "22", Field.Store.YES);
            //var test = new StringField("id", "22", Field.Store.YES);
            var fieldType = new FieldType();

            //var newFeild = new Field("id", "22", Field.Store.YES, Field.Index.ANALYZED);
            //var newFeild2 = new Field("soc", "呵呵", Field.Store.YES, Field.Index.ANALYZED);
            //var newFeild3 = new Field("shot", "内容分类标准以及为读者提供的任何信息", Field.Store.YES, Field.Index.ANALYZED);
            //var newFeild4 = new Field("content", "《人民日报》（电子版）的一切内容(包括但不限于文字、图片、PDF、图表、标志、标识、商标、版面设计、专栏目录与名称、内容分类标准以及为读者提供的任何信息)仅供人民网读者阅读、学习研究使用，未经人民网股份有限公司及/或相关权利人书面授权，任何单位及个人不得将《人民日报》（电子版）所登载、发布的内容用于商业性目的，包括但不限于转载、复制、发行、制作光盘、数据库、触摸展示等行为方式，或将之在非本站所属的服务器上作镜像。否则，人民网股份有限公司将采取包括但不限于网上公示、向有关部门举报、诉讼等一切合法手段，追究侵权者的法律责任。《人民日报》（电子版）的一切内容(包括但不限于文字、图片、PDF、图表、标志、标识、商标、版面设计、专栏目录与名称、内容分类标准以及为读者提供的任何信息)仅供人民网读者阅读、学习研究使用，未经人民网股份有限公司及/或相关权利人书面授权，任何单位及个人不得将《人民日报》（电子版）所登载、发布的内容用于商业性目的，包括但不限于转载、复制、发行、制作光盘、数据库、触摸展示等行为方式，或将之在非本站所属的服务器上作镜像。否则，人民网股份有限公司将采取包括但不限于网上公示、向有关部门举报、诉讼等一切合法手段，追究侵权者的法律责任。", Field.Store.YES, Field.Index.ANALYZED);
            //fieldList.Add(newFeild);
            //fieldList.Add(newFeild2);
            //fieldList.Add(newFeild3);
            //fieldList.Add(newFeild4);
            fieldList.Add(new TextField("id", "22", Field.Store.YES));
            fieldList.Add(new TextField("soc", "呵呵", Field.Store.YES));
            fieldList.Add(new TextField("shot", "内容分类标准以及为读者提供的任何信息", Field.Store.YES));
            fieldList.Add(new TextField("content", "《人民日报》（电子版）的一切内容(包括但不限于文字、图片、PDF、图表、标志、标识、商标、版面设计、专栏目录与名称、内容分类标准以及为读者提供的任何信息)仅供人民网读者阅读、学习研究使用，未经人民网股份有限公司及/或相关权利人书面授权，任何单位及个人不得将《人民日报》（电子版）所登载、发布的内容用于商业性目的，包括但不限于转载、复制、发行、制作光盘、数据库、触摸展示等行为方式，或将之在非本站所属的服务器上作镜像。否则，人民网股份有限公司将采取包括但不限于网上公示、向有关部门举报、诉讼等一切合法手段，追究侵权者的法律责任。《人民日报》（电子版）的一切内容(包括但不限于文字、图片、PDF、图表、标志、标识、商标、版面设计、专栏目录与名称、内容分类标准以及为读者提供的任何信息)仅供人民网读者阅读、学习研究使用，未经人民网股份有限公司及/或相关权利人书面授权，任何单位及个人不得将《人民日报》（电子版）所登载、发布的内容用于商业性目的，包括但不限于转载、复制、发行、制作光盘、数据库、触摸展示等行为方式，或将之在非本站所属的服务器上作镜像。否则，人民网股份有限公司将采取包括但不限于网上公示、向有关部门举报、诉讼等一切合法手段，追究侵权者的法律责任。", Field.Store.YES));

            indexWriter.AddDocument(fieldList);
            indexWriter.Commit();


            while (true)
            {

                // 1、创建Directory
                //var directory = FSDirectory.Open(FileSystems.getDefault().getPath(INDEX_PATH));
                // 2、创建IndexReader
                var directoryReader = DirectoryReader.Open(directory);
                // 3、根据IndexReader创建IndexSearch
                IndexSearcher indexSearcher = new IndexSearcher(directoryReader);

                var queryK = Console.ReadLine();

                // MultiFieldQueryParser表示多个域解析， 同时可以解析含空格的字符串，如果我们搜索"上海 中国" 
                var analyzer = new JieBaAnalyzer(TokenizerMode.Search);
                String[] fields = { "soc", "content" };
                Occur[] clauses = { Occur.SHOULD, Occur.SHOULD };
                Query multiFieldQuery = MultiFieldQueryParser.Parse(LuceneVersion.LUCENE_48, queryK, fields, clauses, analyzer);

                var bb = new Lucene.Net.Search.TermQuery(new Term("shot", queryK));

                var fuzzy = new FuzzyQuery(new Term("content", queryK));
                // 5、根据searcher搜索并且返回TopDocs
                TopDocs topDocs = indexSearcher.Search(fuzzy, 100); // 搜索前100条结果
                Console.WriteLine("找到: " + topDocs.TotalHits);



                QueryScorer scorer = new QueryScorer(fuzzy, "content");
                // 自定义高亮代码
                SimpleHTMLFormatter htmlFormatter = new SimpleHTMLFormatter("<span style=\"backgroud:red\">", "</span>");
                Highlighter highlighter = new Highlighter(htmlFormatter, scorer);
                //highlighter.set(new SimpleSpanFragmenter(scorer));

                foreach (var doc in topDocs.ScoreDocs)
                {
                    var returnDoc = indexSearcher.Doc(doc.Doc);
                    //Console.WriteLine("soc : " + returnDoc.Get("soc"));
                    var resultHiligh = highlighter.GetBestFragments(analyzer, "content", returnDoc.Get("content"), 3);
                    Console.WriteLine(string.Join("", resultHiligh));
                }
                //Console.WriteLine("go... press enter ");
                //Console.ReadLine();
            }

            //            valindexConfig: IndexWriterConfig = new IndexWriterConfig(new StandardAnalyzer());

            //            indexConfig.setOpenMode(IndexWriterConfig.OpenMode.CREATE_OR_APPEND)

            ////  indexConfig.setInfoStream(System.out)

            //            val directory:Directory = FSDirectory.open(Paths.get(indexPath))

            //val indexWriter:IndexWriter = new IndexWriter(directory, indexConfig)


            var segmenter = new JiebaSegmenter();
            var segments = segmenter.Cut("我来到北京清华大学", cutAll: true);
            Console.WriteLine("【全模式】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("我来到北京清华大学");  // 默认为精确模式
            Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("他来到了网易杭研大厦");  // 默认为精确模式，同时也使用HMM模型
            Console.WriteLine("【新词识别】：{0}", string.Join("/ ", segments));

            segments = segmenter.CutForSearch("小明硕士毕业于中国科学院计算所，后在日本京都大学深造"); // 搜索引擎模式
            Console.WriteLine("【搜索引擎模式】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("结过婚的和尚未结过婚的");
            Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("北京大学生喝进口红酒");
            Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("在北京大学生活区喝进口红酒");
            Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("腾讯视频致力于打造中国最大的在线视频媒体平台,以丰富的内容、极致的观看体验");
            Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

            segmenter.DeleteWord("湖南");
            segmenter.AddWord("湖南");
            //segmenter.AddWord("长沙市");
            segments = segmenter.Cut("湖南长沙市天心区");
            Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));
            Console.Read();
        }
    }

    class MysqlDirectory : BaseDirectory
    {
        public override IndexOutput CreateOutput(string name, IOContext context)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        public override bool FileExists(string name)
        {
            throw new NotImplementedException();
        }

        public override long FileLength(string name)
        {
            throw new NotImplementedException();
        }

        public override string[] ListAll()
        {
            throw new NotImplementedException();
        }

        public override IndexInput OpenInput(string name, IOContext context)
        {
            throw new NotImplementedException();
        }

        public override void Sync(ICollection<string> names)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}
