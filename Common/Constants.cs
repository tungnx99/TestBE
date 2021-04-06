using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common
{
    public static class Constants
    {
        public static class Server
        {
            public static readonly string ErrorServer = "Internal Server Error";
        }
        public static class Account
        {
            public static readonly string InvalidAuthInfoMsg = "Invalid email or password";
        }

        public static class Data
        {
            public static readonly string InsertSuccess = "Insert Success";
            public static readonly string UpdateSuccess = "Update Success";
            public static readonly string DeleteSuccess = "Delete Success";
            public static readonly string UploadFail = "Upload Fail";
        }
    }
}
