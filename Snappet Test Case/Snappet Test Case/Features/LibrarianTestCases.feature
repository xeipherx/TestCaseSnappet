Feature: LibrarianTestCases
	This feature file holds our different scenarios for our dear librarian alice


Background: 
	Given User 'alice' is logged in with password 'alice' and scope 'librarian'
	Then The response contains a access token

Scenario: Alice creates a new shelf and adds a new book to the shelf
	Given Alice sets up a new shelf with name 'Cheese' and ID '0'
	When Alice gets a new book called 'Cheesy adventures of peanutbutter' and places it in shelf number '0'
	Then There should be a total of '1' shelves and a total of '1' books

Scenario: Alice Deletes a shelf
	Given Alice sets up a new shelf with name 'Nutella' and ID '1'
	When Alice removes that shelf
	Then There should be a total of one less shelf

