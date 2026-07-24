using System.Globalization;
using System.Resources;

namespace Brawndo_Translation
{
    public static class Resources
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("Brawndo_Translation.Resources.Strings", typeof(Resources).Assembly);

        public static string HomePageTitle =>
            _resourceManager.GetString("HomePageTitle", CultureInfo.CurrentUICulture) ?? "Welcome to Brawndo";

        public static string HomePageDescription =>
            _resourceManager.GetString("HomePageDescription", CultureInfo.CurrentUICulture) ?? "Learn about building Web apps with ASP.NET Core";

        public static string PrivacyPageTitle =>
            _resourceManager.GetString("PrivacyPageTitle", CultureInfo.CurrentUICulture) ?? "Privacy Policy";

        public static string PrivacyPageContent =>
            _resourceManager.GetString("PrivacyPageContent", CultureInfo.CurrentUICulture) ?? "Use this page to detail your site's privacy policy.";

        public static string NavHome =>
            _resourceManager.GetString("NavHome", CultureInfo.CurrentUICulture) ?? "Home";

        public static string NavPrivacy =>
            _resourceManager.GetString("NavPrivacy", CultureInfo.CurrentUICulture) ?? "Privacy";
    }
}
