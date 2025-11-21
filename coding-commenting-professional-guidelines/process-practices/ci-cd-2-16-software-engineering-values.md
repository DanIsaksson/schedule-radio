---
id: kb-process-practices-020
title: "- CI/CD – **2.16 Software Engineering Values**"
domain: process-practices
parent_heading: "**Preface**"
intent: "- CI/CD – **2.16 Software Engineering Values**: ![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-57-0.png) - To ensure the functional correctness of the system, it is essential that each unit of the system is verifiable and has been tested. - A system is only complete if its functional correctness and completeness are proven by"
tags:
  - architecture
  - clean-code
  - functions
  - guideline
  - principles
  - process
  - testing
source_lines:
  start: 311
  end: 2522
---
## - CI/CD
### **2.16 Software Engineering Values**

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-57-0.png)


#### Software Engineering Values


#### Testability


  - To ensure the functional correctness of the system, it is essential that each unit of the system
is verifiable and has been tested.

  - A system is only complete if its functional correctness and completeness are proven by
automatically triggered tests.


#### Maintainability


  - We build durable systems, which means we always must consider how to maintain the
systems.

  - The colleagues who maintain a system can change over time. Therefore, a maintainable
system should be easy to understand. The level of system-specific knowledge required to
carry out maintenance should be kept to a minimum.


#### Consistency


  - To improve flexibility and maintenance, the number of different solutions like code-level
patterns, libraries, third-party services, and application integration patterns, for the same
problem should be kept to a minimum.

  - Having multiple solutions to a problem is better than having no solutions, having a single
solution to a problem is the best.


#### Modularity


  - Things are always changing. To protect our code from this fundamental fact, we need to
limit the impact of change and divide it into reasonably independent, coherent units at all
levels.


#### Simplicity


  - The simplest parts of a system to change are those that do not exist. For any given
requirement, the simplest, minimalist solution should be provided which meets the previous
values.
