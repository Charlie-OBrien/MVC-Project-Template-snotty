using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace Brawndo.Helpers
{
    public static class LanguageHelper
    {
        public static string GetCurrentLanguage(ViewContext viewContext)
        {
            return viewContext.RouteData.Values["language"]?.ToString() ?? "en";
        }

        public static void SetCultureFromLanguage(string language)
        {
            var cultureInfo = new CultureInfo(language switch
            {
                "fr" => "fr-CA",
                "en" => "en-US",
                _ => "en-US"
            });

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }

        public static string GetCultureCode(string language)
        {
            return language switch
            {
                "fr" => "fr-CA",
                "en" => "en-US",
                _ => "en-US"
            };
        }
    }
}
