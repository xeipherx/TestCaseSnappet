Feature: ExceptionHandlingFeature
	This feature file holds our different scenarios for handling weird characters/names


Background: 
	Given User 'alice' is logged in with password 'alice' and scope 'librarian'
	Then The response contains a access token

Scenario: Alice creates a shelf with a weird name and adds a new weird book to the shelf
	Given Alice sets up a new shelf with name '鸳鸯' and ID '0'
	When Alice gets a new book called '鸳鸯' and places it in shelf number '0'
	Then There should be a total of '1' shelves and a total of '1' books

@ignore
Scenario: Alice tries to remove a shelf when there are no shelves
	Given Alice removes shelf with name 'pina coladas' and id '0'
	Then There should be an exception that there is nothing to remove