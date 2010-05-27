@model_mapping
Feature: AutoMapper generation
	In order to create beautiful feature specifications
	As a developer using SpecFlow
	I want to map table rows to custom objects
	
Scenario: Mapping table to Customer
	
	Given the following instance of Customer:
	| FirstName | LastName |
	| John      | Doe      |
	
	Then a Customer called John Doe should exist

Scenario: Mapping consecutive tables to Customer
	
	Given the following instance of Customer:
	| FirstName | LastName |
	| John      | Doe      |

	Given the following instance of Customer:
	| FirstName | LastName |
	| Peter     | Smith    |

	Then 2 instances of Customer should exist

Scenario: Mapping self-referencing table to Customer
	
	Given the following instance of Customer:
	| Id | FirstName | LastName | Parent |
	|  1 | John      | Doe      |   null |
	|  2 | John      | Doe      |      1 |

	 Then I should have Customer 1 referencing to no parent customer
	  And I should have Customer 2 referencing to parent customer 1

Scenario: Mapping tables to Customer and Order, relating Customer by Id

	Given the following instance of Customer:
	| Id |
	| 50 |
	| 99 |

	Given the following instance of Order:
	| Id | Customer |
	|  5 |       99 |
	|  6 |       50 |
	|  7 |       99 |
	
	Then I should have the Order 5 related to Customer 99
	And I should have the Order 6 related to Customer 50
	And I should have the Order 7 related to Customer 99

Scenario: Mapping tables to Order and Details, add to parent list with explicit typing

	Given the following instance of Order:
	| Id |
	|  1 |
	
	And the following instances of Detail added to Details of Order 1:
	| Price | Quantity |
	|   150 |        2 |
	|   200 |        1 |
	
	When I calculate Order 1 total
	
	Then I should get total 500 in Order 1

Scenario: Mapping tables to Order and Details, add to parent list with implicit typing

	Given the following instance of Order:
	| Id |
	|  1 |
	
	And the following instances added to Details of Order 1:
	| Price | Quantity |
	|   150 |        2 |
	|   200 |        1 |
	
	When I calculate Order 1 total
	
	Then I should get total 500 in Order 1

Scenario: Mapping tables to Order and Details, pass to parent method with explicit typing

	Given the following instance of Order:
	| Id |
	|  1 |
	
	And the following instances of Detail passed to AddDetail of Order 1:
	| Price | Quantity |
	|   150 |        2 |
	|   200 |        1 |
	
	When I calculate Order 1 total
	
	Then I should get total 500 in Order 1

Scenario: Mapping tables to Order and Details, pass to parent method with implicit typing

	Given the following instance of Order:
	| Id |
	|  1 |
	
	And the following instances passed to AddDetail of Order 1:
	| Price | Quantity |
	|   150 |        2 |
	|   200 |        1 |
	
	When I calculate Order 1 total
	
	Then I should get total 500 in Order 1
	
Scenario: Mapping from file

	Given instances of Customer from file Data\Customers.gherkin
	Then 15 instances of Customer should exist

Scenario: Mapping consecutively from file

	Given instances of Customer from file Data\Customers.gherkin
	And instances of Customer from file Data\Customers.gherkin
	Then 30 instances of Customer should exist
	
Scenario: Mapping from file taking some rows

	Given take 2 instances of Customer from file Data\Customers.gherkin
	Then 2 instances of Customer should exist
	And the first instance of Customer should have Id 1

Scenario: Mapping from file taking zero rows

	Given take 0 instances of Customer from file Data\Customers.gherkin
	Then 0 instances of Customer should exist

Scenario: Mapping from file taking all rows using -1 convention # This is useful in a scenario outline

	Given take -1 instances of Customer from file Data\Customers.gherkin
	Then 15 instances of Customer should exist

Scenario: Mapping from file skipping some rows

	Given skip 10 instances of Customer from file Data\Customers.gherkin
	Then 5 instances of Customer should exist
	And the first instance of Customer should have Id 11

Scenario: Mapping from file skipping zero rows

	Given skip 0 instances of Customer from file Data\Customers.gherkin
	Then 15 instances of Customer should exist
	And the first instance of Customer should have Id 1

Scenario: Mapping from file skipping and taking some rows

	Given skip 10 and take 2 instances of Customer from file Data\Customers.gherkin
	Then 2 instances of Customer should exist
	And the first instance of Customer should have Id 11

Scenario: Mapping from file and passing to method of instance with explicit typing
    Given the following instance of Group:
    | Id |
    |  1 |
	And instances of Customer passed to AddCustomer of Group 1 from file Data\Customers.gherkin
    Then the Group 1 should have 15 Customers

Scenario: Mapping from file and passing to method of instance with implicit typing
    Given the following instance of Group:
    | Id |
    |  1 |
	And instances passed to AddCustomer of Group 1 from file Data\Customers.gherkin
    Then the Group 1 should have 15 Customers
