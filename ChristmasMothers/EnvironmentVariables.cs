using System;

namespace ChristmasMother
{
    public static class EnvironmentVariables
    {
        public const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        public const string ChristmasMothers_IDENTITYSERVER_AUTHORITY = "ChristmasMothers_IDENTITYSERVER_AUTHORITY";
        public const string ChristmasMothers_IDENTITYSERVER_AUTHORIZATION_ENABLE = "ChristmasMothers_IDENTITYSERVER_AUTHORIZATION_ENABLE";
        public const string ChristmasMothers_APPLICATIONINSIGHTS_INSTRUMENTATIONKEY = "ChristmasMothers_APPLICATIONINSIGHTS_INSTRUMENTATIONKEY";
        public const string ChristmasMothers_CONNECTIONSTRING = "ChristmasMothers_CONNECTIONSTRING";
        public const string ChristmasMothers_DATABASETYPE = "ChristmasMothers_DATABASETYPE";
        public const string ChristmasMothers_SCHEMA = "ChristmasMothers_SCHEMA";

        public static string AspNetCoreEnvironment => ASPNETCORE_ENVIRONMENT.Value<string>();
        public static string ChristmasMotherIdentityServerAuthority => ChristmasMothers_IDENTITYSERVER_AUTHORITY.Value<string>();
        public static bool? ChristmasMotherIdentityServerAuthorizationEnable          
        {
            get
            {
                try
                {
                    var authorizationEnableStrValue = ChristmasMothers_IDENTITYSERVER_AUTHORIZATION_ENABLE.Value<string>();
                    if (authorizationEnableStrValue.ToLower() != "true" &&
                        authorizationEnableStrValue.ToLower() != "false")
                    {
                        throw new FormatException();
                    }
                    return Convert.ToBoolean(ChristmasMothers_IDENTITYSERVER_AUTHORIZATION_ENABLE.Value<string>());
                }
                catch (Exception)
                {
                    return null;
                }
                
            }
        }
        public static string ChristmasMotherApplicationInsightInstrumentationKey => ChristmasMothers_APPLICATIONINSIGHTS_INSTRUMENTATIONKEY.Value<string>();
        public static string ChristmasMotherConnectionString => ChristmasMothers_CONNECTIONSTRING.Value<string>();
        public static string ChristmasMotherDataBaseType => ChristmasMothers_DATABASETYPE.Value<string>();
        public static string ChristmasMotherSchema => ChristmasMothers_SCHEMA.Value<string>();

        private static T Value<T>(this string str) where T : class 
        {
            return Environment.GetEnvironmentVariable(str) as T;
        }

        public static bool IsDevelopment()
        {
            return AspNetCoreEnvironment == "Development";
        }


    }
}