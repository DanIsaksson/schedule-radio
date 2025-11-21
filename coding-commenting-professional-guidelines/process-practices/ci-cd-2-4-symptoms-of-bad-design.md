---
id: kb-process-practices-017
title: "- CI/CD – **2.4 Symptoms of bad design**"
domain: process-practices
parent_heading: "**Preface**"
intent: "- CI/CD – **2.4 Symptoms of bad design**: Many signs can indicate a bad design in architecture. Some of these indicators are more obvious than others, but all of them can lead to problems down the road if they are not addressed."
tags:
  - architecture
  - functions
  - principles
  - process
  - testing
source_lines:
  start: 311
  end: 1242
---
## - CI/CD
### **2.4 Symptoms of bad design**


Many signs can indicate a bad design in architecture. Some of these indicators are more obvious
than others, but all of them can lead to problems down the road if they are not addressed.


One of the most common indicators of a bad design is a high degree of coupling between
components. Coupling is the degree to which one component depends on another. When
components are highly coupled, they are more difficult to change without affecting the other
component. This can lead to a lot of problems when trying to make changes to the system later

on.


Another common indicator of a bad design is a lack of modularity. Modularity is the degree to
which a system is composed of separate, independent modules. A good design will have a high
degree of modularity, which makes it easier to add or remove functionality without affecting the
rest of the system. A system with a low degree of modularity is more difficult to change and can
be more fragile as a result.


There are many other common causes of bad design, but some of the most common include a
failure to understand the problem, a lack of foresight, and a lack of communication among team
members. Often, bad design is the result of a rushed or poorly planned project.


Some common causes of bad design:


```
#### High coupling between components

#### Lack of modularity

#### Never-touch-running-code Syndrome


#### –
Developers are afraid to change code.

#### –
Many workarounds, code is developed around it.

#### –
Changes have unknown and undetected side effects.

#### Small change in requirements leads to big changes in code

#### Reuse through code duplication (Copy-Paste)


#### –
Developers chase after the places that need to be changed.

#### –
The more code the more difficult it becomes to keep track of duplicates.

#### –
Errors have to be patched several times at different places.

#### Cyclic relations between artifacts


#### –
Artifacts that are cyclically coupled cannot be tested individually.

#### –
Artifacts that are used in different cycles often play several roles, which makes them difficult to understand.

#### –
Artifacts that are used in different cycles cannot be exchanged easily.
