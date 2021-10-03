# assessment-rest-api
REST API for getting the current temperature by city name. Give the "city" as query parameters
You can give any city name to fetch the current temperature of any city.
Internally using the Open Weather API to get the current temperature by city name and parsing its value to return to the Temperature model. 

## Environments

* [Development] Example: (https://localhost:5001/)

## Features

* Get the current temperature of the city, use the relative url as temperature/currenttemp?city=covilha

### Requirements

* [Visual Studio code] (https://code.visualstudio.com/download)
* [C# extension for VS Code] (https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
* [DotNet Core installed .netCore 3.1] (https://www.microsoft.com/net/download)


## How to run it locally using Visual Studio Code
If you want to get API up and running locally you can follow this steps:

* Download the repository from git
* Open the repository in VS code
* Make sure that all requirements are installed in the VS code which has the C# support
* Click on New Terminal from the Menu bar Terminal
* Browse the folder structure where the source project file is and run the command dotnet run: VopakAssesmentApp\src\VopakAssesmentApp> dotnet run
* The server will be up and running and will start listening on https://localhost:5001 (SSL) and http://localhost:5000
* Go to your web browser and go to the above url and enter the full API path: example: https://localhost:5001/temperature/currenttemp?city=covilha


## Guide for running acceptance tests
If you want to run the tests locally:
* Open a New Terminal from the same Terminal menu
* Browse to the tests project location and run the command "dotnet test", VopakAssesmentApp\test\VopakAssessment.tests> dotnet test
* You should see all the tests passing and that the test run is successfull
