---
id: kb-design-principles-009
title: "**WBT** **BBT** – **5.5 Packaging Principles - Coupling**"
domain: design-principles
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **5.5 Packaging Principles - Coupling**: _The dependency graph of packages must have no cycles._ - Classes and packages should not contain cyclic dependencies. - Make testing, maintainability, reusability, builds, and releases more difficult if cycles are not eliminated."
tags:
  - architecture
  - classes
  - clean-code
  - example
  - functions
  - guideline
  - principles
  - process
  - testing
source_lines:
  start: 4688
  end: 11748
---
## **WBT** **BBT**
### **5.5 Packaging Principles - Coupling**

#### **5.5.1 Acyclic Dependencies Principle**


_The dependency graph of packages must have no cycles._


  - Classes and packages should not contain cyclic dependencies.

  - Make testing, maintainability, reusability, builds, and releases more difficult if cycles are
not eliminated.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-172-0.png)


Breaking up such cycles is always possible, but there are two possibilities:


_If the dependency from package A to package B is to be inverted._


Use of the DIP :


```
  - Introduction of an interface in package A, which holds the methods required by B.

  - Implementation of these interfaces in the corresponding classes of Package B.


:
#### Restructuring of the packages


  - Collecting all classes of a cycle in a separate package.

  - Introducing one or more new packages with the classes on which the classes outside the
cycle depend.


5.1.1 Example: [Cyclic dependency](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/adp)


```

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-173-0.png)


The noncompliant code example violates the Acyclic Dependencies Principle (ADP) by creating a cyclic dependency between classes A, B, and C . This cyclic dependency makes the code difficult to understand, maintain, and test.


```
#### swcs.dp.adp.before.p1


public class A {


private final B b;


3


```
public A(B b) {


this .b = b;


}


}


```
#### swcs.dp.adp.before.p2


public class B {


private final C c;


3


```
public B(C c) {


this .c = c;


}


}


```


#### swcs.dp.adp.before.p3


public class C {


private final A a;


3


```
public C(A a) {


this .a = a;


}


}

```
![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-174-0.png)


#### Violation against ADP


This design violates the ADP because it introduces cyclic dependencies. Class A depends on B,
class B depends on C, and class C depends on A, creating a loop in the dependency graph.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-174-1.png)


The compliant solution resolves the cyclic dependencies by introducing an interface, C, to break
the loop.


```
#### swcs.dp.adp.after.p1


public class A {


private final B b;


3


```
public A(B b) {


this .b = b;


}


}


```
Class C is refactored into an interface, and its implementation, CImpl, depends on class A .


#### swcs.dp.adp.after.p1


public class CImpl implements C {


private final A a;


3


```
public CImpl(A a) {


this .a = a;


}


}


```
Now, class CImpl implements the C interface and receives an instance of A in its constructor. Class

A and class B remain unchanged.


#### swcs.dp.adp.after.p2


public class B {


private final C c;


3


```
public B(C c) {


this .c = c;


}


}


```
#### swcs.dp.adp.after.p3


public interface C {


}

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-175-0.png)


```
#### No violation against ADP


By introducing the C interface and its implementation CImpl, the cyclic dependencies are resolved.
Class A depends on class B, class B depends on the C interface, and class CImpl depends on class A .
This dependency structure creates a directed acyclic graph (DAG) rather than a cycle.


```
#### **5.5.2 Stable Dependencies Principle**


_Depend on the direction of stability._


  - The dependencies between packages should be in the same direction as the stability.

  - A package should only depend on packages that are more stable than itself.

  - The fewer dependencies a package has on others and the more dependencies other packages
have on that package, the more stable it is.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-177-0.png)


#### Stable and unstable packages


#### Instability metric
The stability of a package is the ratio of outgoing coupling ( Ce ) to total coupling ( Ca + Ce ).


#### I = Instability, 0 ≤I ≤1


I = 0, stable package


I = 1, unstable package


Ca = incoming dependencies (afferent couplings)


Ce = outgoing dependencies (efferent couplings)


An instability of 1 means that no other package depends on this package. This package depends on other packages and is as unstable as a package can be. Its lack of dependencies gives it no reason not to change. Furthermore, the packages it depends on can give it lots of reasons to change.


An instability of 0 means that the package depends on other packages, but does not depend on other packages at all. It is independent and is as stable as it can be. Its dependencies make it difficult to change. Further, it has no dependencies that could force it to change.


The SDP says that the metric of a package should be greater than the metrics of the packages it depends on. The metric of instability should decrease in the direction of dependency.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-178-0.png)


```
#### Instability


How do we achieve stability? The stable packages are not dependent on anything. A change
from a dependent cannot spread to them and cause them to change. They are independent.
Independent classes are classes that do not depend on anything else.
Another reason why a stable package gets stable is that many other classes depend on them. The
more dependencies they have, the harder it is to make changes to them. If we change classes
in the stable package, we would also have to change all the other classes that depend on them.
So, there is a strong constraint that stops us from changing these classes. This fact not changing
these classes increases their stability.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-178-1.png)


```
#### Stable package depend on unstable package


If all packages are maximally stable, the system would be unchangeable. This is not a desirable situation. We want to design our package structure so that some packages are unstable, and some are stable. The changeable packages should be at the top and depend on the stable packages at the bottom. If we place the unstable packages at the top and arrange them in this way, it does not violate the SDP.


```
#### **5.5.3 Stable Abstractions Principles**


_Abstractness increases with stability._


  - Establishes a relationship between stability and abstractness.

  - Packages that are maximally stable should be maximally abstract.

  - Unstable packages should be concrete.

  - The abstractness of a package should be proportional to its stability.


#### Abstractness metric

The abstractness of a package is the ratio of abstract classes like abstract classes and interfaces
in a package to the total number of classes in the package.


#### A = Abstractness, 0 ≤A ≤1


A = 0, completely concrete package


A = 1, completely abstract package


Na = number of abstract classes, interfaces


Nc = total number of classes

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-179-0.png)


An abstractness of 0 means that the package has no abstract classes. An abstractness of 1 means
that the package contains nothing but abstract classes such as interfaces and abstract classes.


```
#### Relationship between Instability (I) and Abstractness (A)


The relationship between I and A can be defined within a graph with A on the vertical axis and I on the horizontal axis. There are two _good_ types of packages on this graph, the first Interface packages that are maximally stable and abstract at the top left at (0,1). The second is the Implementation packages that are maximally unstable, and concrete is at the bottom right at (1,0).


However, not all packages can be classified in one of these two categories. Thus, packages have degrees of abstraction and stability. Therefore, we must accept that there is a place of points on the A/I graph that defines meaningful positions for packages. This can be done with the Main Sequence which is a line from (0,1) to (1,0). This line represents packages whose abstractness is _balanced_ with stability. A packet on the Main Sequence is neither _too abstract_ for its stability, nor is it _too unstable_ for its abstractness. It has the _right_ number of concrete and abstract classes in proportion to its efferent and afferent dependencies.


We can infer in which areas no packages should be, the Zone of Pain and the Zone of Uselessness .


```
#### The Main Sequence


  - Ideal line

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-180-0.png)

  - Even better when package at (0.1) or (1.0), hardly implemented in practice


#### Zone of Pain


  - Stable non-abstract classes

  - Difficult to change because stable, difficult to extend because not abstract

  - Exceptions are utility classes such as String


#### Zone of Uselessness


  - Unstable very abstract classes

  - Are abstract but are nowhere extended

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-180-1.png)


#### Distance from the Main Sequence


The most desirable positions for a package are at one of the two endpoints of the Main Sequence.
However, most packages do not have such ideal attributes. The other packages have the best
characteristics when they are on or near the ideal line, which can be defined with the distance
to Main Sequence. For each package, the distance to the ideal line between maximum stability


and abstractness and maximum instability and concreteness can be calculated. The greater the
distance, the worse the SAP is fulfilled.


or normalized

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-181-0.png)


```
#### D = Distance from the ideal line, 0 ≤D ≤0.707


#### D'= Normalised distance from the ideal line, 0 ≤D' ≤1


A = Abstractness of a package


I = Instability of a package


```
