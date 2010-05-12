@model_mapping
Feature: Model usage

Scenario: Getting not created instance by Id
	
	When I get an instance of Customer with Id 123
	Then I should get null

Scenario: Getting created instance by Id
	
	Given an instance of Customer with Id 123
	When I get an instance of Customer with Id 123
	Then I should get an instance of Customer with Id 123

Scenario: Generating a number of instances
	Given I created 3 instances of Customer
	Then I should have 3 instances of Customer

Scenario: Generating a number of instances and removing them by type
	Given I created 3 instances of Customer
     And I created 2 instances of Order
    When I remove the instances of Customer
	Then I should have 0 instances of Customer
	Then I should have 2 instances of Order

Scenario: Generating a number of instances and removing them all
	Given I created 3 instances of Customer
     And I created 2 instances of Order
    When I remove all the instances
	Then I should have 0 instances of Customer
	Then I should have 0 instances of Order

