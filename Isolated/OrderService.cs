using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isolated
{
    /// <summary>
    ///  http://www.codedata.com.tw/social-coding/csharp-legacy-code-test-1-isolated-by-inheritance-override/
    /// </summary>
    public class OrderService
    {
        private string _filePath = @"c:temp\cash.csv";

        public void SyncBookOrders()
        {
            var orders = this.GetOrders();

            var orderOfBook = orders.Where(a => a.Type == "Book");
            var bookDao = this.GetBookDao();
            foreach (var order in orderOfBook)
            {
                bookDao.Insert(order);
            }
        }

        protected virtual IBookDao GetBookDao()
        {
            return new BookDao();
        }

        protected virtual List<Order> GetOrders()
        {
            var result = new List<Order>();

            using (var sr = new StreamReader(this._filePath, Encoding.UTF8))
            {
                int rowCount = 0;

                while (sr.Peek() > -1)
                {
                    rowCount++;

                    var content = sr.ReadLine();

                    if (rowCount > 1)
                    {
                        string[] line = content.Trim().Split(',');
                        result.Add(this.Mapping(line));
                    }
                }
            }
            
            return result;
        }

        private Order Mapping(string[] line)
        {
            var result = new Order
            {
                ProductName = line[0],
                Type = line[1],
                Price = Convert.ToInt32(line[2]),
                CustomerName = line[3]
            };

            return result;
        }
    }
}
