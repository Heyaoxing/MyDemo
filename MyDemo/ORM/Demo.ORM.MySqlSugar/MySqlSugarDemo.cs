using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MySqlSugar;

namespace Demo.ORM.MySqlSugar
{
    public class MySqlSugarDemo
    {
        public void Get()
        {
            using (var db = SugarDao.GetInstance())
            {
                //---------Queryable<T>,扩展函数查询---------//

                //针对单表或者视图查询
                var dt = db.GetDataTable("select * from t_primary_websites");
                //查询所有
                var t_primary_websites = db.Queryable<t_primary_websites>().ToList();
                var t_primary_websitesDynamic = db.Queryable<t_primary_websites>().ToDynamic();
                var t_primary_websitesJson = db.Queryable<t_primary_websites>().ToJson();

                //查询单条
                var single = db.Queryable<t_primary_websites>().Single(c => c.ID == 1);
                //查询单条没有记录返回空对象
                var single2 = db.Queryable<t_primary_websites>().Where(c => c.ID == 1).SingleOrDefault();

                //查询第一条
                var first = db.Queryable<t_primary_websites>().Where(c => c.ID == 1).First();
                var first2 = db.Queryable<t_primary_websites>().Where(c => c.ID == 1).FirstOrDefault();

                //取10-20条
                var page1 = db.Queryable<t_primary_websites>().Where(c => c.ID > 2).OrderBy("id").Skip(2).Take(3).ToList();

                //上一句的简化写法，同样取10-20条
                var page2 = db.Queryable<t_primary_websites>().Where(c => c.ID > 1).OrderBy("id").ToPageList(2, 3);

                //查询条数
                var count = db.Queryable<t_primary_websites>().Where(c => c.ID > 10).Count();

                //从第2条开始以后取所有
                var skip = db.Queryable<t_primary_websites>().Where(c => c.ID > 10).OrderBy("id").Skip(2).ToList();

                //取前2条
                var take = db.Queryable<t_primary_websites>().Where(c => c.ID > 10).OrderBy("id").Take(2).ToList();

                // Not like 
                string conval = "三";
                var notLike = db.Queryable<t_primary_websites>().Where(c => !c.WebSite_Url.Contains(conval.ToString())).ToList();
                //Like
                conval = "三";
                var like = db.Queryable<t_primary_websites>().Where(c => c.WebSite_Url.Contains(conval)).ToList();

                // 可以在拉姆达使用 ToString和 Convert,比EF出色的地方
                var convert1 = db.Queryable<t_primary_websites>().Where(c => c.WebSite_Url == "a".ToString()).ToList();
                var convert2 = db.Queryable<t_primary_websites>().Where(c => c.ID == Convert.ToInt32("1")).ToList();// 
                var convert3 = db.Queryable<t_primary_websites>().Where(c => DateTime.Now > Convert.ToDateTime("2015-1-1")).ToList();
                var convert4 = db.Queryable<t_primary_websites>().Where(c => DateTime.Now > DateTime.Now).ToList();

                //支持字符串Where 让你解决，更复杂的查询
                var t_primary_websites12 = db.Queryable<t_primary_websites>().Where(c => "a" == "a").Where("id>100").ToList();
                var t_primary_websites13 = db.Queryable<t_primary_websites>().Where(c => "a" == "a").Where("id>100 and id in( select 1)").ToList();


                //存在记录反回true，则否返回false
                bool isAny100 = db.Queryable<t_primary_websites>().Any(c => c.ID == 100);
                bool isAny1 = db.Queryable<t_primary_websites>().Any(c => c.ID == 1);

                int maxId = db.Queryable<t_primary_websites>().Max<t_primary_websites, int>("id");
                int minId = db.Queryable<t_primary_websites>().Where(c => c.ID > 0).Min<t_primary_websites, int>("id");



                //In
                var list1 = db.Queryable<t_primary_websites>().In("id", "1", "2", "3").ToList();
                var list2 = db.Queryable<t_primary_websites>().In("id", new string[] { "1", "2", "3" }).ToList();
                var list3 = db.Queryable<t_primary_websites>().In("id", new List<string> { "1", "2", "3" }).ToList();
                var list4 = db.Queryable<t_primary_websites>().Where(it => it.ID < 10).In("id", new List<string> { "1", "2", "3" }).ToList();

                //分组查询
                //  var list5 = db.Queryable<t_primary_websites>().Where(c => c.ID < 20).GroupBy("sex").ToList();
                //SELECT Sex,Count=count(*)  FROM t_primary_websites  WHERE 1=1  AND  (id < 20)    GROUP BY Sex --生成结果



                //转成list
                List<t_primary_websites> list11 = db.SqlQuery<t_primary_websites>("select * from t_primary_websites");
                //转成list带参
                List<t_primary_websites> list12 = db.SqlQuery<t_primary_websites>("select * from t_primary_websites where id=@id", new { id = 1 });
                //转成dynamic
                dynamic list13 = db.SqlQueryDynamic("select * from t_primary_websites");
                //转成json
                string list14 = db.SqlQueryJson("select * from t_primary_websites");
                //返回int
                var list5 = db.SqlQuery<int>("select  id from t_primary_websites limit 0,1").First();
                //反回键值
                Dictionary<string, string> list6 = db.SqlQuery<KeyValuePair<string, string>>("select id,name from t_primary_websites").ToDictionary(it => it.Key, it => it.Value);
                //反回List<string[]>
                var list7 = db.SqlQuery<string[]>("select  name from t_primary_websites").First();
                //存储过程
                //var spResult = db.SqlQuery<School>("exec sp_school @p1,@p2", new { p1=1,p2=2 });

                var list22 = db.Queryable<t_primary_websites>().Where(c => c.ID < 10).Select(c => new t_primary_websites { ID = c.ID }).ToList();//不支持匿名类转换,也不建议使用

                var list23 = db.Queryable<t_primary_websites>().Where(c => c.ID < 10).Select(c => new { ID = c.ID }).ToDynamic();//匿名类转换
            }
        }

        public void Set()
        {
            using (var db = SugarDao.GetInstance())
            {
                string a = "a";
                var obj = Convert.ToInt32(db.Insert<t_primary_websites>(new t_primary_websites()
                {
                    ID = 100, 
                    Source_ID = 1,
                    Level=10,
                    Status=100,
                    create_time=DateTime.Now,
                    is_erased=1
                }));
            }
        }
    }
}
