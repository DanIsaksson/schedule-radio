---
id: kb-architecture-003
title: "- CI/CD – **2.14 Architecture Documentation**"
domain: architecture
parent_heading: "**Preface**"
intent: "- CI/CD – **2.14 Architecture Documentation**: Designing architecture means making decisions! The documentation is a persistent and communicable artifact for all decisions."
tags:
  - architecture
  - classes
  - comments
  - formatting
  - functions
  - process
source_lines:
  start: 311
  end: 2420
---
## - CI/CD
### **2.14 Architecture Documentation**


Designing architecture means making decisions! The documentation is a persistent and communicable artifact for all decisions. It is taken into account during the planning of the project and focuses more on non-functional than functional requirements. The team works closely together to build a system that meets all requirements. Some development work will address needs other than functional requirements. Therefore, the work on improving the quality of the system must take up a substantial part of the team’s time.


To give all stakeholders of a system, that require information about the architecture, e.g., the internal structure, crosscutting concepts, or fundamental decisions a fundamental architecture overview, it is important to provide architecture documentation.


[Arc42⁶ provides a comprehensive but condensed template for describing architectures. This](https://arc42.org/) template produces uniformed architecture documentation which helps to understand the big picture and connect the dots by looking at multiple areas.


⁷ [The template is structured as follows and contains 12 structure items](https://arc42.org/overview/) :


```
#### 1. Introduction and Goals


Short description of the requirements, driving forces, extract of requirements. Top three or max
five quality goals for the architecture which have the highest priority for the major stakeholders.
A table of important stakeholders with their expectations regarding architecture.


```
#### 2. Constraints


Anything that constrains teams in the design and implementation decisions or decisions about related processes. Can sometimes go beyond individual systems and are valid for whole organizations and companies.


#### 3. Context and Scope


Delimits your system from its external communication partners. Specifies the external interfaces.


#### 4. Solution Strategy


Summary of the fundamental decisions and solution strategies that shape the architecture. Can include technology, top-level decomposition, approaches to achieve top-quality goals, and relevant organizational decisions.


```
#### 5. Building Block View


Static decomposition of the system, abstractions of source code, up to the appropriate level of
detail.


#### 6. Runtime View


The behavior of building blocks as scenarios, covering important use cases or features, interactions at critical external interfaces, operation and administration plus error and exception
behavior.


#### 7. Deployment View


⁶ [https://arc42.org/](https://arc42.org/)
⁷ [https://arc42.org/overview/](https://arc42.org/overview/)


Technical infrastructure with environments, computers, processors, topologies. Mapping of
software building blocks to infrastructure elements.


```
#### 8. Crosscutting Concepts


Overall, principal regulations and solution approaches are relevant in multiple parts of the system.


#### 9. Architectural Decisions


Important, expensive, critical, large-scale, or risky architecture decisions including rationales.


#### 10. Quality Requirements


Quality requirements as scenarios, with a quality tree to provide a high-level overview.


#### 11. Risks and Technical Debt


Known technical risks or technical debt.


#### 12. Glossary


Important domain and technical terms that stakeholders use when discussing the system.
