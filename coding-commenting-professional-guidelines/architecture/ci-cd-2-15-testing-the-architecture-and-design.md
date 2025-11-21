---
id: kb-architecture-004
title: "- CI/CD – **2.15 Testing the Architecture and Design**"
domain: architecture
parent_heading: "**Preface**"
intent: "- CI/CD – **2.15 Testing the Architecture and Design**: The architecture of a system usually is a design that was made at a certain moment in time. However, as the system evolves and developers come and go, it’s hard to keep everyone up to date on a reference architecture or encourage developers to make changes to it, since it’s usually in the form of documents, confluence pages, or something similar."
tags:
  - architecture
  - classes
  - comments
  - principles
  - process
  - testing
source_lines:
  start: 311
  end: 2466
---
## - CI/CD
### **2.15 Testing the Architecture and Design**


The architecture of a system usually is a design that was made at a certain moment in time. However, as the system evolves and developers come and go, it’s hard to keep everyone up to date on a reference architecture or encourage developers to make changes to it, since it’s usually in the form of documents, confluence pages, or something similar. This can result in code that violates architectural specifications increasing the technical debt.


[IntelliJ](https://www.jetbrains.com/) ⁸

IntelliJ comes with a simple tool for architecture analysis. With it, you can generate a dependency matrix for your code. [Eclipse](https://www.eclipse.org/) ⁹

In eclipse you can install various plugins to visualise dependencies and to detect cycles. [eDepend¹⁰, STAN¹¹, jDepend¹², Java Dependency Viewer¹³](https://marketplace.eclipse.org/category/free-tagging/edepend-graphical-dependency-analysis-tool-340) [ArchUnit](https://www.archunit.org/) ¹⁴


With ArchUnit you can check the architecture constraints of your Java application automatically and can run as part of a test suite. It is a free, simple, and extensible library for checking the architecture of Java code, which can check dependencies between packages and classes, layers and slices, cyclic dependencies, and more. [Taikai](https://github.com/enofex/taikai) ¹⁵


Taikai extends the capabilities of the popular ArchUnit library by offering a comprehensive suite of predefined rules tailored for various technologies. It simplifies the enforcement of architectural constraints and best practices in your codebase, ensuring consistency and quality across your projects. [JQAssistant](https://jqassistant.org/) ¹⁶

jQAssistant is a QA tool, which allows the definition and validation of project-specific rules on a structural level. It is built upon the graph database Neo4j and can easily be plugged into the build process to automate the detection of constraint violations and generate reports about user-defined concepts and metrics. [Structure 101](https://structure101.com/) ¹⁷

With Structure 101 you can understand, analyze, (drag ‘n drop) refactor, and control a large, complex codebase. [Sonargraph](https://www.hello2morrow.com/products/sonargraph) ¹⁸

Sonargraph is a powerful static code analyzer that allows you to monitor a software system for technical quality and enforce rules regarding software architecture, metrics, and other aspects in all stages of the development process.


⁸ [https://www.jetbrains.com/](https://www.jetbrains.com/) ⁹ [https://www.eclipse.org/](https://www.eclipse.org/) ¹⁰ [https://marketplace.eclipse.org/category/free-tagging/edepend-graphical-dependency-analysis-tool-340](https://marketplace.eclipse.org/category/free-tagging/edepend-graphical-dependency-analysis-tool-340) ¹¹ [http://stan4j.com/eclipse/](http://stan4j.com/eclipse/) ¹² [https://marketplace.eclipse.org/category/free-tagging/jdepend](https://marketplace.eclipse.org/category/free-tagging/jdepend) ¹³ [https://marketplace.eclipse.org/content/java-dependency-viewer](https://marketplace.eclipse.org/content/java-dependency-viewer) ¹⁴ [https://www.archunit.org/](https://www.archunit.org/) ¹⁵ [https://github.com/enofex/taikai](https://github.com/enofex/taikai) ¹⁶ [https://jqassistant.org/](https://jqassistant.org/) ¹⁷ [https://structure101.com/](https://structure101.com/) ¹⁸ [https://www.hello2morrow.com/products/sonargraph](https://www.hello2morrow.com/products/sonargraph)


[Lattix](https://www.lattix.com/) ¹⁹


Lattix enables you to quickly identify and remediate architectural issues. It gives software architects a fast and visual way to represent an application’s architecture with the Dependency Structure Matrix technology. [Teamscale](https://www.cqse.eu/en/teamscale/overview/) ²⁰


Teamscale supports your team to analyze, monitor, and improve the quality of your software.

[CodeScene](https://codescene.com/) ²¹


Is a multi-purpose tool bridging code, business, and people. See hidden risks and social patterns in your code. Prioritize and reduce technical debt.


¹⁹ [https://www.lattix.com/](https://www.lattix.com/) ²⁰ [https://www.cqse.eu/en/teamscale/overview/](https://www.cqse.eu/en/teamscale/overview/) ²¹ [https://codescene.com/](https://codescene.com/)


```
