@base @shared
Feature: MyEstate

Background: 

	Given I create the following roles
	| Role Name  |
	| Estate[id] |

	Given I create the following api resources
	| Name                 | DisplayName            | Secret  | Scopes           | UserClaims               |
	| estateManagement[id] | Estate Managememt REST | Secret1 | estateManagement[id] | MerchantId,EstateId,role |

	Given I create the following identity resources
	| Name    | DisplayName          | Description                                                 | UserClaims                                                             |
	| openid  | Your user identifier |                                                             | sub                                                                    |
	| profile | User profile         | Your user profile information (first name, last name, etc.) | name,role,email,given_name,middle_name,family_name,EstateId,MerchantId |
	| email   | Email                | Email and Email Verified Flags                              | email_verified,email                                                   |

	Given I create the following clients
	| ClientId           | Name            | Secret  | Scopes                                    | GrantTypes         | RedirectUris                        | PostLogoutRedirectUris               | RequireConsent | AllowOfflineAccess |
	| serviceClient[id]  | Service Client  | Secret1 | estateManagement[id]                      | client_credentials |                                     |                                      |                |                    |
	| estateUIClient[id] | Merchant Client | Secret1 | estateManagement[id],openid,email,profile | hybrid             | http://localhost:[port]/signin-oidc | http://localhost:[port]/signout-oidc | false          | true               |

	Given I have a token to access the estate management resource
	| ClientId          |
	| serviceClient[id] |

	Given I have created the following estates
	| EstateName       |
	| Test Estate [id] |

	And I have created the following operators
	| EstateName       | OperatorName       | RequireCustomMerchantNumber | RequireCustomTerminalNumber |
	| Test Estate [id] | Test Operator [id] | True                        | True                        |

	And I have created the following security users
	| EmailAddress                     | Password | GivenName  | FamilyName | EstateName       |
	| estateuser[id]@testestate1.co.uk | 123456   | TestEstate | User1      | Test Estate [id] |

	Given I am on the application home page

	And I click on the Sign In Button
	
	Then I am presented with a login screen
	
	When I login with the username 'estateuser[id]@testestate1.co.uk' and password '123456'
	
	Then I am presented with the Estate Administrator Dashboard

Scenario: View Estate
	Given I click on the My Estate sidebar option
	Then I am presented with the Estate Details Screen
	And My Estate Details will be shown
	| EstateName       |
	| Test Estate [id] |
