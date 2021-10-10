using System;

namespace Oak.Core
{
    public class JWT
    {
        public static class ClaimTypes
        {
                public static string ID = "id";
                public static string Email = "email";
                public static string Type = "type";
                public static string Expiry = "iat";
                public static string Token = "token";
                public static string ExternalID = "eid";
                public static string Permissions = "permissions";
        }
    }
}