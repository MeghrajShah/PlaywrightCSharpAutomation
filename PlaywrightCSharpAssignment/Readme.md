Commands to run tests:

    To Clean project
    - dotnet clean

    To Build project
    - dotnet build

    To Run All tests 
    - dotnet test

    To Run API tests 
    - dotnet test --filter "Category=API"

    To Run UI tests 
    - dotnet test --filter "Category=UI"

    To Run UI tests written in Screenplay pattern
    - dotnet test --filter "Category=Screenplay"


Setup Guidance:

    Install below: 
        • Install dotnet sdk - brew install --cask dotnet-sdk
        • Check installation using -  dotnet --version
        • Install node js - brew install node
        • brew install --cask powershell
    

    I'm using a Mac - So I have uses VS Code IDE
        • Install VS code - https://code.visualstudio.com/Download
        • Add VS Code plugin - C# Dev kit 
    
    Create new project and add below packages:

        Step	                            Command
    Create project folder	            mkdir PlaywrightCSharpAssignment
    Go to folder	                    cd PlaywrightCSharpAssignment
    Create new Automation project	    dotnet new classlib -n PlaywrightCSharpAssignment
    Add package for Nunit 	            dotnet add package Nunit
    Add Nunit Test Adapter	            dotnet add package Nunit3TestAdapter
    Add package for Playwright	        dotnet add package Microsoft.Playwright.CLI
    Add Nunit package for playwright	dotnet add package Microsoft.Playwright.NUnit
    Add API Testing package	            dotnet add package RestSharp
    Add assertions package	            dotnet add package FluentAssertions
    Add playwright browsers support	    dotnet add package playwright

    Install playwright browsers for chrome, ff, webkit using - 	pwsh bin/Debug/net8.0/playwright.ps1 install



Whats covered in this project

- UI tests to 
    - Sign Up
    - Login
    - Search for products and filter by brand, gender and product type
    - Add items to cart and assert cart contents
    - Remove items from cart an check cart is empty

- API tests
    - Get request to products endpoint 
    - Post Request to searchProduct endpoint and return specific products using varying test case data

- Features Covered
    - Used C#, Playwright, NUNIT to write UI tests , and Rest-Sharp for API tests
    - NUNIT - Test fixtures used , Tagged UI and API tests for easily running UI and API tests separately if required
    - Covers Data driven testing using TestCase annotations
    - Page object Model used
    - Test Cases annotations used for supplying varying test data
    - Screenshots captured for UI tests
    - Different styles of locators used
    - Added Screen play pattern style tests in UITests_Screenplay.cs

- What I would cover If i had the Time:
    - I would add Specflow BDD style tests as well
    - Add more asserts to cover the journey in detail
    - Add more test cases across the flow
    - Allure style Report generation
    - Jenkins config for CI/CD integration on AWS or GCP or Azure-pilelines config for Azure specific Deployment
    - Add some Data Integrity tests as a separate framework
    - Performance metrics reporting


Originality Statement

This is my original work from scratch and all packages used are listed in this file.
