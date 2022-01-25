namespace VDT.TransactionDaily.API.Models.Enums
{
    public static class Enumarations
    {
        /// <summary>
        /// Key ở trong cookie storage
        /// </summary>
        /// created by vdthang 17.01.2022
        public static class CookieKey
        {
            public static string UserID = "UserID";
            public static string UserName = "UserName";
            public static string Email = "Email";
            public static string Authorization = "Authorization";
            public static string SessionID = "SessionID";
            public static string AccessToken = "AccessToken";
            public static string RefreshToken = "RefreshToken";
            public static string DeviceID = "DeviceID";
            public static string DeviceName = "DeviceName";
            public static string UserAgent = "User-Agent";
        }

        public enum ModelState : int
        {
            None = 0,
            Insert = 1,
            Update = 2,
            Delete = 3,
            Sync = 4,
            Duplicate = 5
        }

        public enum Code : int
        {
            Success = 200,
            Accepted = 202,
            NoContent = 204,
            BadRequest = 400,
            Unauthorize = 401,
            Forbidden = 403,
            NotFound = 404,
            WrongFormat = 415,
            Failure = 500,
            Unavailable = 503
        }

        public enum SubCode : int
        {
            Success = 0,
            ErrorInsert = 1,
            ErrorUpdate = 50,
            ErrorDelete = 100,
            ErrorDuplicate = 200,
            ErrorAuthorize = 401,
            ErrorForbidden = 403,
            ErrorNotFound = 404,
            ErrorInvalid = 405,
            Exception = 1000
        }
    }
}
