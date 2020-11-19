Feature: ReaderTestCases
	This feature file holds our different scenarios for our dear reader bob

Background: 
	Given User 'bob' is logged in with password 'bob' and scope 'reader'
	Then The response contains a access token

@ignore
Scenario: Bob wants to get a book
	Given The number of books available is not '0'
	When Bob requests for a book
	Given User 'alice' is logged in with password 'alice' and scope 'librarian'
	Then The response contains a access token
	When Alice checks if a book is available and picks one at random
	Then The book gets removed from the shelf