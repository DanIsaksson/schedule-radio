---
id: kb-design-principles-008
title: "**WBT** **BBT** – **5.4 Packaging Principles - Cohesion**"
domain: design-principles
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **5.4 Packaging Principles - Cohesion**: ![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-171-0.png) _The granule of reuse is the granule of release._ - The elements of reuse are the elements of the release. - Classes that are reused together should be in the same package so that they can be released"
tags:
  - classes
  - clean-code
  - guideline
  - principles
  - process
  - testing
source_lines:
  start: 4688
  end: 11299
---
## **WBT** **BBT**
### **5.4 Packaging Principles - Cohesion**

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-171-0.png)

#### **5.4.1 Release Reuse Equivalency Principle**


_The granule of reuse is the granule of release._


  - The elements of reuse are the elements of the release.

  - Classes that are reused together should be in the same package so that they can be released
together.

  - Reducing the number of modules to be distributed, which in turn reduces the effort of
software distribution.

  - Avoidance of version conflicts during reuse, which can only arise from the simultaneous
release of several packages.

  - The users of classes, which in turn depend on other classes, want to rely on a collective
release of new versions.

#### **5.4.2 Common Closure Principle**


_Classes that change together are packaged together._


  - All classes in a package should be closed to the same type of changes according to the open
closed principle.

  - Corresponds to the single responsibility principle applied to packages.

  - Changes to the requirements of the software that require changes to a class of a module
should only affect the classes of the module.

  - Compliance with this principle allows the software to be broken down into packages in
such a way that future changes can be implemented in only a few modules.

  - This ensures that changes to the software can be made without major side effects and thus
relatively inexpensively.


#### **5.4.3 Common Reuse Principle**


_Classes that are used together are packaged together._


  - Grouping of classes that are used together.

  - Compliance with this principle ensures that units belonging together are subdivided.
