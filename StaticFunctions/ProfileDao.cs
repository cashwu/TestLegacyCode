using System;
using System.Collections.Generic;

namespace StaticFunctions
{
    public static class ProfileDao
    {
        private static IProfileDao _profileDao;

        public static IProfileDao MyProfileDao
        {
            get
            {
                if (_profileDao == null)
                {
                    _profileDao = new ProfileDaoImpl();
                }

                return _profileDao;
            }
            set
            {
                _profileDao = value;
            }
        }

        internal static string GetPassword(string account)
        {
            return MyProfileDao.GetPassword(account);
        }

        internal static string GetToken(string account)
        {
            return MyProfileDao.GetToken(account);
        }
    }
}