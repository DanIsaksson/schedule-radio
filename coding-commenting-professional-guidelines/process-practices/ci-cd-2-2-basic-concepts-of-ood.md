---
id: kb-process-practices-015
title: "- CI/CD – **2.2 Basic concepts of OOD**"
domain: process-practices
parent_heading: "**Preface**"
intent: "- CI/CD – **2.2 Basic concepts of OOD**: Object-oriented programming has four basic concepts: abstraction, encapsulation, inheritance and polymorphism . Although these concepts may seem complex, understanding how they work will help you to understand the basics of software design."
tags:
  - architecture
  - classes
  - clean-code
  - example
  - formatting
  - functions
  - naming
  - principles
  - process
source_lines:
  start: 311
  end: 1094
---
## - CI/CD
### **2.2 Basic concepts of OOD**


Object-oriented programming has four basic concepts: abstraction, encapsulation, inheritance
and polymorphism . Although these concepts may seem complex, understanding how they work
will help you to understand the basics of software design.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-25-0.png)


```
#### Basic concepts of OOD


#### Abstraction


Abstraction implies that the client only interacts with specific attributes and methods of an object. Abstraction uses simplified high-level access to a complex object.


  - Hiding of complexity by ignoring irrelevant details

  - Solves the problem at the design level


#### Encapsulation


Encapsulation is the mechanism for hiding data implementation by restricting access to classes, methods, and variables. An example would be achieving this, by keeping instance variables private and making access methods more visible. To reduce couplings between software components, encapsulation mechanisms are essential.


```
  - Information and data hiding

  - Solves the problem at the implementation level


#### Inheritance


Inheritance allows classes to inherit characteristics of other classes. In practice, parent classes
extend attributes and behaviors to subclasses.


```
  - Extension and specialization

  - Inheritance supports reusability

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-26-0.png)


#### Inheritance


#### Polymorphism


Polymorphism means one name but many forms. It allows designing objects that share behaviors. Using inheritance, objects can overwrite parent behavior with specific behavior. Polymorphism allows the same method to perform different behavior statically and dynamically. The static one is achieved by method overloading and the dynamic by method overriding. Thus, polymorphism is closely connected with inheritance. We can write code that works on the superclass, and it works with any subclass type as well.


```
  - Substitutability

  - Same interface, different behavior

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-26-1.png)


#### Polymorphism


#### Polymorphism


2


List<Employee> employees = List.of(


new Employee("Employee", "Max", "Company"),


new Developer("Developer", "Max", "Company"),


new Manager("Manager", "Max", "Company"));


```
8


employees.forEach(e -> System.out.printf(assembleMessage(e)));


}


11


```
private static String assembleMessage(Employee employee) {


return String.format("%s with an annual salary bonus of %g!\n",


employee.getClass().getName(),


employee.annualSalaryBonus());


}


}


```


Employee with an annual salary bonus of 1000,00!


Developer with an annual salary bonus of 3000,00!


Manager with an annual salary bonus of 5000,00!


```
In this example, the compiler does not know what type of the reference attribute employee is.
They do not know which operation is called on annualSalaryBonus() . The method of the superclass

Employee or the redefined annualSalaryBonus() of the subclass Developer or Manager .


Furthermore, the operation assembleMessage() does not need to be modified if additional subclasses
are added to the Employee superclass.


```
