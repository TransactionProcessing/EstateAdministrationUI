﻿@base @shared
Feature: MyMerchants

Background: 

	Given I create the following roles
	| Role Name  |
	| Estate |
	| Merchant |

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
	| ClientId          |
	| serviceClient |

	Given I have created the following estates
	| EstateName       |
	| Test Estate |

	And I have created the following operators
	| EstateName       | OperatorName       | RequireCustomMerchantNumber | RequireCustomTerminalNumber |
	| Test Estate | Test Operator  | True                        | True                        |

	And I have assigned the following operators to the estates
	| EstateName  | OperatorName  |
	| Test Estate | Test Operator |

	And I have created the following security users
	| EmailAddress                     | Password | GivenName  | FamilyName | EstateName       |
	| estateuser@testestate1.co.uk | 123456   | TestEstate | User1      | Test Estate  |

	Given I create the following merchants
	| MerchantName    | SettlementSchedule | AddressLine1   | Town     | Region      | Country        | ContactName    | EmailAddress                 | EstateName       |
	| Test Merchant 1 | Immediate          | Address Line 1 | TestTown | Test Region | United Kingdom | Test Contact 1 | testcontact1@merchant1.co.uk | Test Estate |
	| Test Merchant 2 | Weekly             | Address Line 1 | TestTown | Test Region | United Kingdom | Test Contact 1 | testcontact1@merchant2.co.uk | Test Estate |
	| Test Merchant 3 | Monthly            | Address Line 1 | TestTown | Test Region | United Kingdom | Test Contact 1 | testcontact1@merchant3.co.uk | Test Estate |

	When I assign the following  operator to the merchants
	| OperatorName    | MerchantName    | MerchantNumber | TerminalNumber | EstateName    |
	| Test Operator  | Test Merchant 1 | 00000001       | 10000001       | Test Estate  |
	| Test Operator  | Test Merchant 2 | 00000001       | 10000001       | Test Estate  |
	| Test Operator  | Test Merchant 3 | 00000001       | 10000001       | Test Estate  |

	When I create the following security users
	| EmailAddress                      | Password | GivenName    | FamilyName | MerchantName    | EstateName    |
	| merchantuser1@testmerchant1.co.uk | 123456   | TestMerchant | User1      | Test Merchant 1 | Test Estate |
	| merchantuser1@testmerchant2.co.uk | 123456   | TestMerchant | User1      | Test Merchant 2 | Test Estate |
	| merchantuser1@testmerchant3.co.uk | 123456   | TestMerchant | User1      | Test Merchant 3 | Test Estate |

	When I add the following devices to the merchant
	| DeviceIdentifier | MerchantName    | EstateName    |
	| TestDevice1      | Test Merchant 1 | Test Estate |
	| TestDevice2      | Test Merchant 2 | Test Estate |
	| TestDevice3      | Test Merchant 3 | Test Estate |

	Given I am on the application home page

	And I click on the Sign In Button
	
	Then I am presented with a login screen
	
	When I login with the username 'estateuser@testestate1.co.uk' and password '123456'
	
	Then I am presented with the Estate Administrator Dashboard

Scenario: Dashboard

@PRTest
Scenario: Make Merchant Deposit
	Given I click on the My Merchants sidebar option
	Then I am presented with the Merchants List Screen
	And the following merchants details are in the list
	| MerchantName    | ContactName    | AddressLine1   | Town     | NumberOfUsers | NumberOfDevices | NumberOfOperators |
	| Test Merchant 1 | Test Contact 1 | Address Line 1 | TestTown | 0             | 1               | 1                 |
	| Test Merchant 2 | Test Contact 1 | Address Line 1 | TestTown | 0             | 1               | 1                 |
	| Test Merchant 3 | Test Contact 1 | Address Line 1 | TestTown | 0             | 1               | 1                 |
	When I click the Make Deposit button for 'Test Merchant 1' from the merchant list
	Then I am presented the make merchant deposit screen
	When I make the following deposit
	| DepositAmount | DepositDate | DepositReference |
	| 1000       | Today       | Test Deposit 1   |
	Then I am presented with the Merchants List Screen
	When I select 'Test Merchant 1' from the merchant list
	Then I am presented the merchant details screen for 'Test Merchant 1'
	And the available balance for the merchant should be 1000.00
	Given I click on the My Merchants sidebar option
	Then I am presented with the Merchants List Screen
	And the following merchants details are in the list
	| MerchantName    | ContactName    | AddressLine1   | Town     | NumberOfUsers | NumberOfDevices | NumberOfOperators |
	| Test Merchant 1 | Test Contact 1 | Address Line 1 | TestTown | 0             | 1               | 1                 |
	| Test Merchant 2 | Test Contact 1 | Address Line 1 | TestTown | 0             | 1               | 1                 |
	| Test Merchant 3 | Test Contact 1 | Address Line 1 | TestTown | 0             | 1               | 1                 |
	When I click the Add New Merchant button
	Then I am presented the new merchant screen
	When I enter the following new merchant details
	| MerchantName    | SettlementSchedule |AddressLine1   | Town     | Region     | PostCode | Country        | ContactName    | ContactEmail                    | ContactPhoneNumber |
	| Test Merchant 4 | Monthly            |Address Line 1 | TestTown | TestRegion | TE57 1NG | United Kingdom | Test Contact 4 | testcontact@testmerchant4.co.uk | 0123456789         |
	When I click the Create Merchant button
	Then I am presented the merchant details screen for 'Test Merchant 4'
	And the merchants settlement schedule is 'Monthly'
	
