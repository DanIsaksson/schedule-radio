---
id: kb-architecture-002
title: "- CI/CD – **2.13 Layered Architecture**"
domain: architecture
parent_heading: "**Preface**"
intent: "- CI/CD – **2.13 Layered Architecture**: Architectural styles and patterns play an important role in software engineering. There are many possibilities for the architecture and design of an application."
tags:
  - architecture
  - classes
  - clean-code
  - example
  - formatting
  - functions
  - guideline
  - naming
  - principles
  - process
  - testing
source_lines:
  start: 311
  end: 2320
---
## - CI/CD
### **2.13 Layered Architecture**

#### **2.13.1 Use of Layered Architecture**


Architectural styles and patterns play an important role in software engineering. There are many possibilities for the architecture and design of an application. The complexity of an application depends on different requirements. A system should be designed in such a way that the architecture is easily reusable, extendable, and maintainable and allows a clear separation of components.


Architectures should not be about frameworks. Frameworks are tools to be used that support you in your desired design of an application. You should not be built your architecture around a framework.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-44-0.png)


```
#### Use of Layered Architectures


A common approach is the so-called n-tier architecture. Here, the building blocks of the
application are divided into layers according to their task. This layering was the de-facto
standard for many applications and therefore is widely known by most architects, designers,
and developers.
Although the layer architecture pattern does not specify the number and types of layers, most
layer architectures consist of three or four layers. For example, smaller applications may have
only three layers, while larger and more complex business applications may have five or more
layers.


A three-tier architecture consists of, for example, the persistence layer, the business layer, and
the presentation layer. It includes clearly defined interfaces and a defined direction of the data
flow. To minimize the coupling of the layers, domain objects are used to transport the data from
layer to layer.


Traditional software architecture defines the following three main layers for a layered software
design:


```
#### Presentation Layer


The task of the presentation layer is to respond to client requests. On the one hand, it presents the data on an interface in a proper form, and on the other hand, it includes the execution and reaction to user actions. When implementing and realizing this layer, the challenge is to keep it free of business logic. For this reason, the presentation layer should not implement business logic, but only call and use it.


```
#### Business Layer


The business layer represents a logical and presentation-independent layer. In it, the business
logic is programmed, i.e., everything that must be done independently of a concrete presentation.
It contains the entire functionality of the business requirements developed in the use cases. The
focus during implementation is on the grouping of related components and the abstraction of
the architecture to be developed.


```
#### Persistence Layer


The persistence layer is responsible for the actual access to the data in the database. The database access must not be performed by any other layer than the persistence layer to achieve the highest possible level of encapsulation. For the higher layers, it must be irrelevant in which kind the objects are stored. For this reason, no direct database call may be used in the business or presentation layer. The persistence layer is only called via the business objects of the business layer.


```
#### How do you organize your classes into packages?


Imagine you’re writing a web application. In this application, you handle tickets and reservations.
Your classes include classes like TicketController, TicketService, TicketRepository for the ticket
handling and RegistrationController, RegistrationService and RegistrationRepository for registration.
So, how do you organize your classes into packages and create a valid architecture?


There are two different ways to structure your packages. You can focus on the technical tiers:


```
  - com.swsc.web

  - com.swsc.service

  - com.swsc.repository


Or you can focus on the domain and separate the packages into feature-driven approach:


  - com.swsc.registration

  - com.swsc.ticket


This approach will be discussed in detail in the following chapters.


#### **2.13.2 Violated Layered Architecture**


In this example a request does not move from layer to layer, this violates the layered architecture. It must go through the layer right below it to get to the next layer below that one.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-46-0.png)


```
#### Violated Layered Architecture


The access of a lower layer to an upper one is also a violation in this example.


The access and the agreements concerning the architecture of an application should be known
within the team or preferably checked automatically. A method that makes this possible is
presented in the chapter _Testing the Architecture and Design_ . This architecture should not
disappear over time and should be valid.


```
#### **2.13.3 Horizontal Layering**


The classic horizontally layered architectural pattern was a solid general-purpose pattern and therefore a good choice for most applications, especially if you were not sure which architectural pattern was best for your application.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-47-0.png)


```
#### Horizontal Layering


Today, however, it is no longer the preferred solution. There are a few things to keep in mind
when choosing this pattern from an architectural point of view. With this pattern, you run the
risk of building a monolith. This pattern cuts the layers horizontally according to purely technical
responsibility. This paradigm eliminates low coupling and high cohesion . This is exactly the
opposite of what we want to achieve.


The different modules become quickly dependent on other parts of the application and are not
separated from each other. If an application is structured purely according to horizontal layers
and not according to features like in this case _Ticket_ and _Reservation_ you end up with a pure
layer architecture without a clear division into feature-based modules.


When you look at the package structure you will recognize that almost all classes must have a

public modifier to have access to the lower layer. Which is certainly not the best approach and
violates the principle of information hiding.


```
#### Package structure - Horizontal Layering


com.swcs.web


(C) TicketController


(C) ReservationController


com.swcs.service


(C) TicketService


(C) ReservationService


com.swcs.repository


(C) TicketRepository


(C) ReservationRepository


```

#### **2.13.4 Feature-Based Layering - Single package**


A better approach would therefore be to divide the application into vertical slices according
to functional responsibilities and keep the technical layering as well. Each module is given
responsibility for a clear and separate functional context. So, instead of coupling across a layer,
we couple vertically along with a slice. Packaging by feature improves coupling and cohesion
by keeping all classes related in the same package or module. This minimizes coupling between
slices and maximizes coupling in a slice. In the long run, this decoupling in slices simplifies
code maintenance because it is clear which part of the code does what. We can make changes
without touching or fully understanding the rest of the code. It makes it easier to make changes
to individual parts of the application, improving both maintainability and expandability.


The two concepts of cohesion and coupling are useful and important, but there is another point
that comes into play in this approach.


The package and module structure by “Package by Feature” help the developer to see what it
does and what functionality it has. The architecture jumps out at you. This is a step toward what
[Uncle Bob calls Screaming Architecture³.](https://blog.cleancoder.com/uncle-bob/2011/09/30/Screaming-Architecture.html)

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-48-0.png)


```
#### Feature Based Layering - Single package


With the single package approach, no sub-packages are used for the individual horizontal layers. This makes it possible to set all classes to package-private and thus prevent the use of these outside the package. However, this can affect the clarity of large modules.


Both horizontal and vertical slicing have their use cases, but it is obvious that vertical combined with horizontal slicing enables a more modern and adaptable architecture.


This approach makes it possible to reduce the visibility of the classes. They can be set to packageprivate and are therefore only available within the feature slices. In the horizontal layering, this was not possible because related classes had to communicate across packets, which forces us to make everything public. The horizontal layering within a slice is still present. The naming conventions like *Controller, *Service and *Repository define the layering.


³ [https://blog.cleancoder.com/uncle-bob/2011/09/30/Screaming-Architecture.html](https://blog.cleancoder.com/uncle-bob/2011/09/30/Screaming-Architecture.html)


```
#### Package structure - Feature Based Layering - Single package


com.swcs.ticket


(C) TicketController


(C) TicketService


(C) TicketRepository


```
5


com.swcs.reservation


(C) ReservationController


(C) ReservationService


(C) ReservationRepository


```
#### The advantages are obvious:


    - You get a low coupling and high cohesion of the slices.

   - The code structure corresponds to the domain, which ensures a consistent understanding
and communication between the developers and the department.

    - As new features and enhancements are mostly technical driven, it is easier to identify the
appropriate modules and to determine the level of change.

   - Changes within a single functional domain can happen within a single part of the system.
Side effects are avoided or reduced in this way because the modules are independent of
each other.

    - Vertical separation promotes forming hard contextual boundaries. Vertical layering enables
easier architectural transformation from a monolithic structure to a distributed structure

built like micro-services.

    - Testability is better because the technical logic of a module is separate from other modules.


#### **2.13.5 Feature-Based Layering - Slices before layers**

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-50-0.png)


#### Feature Based Layering - Slices before layers


The approach of _Slices before layers_ divides the vertically feature slice into technical layers such
as web, service, and repository. This makes the technical layers within the package easier to
recognize. This serves mainly the organization of the codebase and improves the structure of the
code.


However, a very important advantage is lost. To access each other, the classes have to be marked
with public with the consequence that they are visible for other feature slices.


```
#### Package structure - Feature Based Layering - Slices before layers


com.swcs.ticket.web


(C) TicketController


com.swcs.ticket.service


(C) TicketService


com.swcs.ticket.repository


(C) TicketRepository


```
7


com.swcs.reservation.web


(C) ReservationController


com.swcs.reservation.service


(C) ReservationService


com.swcs.reservation.repository


(C) ReservationRepository


```

#### **2.13.6 Feature-Based Layering – Hexagonal Architecture**

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-51-0.png)


#### Feature Based Layering - Hexagonal Architecture


[The hexagonal architecture⁴, or port and adapter architecture, is an architectural pattern used in](https://alistair.cockburn.us/hexagonal-architecture/) software design to structure large applications. It is designed to create loosely coupled application components by using ports and adapters.


This architectural style can also be applied to code within a module. The principle behind it is that the domain logic is centrally encapsulated. The interaction with the user and server-side with this domain lies outside the core. The user side can be an API or a web interface. The server-side

can be a database. The key point here is that the outside world always depends on the domain core and never the other way around.


In the example, the service package defines the interfaces like TicketRepository for the actual access logic to the database. The actual implementation of the interface TicketJdbcRepository is located in the package repository and therefore points in the direction of the domain package.


```
#### Package structure - Feature Based Layering - Hexagonal Architecture


com.swcs.ticket.web


(C) TicketController


com.swcs.ticket.service


(C) TicketService


(I) TicketRepository


com.swcs.ticket.repository


(C) TicketJdbcRepository


```
8


com.swcs.reservation.web


(C) ReservationController


⁴ [https://alistair.cockburn.us/hexagonal-architecture/](https://alistair.cockburn.us/hexagonal-architecture/)


```
com.swcs.reservation.service


(C) ReservationService


(I) ReservationRepository


com.swcs.reservation.repository


(C) ReservationJdbcRepository


```
The advantage of this approach is that the entire business logic is defined centrally in a business component and is decoupled from the outside world. But the visibility here is also too much

open.


#### Reducing dependencies with application events


Direct dependencies between modules are usually created by direct calls and the use of classes from another module. Dependencies should be reduced as far as possible. Cyclic dependencies should be avoided at all costs.


There are several ways to reduce them like application events. Application events can be used for loosely coupled components to exchange information. By replacing the direct call with an application event, you can reverse the dependencies. A module creates an event of a certain type, which is located within the module. Another module registers an event listener, which listens and reacts to this event type. The received module thus has a dependency on the sending module, because it knows this event type and not vice versa. This is especially useful for calls with direct commands to another module like audit logs.

```
#### **2.13.7 The Java Module System**


[With the Java Module System⁵ introduced in Java 9, you get another way to add visibility rules](https://openjdk.java.net/jeps/261)
to your architecture. The above examples were designed without the capabilities of the module
system. But if you use this, it will make it easier for you to organize your code.


The modularity adds a higher level of aggregation above the packages. The packages in one
module are only accessible to other modules if the module explicitly exports them. Even then,
another module can only use these packages if it explicitly declares that it needs the functionality
of the other module.


You will find that taking the module system into account helps you to develop cleaner, more
consistent designs. You don’t need a modular system to design for modularity, but a modular
system makes this much easier.


The book _The Java Module System_ by Nicolai Parlog gives a detailed insight into the features of
the module system.


⁵ [https://openjdk.java.net/jeps/261](https://openjdk.java.net/jeps/261)


```
