---
id: kb-testing-quality-008
title: "**WBT** **BBT** – **4.7 Code Coverage**"
domain: testing-quality
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **4.7 Code Coverage**: Code coverage is a metric that measures at runtime which source code lines have been processed. The measurement is done by executing unit test cases and gives you a metric of the degree to which the source code has been tested."
tags:
  - clean-code
  - functions
  - guideline
  - process
  - testing
source_lines:
  start: 4688
  end: 7507
---
## **WBT** **BBT**
### **4.7 Code Coverage**


Code coverage is a metric that measures at runtime which source code lines have been processed.
The measurement is done by executing unit test cases and gives you a metric of the degree to
which the source code has been tested.


This allows you to make a statement about how comprehensively the code has been tested. It
improves the SW quality through better quality of the test cases and increases the efficiency of
the test cases.


Test coverage should never be understood as metrics about the quality of the tests or the code! It
is only an indicator for problem areas. Can therefore only make negative statements, not positive

ones.


```
#### Measurement techniques:


#### Statement Coverage / Line Coverage


  - Measures whether and how often a single line of code has been run through.

  - Problem: If the line is a logical comparison, a single run is not representative.


#### Decision Coverage / Branch Coverage


  - For case distinctions (if, while, etc.) it is additionally checked that both cases (true and false)
have occurred.


#### Path Coverage


  - Path Coverage measures whether all possible combinations of program flow paths have
been run through.

  - Problem: The number of possibilities increases exponentially with the number of decisions.


#### Function Coverage


  - Measures whether they have been called based on the functions.


#### Race Coverage


  - Concentrates on code points that run in parallel.


Most code coverage tools on the market only support Statement and Decision Coverage, in some cases Path Coverage. Nevertheless, they already provide very valuable statements, e.g. code parts that were simply forgotten during testing and exceptions that have not been considered.


```
#### Challenges:


A 100% coverage is an illusion! Do not try to achieve 100% total code coverage. Achievable
coverage depends strongly on the project type, whereas realistic values are in the range of 80%.
Code coverage should be defined project-specifically and automatically checked during the build

process.


Attaining 100% code coverage sounds good in theory but is almost always a waste of time. We
will spend a lot of effort to get from 80% to 100%, as it is much more difficult than getting from

0% to 20%.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-119-0.png)


```
#### Effort required


Increasing code coverage has lower revenues as well. Don’t waste your time trying to achieve a very high level of test coverage, just to be better positioned with your project. Often stupid tests are written to artificially push them up. It is better to have tests that you can trust and test the features properly. Testing pure setters and getters make not always sense. These tests only cost time and money and do not help the project.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-119-1.png)


```
#### Value from tests


Again, code coverage should not be used as a representation of the quality of a project!


#### Tooling:


[Eclemma](http://www.eclemma.org/) ³²


Java-based code coverage framework, with support of statement and decision coverage.
Provide generation of text, HTML, and XML reports and is part of Eclipse.


³² [http://www.eclemma.org/](http://www.eclemma.org/)


```
