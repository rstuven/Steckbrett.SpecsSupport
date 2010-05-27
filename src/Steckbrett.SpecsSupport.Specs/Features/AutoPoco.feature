@model_mapping
Feature: AutoPoco generation
	In order to be the greatest lazy developer
	As a lazy developer using SpecFlow
	I want to automagically generate random object instances

Scenario: Generating a random instance
	Given a random instance of Customer
	Then 1 random instance of Customer should exist

Scenario: Generating a number of random instances
	Given 3 random instances of Customer
	Then 3 random instances of Customer should exist

Scenario: Generating a consecutive number of random instances
	Given 3 random instances of Customer
	Given 2 random instances of Customer
	Then 5 random instances of Customer should exist

Scenario: Generating random instances related by IList property with explicit typing
	Given a random instance of Order
	And 5 random instances of Detail added to Details of Order 1
	Then a random instance of Order with 5 random Details should exist

Scenario: Generating random instances related by IList property with implicit typing
	Given a random instance of Order
	And 5 random instances added to Details of Order 1
	Then a random instance of Order with 5 random Details should exist

Scenario: Generating random instances related by method with explicit typing
	Given a random instance of Order
	And 5 random instances of Detail passed to AddDetail of Order 1
	Then a random instance of Order with 5 random Details should exist

Scenario: Generating random instances related by method with implicit typing
	Given a random instance of Order
	And 5 random instances passed to AddDetail of Order 1
	Then a random instance of Order with 5 random Details should exist
