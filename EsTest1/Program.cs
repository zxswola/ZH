using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlainElastic.Net;
using PlainElastic.Net.Queries;
using PlainElastic.Net.Serialization;

namespace EsTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            ElasticConnection client = new ElasticConnection("localhost", 9200);
            SearchCommand cmd = new SearchCommand("MyTest", "persons");
            var query = new QueryBuilder<Person>().Query(b => b.Bool(m =>  
                //并且关系
                m.Must(t =>     
                    //分词的最小单位或关系查询
                             t.QueryString(t1 => t1.DefaultField("Name").Query("求"))))   )  
                //分页
            //        .From(0)  
            //        .Size(10)
            ////排序
            //.Sort(c => c.Field("age", SortDirection.desc))
            //////添加高亮
            //.Highlight(h => h 
            //        .PreTags("<b>")
            //        .PostTags("</b>")
            //        .Fields(
            //            f => f.FieldName("Name").Order(HighlightOrder.score))
                //)
                .Build();
            var result = client.Post(cmd, query);
            var serializer = new JsonNetSerializer();
            var list = serializer.ToSearchResult<Person>(result);
        foreach(var doc in list.Documents)
        {
            Console.WriteLine(doc.Id);

        }
        }

        static void Main1(string[] args)
        {
            Person p1 = new Person {Id=2,Age=11,Name="独孤求败",Desc= "剑客" };
            ElasticConnection client = new ElasticConnection("127.0.0.1", 9200);
            var serializer = new JsonNetSerializer();
            //第一个相当于"数据库",第二个相当于表,第三个相当于主键
            IndexCommand cmd = new IndexCommand("MyTest","persons",p1.Id.ToString());//数据区域
            OperationResult result = client.Put(cmd, serializer.Serialize(p1));//放进去
            var indexResult = serializer.ToIndexResult(result.Result);
            if (indexResult.created)
            {
                Console.WriteLine("创建了");
            }
            else
            {
                Console.WriteLine("创建失败"+indexResult.error);
            }

            Console.ReadKey();
        }
    }
}
