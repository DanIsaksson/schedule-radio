---
id: kb-testing-quality-002
title: "- CI/CD – **4.2 Test Classification**"
domain: testing-quality
parent_heading: "**Preface**"
intent: "- CI/CD – **4.2 Test Classification**: Tests can be classified according to their information status. There are three main testing strategies _White-Box-Test_, _Black-Box-Test_, and _Grey-Box-Test_ ."
tags:
  - classes
  - clean-code
  - formatting
  - process
  - testing
source_lines:
  start: 311
  end: 4687
---
## - CI/CD
### **4.2 Test Classification**


Tests can be classified according to their information status. There are three main testing strategies _White-Box-Test_, _Black-Box-Test_, and _Grey-Box-Test_ . WBT is performed by the development
team after coding is complete by testing the internal behavior of the code. BBT is performed by
the professional test engineer by looking at the application. They do not have access to the logic
and the flow of the code. GBT is the combination of WBT and BBT and is done without the

knowledge of the implementation.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-85-0.png)


```
#### Differences between WBT, GBT and BBT
