# Introduction 
This project performs temperature comparison between a website and an api

# Framework Features
1.	CoreLibrary - Supports as a library for the test project(TestSuite). Help in creating driver (only chrome supports as of now can be extended for other browsers). Helper classes for API, File operations, Test Reports, Exception Handler, Test Execution Order(BaseHelper.cs) 
2.	TestSuite - POM maintained for each pages as such Home , Weather. Step definitions used to build functionalities. Test folder contains specs &  api folder contains WeatherAPI
3.	Extent Report - Report directory created under TestSuite/Reports/index.html . Failure cases handled with screenshots
4.	Test - "HappyFlowTest" will assert the celsius and fahrenheit provided from the UI and convert to kelvin and compare it with API data

# Build and Test
1.  Install Visual Studio latest
2.  clone the project
3.  WeatherAutomation/TestSuite.sln - Double click and open the project from Visual Studio
4.  From the VS 2019 build the solution
5.  Open Test Explorer and ensure the test are identified
6.  Run Test
