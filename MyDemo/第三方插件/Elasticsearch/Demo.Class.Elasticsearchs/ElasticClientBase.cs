using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Demo.Class.Elasticsearchs
{
    public class ElasticClientBase
    {
        public ElasticClient GetClient(string indexName)
        {
            var host = "http://localhost:9201";
            var node = new Uri(host);

            var settings = new ConnectionSettings(node);
            settings.DefaultIndex(indexName);
            var client = new ElasticClient(settings);
            return client;
        }
    }
}
