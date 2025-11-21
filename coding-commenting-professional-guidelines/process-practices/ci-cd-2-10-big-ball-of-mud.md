---
id: kb-process-practices-019
title: "- CI/CD – **2.10 Big Ball of Mud**"
domain: process-practices
parent_heading: "**Preface**"
intent: "- CI/CD – **2.10 Big Ball of Mud**: A Big Ball of Mud is a haphazardly structured, sprawling, sloppy, duct-tape-and-balingwire, spaghetti-code jungle. These systems show unmistakable signs of unregulated growth and repeated expedient repair."
tags:
  - architecture
  - clean-code
  - formatting
  - principles
  - process
source_lines:
  start: 311
  end: 1698
---
## - CI/CD
### **2.10 Big Ball of Mud**


A Big Ball of Mud is a haphazardly structured, sprawling, sloppy, duct-tape-and-balingwire, spaghetti-code jungle. These systems show unmistakable signs of unregulated growth and repeated expedient repair. Information is shared promiscuously among distant elements of the system, often to the point where nearly all the important information becomes global or duplicated.


The overall structure of the system may never have been well defined.


If it was, it may have eroded beyond recognition. Programmers with a shred of architectural sensibility shun these quagmires. Only those who are unconcerned about architecture, and, perhaps, are comfortable with the inertia of the day-to-day chore of patching the holes in these failing dikes, are content to work on such systems. [–Brian Foote and Joseph Yoder, Big Ball of Mud².](http://www.laputan.org/mud/mud.html#BigBallOfMud)


Big Ball of Mud is an anti-pattern of software architecture and describes a program that has no recognizable software architecture.


```
#### Most frequent causes:


  - Insufficient experience

  - Lack of awareness for software architecture

  - Not modularized, high coupling and low cohesion

  - Does not comply with the principles of good design

  - Pressure on the implementation team

  - Constantly changing requirements

  - High time pressure

  - Low budget

  - Employee fluctuation

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-40-0.png)


#### Big Ball of Mud


² [http://www.laputan.org/mud/mud.html#BigBallOfMud](http://www.laputan.org/mud/mud.html#BigBallOfMud)
