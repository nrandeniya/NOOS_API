using NOOS_UI.Contracts;
using NOOS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NOOS_UI.Services
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        //inject client
        private readonly IHttpClientFactory _client;

        public SubscriptionRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }




    }
}
