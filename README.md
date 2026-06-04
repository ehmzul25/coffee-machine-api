# Coffee Machine API

## Overview
HTTP API that controls an imaginary internet-connected coffee machine.

## Endpoint
GET /brew-coffee

## Behavior
- Returns 200 OK with coffee message
- Every 5th request returns 503
- April 1 returns 418 "I'm a teapot"

## Tech Stack
- .NET 8 Web API
- NUnit (Unit Testing)
- Integration Testing (WebApplicationFactory)

## How to Run
1. Clone the repo
2. Open in Visual Studio
3. Run the project
4. Test via Swagger
