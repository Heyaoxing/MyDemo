using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Demo.Class.Elasticsearchs
{
    public class CustomerService : ElasticClientBase
    {
          private const string IndexName = "customers";
        private readonly ElasticClient _elasticClient;

        public CustomerService()
        {
            _elasticClient = GetClient(IndexName);
        }
        public bool AddOrUpdate(Customer info)
        {
            var response = _elasticClient.Index(info);
            return response.Version > 0;
        }
    }
}
