# OuinneBiseSharp

[![Build status][build-badge]][build-status]
[![Tests status][tests-badge]][tests-status]
[![Downloads status][downloads-badge]][downloads-status]
[![Version status][version-badge]][version-status]
[![Download][download-badge]][download-link]
[![CodeFactor](https://www.codefactor.io/repository/github/bizydev/ouinnebisesharp/badge)](https://www.codefactor.io/repository/github/bizydev/ouinnebisesharp)     
.NET Standard 2.0 library to access WinBIZ Cloud API. Be aware, not all WinBIZ Cloud API methods are implemented yet.

# Getting Started
1. Install [Bizy.OuinneBiseSharp](https://www.nuget.org/packages/Bizy.OuinneBiseSharp/) NuGet package to your project
2. Create a `OuinneBiseSharpService` instance
``` csharp
var service = new OuinneBiseSharpService(winBizCompanyName, winBizUsername, winBizPassword, winBizCompanyId, winBizYear, winBizKey, appName);
```
| Param             | Exemple Value      | Remark                                                                                                                                                                    |
|-------------------|--------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| winBizCompanyName | Lighthouse Gmbh    | The name of the Company for which the Cloud subscription has been opened. The application must prompt the user for his Company Name.                                      |
| winBizUsername    | TestUser           | The username of the Cloud user. The username is chosen by the user when he subscribes to the service. The application must prompt the user for his username               |
| winBizPassword    | password           | The password of the Cloud user. The password is chosen by the user when he subscribes to the service. The application must prompt the user for the password.              |
| winBizCompanyId   | 1000               | The WinBIZ Folder Number. See  this article if you dont know where to find the winbiz-companyid. The list of available companies can be obtained via the method  Folders. |
| winBizYear        | 2017               | The fiscal year of the WinBIZ Folder specified in winbiz-companyid. The list of available fiscal years can be obtained via the method  Folders.                           |
| winBizKey         | #YOUR_DEV_API_KEY# | The security key obtained from LOGICIAL SA.                                                                                                                               |
| appName           | BizyDev            | Application name, will be used in error messages    

3. Call a method, for exemple `stock`:`available`
```csharp
var stockResult = await _service.Stock(StockMethodsEnum.Available, productId);
```

See `StockMethodsEnum` for availables sub-methods in `stock`.   
4. Explore the [WinBIZ Cloud API](https://winbiz.zendesk.com/hc/en-us/categories/115000186633-API-WinBIZ-Cloud) doc for more infos.
5. Rule the world !

# Build and Test
1. Build    
    1.1. Install [.NET Core 2.0 SDK](https://www.microsoft.com/net/download/windows) if not done yet   
    1.2. Clone the code   
    1.3. Build => this will also restore needed packages   
2. Test   
    2.1 Restore WinBIZ company backup from `files/Entreprise d'Informatique_9996_2018.wb_backup` to your WinBIZ Cloud, this is a basic backup from WinBIZ Eval Company   
    2.2 Set the following environment variables   
    ```powershell
    setx WINBIZ_API_KEY winBizKey
    setx WINBIZ_API_COMPANY winBizCompanyName
    setx WINBIZ_API_USERNAME winBizUsername
    setx WINBIZ_API_PASSWORD winBizPassword
    ```
    2.2' Or update `ApiServiceTests` constructor and replace environment variables with pures values, those values are private so DON'T commit them when creating PRs   
    2.3 Run the tests, all test should be green   
    2.4 Happy testing !    


Thanks to [AppVeyor](https://www.appveyor.com/) for providing free continuous integration for open-source projects.

[build-badge]: https://img.shields.io/appveyor/ci/bizy/ouinnebisesharp.svg?maxAge=3600?logo=appveyor
[build-status]: https://ci.appveyor.com/project/Bizy/ouinnebisesharp

[tests-badge]: https://img.shields.io/appveyor/tests/bizy/ouinnebisesharp.svg?maxAge=3600?logo=appveyor
[tests-status]: https://ci.appveyor.com/project/Bizy/ouinnebisesharp/build/tests

[downloads-badge]: https://img.shields.io/nuget/dt/Bizy.OuinneBiseSharp.svg?maxAge=3600
[downloads-status]: https://www.nuget.org/stats/packages/Bizy.OuinneBiseSharp?groupby=Version

[version-badge]: https://img.shields.io/nuget/v/Bizy.OuinneBiseSharp.svg?maxAge=3600
[version-status]: https://www.nuget.org/packages/Bizy.OuinneBiseSharp/

[download-badge]: https://img.shields.io/badge/NuGet-Download-blue.svg
[download-link]: https://www.nuget.org/packages/Bizy.OuinneBiseSharp
