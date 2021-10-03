@GetTemperature
Feature: GetTemperature

Scenario Outline: Get the temeperature of the city
	Given the city name as <cityName>
	When I make a GET request to the currenttemp endpoint
	Then I get the response
	And the response code is 200
	And the result should not be null

	Examples:
	| cityName  |
	| Covilha   |
	| Delft     |
	| Amsterdam |

Scenario: Get the client error when invalid city name is given
	Given the city name as empty
	When I make a GET request to the currenttemp endpoint
	Then the response code is 400
