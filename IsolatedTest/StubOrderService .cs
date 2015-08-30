using System.Collections.Generic;
using Isolated;

namespace IsolatedTest
{
    internal class StubOrderService : OrderService
    {
        private List<Order> _orders = new List<Order>();
        private IBookDao _bookDao;

        internal void SetOrders(List<Order> orders)
        {
            this._orders = orders;
        }

        protected override List<Order> GetOrders()
        {
            return this._orders;
        }

        internal void SetBookDao(IBookDao bookDao)
        {
            this._bookDao = bookDao;
        }

        protected override IBookDao GetBookDao()
        {
            return this._bookDao;
        }
    }
}