using System.Collections.Generic;
using System.Net.Http;

namespace Isolated
{
    public class BookDao : IBookDao
    {
        public void Insert(Order order)
        {
            var client = new HttpClient();

            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ProductName", order.ProductName),
                new KeyValuePair<string, string>("Type", order.Type),
                new KeyValuePair<string, string>("Price", order.Price.ToString()),
                new KeyValuePair<string, string>("ProductName", order.ProductName)
            };

            var formContent = new FormUrlEncodedContent(postData);

            client.PostAsync("http://api.cash.io/Order", formContent);
        }
    }
}