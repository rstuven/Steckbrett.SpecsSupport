@model_mapping
Feature: Assertions

Background:

    Given the following instances of Customer:
    | Id | FirstName |
    |  1 | A         |
    |  2 | B         |
    |  3 | C         |

Scenario: Counting 0 examples
	
    Then 0 of the following instances of Customer should exist:
    | Id |
    | 99 |

Scenario: Counting 2 examples

    Then 2 of the following instances of Customer should exist:
    | Id |
    | 99 |
    |  2 |
    |  1 |

Scenario: Any example should exist

    Then any of the following instances of Customer should exist:
    | FirstName |
    | X |
    | C |
    | Z |

Scenario: All examples should exist

    Then all of the following instances of Customer should exist:
    | FirstName |
    | B |
    | C |
    | A |

Scenario: No example should exist

    Then any of the following instances of Customer should not exist:
    | FirstName |
    | X |
    | Y |
    | Z |
