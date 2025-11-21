---
id: kb-functions-objects-001
title: "- CI/CD – **3.3 Classes and objects**"
domain: functions-objects
parent_heading: "**Preface**"
intent: "- CI/CD – **3.3 Classes and objects**: Designing Java classes is crucial for creating effective and reusable software. Follow these best practices when designing Java classes: Standard Java Code Conventions : Maintain readability and consistency by adhering to the standard Java code conventions."
tags:
  - architecture
  - classes
  - clean-code
  - comments
  - formatting
  - functions
  - guideline
  - naming
  - principles
  - process
source_lines:
  start: 311
  end: 4523
---
## - CI/CD
### **3.3 Classes and objects**


Designing Java classes is crucial for creating effective and reusable software. Follow these best practices when designing Java classes:


Standard Java Code Conventions : Maintain readability and consistency by adhering to the standard Java code conventions. Use proper indentation, naming conventions, and comment code as necessary.

Single Responsibility : Each Java class should have a clear and single purpose. Avoid combining too many responsibilities within a single class, as it can lead to confusion and maintenance difficulties.

Flexibility and Extensibility : Design classes to be flexible and easily adaptable to future changes. Ensure that new features and functionality can be seamlessly integrated into the existing class without significant modifications.

Consistency : Maintain consistency in your class design throughout your project. Use consistent naming conventions, coding standards, and design patterns. Consistency improves code readability and makes it easier for others to understand and work with your code.

Principle of Least Astonishment (POLA) : Design classes with intuitive and predictable behavior. Users should not be surprised by the behavior of the system. Follow standard conventions and design principles to minimize unexpected behavior.


In conclusion, to design effective Java classes, keep them small, focused on a single responsibility, and use access modifiers wisely.


According to _Clean Code_ by Robert C. Martin, the following best practices can help you write good classes, functions, and variables that are easy to read and modify.

```
#### **3.3.1 Classes**


  - Use noun or noun phrase names for classes.

  - Avoid naming classes as verbs.

  - Hide internals and minimize the scope of variables and methods.

  - Follow the Law of Demeter (LoD) - a class should only have knowledge of its direct
dependencies.

  - Base classes should not have knowledge of their derivatives.

  - Prefer immutability whenever possible.

  - Use dependency injection to decouple dependencies.


#### **3.3.2 Functions**


  - Use verb or verb phrase names for functions.

  - Keep functions as small as possible.

  - Functions should do one thing and do it well - follow the Single Responsibility Principle
(SRP).

  - Use one level of abstraction per function.

  - Avoid code duplication (Don’t Repeat Yourself - DRY).

  - Functions should have no side effects.

  - Group similar functions together.

  - Place functions in a downward direction, with higher-level functions calling lower-level

ones.

  - Use consistent and clear terminology for concepts.

  - Minimize the number of function arguments, ideally zero or one.

  - Avoid using flag arguments - split methods into independent methods without flags.

  - private functions can have longer and more descriptive names and can be refined over time
using the Boy Scout Rule.

  - The visibility of a function determines the length of its name - shorter names for public
functions, longer names for private ones.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-80-0.png)


#### Method name length


#### **3.3.3 Variables**


  - Declare variables close to their usage and minimize their scope.

  - The scope of a variable determines its name’s length - longer names for global variables,
shorter names for scope-limited variables like loop index variables.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-81-0.png)


#### Variable name length
