# Crayon.ExchangeRateService
Code Test for Crayon

# Running
1. Run Crayon.ExchangeRateService
2. Call api by passing query string "api/exchange-rates/{sourceCurrency}/{targetCurrency}/{dates}"
eg: https://localhost:44344/api/exchange-rates/SEK/NOK/2018-02-01,2018-02-15,2018-03-01


# Configuration

Should the service for getting exchange rates differ in environments or ever change this can be updated in appsettings.json

# Nuget Packages

I've made use of Scrutor (https://github.com/khellang/Scrutor) for quicker service registration just because some articles mentioned this was convenient in .NET 5 but it's not a must have.

Used several nuget packages for unit testing but also just helpers which aren't must haves.
NSubstitute
TestStack
Shouldly

# Performance
Some additional performance benefits may come from caching the results from the exchangeapi since historical data never changes. Didn't feel it was necessary in this use case though.

Developer could also make small performance improvement by passing contiguous dates to the exchangehost api but it would unlikely make much difference unless users were requesting many days at once. Since the application is returning an average I would assume a date range would make more sense in a real world scenario, rather than individual dates. The Time Series end point offered by exchangeratehost suggests that is how most users would request this data. 