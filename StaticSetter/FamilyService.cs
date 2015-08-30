using System.Collections.Generic;
using System.Net.Http;

namespace StaticSetter
{
    internal class FamilyService : IStoreService
    {
        public void Ship(Order order)
        {
            var client = new HttpClient();

            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Id", order.Id.ToString()),
                new KeyValuePair<string, string>("Amount", order.Amount.ToString()),
                new KeyValuePair<string, string>("StoreType", order.StoreType.ToString())
            };

            var formContent = new FormUrlEncodedContent(postData);

            var response = client.PostAsync("http://api.family.com/Order", formContent);
            response.Result.EnsureSuccessStatusCode();
        }
    }
}