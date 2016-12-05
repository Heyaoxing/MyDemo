using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Demo.Class.Elasticsearchs
{
    [ElasticsearchType(Name = "customerinfo", IdProperty = "Id")]
    public class Customer
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Desctipt { set; get; }
    }
}
