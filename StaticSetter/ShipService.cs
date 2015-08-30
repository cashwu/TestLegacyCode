using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticSetter
{
    /// <summary>
    /// http://www.codedata.com.tw/social-coding/csharp-test-legacy-code-2-static-setter-injection
    /// </summary>
    public class ShipService
    {
        public void ShippingByStore(List<Order> orders)
        {
            foreach (var order in orders)
            {
                IStoreService storeService = SimpleFactory.GetStoreService(order);
                storeService.Ship(order);
            }
        }
    }
}
