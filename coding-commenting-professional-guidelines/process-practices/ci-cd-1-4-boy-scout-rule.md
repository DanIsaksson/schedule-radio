---
id: kb-process-practices-010
title: "- CI/CD – **1.4 Boy Scout Rule**"
domain: process-practices
parent_heading: "**Preface**"
intent: "- CI/CD – **1.4 Boy Scout Rule**: _Always leave the campground cleaner than you found it._ [–Robert C. Martin⁶](http://cleancoder.com/) ![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-20-0.png) ``` Boy Scouts have a rule regarding camping that they should leave the campground cleaner than they found it."
tags:
  - architecture
  - clean-code
  - comments
  - functions
  - guideline
  - naming
  - process
  - testing
source_lines:
  start: 311
  end: 804
---
## - CI/CD
### **1.4 Boy Scout Rule**


_Always leave the campground cleaner than you found it._
[–Robert C. Martin⁶](http://cleancoder.com/)

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-20-0.png)


```
#### Boy Scout Rule


Boy Scouts have a rule regarding camping that they should leave the campground cleaner than they found it. By ensuring this rule, they can guarantee that they do not cause new problems, at least as far as the cleanliness of the area is concerned.


_What if we apply this rule to our code?_


The Boy Scout Rule suggests that you simply try to make sure that each time you commit, you leave the code better than when you found it. This means that every time you make a change, you improve the codebase. Maybe only slightly. Improve a variable name, clean up incomplete documentation, extract a large method to multiple smaller ones or add a missing test.


Keeping code clean is a constant challenge, and developers and software teams must decide if, when, and how they want to keep their code clean. Resolving these technical debts⁷ through refactoring is necessary to keep the code in a state where it remains economically viable to extend and to maintain it.


_When does it make sense to spend time working on improving the code?_


Over time, the quality of the source code on which a system is based tends to deteriorate, leading to increasing technical debt. Some teams take the approach of discontinuing all value-adding work and trying to clean up the codebase. This usually results in some refactoring sprints or even a complete rewrite. Neither of these is a solution from a business perspective. Feature development and bug-fixing must continue, and refactoring should not take up most of the time. According to the Boy Scout Rule, teams can improve the quality of their code over time while continuing to deliver value to their customers and stakeholders. In this way, continuous improvement allows you to move in a corridor where technical debt does not dominate your system. Do not let your system exceed a point where the application becomes unmaintainable.

”⁸ Thus “ _Try to leave the world a little better than you found it_ .


⁶ [http://cleancoder.com/](http://cleancoder.com/) ⁷ WardCunningham:*TheWyCashPortfolioManagementSystem*;ExperienceReport,OOPSLA’92,1992 ⁸ Robert Baden-Powell, the father of the Boy Scouts, and the original of this expression. https://en.wikipedia.org/wiki/Robert_BadenPowell,_1st_Baron_Baden-Powell


Introduction to Software Craftsmanship and Clean Code 8

```
