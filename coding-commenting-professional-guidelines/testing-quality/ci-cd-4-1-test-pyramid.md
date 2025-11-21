---
id: kb-testing-quality-001
title: "- CI/CD – **4.1 Test Pyramid**"
domain: testing-quality
parent_heading: "**Preface**"
intent: "- CI/CD – **4.1 Test Pyramid**: Mike Cohn introduced the Test Pyramid in his book _Succeeding with Agile_ . It is a great way to visualize different levels of testing."
tags:
  - architecture
  - classes
  - clean-code
  - guideline
  - process
  - testing
source_lines:
  start: 311
  end: 4670
---
## - CI/CD
### **4.1 Test Pyramid**

Mike Cohn introduced the Test Pyramid in his book _Succeeding with Agile_ . It is a great way
to visualize different levels of testing. It also tells you how many tests to do on each level. The
original test pyramid consists of three layers that your test suite should consist of _Unit Tests_,
_Service Tests_ and _User Interface Tests_ .

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-84-0.png)


```
#### The Test Pyramid


According to this pyramid, write many small and fast unit tests. Write some more coarse-grained integration tests and very few high-level tests that test your application from top to bottom.


[Be careful not to end up with a Testing Pyramids & Ice-Cream Cones¹ that is a complete waste](https://alisterbscott.com/kb/testing-pyramids/) of time and maintenance.


```
#### Unit Tests


Unit tests focus on a single class and are the easiest, cheapest, and fastest to complete. Unit
tests are written at an early stage so that we get immediate feedback and know exactly
where the bugs are.
```
#### Integration / Component / Acceptance Tests

Integration tests focus on the proper integration of different classes and modules. Compared to unit tests, they may require more specialized tools either for preparing the test environment or for interaction.

#### UI Tests

UI tests focus on the verification of a system from the client‘s point of view. The entire application is tested in a real scenario, such as communication with the database, network, hardware, and other applications.


¹ [https://alisterbscott.com/kb/testing-pyramids/](https://alisterbscott.com/kb/testing-pyramids/)


```
