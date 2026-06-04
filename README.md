# Coffee Machine API – Weather Enhancement

## Overview  
Extension of the original coffee machine API to include weather-based behavior using a third-party service abstraction.

## Endpoint  
GET /brew-coffee

## Added Behavior  
- If temperature is greater than 30°C, returns "Your refreshing iced coffee is ready"
- Otherwise returns "Your piping hot coffee is ready"  

## Notes
- Added IWeatherService abstraction  
- Uses FakeWeatherService by default for consistent behavior  
- Includes OpenWeatherService as sample of real API integration (does not include API key)
