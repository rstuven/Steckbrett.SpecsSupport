@model_mapping
Feature: Model usage

Scenario: Getting not created instance by Id
	
    Then an instance of Customer with Id 123 should not exist

Scenario: Getting created instance by Id
	
	Given an instance of Customer with Id 123
    Then an instance of Customer with Id 123 should exist

Scenario: Generating a number of instances
	Given I created 3 instances of Customer
	Then 3 instances of Customer should exist

Scenario: Generating a number of instances and removing them by type
	Given I created 3 instances of Customer
     And I created 2 instances of Order
    When I remove the instances of Customer
	Then 0 instances of Customer should exist
	Then 2 instances of Order should exist

Scenario: Generating a number of instances and removing them all
	Given I created 3 instances of Customer
     And I created 2 instances of Order
    When I remove all the instances
	Then 0 instances of Customer should exist
	Then 0 instances of Order should exist

