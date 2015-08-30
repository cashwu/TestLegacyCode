using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectEqualityTest
{
    internal class Order : IEquatable<Order>
    {
        public int Price { get; set; }

        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            if (order != null)
            {
                return this.Equals(order);
            }

            return false;
        }

        public bool Equals(Order other)
        {
            return this.Id == other.Id && this.Price == other.Price;
        }
    }
}
