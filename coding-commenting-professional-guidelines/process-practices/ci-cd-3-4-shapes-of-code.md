---
id: kb-process-practices-023
title: "- CI/CD – **3.4 Shapes of code**"
domain: process-practices
parent_heading: "**Preface**"
intent: "- CI/CD – **3.4 Shapes of code**: Who doesn’t know it, you sit back, look at the code from a distance and recognize patterns. These patterns tell you a lot about the code you are looking at."
tags:
  - classes
  - clean-code
  - comments
  - formatting
  - functions
  - guideline
  - naming
  - principles
  - process
source_lines:
  start: 311
  end: 4629
---
## - CI/CD
### **3.4 Shapes of code**


Who doesn’t know it, you sit back, look at the code from a distance and recognize patterns. These
patterns tell you a lot about the code you are looking at. The outlines, peaks, valleys, and spaces
between the lines of code can be used to detect certain antipatterns and improve the code.


So lean back, take a few steps back or set the screen resolution way too high and be excited about
what you discover.

```
#### **3.4.1 Spikes**


#### Advantage for understanding:


Each spike can first be analyzed on its own, even though it may be interdependent.


#### Advantage for refactoring:


Each spike is a potential candidate for transferring code into a separate private function and replacing it by calling that function.

#### **3.4.2 Paragraphs**


#### Advantage for understanding:


You know that the algorithm works in steps, and you know where the steps are in the code.


#### Advantage for refactoring:


Since steps should be somewhat distinct from each other, each step is a good candidate for outsourcing its code to a function. The resulting code would be a sequence of function calls. This would increase the level of abstraction and make the code more expressive.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-82-0.png)

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-82-1.png)

```
#### **3.4.3 Paragraphs with headers**


#### Advantage for understanding:


As with paragraphs. The developer who wrote this has made your task easier by
adding information to each step.


#### Advantage for refactoring:


As with paragraphs. You can use some terms in the comments as inspiration for
function names. After refactoring, the comments become redundant and can be
removed.

```
#### **3.4.4 Suspicious comments**


#### Advantage for understanding:


Not all comments are an advantage, and the code is often not a good one.


#### Advantage for refactoring:


Use the terms in the comments to rename the function and its parameters and remove the comments.

#### **3.4.5 Intensive use of an object**


#### Advantages for understanding:


The role of this code section is to set up this object.


#### Advantages for refactoring:


The function has several responsibilities, and one of them is to work with this object. Refactor this responsibility in a function to reduce the number of responsibilities (ideally to one) of the main function.


More information can be found here:

https://www.fluentcpp.com/2020/01/14/the-shapes-of-code/


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-83-0.png)

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-83-1.png)

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-83-2.png)
```
# **4. Software Quality Assurance**
