---
id: kb-testing-quality-004
title: "**WBT** **BBT** – **4.3 Test-driven Development (TDD)**"
domain: testing-quality
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **4.3 Test-driven Development (TDD)**: _When we write a test… We are telling ourselves a story about how the operation will look from_ _the outside_ – Kent Beck - Test-driven programming Motivate any behavioral change to the code through an automated test. - Refactoring Keep the code in _simple form_ and develop the design step by step during the programming."
tags:
  - classes
  - clean-code
  - example
  - functions
  - process
  - testing
source_lines:
  start: 4688
  end: 4799
---
## **WBT** **BBT**
### **4.3 Test-driven Development (TDD)**


_When we write a test… We are telling ourselves a story about how the operation will look from_ _the outside_ – Kent Beck


#### What is test-driven Development?


  - Test-driven programming


#### –
Motivate any behavioral change to the code through an automated test.

  - Refactoring


#### –
Keep the code in _simple form_ and develop the design step by step during the programming.

  - Frequent integration


#### –
Integrate the code as often as necessary.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-86-0.png)


#### Test-driven Development Workflow


#### Why test-driven development?


  - TDD ensures the quality and maintainability of software.

  - Tests ensure that the existing functions are retained when extending and revising.

  - Refactoring extends the productive life of the software.

  - Code is often difficult to test afterward.

  - Test-First emphasizes the user view.

  - Software development without testing is like climbing without rope and hooks.


#### Why does Test-driven development improve the quality?


#### If you can’t write a test for something you don’t understand it


The practice of writing tests before implementation forces to deal more intensively with the problem to be solved and thus leads to a higher quality of the code, which is also more comprehensible for other developers. As a side-effect, TDD is also forced to deal with the question of what customers expect from the software to be written. TDD is therefore basically not a test method but a design method.


```
#### Without a regression test you can’t clean the code


Without the possibility of quick testing, you often do not risk changing existing code. It is better
to copy the code of a class, for example, into a new class, which is then extended with additional
features. The result is an overloaded, confusing project with large amounts of superfluous code.


```
#### Fast feedback cycles save time and money


If you write the test first, the correctness of the implementation of a feature can be checked immediately. As a result, you always have certainty about when you have finished developing a feature.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-87-0.png)


```
#### Debug Later Programming


Without these rapid feedback cycles, you often get lost in the search for the causes of a bug. TDD
helps to break down a big problem into smaller, manageable, and testable problems.


#### You are going to test it anyway, spend the time to do it right


Regression tests are indispensable anyway. Without performing them regularly, testing large
applications becomes almost impossible. Of course, regression tests can also be written afterward,
but then one does not benefit from the better design quality provided by TDD. Secondly, time
tends to become limited as a deadline comes, so subsequent testing often comes up short.


```
#### It makes my work more enjoyable


Every successful test gives you the feeling of having created something valuable: a piece of executable software that you can proudly show to other developers.
