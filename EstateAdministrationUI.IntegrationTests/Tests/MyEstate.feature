@base @shared
Feature: MyEstate

Background: 

	Given I create the following roles
	| Role Name  |
	| Estate |

	Given I create the following api scopes
	| Name                 | DisplayName                  | Description                        |
	| estateManagement | Estate Managememt REST Scope | A scope for Estate Managememt REST |
	| transactionProcessor | Transaction Processor REST Scope | Scope for Transaction Processor REST |
	| fileProcessor | File Processor REST Scope | Scope for File Processor REST |

	Given I create the following api resources
	| Name             | DisplayName            | Secret  | Scopes           | UserClaims               |
	| estateManagement | Estate Managememt REST | Secret1 | estateManagement | merchantId,estateId,role |
	| transactionProcessor | Transaction Processor REST | Secret1 | transactionProcessor | merchantId,estateId,role |
	| fileProcessor | File Processor REST | Secret1 | fileProcessor | merchantId,estateId,role |

	Given I create the following identity resources
	| Name    | DisplayName          | Description                                                 | UserClaims                                                             |
	| openid  | Your user identifier |                                                             | sub                                                                    |
	| profile | User profile         | Your user profile information (first name, last name, etc.) | name,role,email,given_name,middle_name,family_name,estateId,merchantId |
	| email   | Email                | Email and Email Verified Flags                              | email_verified,email                                                   |

	Given I create the following clients
	| ClientId       | Name            | Secret  | Scopes                                                                   | GrantTypes         | RedirectUris                         | PostLogoutRedirectUris                | RequireConsent | AllowOfflineAccess | ClientUri            |
	| serviceClient  | Service Client  | Secret1 | estateManagement,transactionProcessor                                    | client_credentials |                                      |                                       |                |                    |                      |
	| estateUIClient | Merchant Client | Secret1 | estateManagement,fileProcessor,transactionProcessor,openid,email,profile | hybrid             | https://localhost:[port]/signin-oidc | https://localhost:[port]/signout-oidc | false          | true               | https://[url]:[port] |

	Given I have a token to access the estate management resource
	| ClientId      |
	| serviceClient |

	Given I have created the following estates
	| EstateName  |
	| Test Estate |

	And I have created the following operators
	| EstateName  | OperatorName  | RequireCustomMerchantNumber | RequireCustomTerminalNumber |
	| Test Estate | Test Operator | True                        | True                        |

	And I have created the following security users
	| EmailAddress                 | Password | GivenName  | FamilyName | EstateName  |
	| estateuser@testestate1.co.uk | 123456   | TestEstate | User1      | Test Estate |

	Given I am on the application home page

	And I click on the Sign In Button
	
	Then I am presented with a login screen
	
	When I login with the username 'estateuser@testestate1.co.uk' and password '123456'
	
	Then I am presented with the Estate Administrator Dashboard

Scenario: View Estate
	Given I click on the My Estate sidebar option
	Then I am presented with the Estate Details Screen
	And My Estate Details will be shown
	| EstateName       |
	| Test Estate |

Scenario: View My Operators
	Given I click on the My Operators sidebar option
	Then I am presented with the Operators List Screen
	And the following operator details are in the list
	| OperatorName       |
	| Test Operator |

Scenario: Create New Operator
	Given I click on the My Operators sidebar option
	Then I am presented with the Operators List Screen
	And the following operator details are in the list
	| OperatorName       |
	| Test Operator |
	When I click the Add New Operator button
	Then I am presented the new operator screen
	When I enter the following new operator details
	| OperatorName    |
	| Test New Operator |
	When I click the Create Operator button
	Then I am presented with the Operators List Screen
	And the following operator details are in the list
	| OperatorName       |
	| Test Operator |
	| Test New Operator |

@PRTest
Scenario: Create New Contract
	Given I click on the My Contracts sidebar option
	Then I am presented with the Contracts List Screen
	# Create Contract
	When I click the Add New Contract button
	Then I am presented the new contract screen
	When I enter the following new contract details
	| OperatorName       | ContractDescription |
	| Test Operator | Test Contract       |
	When I click the Create Contract button
	Then I am presented with the Contracts List Screen
	And the following contract details are in the list
	| ContractDescription |
	| Test Contract       |	
	# Create Products - Fixed Value
	When I click the Products Link for 'Test Contract'
	Then I am presented with the Products List Screen
	When I click the Add New Product button
	Then I am presented the new product screen
	When I enter the following new product details
	| ProductName         | DisplayText | Value  |
	| 100 KES Topup       | 100 KES     | 100.00 |
	When I click the Create Product button
	Then I am presented with the Products List Screen
	And the following product details are in the list
	| ProductName         |
	| 100 KES Topup       |
	# Create Transaction Fee - Fixed Value Product
	When I click the Transaction Fees Link for '100 KES Topup'
	Then I am presented with the Transaction Fee List Screen
	When I click the Add New Transaction Fee button
	Then I am presented the new transaction fee screen
	When I enter the following new transaction fee details
	| Description         | CalculationType | FeeType  | Value |
	| Merchant Commission | Percentage      | Merchant | 0.05  |
	When I click the Create Transaction Fee button
	Then I am presented with the Transaction Fee List Screen
	And the following fee details are in the list
	| Description        |
	| Merchant Commission  |

	# Create Products - Variable Value
	#When I click the Create Product button
	#Then I am presented the new product screen
	#When I enter the following new product details
	#| ProductName        | DisplayText | Value |
	#| Custom Value Topup | Custom      |       |
	#When I click the Create Product button
	#Then I am presented with the Products List Screen
	#And the following product details are in the list
	#| ProductName        |
	#| Custom Value Topup |
	
	## Create Transaction Fee - Variable Value Product
	#When I click the Transaction Fees Link for 'Custom Value Topup'
	#When I click the Create Transaction Fee button
	#Then I am presented the new transaction fee screen
	#When I enter the following new transaction fee details
	#| Description         | CalculationType | FeeType  | Value |
	#| Merchant Commission | Percentage      | Merchant | 0.05  |
	#When I click the Create Transaction Fee button
