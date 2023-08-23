# Azure Betting Odds App

[![ASP.NET Core Web App Build/Test/Deploy workflow](https://github.com/chrisbrown-01/Azure-Betting-Odds-App/actions/workflows/Betting-Odds-App.yml/badge.svg)](https://github.com/chrisbrown-01/Azure-Betting-Odds-App/actions/workflows/Betting-Odds-App.yml)

[![Azure Function Build/Test/Deploy workflow](https://github.com/chrisbrown-01/Azure-Betting-Odds-App/actions/workflows/BettingOddsApiAzureFunction.yml/badge.svg)](https://github.com/chrisbrown-01/Azure-Betting-Odds-App/actions/workflows/BettingOddsApiAzureFunction.yml)

[![CodeQL](https://github.com/chrisbrown-01/Azure-Betting-Odds-App/actions/workflows/codeql.yml/badge.svg)](https://github.com/chrisbrown-01/Azure-Betting-Odds-App/actions/workflows/codeql.yml)

This is an ASP.NET Core Razor pages web app that calculates winnings and total payout amounts when provided betting odds and the amount of the bet in dollars. Users can also search for an MMA fighter's name and 
retrieve betting odds if they have an upcoming bout. All aspects of this application are completely cloud-hosted in Azure. 

###Web App###
- Visit the site here: https://betting-odds-app.azurewebsites.net/
- Hosted in the cloud with Azure App Service
- Upon search of an MMA fighter's name, an HTTP request is made to the Azure Function which returns any betting lines
- Full CI/CD pipeline implemented with automated build/test/deploy upon new commits using Github Actions and Azure Deployment Center integration
- Test suite implemented using xUnit and Playwright (NUnit option)

###Azure Function###
- HTTP-triggered Azure function that returns any upcoming bout odds for a fighter specified by name
- Upon receiving HTTP GET request with fighter name in query string, the function makes a request to the https://the-odds-api.com/ API, caches the response then parses for matching fighter names
- Returns a JSON response of any upcoming betting lines and providers
- Configuration variables set via Azure Configuration settings

