---
id: kb-general-002
title: "**Tweet This Book!**"
domain: general
parent_heading: "General"
intent: "**Tweet This Book!**: [Please help Martin Hock by spreading the word about this book on Twitter!](http://twitter.com) [The suggested hashtag for this book is #CleanCodeFundamentals.](https://twitter.com/search?q=%23CleanCodeFundamentals) Find out what other people are saying about the book by clicking on this link to search for this hashtag on Twitter:"
tags:
  - architecture
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
  start: 21
  end: 227
---
## **Tweet This Book!**

[Please help Martin Hock by spreading the word about this book on Twitter!](http://twitter.com)


[The suggested hashtag for this book is #CleanCodeFundamentals.](https://twitter.com/search?q=%23CleanCodeFundamentals)


Find out what other people are saying about the book by clicking on this link to search for this
hashtag on Twitter:


[#CleanCodeFundamentals](https://twitter.com/search?q=%23CleanCodeFundamentals)


```
# **Contents**

Preface . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . i

What is the goal of this book? . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . i Which topics can you expect? . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . i Who should read this book? . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . ii

What you must learn about Software Development yourself? . . . . . . . . . . . . . . . ii What about the code examples and typographic conventions? . . . . . . . . . . . . . . iii Which literature is this book based on? . . . . . . . . . . . . . . . . . . . . . . . . . . . . iii

Giving Feedback? . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . iv


Introduction to Software Craftsmanship and Clean Code . . . . . . . . . . . . . . . 1 1 A Passion for Software Development . . . . . . . . . . . . . . . . . . . . . . . . . 1 2 Manifesto for Software Craftsmanship . . . . . . . . . . . . . . . . . . . . . . . . 4 3 Clean Code Developer . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 5 4 Boy Scout Rule . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 7 5 Broken Windows Theory . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 8 6 Cargo Cult Programming . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 9 7 Knowledge - Expertise . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 10


Basics of Software Design . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 11 1 Software Design Pyramid . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 11 2 Basic concepts of OOD . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 12 3 Goals of Software Design . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 15 4 Symptoms of _bad_ design . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 17 5 Criteria for _good_ design . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 18 6 Information Hiding . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 19 7 Cohesion . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 22

8 Coupling . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 24 9 Cohesion - Coupling . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 25 10 Big Ball of Mud . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 27 11 Architecture Principles . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 28 12 Cognitive Psychology and Architectural Principles . . . . . . . . . . . . . . . . 29 13 Layered Architecture . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 31 13.1 Use of Layered Architecture . . . . . . . . . . . . . . . . . . . . . . . . 31 13.2 Violated Layered Architecture . . . . . . . . . . . . . . . . . . . . . . . 33 13.3 Horizontal Layering . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 34 13.4 Feature-Based Layering - Single package . . . . . . . . . . . . . . . . 35 13.5 Feature-Based Layering - Slices before layers . . . . . . . . . . . . . 37


CONTENTS


13.6 Feature-Based Layering – Hexagonal Architecture . . . . . . . . . . 38 13.7 The Java Module System . . . . . . . . . . . . . . . . . . . . . . . . . . 39 14 Architecture Documentation . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 40

15 Testing the Architecture and Design . . . . . . . . . . . . . . . . . . . . . . . . . 42 16 Software Engineering Values . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 44 17 Team Charter . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 45


Clean Code Best Practices . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 46

1 Communicate through code . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 46 1.1 Use Java code conventions and avoid misinformation . . . . . . . . 46 1.2 Choose an expressive name and avoid mental mapping . . . . . . . 48 1.3 Make differences clear with meaningful variable names . . . . . . . 48 1.4 Use pronounceable names . . . . . . . . . . . . . . . . . . . . . . . . . 49 1.5 Do not hurt the readers . . . . . . . . . . . . . . . . . . . . . . . . . . . 50

1.6 Don‘t add redundant context . . . . . . . . . . . . . . . . . . . . . . . 51

1.7 Don’t add words without additional meaning . . . . . . . . . . . . . 53 1.8 Don’t use _and_ or _or_ in method names . . . . . . . . . . . . . . . . . . 54

1.9 Use positive names for boolean variables and functions . . . . . . . 55 1.10 Respect the order within classes . . . . . . . . . . . . . . . . . . . . . 56 1.11 Group by line break . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 58 1.12 Prefer self-explanatory code instead of comments . . . . . . . . . . . 59 1.13 Refactor step by step . . . . . . . . . . . . . . . . . . . . . . . . . . . . 61 2 Bad comments . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 62

2.1 Redundant comments . . . . . . . . . . . . . . . . . . . . . . . . . . . . 62

2.2 Misleading comments . . . . . . . . . . . . . . . . . . . . . . . . . . . . 62 2.3 Mandatory comments . . . . . . . . . . . . . . . . . . . . . . . . . . . . 63 2.4 Diary comments . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 63 2.5 Gossip . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 64 2.6 Position identifier . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 64

2.7 Write-ups and incidental remarks . . . . . . . . . . . . . . . . . . . . 65 2.8 Don’t leave commented out code in your codebase . . . . . . . . . . 65 2.9 Rules for commenting . . . . . . . . . . . . . . . . . . . . . . . . . . . . 65 3 Classes and objects . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 66 3.1 Classes . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 66

3.2 Functions . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 67

3.3 Variables . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 68

4 Shapes of code . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 69 4.1 Spikes . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 69 4.2 Paragraphs . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 69 4.3 Paragraphs with headers . . . . . . . . . . . . . . . . . . . . . . . . . . 70 4.4 Suspicious comments . . . . . . . . . . . . . . . . . . . . . . . . . . . . 70 4.5 Intensive use of an object . . . . . . . . . . . . . . . . . . . . . . . . . . 70


Software Quality Assurance . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 71 1 Test Pyramid . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 71 2 Test Classification . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 72

3 Test-driven Development (TDD) . . . . . . . . . . . . . . . . . . . . . . . . . . . . 73


CONTENTS


4 Unit testing with JUnit 5 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 75 4.1 Unit Tests . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 75

4.2 JUnit 5 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 75

4.3 First unit test . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 77

4.4 Assertions . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 78

4.4.1 assertEquals() / assertArrayEquals() . . . . . . . . . . . . . . 78

4.4.2 assertSame() / assertNotSame() . . . . . . . . . . . . . . . . . 78

4.4.3 assertTrue() / assertFalse() / assertAll() . . . . . . . . . . . 79

4.4.4 assertNull() / assertNotNull() . . . . . . . . . . . . . . . . . 79

4.4.5 assertThrows() . . . . . . . . . . . . . . . . . . . . . . . . . . 79

4.4.6 assertTimeout() . . . . . . . . . . . . . . . . . . . . . . . . . 80

4.4.7 fail() . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 80

4.5 Annotations . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 81

4.5.1 @Test . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 82

4.5.2 @BeforeEach / @AfterEach . . . . . . . . . . . . . . . . . . . . 83

4.5.3 @BeforeAll / @AfterAll . . . . . . . . . . . . . . . . . . . . . 84

4.5.4 @Disabled . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 85

4.5.5 @DisplayName . . . . . . . . . . . . . . . . . . . . . . . . . . . 86

4.5.6 @Tag . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 86

4.5.7 @Timeout . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 87

4.6 Assumptions . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 88

4.6.1 assumeFalse() . . . . . . . . . . . . . . . . . . . . . . . . . . 88

4.6.2 assumeTrue() . . . . . . . . . . . . . . . . . . . . . . . . . . . 88

4.6.3 assumingThat() . . . . . . . . . . . . . . . . . . . . . . . . . . 88

4.7 Parameterized Tests . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 89

4.7.1 @ValueSource . . . . . . . . . . . . . . . . . . . . . . . . . . . 89

4.7.2 @MethodSource . . . . . . . . . . . . . . . . . . . . . . . . . . 90

4.7.3 @CsvSource . . . . . . . . . . . . . . . . . . . . . . . . . . . . 91

4.7.4 @CsvFileSource . . . . . . . . . . . . . . . . . . . . . . . . . . 91

4.7.5 @ArgumentsSource . . . . . . . . . . . . . . . . . . . . . . . . . 92

5 More on Unit Tests . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 93

5.1 Heuristics . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 93

5.2 Naming of test methods . . . . . . . . . . . . . . . . . . . . . . . . . . 96 5.3 Object Mother . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 97 5.4 Test Data Builder . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 98

5.5 F.I.R.S.T . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 100

6 Mocking with Mockito . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 101 6.1 Types of Test Double . . . . . . . . . . . . . . . . . . . . . . . . . . . . 101 6.2 Activation . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 102

6.3 Annotations . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 103

6.3.1 @Mock . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 103

6.3.2 @Spy . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 104

6.3.3 @Captor . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 104 7 Code Coverage . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 105 8 Static Code Analysis . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 107 9 Continuous Integration . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 108


CONTENTS


9.1 Differences between CI, CD, and CD . . . . . . . . . . . . . . . . . . 109

9.2 CI Workflow . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 111

9.3 Preconditions . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 112

9.4 Advantages and Disadvantages . . . . . . . . . . . . . . . . . . . . . . 113 9.5 Best Practices . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 113


Design Principles . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 114 1 Goal of Design Principles . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 114 2 Overview of Design Principles . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 114 3 SOLID Principles . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 116 3.1 Single Responsibility Principle . . . . . . . . . . . . . . . . . . . . . . 116 3.1.1 Example: Modem . . . . . . . . . . . . . . . . . . . . . . . . . 116 3.1.2 Example: Book . . . . . . . . . . . . . . . . . . . . . . . . . 119 3.1.3 Example: Product . . . . . . . . . . . . . . . . . . . . . . . . 121 3.2 Open Closed Principle . . . . . . . . . . . . . . . . . . . . . . . . . . . 122 3.2.1 Example: LoanRequestHandler . . . . . . . . . . . . . . . . . 122 3.2.2 Example: Shape . . . . . . . . . . . . . . . . . . . . . . . . . 126 3.2.3 Example: HumanResourceDepartment . . . . . . . . . . . . . . . 128 3.2.4 Example: Calculator . . . . . . . . . . . . . . . . . . . . . . 130 3.2.5 Example: FileParser . . . . . . . . . . . . . . . . . . . . . . 132 3.3 Liskov Substitution Principle . . . . . . . . . . . . . . . . . . . . . . . 135 3.3.1 Example: Rectangle . . . . . . . . . . . . . . . . . . . . . . . 135 3.3.2 Example: Coupon . . . . . . . . . . . . . . . . . . . . . . . . 139 3.3.3 Example: Bird . . . . . . . . . . . . . . . . . . . . . . . . . 143 3.4 Interface Segregation Principle . . . . . . . . . . . . . . . . . . . . . . 146 3.4.1 Example: MultiFunctionDevice . . . . . . . . . . . . . . . . . 146 3.4.2 Example: TechEmployee . . . . . . . . . . . . . . . . . . . . . 149 3.4.3 Example: StockOrder . . . . . . . . . . . . . . . . . . . . . . 153 3.5 Dependency Inversion Principle . . . . . . . . . . . . . . . . . . . . . 154 3.5.1 Example: UserService . . . . . . . . . . . . . . . . . . . . . 154 3.5.2 Example: Logger . . . . . . . . . . . . . . . . . . . . . . . . 156 4 Packaging Principles - Cohesion . . . . . . . . . . . . . . . . . . . . . . . . . . . . 158 4.1 Release Reuse Equivalency Principle . . . . . . . . . . . . . . . . . . . 158 4.2 Common Closure Principle . . . . . . . . . . . . . . . . . . . . . . . . 158 4.3 Common Reuse Principle . . . . . . . . . . . . . . . . . . . . . . . . . . 159 5 Packaging Principles - Coupling . . . . . . . . . . . . . . . . . . . . . . . . . . . . 159 5.1 Acyclic Dependencies Principle . . . . . . . . . . . . . . . . . . . . . . 159 5.1.1 Example: Cyclic dependency . . . . . . . . . . . . . . . . . . 160 5.2 Stable Dependencies Principle . . . . . . . . . . . . . . . . . . . . . . . 164 5.3 Stable Abstractions Principles . . . . . . . . . . . . . . . . . . . . . . . 166 6 Further Design Principles . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 169 6.1 Speaking Code Principle . . . . . . . . . . . . . . . . . . . . . . . . . . 169 6.2 Keep It Simple (and) Stupid! . . . . . . . . . . . . . . . . . . . . . . . . 169 6.3 Don’t Repeat Yourself / Once and Only Once . . . . . . . . . . . . . 170 6.4 You Ain’t Gonna Need It! . . . . . . . . . . . . . . . . . . . . . . . . . 170

6.5 Separation Of Concerns . . . . . . . . . . . . . . . . . . . . . . . . . . 171


CONTENTS


Design Patterns of the Gang of Four . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 172 1 Creational . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 175

1.1 Singleton . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 175 1.1.1 Example: Lazy loading . . . . . . . . . . . . . . . . . . . . . 175 1.1.2 Example: Eager loading . . . . . . . . . . . . . . . . . . . . 176 1.1.3 Example: Enum singleton . . . . . . . . . . . . . . . . . . . . 176 1.2 Builder . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 177

1.2.1 Example: MealBuilder . . . . . . . . . . . . . . . . . . . . . 178 1.2.2 Example: PizzaBuilder . . . . . . . . . . . . . . . . . . . . . 180 1.2.3 Example: Email . . . . . . . . . . . . . . . . . . . . . . . . . 182 1.2.4 Example: ImmutablePerson . . . . . . . . . . . . . . . . . . . 184 1.3 Factory Method . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 187 1.3.1 Example: Logger . . . . . . . . . . . . . . . . . . . . . . . . 187 1.3.2 Example: Department . . . . . . . . . . . . . . . . . . . . . . 189 1.4 Abstract Factory . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 192 1.4.1 Example: Car . . . . . . . . . . . . . . . . . . . . . . . . . . 193 1.5 Prototype . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 198 1.5.1 Example: Person - Shallow copy . . . . . . . . . . . . . . . . 198 1.5.2 Example: Person - Deep copy . . . . . . . . . . . . . . . . . 201 1.5.3 Example: Person - Copy constructor / factory . . . . . . . . 203 2 Structural . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 206

2.1 Facade . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 206

2.1.1 Example: Travel . . . . . . . . . . . . . . . . . . . . . . . . 207 2.1.2 Example: SmartHome . . . . . . . . . . . . . . . . . . . . . . . 207 2.2 Decorator . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 210

2.2.1 Example: Message . . . . . . . . . . . . . . . . . . . . . . . . 211 2.2.2 Example: Window . . . . . . . . . . . . . . . . . . . . . . . . 212 2.3 Adapter . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 215 2.3.1 Example: Sorter . . . . . . . . . . . . . . . . . . . . . . . . 216 2.3.2 Example: TextFormatter . . . . . . . . . . . . . . . . . . . . 217 2.4 Composite . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 219 2.4.1 Example: Graphic . . . . . . . . . . . . . . . . . . . . . . . . 220 2.4.2 Example: Organization Chart . . . . . . . . . . . . . . . . . 222 2.5 Bridge . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 224 2.5.1 Example: Message . . . . . . . . . . . . . . . . . . . . . . . . 224 2.5.2 Example: Television . . . . . . . . . . . . . . . . . . . . . . 227 2.6 Flyweight . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 230 2.6.1 Example: Font . . . . . . . . . . . . . . . . . . . . . . . . . 231 2.6.2 Example: City . . . . . . . . . . . . . . . . . . . . . . . . . 232 2.7 Proxy . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 234 2.7.1 Example: Spaceship . . . . . . . . . . . . . . . . . . . . . . . 234 2.7.2 Example: ImageViewer . . . . . . . . . . . . . . . . . . . . . 236 3 Behavioural . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 238

3.1 State . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 238

3.1.1 Example: MP3Player . . . . . . . . . . . . . . . . . . . . . . . 238 3.1.2 Example: Door . . . . . . . . . . . . . . . . . . . . . . . . . 241


CONTENTS


3.2 Template Method . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 244 3.2.1 Example: Compiler . . . . . . . . . . . . . . . . . . . . . . . 244 3.2.2 Example: Callbackable . . . . . . . . . . . . . . . . . . . . . 245 3.3 Strategy . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 249 3.3.1 Example: Compression . . . . . . . . . . . . . . . . . . . . . 249 3.3.2 Example: LogFormatter . . . . . . . . . . . . . . . . . . . . . 251 3.4 Observer . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 254

3.4.1 Example: DataStore . . . . . . . . . . . . . . . . . . . . . . . 255 3.4.2 Example: Influencer . . . . . . . . . . . . . . . . . . . . . . 256 3.5 Chain of Responsibility . . . . . . . . . . . . . . . . . . . . . . . . . . . 258 3.5.1 Example: Purchase . . . . . . . . . . . . . . . . . . . . . . . 258 3.5.2 Example: Authentication . . . . . . . . . . . . . . . . . . . . 261 3.6 Command . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 264

3.6.1 Example: FileSystem . . . . . . . . . . . . . . . . . . . . . . 264 3.6.2 Example: Television . . . . . . . . . . . . . . . . . . . . . . 267 3.7 Interpreter . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 270 3.7.1 Example: HexBinary . . . . . . . . . . . . . . . . . . . . . . . 270 3.7.2 Example: Calculator . . . . . . . . . . . . . . . . . . . . . . 273 3.8 Iterator . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 276

3.8.1 Example: Cars - intern . . . . . . . . . . . . . . . . . . . . 277 3.8.2 Example: Cars - extern . . . . . . . . . . . . . . . . . . . . 278 3.9 Mediator . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 280

3.9.1 Example: Chat . . . . . . . . . . . . . . . . . . . . . . . . . 280 3.9.2 Example: Aircraft . . . . . . . . . . . . . . . . . . . . . . . 283 3.10 Memento . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 286

3.10.1 Example: Editor . . . . . . . . . . . . . . . . . . . . . . . . 286 3.10.2 Example: Balance . . . . . . . . . . . . . . . . . . . . . . . . 290 3.11 Visitor . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 292

3.11.1 Example: Fridge . . . . . . . . . . . . . . . . . . . . . . . . 292 3.11.2 Example: Figures . . . . . . . . . . . . . . . . . . . . . . . . 295


```
