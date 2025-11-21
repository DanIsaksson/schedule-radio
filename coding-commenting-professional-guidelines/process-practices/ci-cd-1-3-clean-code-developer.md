---
id: kb-process-practices-009
title: "- CI/CD – **1.3 Clean Code Developer**"
domain: process-practices
parent_heading: "**Preface**"
intent: "- CI/CD – **1.3 Clean Code Developer**: [The Clean Code Developer (CCD)³ is an initiative for more professionalism in software](https://clean-code-developer.com/) development. CCD defines a collection of principles and practices."
tags:
  - architecture
  - classes
  - clean-code
  - formatting
  - functions
  - naming
  - principles
  - process
  - testing
source_lines:
  start: 311
  end: 765
---
## - CI/CD
### **1.3 Clean Code Developer**


[The Clean Code Developer (CCD)³ is an initiative for more professionalism in software](https://clean-code-developer.com/) development. CCD defines a collection of principles and practices. These are divided into different degrees. However, it also includes a collection of specific values, such as evolvability, correctness, production efficiency and continuous improvement .


Through constant iteration of the different degrees, these principles and practices are becoming part of the way we act. This creates an awareness of quality within the development process. Those who practice daily will be able to intuitively retrieve this knowledge


Below you can see an overview of the degrees of the CCD system, which the iterative cycle goes through. The degrees build on each other in terms of difficulty and benefits. A developer who has not yet started with CCD owns the black grade. If someone has not yet started with CCD own the black grade. A black grade only indicates the initial interest in CCD.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-18-0.png)


```
#### Clean Code Developer Grade Cycle


#### Clean Code Developer Grades


Grade Principles Practices
Red Don’t Repeat Yourself (DRY) Using a Version Control System
Keep it simple, stupid (KISS) Refactoring Patterns Rename and Extract
Method
Beware of Optimizations Boy Scout Rule
Favour composition over inheritance Root Cause Analysis
Daily Reflection


Orange One level of abstraction Issue Tracking
Single Responsibility Principle (SRP) Automatic Integration Tests
Separation of Concerns (SoC) Read, Read, Read
Source Code Convention Reviews


Yellow Information hiding Attend Conferences


³ [https://clean-code-developer.com/](https://clean-code-developer.com/)


Introduction to Software Craftsmanship and Clean Code 6


```
#### Clean Code Developer Grades


Grade Principles Practices Principle of least astonishment (POLA) Automatic Unit Tests Liskov Substitution Principle (LSP) Mockups Interface Segregation Principle (ISP) Code Coverage Analysis Dependency Inversion Principle (DIP) Complex Refactoring


Green Open Closed Principle (OCP) Continuous Integration Tell, don´t ask Static Code Analysis Law of Demeter (LoD) Inversion of Control Container Share your Experience


Blue Implementation matches Design Continuous Deployment You Ain’t Gonna Need It (YAGNI) Component Orientation Separation of Design and Implementation Iterative Development TDD


White All principles flow together All practices flow together and includes all other grades and includes all other grades


A developer finishes with the white grade when he is aware of all the CCD values. In this grade, he follows a constant repetition of the cycle, which serves the purpose of refining the use of the aspects of the individual grades. This reflects our job as developers, in which we constantly keep our knowledge up to date and refine our tooling and practices. Only experienced and advanced software developers will be able to work in white grade.


A more visualized overview of all principles, practices, and values can be found here.⁴⁵


⁴ https://unclassified.software/de/topics/clean-code ⁵ http://michael.hoennig.de/download/CCD-Poster.pdf


Introduction to Software Craftsmanship and Clean Code 7

```
