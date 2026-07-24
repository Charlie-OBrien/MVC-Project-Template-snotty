# Localization Setup Guide

## Overview
This document explains how the language routing and translation system is set up in the Brawndo MVC application.

## What Has Been Done

### 1. URL Routing with Language Parameter
- **File Modified**: `Program.cs`
- **Route Pattern**: `{language=en}/{controller=Home}/{action=Index}/{id?}`
- **Default**: English (`en`)
- **Examples**:
  - `https://localhost:7156/en/Home/Index` → English version
  - `https://localhost:7156/fr/Home/Privacy` → French version
  - `https://localhost:7156/` → Redirects to `/en/`

### 2. Navbar Language Switcher
- **File Modified**: `Views/Shared/_Layout.cshtml`
- **Location**: Top-right corner of navbar
- **Features**:
  - Dropdown button showing current language (EN/FR)
  - Links to switch between English and French
  - All navigation links automatically include the language parameter
  - Maintains language on navigation

### 3. Updated Navigation Links
- **Files Modified**: 
  - `Views/Shared/_Layout.cshtml` (navbar and footer)
  - `Views/Home/Index.cshtml`
  - `Views/Home/Privacy.cshtml`
- **All links now include**: `asp-route-language="@currentLanguage"`
- **Result**: Clicking links preserves the current language selection

### 4. Project Reference
- **File Modified**: `Brawndo.csproj`
- **Added**: Reference to `Brawndo_Translation` project
- **Allows**: Using `Brawndo_Translation.Resources` in views

### 5. Language Helper Class
- **File Created**: `Brawndo/Helpers/LanguageHelper.cs`
- **Methods**:
  - `GetCurrentLanguage(ViewDataDictionary)` - Gets language from route
  - `SetCultureFromLanguage(string)` - Sets culture for current thread
  - `GetCultureCode(string)` - Maps language code to culture info

## Next Steps: Creating .resx Files

### Step 1: Create Resource Files in Brawndo_Translation Project

1. Create a `Resources` folder in the `Brawndo_Translation` project
2. Create resource files:
   - `Resources/Strings.resx` (English - default)
   - `Resources/Strings.fr-CA.resx` (French - Canadian)

### Step 2: Add Translation Keys to .resx Files

**In Strings.resx (English):**
```
Key: HomePageTitle
Value: Welcome to Brawndo

Key: HomePageDescription
Value: Learn about building Web apps with ASP.NET Core

Key: PrivacyPageTitle
Value: Privacy Policy

Key: PrivacyPageContent
Value: Use this page to detail your site's privacy policy.

Key: NavHome
Value: Home

Key: NavPrivacy
Value: Privacy
```

**In Strings.fr-CA.resx (French):**
```
Key: HomePageTitle
Value: Bienvenue à Brawndo

Key: HomePageDescription
Value: Découvrez comment créer des applications Web avec ASP.NET Core

Key: PrivacyPageTitle
Value: Politique de confidentialité

Key: PrivacyPageContent
Value: Utilisez cette page pour détailler la politique de confidentialité de votre site.

Key: NavHome
Value: Accueil

Key: NavPrivacy
Value: Confidentialité
```

### Step 3: Configure Brawndo_Translation Project

Edit `Brawndo_Translation/Brawndo_Translation.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  
  <ItemGroup>
    <EmbeddedResource Include="Resources/Strings.resx" />
    <EmbeddedResource Include="Resources/Strings.fr-CA.resx" />
  </ItemGroup>
</Project>
```

### Step 4: Create a Resources Helper Class

Create `Brawndo_Translation/Resources.cs`:

```csharp
using System.Globalization;
using System.Resources;

namespace Brawndo_Translation
{
    public static class Resources
    {
        private static readonly ResourceManager _resourceManager = 
            new ResourceManager("Brawndo_Translation.Resources.Strings", 
                typeof(Resources).Assembly);

        public static string HomePageTitle => 
            _resourceManager.GetString("HomePageTitle") ?? "Welcome to Brawndo";

        public static string HomePageDescription => 
            _resourceManager.GetString("HomePageDescription") ?? "Learn about building Web apps with ASP.NET Core";

        public static string PrivacyPageTitle => 
            _resourceManager.GetString("PrivacyPageTitle") ?? "Privacy Policy";

        public static string PrivacyPageContent => 
            _resourceManager.GetString("PrivacyPageContent") ?? "Use this page to detail your site's privacy policy.";

        public static string NavHome => 
            _resourceManager.GetString("NavHome") ?? "Home";

        public static string NavPrivacy => 
            _resourceManager.GetString("NavPrivacy") ?? "Privacy";
    }
}
```

### Step 5: Update Views to Use Translations

**Example: Views/Home/Index.cshtml**

Replace:
```html
<h1 class="display-4">Welcome</h1>
```

With:
```html
<h1 class="display-4">@Brawndo_Translation.Resources.HomePageTitle</h1>
```

Replace:
```html
<p>Learn about <a href="...">building Web apps with ASP.NET Core</a>.</p>
```

With:
```html
<p>@Brawndo_Translation.Resources.HomePageDescription</p>
```

### Step 6: Update Controllers to Set Culture (Optional)

To make `DateTime.Now.ToString()` and number formatting respect the current language, add this to `HomeController`:

```csharp
public class HomeController : Controller
{
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var language = RouteData.Values["language"]?.ToString() ?? "en";
        Brawndo.Helpers.LanguageHelper.SetCultureFromLanguage(language);
        return View();
    }

    public IActionResult Privacy()
    {
        var language = RouteData.Values["language"]?.ToString() ?? "en";
        Brawndo.Helpers.LanguageHelper.SetCultureFromLanguage(language);
        return View();
    }
}
```

## Testing the Setup

1. **Build the solution** - Ensure no build errors
2. **Run the application** - Start the Brawndo app
3. **Test URLs**:
   - Navigate to `https://localhost:7156/en/Home/Index` - should show English
   - Navigate to `https://localhost:7156/fr/Home/Index` - should show French
   - Click language switcher button - should toggle between EN/FR
4. **Test navigation**:
   - Click navbar links - language should persist
   - Check footer privacy link - should maintain language

## Cookie/Session Storage (Future Enhancement)

When ready, you can add cookie-based language persistence:

```csharp
// In Program.cs
app.Use(async (context, next) =>
{
    var language = context.Request.RouteValues["language"]?.ToString() ?? "en";
    if (context.Request.Cookies.TryGetValue("language", out var cookieLanguage))
    {
        language = cookieLanguage;
    }
    
    context.Response.Cookies.Append("language", language, 
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
    
    await next();
});
```

Then redirect to the stored language if not in the URL.

## File Summary

**Modified Files:**
- `Program.cs` - Added language route parameter and root redirect
- `Views/Shared/_Layout.cshtml` - Added language switcher, updated links
- `Brawndo.csproj` - Added Brawndo_Translation reference

**New Files:**
- `Brawndo/Helpers/LanguageHelper.cs` - Helper methods for language management
- `Views/Home/Index.cshtml` - Added language indicator and examples
- `Views/Home/Privacy.cshtml` - Added language indicator and examples

**To Create:**
- `Brawndo_Translation/Resources/Strings.resx`
- `Brawndo_Translation/Resources/Strings.fr-CA.resx`
- `Brawndo_Translation/Resources.cs`
