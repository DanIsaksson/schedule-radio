---
id: kb-architecture-001
title: "- CI/CD – **2.11 Architecture Principles**"
domain: architecture
parent_heading: "**Preface**"
intent: "- CI/CD – **2.11 Architecture Principles**: - Think of several options and then try the simplest option first. - Always ask: _Is there an easier way to do this?_ - Think about the future maintainers, assume that they will be you, and work on that basis."
tags:
  - architecture
  - comments
  - formatting
  - guideline
  - naming
  - principles
  - process
source_lines:
  start: 311
  end: 1767
---
## - CI/CD
### **2.11 Architecture Principles**


#### Keep it simple stupid (KISS)


  - Think of several options and then try the simplest option first.

  - Always ask: _Is there an easier way to do this?_

  - Think about the future maintainers, assume that they will be you, and work on that basis.

  - When you look at existing systems, ask: _Do I need all this stuff_ or _Do we need it at all?_


#### You’re not going to need it (YAGNI)


  - The architecture should be intended to support current and future requirements agreed
with business stakeholders.


#### Principle of least astonishment (POLA)


  - Keep things consistent so that people are not surprised when they find that a similar task
is being done in a different place differently.

  - If a difference is needed, document why.

  - Consistency guides understanding, if you name things wrong or call the same thing by
different names, you increase complexity.


#### Separation of concerns (SoC)


  - Keep related things together, and unrelated things apart.


#### If it hurts, do it more often


  - When we try to avoid difficulties, we are faced with increasing complexity. So, complexity
is reduced if we perform difficult tasks more often.

  - https://martinfowler.com/bliki/FrequencyReducesDifficulty.html


#### Leave your code better than you found it (Boy Scout Rule)


  - Keep improving your code continuously, meaning that every time that we make a change
in our code base, improve the code.

  - No matter what the reasons are, we don’t want to end up with additional technical debt.
Every time we see code smell, we should try to eliminate the rot.

  - If we all follow this approach, the system would gradually improve, and the deterioration
would stop.


#### Talk to people


  - You can’t find all the information you need in the documentation or existing code - you
must talk to people.
