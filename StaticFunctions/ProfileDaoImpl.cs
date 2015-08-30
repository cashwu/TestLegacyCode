using System;
using System.Collections.Generic;

namespace StaticFunctions
{
    public class ProfileDaoImpl : IProfileDao
    {
        private static Dictionary<string, string> fakePasswordDataset = new Dictionary<string, string>
        {
            {"cash", "1234"},
            {"demo", "!@#$"},
        };

        public string GetPassword(string account)
        {
            if (!fakePasswordDataset.ContainsKey(account))
            {
                throw new Exception("account not exist");
            }

            return fakePasswordDataset[account];
        }

        public string GetToken(string account)
        {
            var seed = new Random((account.GetHashCode() + (int)DateTime.Now.Ticks) & 0x0000FFFF);
            var result = seed.Next(0, 999999);
            return result.ToString("000000");
        }
    }
}