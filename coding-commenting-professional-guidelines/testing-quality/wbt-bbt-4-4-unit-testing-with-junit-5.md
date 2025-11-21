---
id: kb-testing-quality-005
title: "**WBT** **BBT** – **4.4 Unit testing with JUnit 5**"
domain: testing-quality
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **4.4 Unit testing with JUnit 5**: - Changes to the code can be checked immediately - Better code quality/stability or security - Less hesitation before making changes to core components - Shorter development times, despite additional tests to be implemented - Easier refactoring - Simplified documentation of the implementation - Easy implementation of the tests"
tags:
  - architecture
  - classes
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
  end: 6421
---
## **WBT** **BBT**
### **4.4 Unit testing with JUnit 5**

#### **4.4.1 Unit Tests**


#### Benefits of unit tests:


  - Changes to the code can be checked immediately

  - Better code quality/stability or security

  - Less hesitation before making changes to core components

  - Shorter development times, despite additional tests to be implemented

  - Easier refactoring

  - Simplified documentation of the implementation


#### Requirements for unit tests:


  - Easy implementation of the tests

  - Fast and automated test execution

  - Thorough analysis of the test results

#### **4.4.2 JUnit 5**


[JUnit 5² is a simple Java unit testing framework helping testing on the JVM with a focus on Java](https://junit.org/junit5/) 8 language features, extensibility, and a modern programming API.


#### What are the goals of JUnit 5?


  - Reduce test creation effort to the absolute minimum

  - Easy to learn and use

  - Avoid redundant work

  - Tests must


#### –
be repeatedly applicable

#### –
can be produced separately

**–** be incremental

#### –
can be freely combined

#### –
be feasible also by others than the author

#### –
can also be evaluated by others than the author

  - Enable the use of test data


#### –
Reusability of test data

#### –
Test data generation is usually more complex than the test itself


² [https://junit.org/junit5/](https://junit.org/junit5/)


#### What should we test?


The best thing about unit tests is that you can write them for all your production code classes, regardless of their functionality, complexity, or internal structure. You can perform unit tests for controllers, services, repositories, domain classes, or utility classes. Follow the rule of one test class per production class. This is a good start, but not a must. If you feel that you need to split your test into several test classes, then it’s fine, do that.


A unit test class should test the public interface of the class. If you do this, all other methods will be tested automatically in an indirect way.


If the package structure of your test class is the same as the production class, protected and

package-private methods are accessible from a test class and can be tested as well but testing these methods may be too far. Private methods cannot be tested anyway, because you cannot call them from another test class.


Your test suite should ensure that all your non-trivial code paths are tested. At the same time, they should not be too closely bound to your implementation.


```
#### How to structure tests?


All structure technics has the following steps in common:


    - Setting up the test data

   - Calling the method under test

    - Assert that the expected results are returned


[There are two common patterns, which can be applied to your test structure like Arrange, Act,](https://xp123.com/articles/3a-arrange-act-assert/)
[Assert³ and Given, When, Then⁴, which was developed as part of Behavior-Driven Development⁵](https://xp123.com/articles/3a-arrange-act-assert/)
(BDD).


```
#### What about project structure and naming conventions?


[Nowadays all developers use standard tools for building their projects like Maven⁶ or Gradle⁷.](https://maven.apache.org/) Thus, all production source code resides in src/main/java and all test source files are in src/test/java .


#### Typical project directory structure


1 └── src


2 └── main


3 ├── java

│ └── HelloWorld.java


test


└── java

└── HelloWorldTest.java


Test class names like the above usually will be prefixed with Test . This is a very common naming convention and should be followed consistently. It enables the developer to understand at once which class is being tested. Furthermore, some tools rely on convention.


³ [https://xp123.com/articles/3a-arrange-act-assert/](https://xp123.com/articles/3a-arrange-act-assert/) ⁴ [https://martinfowler.com/bliki/GivenWhenThen.html](https://martinfowler.com/bliki/GivenWhenThen.html) ⁵ [https://dannorth.net/introducing-bdd/](https://dannorth.net/introducing-bdd/) ⁶ [https://maven.apache.org/](https://maven.apache.org/) ⁷ [https://gradle.org/](https://gradle.org/)


```
#### **4.4.3 First unit test**


#### First simple JUnit 5 test


class SimpleTest {


2


private Collection<String> collection = new ArrayList<>();


4


```


void testEmptyCollection() {


assertTrue(collection.isEmpty());


}


```
9


void testOneItemCollection() {


collection.add("First JUnit Test");


```
13


assertEquals(1, collection.size());


}


}


```
#### Execution order


testEmptyCollection()


testOneItemCollection()


[Execution order](https://junit.org/junit5/docs/current/user-guide/#writing-tests-test-execution-order) ⁸ of tests:


By default, test methods will be ordered using an algorithm that is deterministic but intentionally
nonobvious. This ensures that subsequent runs of a test suite execute test methods in the same
order, thereby allowing for repeatable builds. Even if unit tests should normally not depend on
the order in which they are executed, there are times when it is necessary to enforce a certain
order of execution of the test method. To specify the order in which test methods are executed,
annotate your test class or test interface with @TestMethodOrder and provide the preferred MethodOrder
implementation.


With JUnit 5 you can reduce the visibility of the test classes and methods. With JUnit 4
the classes and methods had to be public . In JUnit 5 test classes can have any visibility, it
is recommended to use the default package visibility, which improves the test codebase.


⁸ [https://junit.org/junit5/docs/current/user-guide/#writing-tests-test-execution-order](https://junit.org/junit5/docs/current/user-guide/#writing-tests-test-execution-order)


```
#### **4.4.4 Assertions**


[Assertions⁹ is a collection of utility methods that support asserting conditions in tests. JUnit 5](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Assertions.html) contains many of the assertions of JUnit 4 and several interesting new assertions.


It also provides support for lambda expressions that can be used in assertions. The advantage of using lambda expressions for the assertion message is that it is evaluated lazily, which can reduce time and resources by avoiding the construction of complex messages.


The assertion result of a test provides a specific value. A failed assertion will throw an

AssertionFailedError or subclass thereof.


4.4.1 assertEquals() / assertArrayEquals()


Assert that expected and actual are equal.


```
#### Equality


void equality() {


Calculator calculator = new Calculator();


```
4


assertEquals(2, calculator.add(1, 1));


assertEquals(4, calculator.multiply(2, 2), "Optional failure message");


7


char [] expected = {'J','u','n','i','t'};


char [] actual = "Junit".toCharArray();


10


assertArrayEquals(expected, actual);


}


4.4.2 assertSame() / assertNotSame()


Assert that expected and actual refer to the same object.


```
#### Identity


void identity() {


User john = new User(1, "John", "Rambo");


User rocky = new User(2, "Rocky", "Balboa");


```
5


assertSame(john, john);


assertNotSame(john, rocky);


}


```
⁹ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Assertions.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Assertions.html)


4.4.3 assertTrue() / assertFalse() / assertAll()


Assert that the supplied condition is true or false. assertAll() assert that all supplied executables
do not throw exceptions. It is a possibility to group assertions and have all the failed assertions
reported together.


```
#### Truth


void truth() {


assertTrue('a' < 'b', () -> "Assertion messages can be lazily evaluated"


+ "to avoid constructing complex messages unnecessarily.");


}


```
6


void groupedTruth() {


Person person = new Person("Jane", "Doe");


```
10


assertAll("person",


() -> assertEquals("Jane", person.firstName()),


() -> assertEquals("Doe", person.lastName())


);


}


```
**4.4.4.4** **assertNull()** **/** **assertNotNull()**


Assert that actual is null or not.


#### Existence


void existence() {


assertNotNull( new Calculator());


assertNull( null );


}


```
4.4.5 assertThrows()


Assert that execution of the supplied executable throws an exception of the expected type and return the exception.


```
#### Exceptions


void exceptions() {


Calculator calculator = new Calculator();


```
4


Exception exception = assertThrows(ArithmeticException.class, () -> calculator.divide(1, 0));


assertEquals("/ by zero", exception.getMessage());


}


```


4.4.6 assertTimeout()


Assert that execution of the supplied executable completes before the given timeout is exceeded.
This can be useful for testing the performance and efficiency of code, and for ensuring that it
does not take an excessive amount of time to execute.


```
#### Timeout


void timeout() {


assertTimeout(Duration.ofSeconds(5), () -> TimeUnit.SECONDS.sleep(1));


}


```
**4.4.4.7** **fail()**


Is used to intentionally cause a test to fail.


#### Failing


void failing() {


fail("a failing test");


}


```

#### **4.4.5 Annotations**


**[JUnit supports the following annotations](https://junit.org/junit5/docs/current/user-guide/#writing-tests-annotations)** ¹⁰ **for configuring tests:**


#### @BeforeAll


Methods that are executed when the execution definition is started, before all tests.

Annotated method must be static .


#### @AfterAll


Methods that are executed after all tests, before closing the execution definition. For
> example, resources can be released here. Annotated method must be static .


#### @BeforeEach


Methods that are performed before each test.


#### @AfterEach


Methods that are performed after each test.


#### @Test


The actual test methods. Only methods with this annotation are executed as tests.


#### @Disabled


Temporary deactivation of test methods.

@Tag [Used to declare tags for filtering, either at class or method level. Like Categories¹¹ in JUnit](https://github.com/junit-team/junit4/wiki/Categories)


```
#### @DisplayName

Specification of user-defined names for test classes and methods.


#### @Timeout


Is used to fail a test if its execution exceeds a certain duration.


#### @Nested


Is used to signal that the annotated class is nested. The nested test class must be an inner
class, meaning a non-static class. The nested class can share the setup and state with an
instance of its enclosing class. And, since inner classes cannot have static fields and methods,
this prohibits the use of the @BeforeAll and @AfterAll annotations in nested tests.


```
#### @ParameterizedTest


Is used to signal that the annotated method is a parameterized test method. You have to pick up at least one source of arguments. There are several types of parameter sources you can pick from.


```
#### @TempDir

Can be used to annotate a non-private field in a test class or a parameter in a lifecycle
method or test method of type Path or File that should be resolved into a temporary directory.


¹⁰ [https://junit.org/junit5/docs/current/user-guide/#writing-tests-annotations](https://junit.org/junit5/docs/current/user-guide/#writing-tests-annotations)
¹¹ [https://github.com/junit-team/junit4/wiki/Categories](https://github.com/junit-team/junit4/wiki/Categories)


4.5.1 @Test


```
    - [@Test](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Test.html) ¹² marks the methods to be tested.

    - The annotated methods must not be private or static .

    - Must not return a value, thus the method should return void .

    - Only with @Test annotated methods will be executed.


#### @Test


1 **class TestAnnotationTest** {


2


void simple() {


Collection<Object> collection = new ArrayList<>();


assertTrue(collection.isEmpty());


}


```
8


void lambdaExpressions() {


assertTrue(Stream.of(1, 2, 3)


.stream()


.mapToInt(i -> i)


.sum() > 5, () -> "Sum should be greater than 5");


}


```
16


void groupAssertions() {


int [] numbers = {0, 1, 2, 3, 4};


```
20


assertAll("numbers",


() -> assertEquals(numbers[0], 1),


() -> assertEquals(numbers[3], 3),


() -> assertEquals(numbers[4], 1)


);


}


}


```
¹² [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Test.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Test.html)


4.5.2 @BeforeEach / @AfterEach


```
    - [@BeforeEach](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/BeforeEach.html) ¹³ is used to specify that the annotated method should be executed before **each**
test in the current test class.

    - [@AfterEach](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/AfterEach.html) ¹⁴ is used to specify that the annotated method should be executed after **each** test
in the current test class.


#### @BeforeEach / @AfterEach


class BeforeAndAfterEachAnnotationsTest {


private Collection<String> collection;


3


```
@BeforeEach


void setUp() {


collection = new ArrayList<>();


}


```
8


@AfterEach


void tearDown() {


collection.clear();


}


```
13


void testEmptyCollection() {


assertTrue(collection.isEmpty());


}


```
18


void testOneItemCollection() {


collection.add("itemA");


assertEquals(1, collection.size());


}


}


```
#### Execution order


setUp()


testEmptyCollection()


tearDown()


```
4


setUp()


testOneItemCollection()


tearDown()


```
¹³ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/BeforeEach.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/BeforeEach.html) ¹⁴ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/AfterEach.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/AfterEach.html)


4.5.3 @BeforeAll / @AfterAll


```
    - [@BeforeAll](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/BeforeAll.html) ¹⁵ is used to specify that the annotated method should be executed before **all** tests
in the current test class.

    - [@AfterAll](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/AfterAll.html) ¹⁶ is used to specify that the annotated method should be executed after **all** tests in
the current test class.

    - These annotated methods must be static void and are only executed **once** for a given test
class.


#### @BeforeAll / @AfterAll


class BeforeAndAfterAllAnnotationsTest {


private Collection<String> collection;


3


```
@BeforeAll


static void oneTimeSetUp() {}


@AfterAll


static void oneTimeTearDown() {}


```
8


@BeforeEach


void setUp() {


collection = new ArrayList<>();


}


@AfterEach


void tearDown() {


collection.clear();


}


```
17


void testEmptyCollection() {


assertTrue(collection.isEmpty());


}


void testOneItemCollection() {


collection.add("itemA");


assertEquals(1, collection.size());


}


}


```
#### Execution order


1 oneTimeSetUp()


2


setUp()


testEmptyCollection()


tearDown()


```
6


setUp()


testOneItemCollection()


tearDown()


```
10


oneTimeTearDown()


¹⁵ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/BeforeAll.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/BeforeAll.html)
¹⁶ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/AfterAll.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/AfterAll.html)


4.5.4 @Disabled


```
    - [@Disabled](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Disabled.html) ¹⁷ is used to specify that the annotated test class or test method is currently disabled
and should not be executed.

    - A reason can be declared to the annotation as well to provide more information why the
test class or test method is disabled.

    - Can be applied at the class level as well, which disables **all** test methods within that class.


#### Exclusion of test methods


1 **class DisabledAnnotationTest** {


2


@Disabled


void disabled() {


}


}


```
#### Exclusion of test methods with comment


1 **class DisabledAnnotationTest** {


2


@Disabled("not ready yet")


void disabledWithComment() {


}


}


```
#### Exclusion of test classes


1 @Disabled


2 **class DisabledAnnotationTest** {


3


void disabled() {


}


```
7


void disabledAsWell() {


}


}


```
¹⁷ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Disabled.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Disabled.html)


4.5.5 @DisplayName


```
    - [@DisplayName](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/DisplayName.html) ¹⁸ is used to declare a custom display name for the annotated test class or test
method.

    - Display names are used for reporting in IDEs and build tools and may contain spaces,
special characters, and even emoji.


#### @DisplayName


@DisplayName("A special test case")


class DisplayNameAnnotationTest {


3


```


@DisplayName("Custom test name containing spaces")


void testWithDisplayNameContainingSpaces() {


}


```
8


@DisplayName("°￿°")


void testWithDisplayNameContainingSpecialCharacters() {


}


```
13


@DisplayName("￿")


void testWithDisplayNameContainingEmoji() {


}


}


```
**4.4.5.6** **@Tag**


    - [@Tag](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Tag.html) ¹⁹ marks test classes or test methods.

    - These tags can later be used to filter the recognition and execution of tests.


#### @Tag


@Tag("fast")


@Tag("smoke-test")


class TagAnnotationTest {


```
4


@Tag("math")


void testingMathCalculation() {


}


}


```
¹⁸ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/DisplayName.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/DisplayName.html)
¹⁹ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Tag.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Tag.html)


4.5.7 @Timeout


```
    - [@Timeout](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Timeout.html) ²⁰ is used to define a timeout for a method or all testable methods within one class

and its @Nested classes.

    - Applying this annotation to a test class has the same effect as applying it to all testable
methods.


#### @Timeout


1 **class TimeoutAnnotationTest** {


2


@BeforeEach


@Timeout(5)


void setUp() {


_// fails if execution time exceeds 5 seconds_


}


```
8


@Timeout(value = 100, unit = TimeUnit.MILLISECONDS)


void failsIfExecutionTimeExceeds100Milliseconds() {


_// fails if execution time exceeds 100 milliseconds_


}


```
14


}


²⁰ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Timeout.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Timeout.html)


```
#### **4.4.6 Assumptions**


[Assumptions²¹ are used to perform tests only if certain conditions are met. This is typically used](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Assumptions.html)
for external conditions that are necessary for the test to run properly. If the condition does not
apply, the method is exited.


4.6.1 assumeFalse()


Is used to skip a test if a given condition is false.


```
#### assumeFalse()


void falseAssumption() {


assumeFalse(5 < 1);


```
4


assertEquals(7, 5 + 2);


}


4.6.2 assumeTrue()


Is used to skip a test if a given condition is true.


```
#### assumeTrue()


void trueAssumption() {


assumeTrue(5 > 1);


```
4


assertEquals(7, 5 + 2);


}


4.6.3 assumingThat()


It can be used to skip a test if certain conditions are not met.


```
#### assumingThat()


void assumptionThat() {


String message = "Just a assumptionThat test";


```
4


´ assumingThat(


"Just a assumptionThat test".equals(message),


() -> assertEquals(2 + 2, 4)


);


}


```
²¹ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Assumptions.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.api/org/junit/jupiter/api/Assumptions.html)


#### **4.4.7 Parameterized Tests**


[Parameterised tests²² make it possible to run a test several times with different arguments. They](https://junit.org/junit5/docs/current/user-guide/#writing-tests-parameterized-tests) are declared just like normal @Test methods but use the @ParameterizedTest annotation instead.


#### JUnit supports different sources as arguments:


    - @ValueSource

    - @EnumSource

    - @MethodSource

    - @CsvSource

    - @CsvFileSource

    - @ArgumentsSource

    - @EnumSource

    - @NullSource

    - @EmptySource

    - @NullAndEmptySource


**4.4.7.1** **@ValueSource**


    - [@ValueSource](https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/ValueSource.html) ²³ provides access to an array of values.

    - Supported types are shorts, bytes, ints, longs, floats, doubles, chars, booleans, strings and classes .

    - Only one of the supported types may be specified per @ValueSource declaration.


#### @ValueSource


@ParameterizedTest


@ValueSource(strings = { "otto", "level", "radar", "rotor", "kayak" })


void palindromes(String candidate) {


assertTrue(StringUtils.isPalindrome(candidate));


}


```
6


@ParameterizedTest


@ValueSource(ints = { 1, 2, 3 })


void testWithValueSource( int argument) {


assertTrue(argument > 0 && argument < 4);


}


```
²² [https://junit.org/junit5/docs/current/user-guide/#writing-tests-parameterized-tests](https://junit.org/junit5/docs/current/user-guide/#writing-tests-parameterized-tests) ²³ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/ValueSource.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/ValueSource.html)


4.7.2 @MethodSource


```
    - [@MethodSource](https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/MethodSource.html) ²⁴ provides access to values returned from factory methods of the class in which
this annotation is declared or from static factory methods in external classes referenced by
fully qualified method name.

    - Each factory method must generate a stream of arguments.

    - A stream is anything that JUnit can convert into a Stream, such as Stream, DoubleStream,

LongStream, IntStream, Collection, Iterator, Iterable, an array of objects, or an array of primitives.

    - Factory methods must be static and not declare any parameters.


#### @MethodSource - Stream<String>


@ParameterizedTest


@MethodSource("stringProvider")


void testWithExplicitLocalMethodSource(String argument) {


assertNotNull(argument);


}


```
6


static Stream<String> stringProvider() {


return Stream.of("JUnit 5", "rocks!");


}


```
#### @MethodSource - IntStream


@ParameterizedTest


@MethodSource("range")


void testWithRangeMethodSource( int argument) {


assertNotEquals(9, argument);


}


```
6


static IntStream range() {


return IntStream.range(0, 20).skip(10);


}


```
#### @MethodSource - Stream<Arguments>


@ParameterizedTest


@MethodSource("stringIntAndListProvider")


void testWithMultiArgMethodSource(String str, int num, List<String> list) {


assertEquals(5, str.length());


assertTrue(num >=1 && num <=2);


assertEquals(2, list.size());


}


```
8


static Stream<Arguments> stringIntAndListProvider() {


return Stream.of(


arguments("apple", 1, Arrays.asList("a", "b")),


arguments("lemon", 2, Arrays.asList("x", "y"))


);


}


```
²⁴ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/MethodSource.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/MethodSource.html)


4.7.3 @CsvSource


```
    - [@CsvSource](https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/CsvSource.html) ²⁵ reads comma-separated values (CSV) from one or more supplied CSV lines.


#### @CsvSource


@ParameterizedTest


@CsvSource({


"Bischofsmais, 1",


"Osternohe, 2",


"'Leogang, Austria', 0xF1",


"'Porte du Soleil, France', 4",


})


void testWithCsvSource(String bikepark, int rank) {


assertNotNull(bikepark);


assertNotEquals(0, rank);


}


```
**4.4.7.4** **@CsvFileSource**


#### @CsvFileSource


@ParameterizedTest


@CsvFileSource(resources = "/two-column.csv", numLinesToSkip = 1)


void testWithCsvFileSource(String country, int reference) {


assertNotNull(country);


assertNotEquals(0, reference);


}


```
#### @CsvFileSource


1 #two-column.csv


2 Country, reference


3 Sweden, 1


4 Germany, 2


5 Australia, 3


6 "United States of America", 4


²⁵ [https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/CsvSource.html](https://junit.org/junit5/docs/current/api/org.junit.jupiter.params/org/junit/jupiter/params/provider/CsvSource.html)


4.7.5 @ArgumentsSource


```
#### @ArgumentsSource


@ParameterizedTest


@ArgumentsSource(BikeArgumentsProvider.class)


void testWithArgumentsSource(String argument) {


assertNotNull(argument);


}


```
#### ArgumentsProvider


1 **public class BikeArgumentsProvider implements** ArgumentsProvider {


2


public Stream<? extends Arguments> provideArguments(ExtensionContext context) {


return Stream.of("Specialized", "Cube").map(Arguments::of);


}


}


```
