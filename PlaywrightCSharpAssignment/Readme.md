Commands to run tests

    To Run All tests 
    - dotnet test

    To Run API tests 
    - dotnet test --filter "Category=API"

    To Run UI tests 
    - dotnet test --filter "Category=UI"

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
    - Page object Model used
    - Test Cases annotation used for supplying varying test data
    - Some screen shots captured 
    - Different styles of locators used

- What I would cover If i had the Time:
    - I would add specflow BDD style tests
    - Add more asserts
    - Add more test cases across the flow
    - Allure style Report generation
    - Jenkins integration for CI/CD or Azure pilepines config for Azure specific Deployment



