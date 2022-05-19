# Sitecore-XM-Integrations
Sitecore-XM-Integrations for Composible DXP Solutions


## Sitecore.Foundation.Extensions Project (Now included in the main Solution)
1. This includes build-in extension method functionality for the client's wanting to integrate with a Traditional Sitecore XM/XP instance implementation 
    - These extension methods can be accessed via the following files under the src\Middleware\src\SitecoreExtensions\code folder:
        - \Extensions\FieldExtensions.cs => Extension methods for Sitecore Field Item object from the Sitecore XM/XP instance        
            - Please see the method comments that indicate what each extension method is responsible for
        - \Extensions\ItemExtensions.cs => Extension methods for Sitecore Item objects from the Sitecore XM/XP instance
            - Please see the method comments that indicate what each extension method is responsible for
        - \Extensions\SitecoreLoggingExtensions.cs => Extension methods for Sitecore Field Item object from the Sitecore XM/XP instance
            - This method can be used with or without a Sitecore XM/XP instance
            - Note the LogException() method contains the following configurable params
                - bcreateCustomLogFile (bool param) => Which determines the logging exceptions in a newly created custom log file/s or in 
                the default Sitecore XM/XP created log files. Note the param should always be passed in as true if the client is not using a Sitecore XM/XP instance
                - appLogFileKey (string param) => This applies an application prefix key to the name of the custom file being created. 
                    - Note: if the appLogFileKey is empty the fallback file prefix key will be applied as 'CustomApiLogs'
                    - Note: 1 log file gets created per day per application (i.e. if you have more than 1 site/application under the webroot).
                    - Note: a Sitecore XM/XP instance can handle as many sites/applications under the same webroot (each with there own Domain Url and branding and layout)
            - Note the LogApiResponseMessages() and LogExceptionNotification() methods content the following configrable params
                - These methods can be used with or without a Sitecore XM/XP instance, i.e. will always create custom log files
                - appLogFileKey (string param) => This applies an application prefix key to the name of the custom file being created. 
                    - Note: if the appLogFileKey is empty the fallback file prefix key will be applied as 'CustomApiLogs'
                    - Note: 1 log file gets created per day per application (i.e. if you have more than 1 site/application under the webroot).
                    - Note: a Sitecore XM/XP instance can handle as many sites/applications under the same webroot (each with there own Domain Url and branding and layout)
            - Please see the method comments that indicate what each extension method is responsible for
        - \Extensions\HTMLHelperExtensions.cs => Extension methods for handling MvcHtmlString repsonses on Sitecore Item object's data values from the Sitecore XM/XP instance
            - Please see the method comments that indicate what each extension method is responsible for
2. The following is now included a build-in extension method functionality for clients whether or not they using a Traditional Sitecore XM/XP instance implementation for their applications
    - These extension methods can be accessed via the following files under the src\Middleware\src\SitecoreExtensions\code folder:
        - \Extensions\RenderingExtensions.cs => Extension methods for .NET MVC or .NET Core MVC rendering backend rendering engine
            - Please see the method comments that indicate what each extension method is responsible for
        - \Extensions\RenderingParameterExtensions.cs => Extension methods for .NET MVC or .NET Core MVC rendering backend rendering engine
            - Please see the method comments that indicate what each extension method is responsible for
        - \Extensions\DataTypeExtensions.cs => Static class Extension methods for various .NET data types (i.e string, DateTime, int, Guid, etc...)
            - Please see the method comments that indicate what each extension method is responsible for
        - \Extensions\Helpers.cs => Static class Extension Helper methods
            - Please see the method comments that indicate what each extension method is responsible for
            - Examples:
                - ToJson(this object value) => <returns>The JSon string value from the JSon object</returns>
                - ToJsonObject(this object value) => <returns>The JSon object value</returns>
                - GetMethodName([CallerMemberName] string callerMethod = "") => <returns>The Calling Method's name string value or empty</returns>
                - SetMethodName(string callerMethod) => <returns>The callerMethod string value using the callerMethod param</returns>
                - FileDateStamp(DateTime dateTime) => <returns>The File DateStamp string value</returns> (format as MM/dd/yyyy)
        - \Extensions\JsonNetResult.cs => Extension methods for JsonNetResult responses
            - Please see the method comments that indicate what each extension method is responsible for
        - \Extensions\SessionExtensions.cs => Static class Extension methods for the HttpSessionStateBase object
            - Please see the method comments that indicate what each extension method is responsible for
            - Examples:
                - GetAndRemove(this HttpSessionStateBase session, string key) => <returns>The HttpSessionStateBase object after clearing it's session value</returns>
        - \Extensions\StringExtensions.cs => Static class Extension methods on string objects
            - Please see the method comments that indicate what each extension method is responsible for
            - Examples:
                - Humanize(this string input) => <returns>The Humanize value from as string object, after replacing not allowed characters, returned as a string value</returns>
                - ToCssUrlValue(this string url) => <returns>The CssUrlValue from as string object, as a string value</returns>
                - StripHtml(this string html) => <returns>The content from a Html string object retrieved from the HtmlDocument object, as a string value</returns>
                - IHtmlString RenderValue(this string value) => <returns>The clean Html content, as a Html string value</returns>
                - RemoveSpecifiedChars(this string value, string regexPattern, bool setLowerCase = false) => <returns>The string value after removing an array of regex characters from the string, as a string value</returns>