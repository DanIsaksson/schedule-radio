---
id: kb-design-principles-001
title: "- CI/CD – **2.7 Cohesion**"
domain: design-principles
parent_heading: "**Preface**"
intent: "- CI/CD – **2.7 Cohesion**: _Measure of the_ _**affiliation**_ _of the elements of a component._ Cohesion refers to the degree to which the elements within a module belong together. It measures the strength of the relationships between the functionalities within a single module."
tags:
  - architecture
  - classes
  - clean-code
  - functions
  - principles
  - process
  - testing
source_lines:
  start: 311
  end: 1533
---
## - CI/CD
### **2.7 Cohesion**


_Measure of the_ _**affiliation**_ _of the elements of a component._


#### High cohesion of a component improves understanding, maintenance and adaptation.


Cohesion refers to the degree to which the elements within a module belong together. It measures
the strength of the relationships between the functionalities within a single module. High
cohesion is a desirable attribute in software design as it enhances the clarity, maintainability,
and adaptability of the code.


High cohesion in a component significantly improves the following aspects:


  Understanding: When the elements within a module are closely related, it becomes easier
for developers to grasp the module’s functionality and purpose. This clarity aids in quicker
onboarding and smoother collaboration.

```
  - **Maintenance:** Highly cohesive modules are simpler to maintain because changes or fixes
are localized. This reduces the risk of introducing bugs in unrelated parts of the codebase.

Adaptation: Modules with high cohesion can be more easily adapted or extended. Since related functionalities are grouped together, adding new features or modifying existing ones is straightforward and less error-prone.


The following diagram illustrates the concept of cohesion:

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-35-0.png)


```
#### Cohesion


Code with high cohesion is inherently easier to maintain. When a module’s elements are closely
related, the module can be designed, written, and tested as a cohesive unit. This modularity
ensures that all relevant code is encapsulated within the module, making it more straightforward
to manage and evolve over time.


In contrast, low cohesion means that the functionality is scattered throughout the codebase,
leading to several issues:


  Increased Complexity: Scattered functionality makes it harder to understand the purpose
and behavior of the code.

  Higher Risk of Bugs: Changes in one part of the code can inadvertently affect other
unrelated parts, increasing the likelihood of bugs.

```
  - **Difficult Maintenance:** Identifying and fixing issues becomes more challenging when
functionality is not localized.


Cohesion can be categorized into different degrees:


  - **Functional Cohesion:** All elements within the module contribute to a single well-defined
task. This is the highest and most desirable level of cohesion.

Sequential Cohesion: Elements are related such that the output of one part serves as the input for another, forming a sequence of operations.

```
  - **Communicational Cohesion:** Elements operate on the same data set or contribute to the
same task but are not necessarily executed in sequence.

  - **Procedural Cohesion:** Elements are related by sequence, but their actions are only loosely
connected. They are part of a procedure but do not necessarily contribute to a single task.

  Temporal Cohesion: Elements are related by timing; they are executed together at a
particular time, such as initialization tasks.

  Logical Cohesion: Elements are grouped because they perform similar types of operations,
such as error handling, but are not related functionally.

```
  - **Coincidental Cohesion:** Elements have no meaningful relationship to one another and are
grouped arbitrarily. This is the lowest and least desirable level of cohesion.


To achieve high cohesion, consider the following practices:


Single Responsibility Principle (SRP): Ensure that each module or class has only one reason to change. This principle helps in creating highly cohesive modules.

Encapsulation: Keep related data and behavior together. Encapsulation not only improves cohesion but also enhances the integrity of the module.

```
  - **Clear Module Boundaries:** Define clear boundaries for modules based on their function
ality. Avoid mixing unrelated responsibilities within a single module.

  Refactoring: Regularly refactor code to improve cohesion. This includes splitting large,
unwieldy modules into smaller, more cohesive ones.


```
