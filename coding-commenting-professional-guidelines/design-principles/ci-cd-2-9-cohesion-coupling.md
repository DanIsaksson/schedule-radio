---
id: kb-design-principles-003
title: "- CI/CD – **2.9 Cohesion - Coupling**"
domain: design-principles
parent_heading: "**Preface**"
intent: "- CI/CD – **2.9 Cohesion - Coupling**: Cohesion and coupling are among the most important attributes for the quality of a design. In software development, coupling means the measure for the dependencies between components, while cohesion stands for the measure of the unity of the elements of a component."
tags:
  - architecture
  - clean-code
  - formatting
  - guideline
  - principles
  - process
  - testing
source_lines:
  start: 311
  end: 1652
---
## - CI/CD
### **2.9 Cohesion - Coupling**


Cohesion and coupling are among the most important attributes for the quality of a design. In software development, coupling means the measure for the dependencies between components, while cohesion stands for the measure of the unity of the elements of a component. It is very important to understand what impact these two parameters can have on a system and how they can be influenced.


```
#### To sum it up:


  - Cohesion represents the degree to which a part of a codebase represents a logically single,

atomic unit.

  - Coupling represents the degree to which a single unit is independent of others.

  - Encapsulate information, make modules **highly cohesive**, and **decrease coupling** among
modules.

  - Try to follow the guideline at all levels of your codebase.

  - It is impossible to achieve complete decoupling without affecting cohesion, and vice versa.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-39-0.png)


#### Cohesion - Coupling


By focusing on these principles, developers can create systems that are modular, maintainable,
and robust, paving the way for long-term success and ease of development.


```
