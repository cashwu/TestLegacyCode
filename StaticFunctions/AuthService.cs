﻿namespace StaticFunctions
{
    /// <summary>
    ///  http://www.codedata.com.tw/social-coding/c-test-legacy-code-4-static-functions
    /// </summary>
    public class AuthService
    {
        private IProfileDao _profileDao;

        public IProfileDao MyProfileDao
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

        public bool IsValid(string account, string password)
        {
            string passwordFormDao = this.MyProfileDao.GetPassword(account);
            string token = this.MyProfileDao.GetToken(account);

            var validPassword = passwordFormDao + token;

            var isValid = validPassword == password;

            return isValid;
        } 
    }
}