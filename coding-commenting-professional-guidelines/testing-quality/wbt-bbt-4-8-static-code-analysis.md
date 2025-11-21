---
id: kb-testing-quality-009
title: "**WBT** **BBT** – **4.8 Static Code Analysis**"
domain: testing-quality
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **4.8 Static Code Analysis**: Static code analysis is a static software test procedure. In contrast to dynamic test procedures, the software to be tested does not have to be executed."
tags:
  - architecture
  - classes
  - functions
  - guideline
  - principles
  - process
  - testing
source_lines:
  start: 4688
  end: 7564
---
## **WBT** **BBT**
### **4.8 Static Code Analysis**


Static code analysis is a static software test procedure. In contrast to dynamic test procedures, the software to be tested does not have to be executed.


Therefore, only the source code is needed. There is no need to run the system or a special test environment. It can be done by manual testing, but also automatically by a program. But should be anchored as early as possible in the development process.


```
#### Static code analysis is also used in compilers and IDEs for:


  - Type check, _e.g. correct use of types_

  - Style check, _e.g. use of code guidelines_

  - Bug finding, _e.g. determining the use of uninitialized variables_


#### Tooling:


[SonarQube](http://www.sonarqube.org/) ³³

Platform for static code analysis of the technical quality. Supports not only Java but also the
analysis of other programming languages. Analyses the code for different quality ranges
and displays the results graphically.
[Checkstyle](http://checkstyle.sourceforge.net/) ³⁴

Checkstyle is an open-source program that supports the compliance of coding conventions
in Java programs.
[SpotBugs](https://spotbugs.github.io/) ³⁵

Open source program that uses static code analysis to check Java programs for numerous
bugs. Is the successor of FindBugs.
[PMD](https://pmd.github.io/) ³⁶


PMD is a static source code analyzer. It finds common programming flaws like unused
variables, empty catch blocks, unnecessary object creation, and so forth. Include CPD, a
copy-paste detector.
[UCDetector](http://www.ucdetector.org/) ³⁷


Unnecessary Code Detector searches for non-referenced methods and classes and advises
on the visibility of methods and classes.


³³ [http://www.sonarqube.org/](http://www.sonarqube.org/)
³⁴ [http://checkstyle.sourceforge.net/](http://checkstyle.sourceforge.net/)
³⁵ [https://spotbugs.github.io/](https://spotbugs.github.io/)
³⁶ [https://pmd.github.io/](https://pmd.github.io/)
³⁷ [http://www.ucdetector.org/](http://www.ucdetector.org/)


```
