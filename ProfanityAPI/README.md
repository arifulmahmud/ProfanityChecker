# ProfanityChecker
This a sample WEB API(.NET C#) to check profanity in a given file. Project is developed with ASP.NET WEB API 2 [https://dotnet.microsoft.com/apps/aspnet/apis].<br/>
One API endpoint is exposed [{Server_URI}/api/profanity]

### Setup the environment for running the application 
Copy the folder `ProfanityChecker\ProfanityAPI` in your prefered location and Go to ProfanityAPI folder and open ProfanityAPI.sln project with Visual Studio. Install required dependencies with Nuget packages. 


### Run the application

Build and Run the project with Visual Studio. Debug the application with Visual Studio (F5 shortcut), the application should be running with IIS express.<br />
Same API is running live in Azure : [http://profanityapp.azurewebsites.net/api/profanity]. So the API can be tested using Postman/ with the client application<br />
This project includes simple test case as well, Visual Studio menu -> Test -> Run All Tests. <br/>
You can also attach the project with local IIS server. Just add an application pointing to : `ProfanityChecker\ProfanityAPI\ProfanityAPI`<br/> after building the project with Visual Studio. 
Make sure to cange `const apiEndpoint` in `ProfanityClientApp/src/App.js` accordingly.