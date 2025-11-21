---
id: kb-testing-quality-006
title: "**WBT** **BBT** – **4.5 More on Unit Tests**"
domain: testing-quality
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **4.5 More on Unit Tests**: - Test cases are based on requirements and features, not on the methods to be tested. - There is no 1:1 correspondence between the test case method and the tested method."
tags:
  - classes
  - clean-code
  - comments
  - example
  - formatting
  - functions
  - guideline
  - naming
  - principles
  - process
  - testing
source_lines:
  start: 4688
  end: 7012
---
## **WBT** **BBT**
### **4.5 More on Unit Tests**

#### **4.5.1 Heuristics**


#### Testing features, not methods


    - Test cases are based on requirements and features, not on the methods to be tested.

   - There is no 1:1 correspondence between the test case method and the tested method.

    - Test cases should document how to use the classes correctly using examples.


class EuroTest {


@Test void newEuro() {...}


@Test void getIntAmount() {...}


@Test void getAmount() {...}


@Test void plus() {...}


@Test void minus() {...}


}


```


class EuroTest {


@Test void createFromInt() {...}


@Test void createFromString() {...}


@Test void rounding() {...}


@Test void simpleAddition() {...}


}


```
#### Testing at the edges


    - Most algorithmic errors occur at the edges of the allowed value ranges.

    - Can the value range be divided into several samples equivalent for the test? At least one
sample per equivalence class!

   - What is not tested may not work.


#### Implementation independence


    - Tests are directed against the public class interface.

    - Tests based on the innards of a class are extremely fragile.

    - Wanting access to variables or private methods shows that the code still lacks a crucial
design idea.


#### Orthogonal test cases


    - Test cases are independent of each other if they refer to orthogonal aspects.


    - Often a test can be extremely simplified by making assumptions that another test has
already verified.

    - If one gets into the trouble of having to adapt too many tests just to make a code change,
the tests are not orthogonal.


#### Record results in the test


    - Expected values are coded as constants, not calculated again in the test.

    - If we reproduce application logic in the test, we also reproduce its errors.


#### Performance


1 **class EuroTest** {


2


void multiYearInterest() {


double amount = 100.0;


double interest = 5.0;


double expectedInterest = amount * Math.pow((1 + interest / 100.0), 3.0);


```
8


assertEquals(expectedInterest, calculator.interest(amount, interest, 3), 0.001);


}


}


```


1 **class EuroTest** {


2


void multiYearInterest() {


double amount = 100.0;


double interest = 5.0;


```
7


assertEquals(115.76, calculator.interest(amount, interest, 3), 0.001);


}


}


```
#### Do not forget about exceptions and errors!


    - Often the error cases are insufficiently tested. But exactly these are important to understand
how the code behaves when it is not used correctly.


#### Remove redundancy in test code!


    - The DRY Principle applies to test code as well.

    - Redundancies should be avoided to reduce the impact on test code in the event of future
changes to the production code.


#### Keep test cases short and understandable!


  - Do not write tests with big test methods.

  - Split the test into multiple test methods.


#### Choose meaningful test case names!


  - Naming is hard but try to communicate with the method test name what the test is doing.

  - Be consistent and choose a naming approach that fits best.

  - Test features not methods, this helps to divide tests in clean test cases with mean fulling
method names.


#### Do not trade test code as the second citizen in your codebase!


  - Test code should be no less important than production code.

  - Do not treat test code worse, treat it like production code and use the same coding standards.


#### Remove flaky tests!


  - Since software tests serve as an early warning of potential regressions, they should always
work reliably. A failed test should be cause for concern, and a broken build should
immediately investigate why the test failed. It is a stop-the-world event.

  - This approach can only work for tests that fail in a deterministic way. A test that sometimes
fails and sometimes passes is unreliable and completely corrupts the entire test suite. This
can have negative effects on the team regarding tests.

  - Developers no longer trust tests and soon ignore them. Even if nonflaky tests fail, it is
difficult to detect them in several broken tests. On the other hand, it is difficult to understand
whether new failures are new or whether they come from existing flaky tests.


```
#### **4.5.2 Naming of test methods**


There are several ways to name the test methods. The most important thing is to be consistent and to define a convention within the team.


#### Approach 1:


Describe the facts of the test case.


#### Fact of the test as name


@Test void newAccount() {}


@Test void withdraw() {}


@Test void cannotWithdrawNegativeAmount() {}


@Test void cannotWithdrawUncoveredAmount() {}


```
#### Approach 2:


Describe the desired behavior of the test case.


#### Desired behaviour as name


@Test newAccountShouldReturnCustomer() {}


@Test newAccountShouldHaveZeroBalance() {}


@Test withdrawShouldReduceBalanceByAmount() {}


@Test withdrawNegativeAmountShouldThrowException() {}


@Test withdrawNegativeAmountShouldNotChangeBalance() {}


```

#### **4.5.3 Object Mother**


[An Object Mother²⁶ is a class that can be used in testing, which allows us to provide pre-](https://martinfowler.com/bliki/ObjectMother.html) configured objects for our tests. This example here is rather trivial but imagine this in a real application with classes having many attributes and/or compositions.


Most tests no longer instantiate objects themselves but go through a factory. In a future evolution, if a new mandatory attribute is added to a class, then all you must do is to modify the factory and all the tests will pass.


The combination of builders allows the objects generated by the Object Mother to be customized for the needs of the test. Otherwise, it would be necessary to multiply the factories for each need with a method taking in parameter all the necessary information for the tested case.


```
#### Classical approach


1 User user = **new** User("user", "password", "USER")


2 Authentication auth = **new** UsernamePasswordAuthenticationToken(user, **null**, user.authorities());


#### Object Mother Pattern


Authentication user = TestAuthentications.authenticatedUser();


Authentication admin = TestAuthentications.authenticatedAdmin();


Authentication tester = TestAuthentications.authenticated( new User("user", "password", "TESTER"));


```
#### Object Mother Pattern implementation


public final class TestAuthentications {


2


private static final User USER = new User("user", "password", "USER");


private static final User ADMIN = new User("admin", "password", "USER, ADMIN");


5


private TestAuthentications() {


}


8


```
public static Authentication authenticatedAdmin() {


return authenticated(ADMIN);


}


```
12


public static Authentication authenticatedUser() {


return authenticated(USER);


}


```
16


public static Authentication authenticated(User user) {


return new UsernamePasswordAuthenticationToken(user, null, user.authorities());


}


}


```
²⁶ [https://martinfowler.com/bliki/ObjectMother.html](https://martinfowler.com/bliki/ObjectMother.html)


#### **4.5.4 Test Data Builder**


The Object Mother and Builder patterns each bring their own set of advantages to our tests but can also be combined to further enhance the readability and maintainability of our tests. For a complex object the Test Data Builder Pattern described in the book _Growing Object-Oriented_ _Software, Guided by Tests_ is a perfect solution for creating test objects.


```
#### Use of a Test Data Builder


1 Invoice anInvoice = **new** InvoiceBuilder().build();


#### Builder Pattern


1 **public class InvoiceBuilder** {


2


Recipient recipient = new RecipientBuilder().build();


InvoiceLines lines = new InvoiceLines( new InvoiceLineBuilder().build());


PoundsShillingsPence discount = PoundsShillingsPence.ZERO;


```
6


public InvoiceBuilder withRecipient(Recipient recipient) {


this .recipient = recipient;


}


public InvoiceBuilder withInvoiceLines(InvoiceLines lines) {


this .lines = lines;


}


public InvoiceBuilder withDiscount(PoundsShillingsPence discount) {


this .discount = discount;


}


public Invoice build() {


return new Invoice(recipient, lines, discount);


}


}


```
#### Combined Builder Pattern


Invoice invoiceWithNoPostcode = new InvoiceBuilder()


.withRecipient( new RecipientBuilder()


.withAddress( new AddressBuilder()


.withNoPostcode()


.build())


.build())


.build();


```


#### Combined Builder Pattern, shorter


Invoice invoice = new InvoiceBuilder()


.withRecipient( new RecipientBuilder().withAddress( new AddressBuilder().withNoPostcode())))


.build();


```
#### Combined Builder Pattern, even shorter


Invoice invoice = anInvoice()


.fromRecipient(aRecipient().withAddress(anAddress().withNoPostcode()))


.build();


```
#### Combined Builder Pattern, shortest


Invoice invoice = anInvoice()


.from(aRecipient().with(anAddress().withNoPostcode()))


.build();


```

#### **4.5.5 F.I.R.S.T**


Follow five rules that help writing clean tests, which define the **F.I.R.S.T** ²⁷ acronym.


#### Fast


  - Tests should be fast.

  - If tests are slow, they are run less frequently and therefore do not find problems early
enough.


#### Independent


  - Tests should not be interdependent but should be executable independently.

  - A test should not set conditions for the next test and should be executable in any order.

  - If tests are interdependent, the first one to fail triggers a cascade of further failed tests
downstream.


#### Repeatable


  - Tests should be repeatable in any environment.

  - Tests should be executable in the production environment, in the QA environment, and on
the laptop on the train without a network.


#### Self-Validating


  - Tests should have a boolean output.

  - Either they are passed, or they fail.

  - The manual study of log files should be avoided.

  - If the tests do not validate themselves, failure can become subjective and a long manual
evaluation may be required to execute the tests.


#### Timely


  - Tests must be written in time.

  - Unit tests should be written just before the production code which ensures that they are
passed.

  - If tests are developed after the production code, the production code may be difficult to test.


²⁷
**Robert, Martin** : _Clean Code – A Handbook of Agile Software Craftsmanship_
