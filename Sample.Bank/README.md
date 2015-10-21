Solution Sample.Bank
====================
Solutio to convert bank account number to 14 digit Finnish Bank Account number

Sample.Bank Solution Strucure
-----------------------------
	|---Sample.Bank.Main - Console Application, just to run DLL with sample data
	|---Sample.Bank.Service - DLL Class implementation of AccountNumber
	|---Sample.Bank.Test - Unit Test Methods

Features
--------
* Accepts bank account number in non-electronic format and coverts to electronic format
* Check Digit verification and exception
* Easy to follow simple logical solution 
* TDD approach used to quickly test DLL
* Sample console application as per the requirement

Unit Tests
----------
* ArgumentErrorExceptionTest
* ArgumentOutOfRangeExceptionTest
* CommercialBankAccountNumberTest
* CommercialBankAccountNumberFourDigitTest
* SavingsBankAccountNumberTest
* SavingsBankAccountNumberLastFourDigitsTest
* SavingsBankAccountNumberLastEightDigitsTest
* SavingsBankAccountNumberLastTwoDigitsTest

Approach
--------
The main logic is implemented in addPaddingZeros() and checkDigit(). Followed the Requirement Spec to create
a DLL. Padding zeros is decided based on the bank type. Check digit calculation is coded as per the requirement
spec document.

Future Enhancements/Improvements
--------------------------------
* Functions addPaddingZeros() and checkDigit() are implemented with first-cut logic. 
  They could be implemented with an optimized logic
* More unit tests to detect bugs

Build Environment
-----------------
* C# Language
* Visual Studio Community 2015

Known Issues
------------
* No runtime errors
* Unit Test framework does not generate/store the test result