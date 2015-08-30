using System.Collections.Generic;
using Isolated;
using NSubstitute;
using NUnit.Framework;

namespace IsolatedTest
{
    [TestFixture]
    public class OrderTest
    {
        [Test]
        public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
        {
            var target = new StubOrderService();

            var orders = new List<Order>
            {
                new Order {Type = "Book", Price = 100, ProductName = "PC book"},
                new Order {Type = "CD", Price = 200, ProductName = "CD"},
                new Order {Type = "Book", Price = 300, ProductName = "POP book"}
            };

            target.SetOrders(orders);

            var stubBookDao = Substitute.For<IBookDao>();
            target.SetBookDao(stubBookDao);

            target.SyncBookOrders();

            stubBookDao.Received(2).Insert(Arg.Is<Order>(a => a.Type == "Book"));
        }
    }
}