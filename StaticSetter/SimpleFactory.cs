namespace StaticSetter
{
    public class SimpleFactory
    {
        private static IStoreService familyservice;
        private static IStoreService sevenService;

        public static void SetSevenService(IStoreService stub)
        {
            sevenService = stub;
        }

        public static void SetFamilyService(IStoreService stub)
        {
            familyservice = stub;
        }

        public static IStoreService GetStoreService(Order order)
        {
            if (order.StoreType == StoreType.Family)
            {
                return familyservice ?? new FamilyService();
            }
            else
            {
                return sevenService ?? new SevenService();
            }
        }
    }
}