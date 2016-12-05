using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Demo.Class.Elasticsearchs
{
    public class TrackingInfoService : ElasticClientBase
    {
        private const string IndexName = "trackinfos";
        private readonly ElasticClient _elasticClient;

        public TrackingInfoService()
        {
            _elasticClient = GetClient(IndexName);
        }

        public bool AddOrUpdate(TrackInfo info)
        {
            var index = info.ToIndex();
            index.InfoUpdatedOn = DateTime.Now;
            var response = _elasticClient.Index(index);
            return response.Version > 0;
        }

        public bool Delete(string number)
        {
            number = number.ToLowerInvariant();
            QueryContainer query = new TermQuery { Field = "id", Value = number };
            var searchResults = _elasticClient.Search<TrackInfoIndex>(s => s
                            .Index(IndexName)
                            .Query(p => query));

            foreach (var doc in searchResults.Documents)
            {
                _elasticClient.Delete<TrackInfoIndex>(doc.Id, d => d.Index(IndexName));
            }

            return true;
        }

      
      

        public TrackInfoIndex Get(string number)
        {
            number = number.ToLowerInvariant();
            var searchResults = _elasticClient.Search<TrackInfoIndex>(sc => sc
                .PostFilter(q => QueryContainerPosition(number, q))

                );

            if (searchResults.ApiCall.Success)
                return searchResults.Documents.OrderByDescending(p => p.CreatedOn).FirstOrDefault();
            else
                throw searchResults.OriginalException;


        }
        private QueryContainer QueryContainerPosition(string number, QueryContainerDescriptor<TrackInfoIndex> q)
        {
            var query1 = q.Term(s => s.Id, number);
            //return query1;

            var query2 = q.Term(s => s.FilterCustomerOrderNumber, number);

            var query3 = q.Term(s => s.FilterTrackingNumber, number);

            return query1 || query2 || query3;
        }

        public List<TrackInfoIndex> GetList(string[] numbers)
        {
            var nums = numbers.Select(n => n.ToLowerInvariant()).GroupBy(p => p).Select(p => p.Key).ToArray();

            var searchResults = _elasticClient.Search<TrackInfoIndex>(sc => sc
                .PostFilter(q => QueryContainerPosition(nums, q))
                .From(0)
                .Size(200)
                );

            if (searchResults.ApiCall.Success)
                return searchResults.Documents.ToList();
            else
                throw searchResults.OriginalException;

        }

        private QueryContainer QueryContainerPosition(string[] number, QueryContainerDescriptor<TrackInfoIndex> q)
        {
            var query1 = q.Terms(s => s.Field(f => f.Id).Terms(number));

            var query2 = q.Terms(s => s.Field(f => f.FilterCustomerOrderNumber).Terms(number));

            var query3 = q.Terms(s => s.Field(f => f.FilterTrackingNumber).Terms(number));

            return query1 || query2 || query3;
        }
    }
}
