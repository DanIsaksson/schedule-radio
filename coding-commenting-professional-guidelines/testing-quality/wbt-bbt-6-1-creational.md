---
id: kb-testing-quality-011
title: "**WBT** **BBT** – **6.1 Creational**"
domain: testing-quality
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **6.1 Creational**: _Ensure a class only has one instance, an provide a global point of access to it._ The Singleton design pattern ensures that there is only one instance of a class. This instance is globally accessible and reused throughout the application."
tags:
  - classes
  - clean-code
  - example
  - principles
  - process
source_lines:
  start: 4688
  end: 12117
---
## **WBT** **BBT**
### **6.1 Creational**

#### **6.1.1 Singleton**


_Ensure a class only has one instance, an provide a global point of access to it._


The Singleton design pattern ensures that there is only one instance of a class. This instance is
globally accessible and reused throughout the application.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-188-0.png)


Singletons are not created at each request. Only single instance is reused again and again.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-188-1.png)


1.1.1 Example: [Lazy loading](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/singleton/lazy)


Lazy loading delays the creation of the Singleton instance until it is needed. This approach is
thread-safe and ensures that the instance is created only when required.


```
#### Lazy loading


public final class Singleton {


private static Singleton instance;


3


private Singleton() {}


5


```
public static synchronized Singleton getInstance() {


if (instance == null ) {


instance = new Singleton();


}


```
10


return instance;


}


}


```


1.1.2 Example: [Eager loading](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/singleton/eager)


Eager loading creates the Singleton instance at the time of class loading. This approach is simple
and thread-safe without synchronization overhead.


```
#### Eager loading


public final class Singleton {


private static final Singleton instance = new Singleton();


3


private Singleton() {}


5


```
public static Singleton getInstance() {


return instance;


}


}


```
1.1.3 Example: [Enum singleton](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/singleton/usingenum)


Using an enum to implement a Singleton provides serialization for free and guarantees against multiple instantiations. This is the preferred approach as it simplifies the Singleton pattern.²


```
#### Enum singleton, is the preferred approach


1 **public enum** Singleton {
