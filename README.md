# Sitecore-XM-Integrations
## Sitecore-XM-Integrations for Composible DXP Solutions

### Sitecore.Foundation.DynamicSitemapAPI Project (Now included in the main Solution)
#### For Implemntation use of this project into your Solution complete the following:
1. Copy the following DLL files into your project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - .\NugetPackageSolutions\Sitecore.Foundation.DynamicSitemapAPI\Sitecore.Foundation.DynamicSitemapAPI.dll
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
    - .\NugetPackageSolutions\Sitecore.Foundation.DynamicSitemapAPI\Sitecore.Foundation.DynamicSitemapAPI.dll.config
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
2. Copy the 'AppConfig' as is, into your XM/XP project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Foundation.DynamicSitemapAPI\AppConfig => to => [ProjectFolder]\AppConfig

#### Summary Documention (Pending)

### Sitecore.Foundation.Extensions Project (Now included in the main Solution)
#### For Implemntation use of this project into your Solution complete the following:
1. Copy the following DLL files into your project solution (i.e. can be used even without a Traditional Sitecore XM/XP instance):
    - .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\Sitecore.Foundation.SitecoreExtensions.dll
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
    - .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\Sitecore.Foundation.SitecoreExtensions.deps.json
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
2. Copy the 'AppConfig' as is, into your XM/XP project solution(i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\AppConfig => to => [ProjectFolder]\AppConfig

#### Summary Documention 
1. This includes build-in extension method functionality for the client's wanting to integrate with a Traditional Sitecore XM/XP instance implementation 
    - These extension methods can be accessed via the following files under the src\Middleware\src\SitecoreExtensions\code folder:
        - \Extensions\FieldExtensions.cs => Extension methods for Sitecore Field Item object from the Sitecore XM/XP instance        
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
        - \Extensions\ItemExtensions.cs => Extension methods for Sitecore Item objects from the Sitecore XM/XP instance
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
        - \Extensions\SitecoreLoggingExtensions.cs => Extension methods for Sitecore Field Item object from the Sitecore XM/XP instance
            - This method can be used with or without a Sitecore XM/XP instance
            - Note the LogException() method contains the following configurable params
                - createCustomLogFile (bool param) => Which determines the logging exceptions in a newly created custom log file/s or in 
                the default Sitecore XM/XP created log files. Note the param should always be passed in as true if the client is not using a Sitecore XM/XP instance
                - appLogFileKey (string param) => This applies an application prefix key to the name of the custom file being created. 
                    - Note: if the appLogFileKey is empty the fallback file prefix key will be applied as 'CustomApiLogs'
                    - Note: 1 log file gets created per day per application (i.e. if you have more than 1 site/application under the webroot).
                    - Note: a Sitecore XM/XP instance can handle as many sites/applications under the same webroot (each with there own Domain Url and branding and layout)
            - Note the LogApiResponseMessages() and LogExceptionNotification() methods contains the following configrable params
                - These methods can be used with or without a Sitecore XM/XP instance, i.e. will always create custom log files
                - appLogFileKey (string param) => This applies an application prefix key to the name of the custom file being created. 
                    - Note: if the appLogFileKey is empty the fallback file prefix key will be applied as 'CustomApiLogs'
                    - Note: 1 log file gets created per day per application (i.e. if you have more than 1 site/application under the webroot).
                    - Note: a Sitecore XM/XP instance can handle as many sites/applications under the same webroot (each with there own Domain Url and branding and layout)
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
        - \Extensions\HTMLHelperExtensions.cs => Extension methods for handling MvcHtmlString repsonses on Sitecore Item object's data values from the Sitecore XM/XP instance
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
2. The following is now included a build-in extension method functionality for clients whether or not they using a Traditional Sitecore XM/XP instance implementation for their applications
    - These extension methods can be accessed via the following files under the src\Middleware\src\SitecoreExtensions\code folder:
        - \Extensions\RenderingExtensions.cs => Extension methods for .NET MVC or .NET Core MVC rendering backend rendering engine
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
        - \Extensions\RenderingParameterExtensions.cs => Extension methods for .NET MVC or .NET Core MVC rendering backend rendering engine
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
        - \Extensions\DataTypeExtensions.cs => Static class Extension methods for various .NET data types (i.e string, DateTime, int, Guid, etc...)
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
        - \Extensions\Helpers.cs => Static class Extension Helper methods
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
            - Examples:
                - ToJson(this object value) => <returns>The JSon string value from the JSon object</returns>
                - ToJsonObject(this object value) => <returns>The JSon object value</returns>
                - GetMethodName([CallerMemberName] string callerMethod = "") => <returns>The Calling Method's name string value or empty</returns>
                - SetMethodName(string callerMethod) => <returns>The callerMethod string value using the callerMethod param</returns>
                - FileDateStamp(DateTime dateTime) => <returns>The File DateStamp string value</returns> (format as MM/dd/yyyy)
        - \Extensions\JsonNetResult.cs => Extension methods for JsonNetResult responses
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
        - \Extensions\StringExtensions.cs => Static class Extension methods on string objects
            - Please see the method comments/documentation (auto generated by the GhostDoc tool), that provides the correct code documentation for this file
            - Examples:
                - Humanize(this string input) => <returns>The Humanize value from as string object, after replacing not allowed characters, returned as a string value</returns>
                - ToCssUrlValue(this string url) => <returns>The CssUrlValue from as string object, as a string value</returns>
                - StripHtml(this string html) => <returns>The content from a Html string object retrieved from the HtmlDocument object, as a string value</returns>
                - IHtmlString RenderValue(this string value) => <returns>The clean Html content, as a Html string value</returns>
                - RemoveSpecifiedChars(this string value, string regexPattern, bool setLowerCase = false) => <returns>The string value after removing an array of regex characters from the string, as a string value</returns>

### Sitecore.Feature.Template.Solution Project (Now included in the main Solution)
#### For Implemntation use of this project into your Solution complete the following:
1. Copy the following DLL files into your project solution (i.e. can be used even without a Traditional Sitecore XM/XP instance):
    - .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\Sitecore.Foundation.SitecoreExtensions.dll
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
    - .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\Sitecore.Foundation.SitecoreExtensions.deps.json
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
2. Copy the 'AppConfig' as is, into your XM/XP project solution(i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\AppConfig => to => [ProjectFolder]\AppConfig
3. Copy the following DLL files into your project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - .\NugetPackageSolutions\Sitecore.Feature.GlobalComponentLibrary\Sitecore.Feature.GlobalComponentLibrary.dll
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
    - .\NugetPackageSolutions\Sitecore.Feature.GlobalComponentLibrary\Sitecore.Feature.GlobalComponentLibrary.dll.config
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
4. Copy the 'AppConfig' as is, into your XM/XP project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Feature.GlobalComponentLibrary\AppConfig => to => [ProjectFolder]\AppConfig

#### Summary Documention (Pending)


### Sitecore.Project.Template.Solution Project (Now included in the main Solution)
#### For Implemntation use of this project into your Solution complete the following:
1. Copy the following DLL files into your project solution (i.e. can be used even without a Traditional Sitecore XM/XP instance):
    - .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\Sitecore.Foundation.SitecoreExtensions.dll
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
    - .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\Sitecore.Foundation.SitecoreExtensions.deps.json
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
2. Copy the 'AppConfig' as is, into your XM/XP project solution(i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Foundation.SitecoreExtensions\AppConfig => to => [ProjectFolder]\AppConfig
3. Copy the following DLL files into your project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - .\NugetPackageSolutions\Sitecore.Feature.GlobalComponentLibrary\Sitecore.Feature.GlobalComponentLibrary.dll
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
    - .\NugetPackageSolutions\Sitecore.Feature.GlobalComponentLibrary\Sitecore.Feature.GlobalComponentLibrary.dll.config
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
4. Copy the 'AppConfig' folder as is, into your XM/XP project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Feature.GlobalComponentLibrary\AppConfig => to => [ProjectFolder]\AppConfig
5. Copy the following DLL files into your project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - .\NugetPackageSolutions\Sitecore.Project.Template.Solution\Sitecore.Project.Template.Solution.dll
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
    - .\NugetPackageSolutions\Sitecore.Project.Template.Solution\Sitecore.Project.Template.Solution.dll.config
        - Copy to [ProjectFolder]\Dependencies folder (must create this folder) and reference in the project from here
6. Copy the 'AppConfig' folder as is, into your XM/XP project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Project.Template.Solution\AppConfig => to => [ProjectFolder]\AppConfig
        - Open the 'SiteName.Project.WebsiteDEV.config'; 'SiteName.Project.WebsiteQA.config.disabled'; 
        'SiteName.Project.WebsiteUAT.config.disabled' & 'SiteName.Project.WebsitePROD.config.disabled' files:
            - Replace all referenced instances of '{SiteName}' with actual Sitecore Site Item Name, in the Sitecore Content Editor
            - Replace all referenced instances of '{SiteUrlLowerCase}' with actual site Url in lowercase without the www prefix
            - NOTE THE CONFIG FILES, NAMED WITH DISABLED ARE FOR THE NEXT ENVIRONMENTS UP
            - Replace the 'SiteName.' part of all these config files, with the actual name of the Site Item Name, in the Sitecore Content Editor
        - Open the 'SiteName.Project.WebsiteSettings.config' file 
            - Replace all referenced instances of '{SiteName}' with actual Sitecore Site Item Name, in the Sitecore Content Editor
7. Copy the 'Content\SampleContent' folder as is, into your XM/XP project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Project.Template.Solution\Content => to => [ProjectFolder]\Content
        - Rename the '[ProjectFolder]\Content\SampleContent' folder with the actual name of the Site Item Name, in the Sitecore Content Editor
        - Rename all the references in the views for '[ProjectFolder]\Content\SampleContent' folder
8. Copy the 'Scripts' folder as is, into your XM/XP project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Project.Template.Solution\Scripts => to => [ProjectFolder]\Scripts
9. Copy the 'Views\SampleContent' folder as is, into your XM/XP project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Project.Template.Solution\Views => to => [ProjectFolder]\Views
        - Rename the '[ProjectFolder]\Content\SiteNameFolder' folder with the actual name of the Site Item Name, in the Sitecore Content Editor
10. Copy the 'favicon_sitecore.ico' file as is, into your XM/XP project solution (i.e. can only be used even with a Traditional Sitecore XM/XP instance):
    - e.g .\NugetPackageSolutions\Sitecore.Project.Template.Solution\favicon_sitecore.ico => to => [ProjectFolder]
        - Add your own fav icon for each site (i.e. capable of multiples sites under the webroot), using the following format 'favicon_{sitename}.ico' 
        with the actual name of the Site Item Name, in the Sitecore Content Editor (i.e. filename should always be in lowercase)

#### Summary Documention (Pending)