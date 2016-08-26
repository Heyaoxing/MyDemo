using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar;

namespace Demo.ORM.MySqlSugar
{
    public class SugarDao
    {
        private static readonly string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString(); //这里可以动态根据cookies或session实现多库切换
        //禁止实例化
        private SugarDao()
        {

        }
        public static SqlSugarClient GetInstance()
        {
            var reval = new SqlSugarClient(connection);
            //设置流水号
            reval.SetSerialNumber(_nums);
            return reval;
        }

        /// <summary>
        /// 页面所需要的过滤函数
        /// </summary>
        private static List<PubModel.SerialNumber> _nums = new List<PubModel.SerialNumber>(){
              new PubModel.SerialNumber(){TableName="t_primary_websites", FieldName="WebSite_Url", GetNumFunc=()=>{
                  return "stud-"+DateTime.Now.ToString("yyyy-MM-dd");
              }}
            };

    }
}
