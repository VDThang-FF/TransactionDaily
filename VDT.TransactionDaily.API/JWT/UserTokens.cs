﻿namespace VDT.TransactionDaily.API.JWT
{
    public class UserTokens
    {
        public string AccessToken
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public TimeSpan Validaty
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get;
            set;
        }
        public uint Id
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public DateTime ExpiredTime
        {
            get;
            set;
        }
        public DateTime RefreshExpiredTime
        {
            get;
            set;
        }
    }
}
