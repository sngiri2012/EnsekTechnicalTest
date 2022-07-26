Feature: Orders
	Simple calculator for adding two numbers
Background: 
	Given the user athenticated with username as 'test' and password as 'testing'

@orderFuelType @all
Scenario: Reset test data
	Given the user reset the test data
    When the user purchases '1' units of each fuel type
	Then the above purchased orders should be in the order list
 
@printOrderCount @all
Scenario: Print how many orders created before today
   Given the user counts and prints the number of orders created before today
