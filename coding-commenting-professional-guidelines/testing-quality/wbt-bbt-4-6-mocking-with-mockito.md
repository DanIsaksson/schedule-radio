---
id: kb-testing-quality-007
title: "**WBT** **BBT** – **4.6 Mocking with Mockito**"
domain: testing-quality
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **4.6 Mocking with Mockito**: [Mockito²⁸ is a Java mocking framework for creating test double objects for unit tests of Java](https://site.mockito.org/) programs. Most of the time the code we write depends on other dependencies."
tags:
  - architecture
  - classes
  - clean-code
  - comments
  - formatting
  - functions
  - principles
  - process
  - testing
source_lines:
  start: 4688
  end: 7396
---
## **WBT** **BBT**
### **4.6 Mocking with Mockito**


[Mockito²⁸ is a Java mocking framework for creating test double objects for unit tests of Java](https://site.mockito.org/)

programs.


#### Why do we need test double objects?


Most of the time the code we write depends on other dependencies. If we are using unit testing,
often the code delegates some work to other methods in other classes or our test must depend
on those methods. But we want the tests to be independent of all other dependencies.


Test double objects help us to isolate a unit of code and test it alone. Replace DOCs ( _Depended_
_On Component_ ) and get control over the environment in which the SUT ( _System Under Test_ ) is
running and test the state and verify interactions.

```
#### **4.6.1 Types of Test Double**


Test Double is a generic term used by _Gerard Meszaros_ in his book _xUnit Test Patterns_ to mean any case where you replace a production object for testing purposes.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-114-0.png)


#### Types of Test Double


#### Test stub


used for providing the tested code with _indirect input_ .
#### Mock object

used for verifying _indirect output_ of the tested code, by first defining the expectations before the tested code is executed.

#### Test spy

used for verifying _indirect output_ of the tested code, by asserting the expectations afterward, without having defined the expectations before the tested code is executed. It helps in recording information about the indirect object created.
```
#### Fake object

used as a simpler implementation, e.g. using an in-memory database in the tests instead of
doing real database access.
#### Dummy object

used when a parameter is needed for the tested method but without needing to use the
parameter.


²⁸ [https://site.mockito.org/](https://site.mockito.org/)


Dummies and stubs are used for preparing the environment for testing. Spies and mocks are
for verifying the correctness of the communication.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-115-0.png)


```
#### Types of Test Double


[There are two styles²⁹ regarding mocking, the classical and the mockist TDD style. The classic](https://martinfowler.com/articles/mocksArentStubs.html#ClassicalAndMockistTesting) TDD style is to use real objects when possible and a double when it is uncomfortable to use the real thing. However, a mocking TDD practitioner will always use a mock.

```
#### **4.6.2 Activation**


Activating Mockito can be done with JUnit 5 @ExtendWith annotation or programmatically, by
invoking MockitoAnnotations.openMocks() .


#### @ExtendWith


@ExtendWith(MockitoExtension.class)


class MockitoAnnotationTest {


...


}


```
#### MockitoAnnotations.openMocks()


1 **class MockitoTest** {


2


@BeforeEach


void init() {


MockitoAnnotations.openMocks( this );


}


}


```
²⁹ [https://martinfowler.com/articles/mocksArentStubs.html#ClassicalAndMockistTesting](https://martinfowler.com/articles/mocksArentStubs.html#ClassicalAndMockistTesting)


#### **4.6.3 Annotations**


[Mockito supports the following annotations³⁰ for configuring mocked tests:](https://javadoc.io/doc/org.mockito/mockito-core/latest/org/mockito/Mockito.html)


#### @Mock


Will create a new mock implementation for the given class. It will not create a real object.

**@Spy** Create a wrapper around a real instance and spy on that real object.


#### @InjectMocks

Will inject the created mock to a given class instance.


#### @Captor

[Is used to create an ArgumentCaptor³¹ instance which is used to capture method argument](https://javadoc.io/doc/org.mockito/mockito-core/latest/org/mockito/ArgumentCaptor.html)
values for further assertions.


6.3.1 @Mock


With @Mock, a mock instance of a class will be created.


```
    - The instance is entirely instrumented to track interactions with it.

    - This is not a real object and does not maintain the state changes.


**Manually with** **Mockito.mock()**


void whenNotUsingMockAnnotation() {


List mock = Mockito.mock(ArrayList.class);


```
4


mock.add("Java");


Mockito.verify(mock).add("Java");


assertEquals(0, mock.size());


```
8


Mockito.when(mock.size()).thenReturn(2020);


assertEquals(2020, mock.size());


}


```
@Mock Annotation


@Mock


List<String> mock;


3


```


void whenUsingMockAnnotation() {


mock.add("Java");


Mockito.verify(mock).add("Java");


assertEquals(0, mock.size());


```
9


Mockito.when(mock.size()).thenReturn(2020);


assertEquals(2020, mock.size());


}


```
³⁰ [https://javadoc.io/doc/org.mockito/mockito-core/latest/org/mockito/Mockito.html](https://javadoc.io/doc/org.mockito/mockito-core/latest/org/mockito/Mockito.html) ³¹ [https://javadoc.io/doc/org.mockito/mockito-core/latest/org/mockito/ArgumentCaptor.html](https://javadoc.io/doc/org.mockito/mockito-core/latest/org/mockito/ArgumentCaptor.html)


6.3.2 @Spy


With @Spy, a real instance of the class will be created.


```
    - You can track every interaction with it.

    - It maintains the state changes.


**Manually with** **Mockito.spy()**


void whenNotUsingSpyAnnotation() {


List<String> spy = Mockito.spy( new LinkedList<>());


```
4


spy.add("Java");


Mockito.verify(spy).add("Java");


assertEquals(1, spy.size());


```
8


Mockito.doReturn(2020).when(spy).size();


assertEquals(2020, spy.size());


}


```
@Spy Annotation


@Spy


List<String> spy = new LinkedList<>();


3


```


void whenUsingSpyAnnotation() {


spy.add("Java");


Mockito.verify(spy).add("Java");


assertEquals(1, spy.size());


```
9


Mockito.doReturn(2020).when(spy).size();


assertEquals(2020, spy.size());


}


```
**4.6.3.3** **@Captor**


#### @Captor


1 @ExtendWith(MockitoExtension.class)


2 **class MockitoCaptorTest** {


3


4 @Mock


5 **private** List mock;


6


7 @Captor


8 **private** ArgumentCaptor captor;


9


void whenUseCaptorAnnotation() {


mock.add("Java");


Mockito.verify(mock).add(captor.capture());


```
14


assertEquals("Java", captor.getValue());


}


}


```
