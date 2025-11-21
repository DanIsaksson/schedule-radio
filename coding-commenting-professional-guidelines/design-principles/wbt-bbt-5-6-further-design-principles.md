---
id: kb-design-principles-010
title: "**WBT** **BBT** – **5.6 Further Design Principles**"
domain: design-principles
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **5.6 Further Design Principles**: The Speaking Code principle is a design principle that says that code should be written so that it is easy to read and understand. This is important because code is often read more than it is written."
tags:
  - architecture
  - classes
  - comments
  - example
  - formatting
  - functions
  - guideline
  - naming
  - principles
  - process
  - testing
source_lines:
  start: 4688
  end: 11990
---
## **WBT** **BBT**
### **5.6 Further Design Principles**

#### **5.6.1 Speaking Code Principle**


The Speaking Code principle is a design principle that says that code should be written so that
it is easy to read and understand. This is important because code is often read more than it is
written. When we write code, we’re communicating with computers, but when we read code,
we’re communicating with other humans. That’s why it’s important to make sure our code is
easy to read. So following this principle can also make it easier to work with other people on
code projects.


```
   - Code should **communicate** its purpose, even without comment and documentation.

   - Comments are no substitute for bad code.

   - Clear and expressive code with few comments is much better than messy and complex code
with numerous comments.

    - Instead of spending time writing comments that explain the chaos, spend time cleaning up
the chaos.

   - Comment must be adjusted when the code is changed.

    - Using of whitespaces and consistent formatting.


1 **if** ((bike.type & TYP_FLAG) && bike.status == READY) { ... }


1 **if** (bike.isRentable()) { ... }

#### **5.6.2 Keep It Simple (and) Stupid!**


No one is in a position to manage large complexity. Not in software, not in any other aspect of life. Complexity is the killer of good software and therefore simplicity is the enabler. Simplicity is something that takes a lot of time and practice to recognize and produce. Of course, many rules can be followed.


An example would be to have methods with only a few parameters. In the best case, none or only one. We can write "Keep It Simple (and) Stupid!".contains("Simple") and without reading any documentation, everyone will immediately understand what this does and why. The method does one thing and not more. There is no complicated context and no other arguments that can be passed to the method. There are no _special cases_ or any caveats.


```
   - Means that always the **simplest** solution of a problem should be chosen.

    - Avoid code that is too complex and therefore too complicated.

    - Finding simple solutions is a rule that helps to avoid various errors.

    - A healthy rejection of non-simple solutions is a sign of searching for good code.

    - The more difficult code is to explain, the more likely it is that it is more complicated than
necessary and is not the most elegant solution.

    - Make things as simple as possible, but not simpler!


#### **5.6.3 Don’t Repeat Yourself / Once and Only Once**


Don’t Repeat Yourself (DRY) is a principle of software development aimed at reducing repetition
of software development patterns, replacing it with abstractions or using data structures. When
the DRY principle is applied successfully, a modification to any one element of a system does
not require a similar change to other logically unrelated elements.


Once and Only Once (OOO) is a similar but more specific software development principle
which states that for each element of software, there should be only one single, unambiguous
representation within the system. This can be accomplished by ensuring that code is not
duplicated, and by abstracting code into modules or functions that can be reused.


```
  - Refers to the avoidance of **duplicated code**, i.e. code fragments that are implemented in
the same or very similar way in several places.

  - Redundant existing source code is difficult to maintain, as consistency between the
individual duplicates must be ensured.

  - In systems that remain loyal to the DRY principle, however, changes need only be made in
one place.


As a programmer, it is important to avoid repeating yourself. This not only saves time, but also makes your code more reliable and easier to maintain. When you need to make a change, you only have to do it in one place, and the change will propagate throughout your code. This is in contrast to Write Everything Twice (WET) programming, in which you write code that is duplicated, and you have to make changes in multiple places. DRY programming is a more efficient and effective way to write code, and it is something that all programmers should strive for.

```
#### **5.6.4 You Ain’t Gonna Need It!**


You Ain’t Gonna Need It, also known as YAGNI, is a programming principle that suggests that
programmers should not add features or functionality to a software system unless it is absolutely

necessary.


Resist the temptation to add features or functionality that you think might be needed in the future,
and instead focus on building only what is needed right now. This may seem like a no-brainer,
but in practice it can be difficult to resist the temptation to add “just in case” features.


YAGNI can help to keep code clean and maintainable, and can also help to reduce development
time and costs.


```
  - Where no requirement, also no implementation!

  - A program should only implement functionality when this functionality is needed.

  - Contrary to this approach, in practice it is often attempted to prepare programs for possible
future change requests and features through additional or more general code. _“…this is what_ _THEY will demand soon anyway…”_

  - Such code can be very annoying to read, understand, change existing functionality, and
costs time and money!


#### **5.6.5 Separation Of Concerns**


Separation of Concerns (SoC) is a principle to relieve the complexity of a program by dividing it into distinct parts, each with a separate concern.


When done effectively, separation of concerns can make code easier to maintain and improve over time. This is because each concern can be isolated and changed without affecting the others. This can make code more modular and reusable, and less error-prone.


Overall, separation of concerns programming is a good thing. It can help you keep your code organized and make it easier to maintain.


```
  - A component should have exactly one task.

  - Avoid mixing of several responsibilities in e.g. one class.

  - Components become simpler, easier to understand, maintain and have better reusability.

  - The complex tasks of a unit should be self-contained ( **high cohesion** ).

  - The unit should depend as little as possible on other units ( **low coupling** ).


# **6. Design Patterns of the Gang of** **Four**

_Each pattern describes a problem that occurs over and over again in our environment, and then_
_describes the core of the solution to that problem, in such a way that you can use this solution a_
_million times over, without ever doing it the same way Twice._
- Christopher Alexander 1977, A Pattern Language: Towns, Buildings, Construction


  - Design patterns originally come from architecture.

  - 1977 Christoph Alexander describes in his book _A Pattern Language. Towns, Buildings,_
_Construction_ the design patterns for landscape and urban planning, among other things.

  - Kent and Cunningham drew their inspiration for design patterns in software development
from Alexander (about 1987).

  - Gamma, Vlissides, Johnson, and Helm transferred the ideas to software technology and
designed a pattern catalog.

  - The Gang of Four (GoF) published the book “Design Patterns” in 1995 and made design
patterns popular.


Design patterns are solution patterns for recurring design problems in software design. There a
based-on experience, they are often only understandable and comprehensible with appropriate
background knowledge and personal experience. They establish communication by forming
a vocabulary (Pattern Language) that developers use to discuss design. The basics of objectoriented design like _Encapsulation_, _Responsibility_, _Polymorphism_ and _Inheritance_ must be
understood.


```
#### Why are design patterns important?


Many problems repeat themselves in software development. Class hierarchies become inflexible over time and individual classes grow uncontrollably. New extensions are very difficult to implement, and the dependencies of a class increase so that the function can be fulfilled. The encapsulation of classes is becoming softened and more and more of the internal state is being exposed.


The wheel does not have to be reinvented every time, at least the schematic solutions are well documented. Since design patterns have existed for a long time, the solutions have been proven many times and the consequences are well understood. Furthermore, software design can be discussed on a higher level of abstraction and thus more efficiently since the wording is already self-explanatory and known to the developers. Instead of looking for the solution to a problem, there is a sense of recognizing connections that suggest the use of a design pattern.


```
#### Classification of design patterns according to GoF


The 23 design patterns of the Gang of Four are divided into three groups. The categories describe
the intention that is to be achieved with a design pattern.


```
#### Creational Patterns

_How objects or even class hierarchies can be created?_


Group Pattern Description Creational Singleton Ensure a class only has one instance, and provide a global point of

access to it. Builder Separate the construction of a complex object from its representation so that the same construction process can create different representations. Factory Method Define an interface for creating an object, but let the subclasses decide which class to instantiate. Factory Method lets a class defer instantiation to subclasses. Abstract Factory Provide an interface for creating families of related or dependent objects without specifying their concrete class. Prototype Specify the kinds of objects to create using a prototypical instance, and create new objects by copying this prototype.


```
#### Structural Patterns

_What structure must a composition of classes and objects have to solve a certain problem?_


Group Pattern Description
Structural Facade Provide a unified interface to a set of interfaces in a subsystem. Facade
defines a higher-level interface that makes the subsystem easier to use.
Decorator Attach additional responsibilities to an object dynamically. Decorators
provide a flexible alternative to subclassing for extending functionality.
Adapter Convert the interface of a class into another interface clients expect.
Adapter lets classes work together that couldn‘t otherwise because of
incompatible interfaces.
Composite Compose objects into tree structures to represent part-whole hierarchies.
Composite lets clients treat individual objects and compositions of objects
uniformly.
Bridge Decouple an abstraction from its implementation so that the two can vary
independently.
Flyweight Use sharing to support large numbers of fine-grained objects efficiently.
Proxy Provide a surrogate or placeholder for another object to control access to it.


```
#### Behavioural Patterns

_How do the objects work together at runtime? How is responsibility distributed among them?_


Group Pattern Description Behavioral State Allow an object to alter its behavior when its internal state changes. The object will appear to change its class. Template Method Define a skeleton of an algorithm in an operation, deferring some steps to subclasses. Template Method lets subclasses redefine certain steps of an algorithm without changing the algorithm‘s structure. Strategy Define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy lets the algorithm vary independently from clients that use it.


Group Pattern Description Observer Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically. Chain of Responsibility Avoid coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. Chain the receiving objects and pass the request along the chain until an object handles it. Command Encapsulates a request as an object, thereby letting you parameterize clients with different requests, queue or log requests, and support undoable operations. Interpreter Given a language, define a representation for its grammar along with an interpreter that uses the representation to interpret sentences in the language. Iterator Provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation. Mediator Define an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly, and it lets you vary their interaction independently. Memento Without violating encapsulation, capture and externalize an object‘s internal state so that the object can be restored to this state later. Visitor Represent an operation to be performed on the elements of an object structure. Visitor lets you define a new operation without changing the classes of the elements on which it operates.


With the introduction of lambdas in Java 8 some design patterns can be implemented in [a different way¹ with lambdas.](https://github.com/mariofusco/from-gof-to-lambda)


¹ [https://github.com/mariofusco/from-gof-to-lambda](https://github.com/mariofusco/from-gof-to-lambda)


```
