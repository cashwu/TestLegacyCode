using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Equivalency;
using NSubstitute;
using NUnit.Framework;
using StaticSetter;

namespace StaticSetterTest
{
    [TestFixture]
    public class ShipServiceTest
    {
        [Test]
        public void TestShippingByStore_Seven_1_Order_Family_2_Orders()
        {
            var target = new ShipService();

            var orders = new List<Order>
            {
                new Order {StoreType = StoreType.Seven, Id = 1},
                new Order {StoreType = StoreType.Family, Id = 2},
                new Order {StoreType = StoreType.Family, Id = 3},
            };

            var stubSeven = Substitute.For<IStoreService>();
            SimpleFactory.SetSevenService(stubSeven);

            var stubFamily = Substitute.For<IStoreService>();
            SimpleFactory.SetFamilyService(stubFamily);

            target.ShippingByStore(orders);

            stubSeven.Received(1).Ship(Arg.Is<Order>(a => a.StoreType == StoreType.Seven));
            stubFamily.Received(2).Ship(Arg.Is<Order>(a => a.StoreType == StoreType.Family));
        }
    }
}
