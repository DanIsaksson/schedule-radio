---
id: kb-formatting-structure-001
title: "- CI/CD – **2.6 Information Hiding**"
domain: formatting-structure
parent_heading: "**Preface**"
intent: "- CI/CD – **2.6 Information Hiding**: Simplify and reduce access to a class by hiding details, methods, and members that shouldn‘t be called and accessed by a client. ![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-32-0.png) ``` In Java, classes and class members are access controlled."
tags:
  - classes
  - clean-code
  - comments
  - formatting
  - functions
  - guideline
  - process
source_lines:
  start: 311
  end: 1432
---
## - CI/CD
### **2.6 Information Hiding**


Simplify and reduce access to a class by hiding details, methods, and members that shouldn‘t be
called and accessed by a client.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-32-0.png)


```
#### Information Hiding


In Java, classes and class members are access controlled. This mechanism prevents the users of a package or class from depending on unnecessary details of the implementation of that package or class.


The access is specified by the access modifiers public, protected or private . In the absence of an access modifier, it is the default access, also called package-private access.


The following table and illustration show the access control rules in Java.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-32-1.png)


```
#### Visibility in Java


¹
#### :
#### [According to the Java Language Specification (JLS)](https://docs.oracle.com/javase/specs/jls/se15/html/jls-6.html#jls-6.6)


#### public

A public class member or constructor is accessible throughout the package where it is
declared and from any other package.


¹ [https://docs.oracle.com/javase/specs/jls/se15/html/jls-6.html#jls-6.6](https://docs.oracle.com/javase/specs/jls/se15/html/jls-6.html#jls-6.6)


```
#### protected

A protected member or constructor of an object may be accessed from outside the package in which it is declared only by code that is responsible for the implementation of that object. Can only be applied to fields, constructors, and methods, not to classes.


```
#### package-private

If none of the access modifiers public, protected, or private are specified, a class member or
constructor has package-private access. It is accessible throughout the package that contains
the declaration of the class in which the class member is declared, but the class member or
constructor is not accessible in any other package.


```
#### private

A private class member or constructor is accessible only within the body of the top-level class that encloses the declaration of the member or constructor.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-33-0.png)


```
#### Visibility in Java


Declare members as private and provide accessible methods for private fields. Let distributed
classes communicate only through this method calls.


If a class, interface, method, or field is part of a published API, it can be declared public . Other
classes and members should be declared as either package-private or private . The exposure of
fields and methods that give access to the mutable state of a class via interfaces must be avoided.
This is because interfaces only allow publicly accessible methods, which are part of the API of
the class. An exception is the implementation of methods that expose a public immutable view of
a modifiable object. Modifiable classes should provide copy functionality to allow secure passing
of instances to the client code.


```
#### Inheritance and visibility:


If a method is overwritten, the new method cannot have stronger access control than the original. In the case of private no inheritance is possible. More precisely, no overwriting takes place and access to the superclass method via super is not possible.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-34-0.png)


```
#### Visibility and Inheritance
