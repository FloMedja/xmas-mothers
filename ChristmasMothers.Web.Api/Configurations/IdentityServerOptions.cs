using System.Collections.Generic;

namespace ChristmasMothers.Web.Api.Configurations
{
    public class IdentityServerOptions
    {
        public const string CONFIGURATION_SECTION = "IdentityServer";

        public string Authority { get; set; }
        public IEnumerable<string> AllowedScopes { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public bool LegacyAudienceValidation { get; set; }
        public bool EnableAuthorization { get; set; }

        public void RefreshOptions()
        {
            var envVarAuthority = EnvironmentVariables.ChristmasMotherIdentityServerAuthority;
            if (envVarAuthority != null)
            {
                Authority = envVarAuthority;
            }

            if (EnvironmentVariables.ChristmasMotherIdentityServerAuthorizationEnable.HasValue)
            {
                EnableAuthorization = EnvironmentVariables.ChristmasMotherIdentityServerAuthorizationEnable.Value;
            }

        }
    }

}