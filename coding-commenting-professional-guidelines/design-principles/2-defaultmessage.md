---
id: kb-design-principles-013
title: "2 DEFAULTMESSAGE"
domain: design-principles
parent_heading: "**6. Design Patterns of the Gang of** **Four**"
intent: "2 DEFAULTMESSAGE: 2020-12-29T13:04:38.388325800 DefaultMessage 2020-12-29T13:04:38.416309600 DEFAULTMESSAGE 2.2.2 Example: [Window](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/decorator/window) ``` ![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-225-0.png)"
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
  start: 15642
  end: 16660
---
## 2 DEFAULTMESSAGE


2020-12-29T13:04:38.388325800 DefaultMessage


2020-12-29T13:04:38.416309600 DEFAULTMESSAGE


2.2.2 Example: [Window](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/decorator/window)


```
#### Window

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-225-0.png)


public interface Window {


void renderWindow();


}


```


#### SimpleWindow


public class SimpleWindow implements Window {


public void renderWindow() {


_// implementation of rendering details_


}


}


```
#### DecoratedWindow


public class DecoratedWindow implements Window {


private Window window = null ;


3


```
public DecoratedWindow(Window window) {


this .window = window;


}


```
7


public void renderWindow() {


window.renderWindow();


}


}


```
#### ScrollableWindow


public class ScrollableWindow extends DecoratedWindow {


private Object scrollBar = null ;


3


```
public ScrollableWindow(Window window) {


super (window);


}


```
7


public void renderWindow() {


super .renderWindow();


renderScrollBar();


}


```
12


private void renderScrollBar() {


scrollBar = new Object(); _// prepare scrollbar_


_// render scrollbar_


}


}


```


Window window = new SimpleWindow();


window.renderWindow();


```
5


_// at some point later maybe text size becomes larger_


_// than the window thus the scrolling behavior must be added_


8


_// decorate old window, now window object has additional behavior_


window = new ScrollableWindow(window);


11


```
window.renderWindow();


}


}


```


    - Decorator offers **more possibilities** than inheritance, as it is not static and therefore defines
a behavior of the subclass.

    Slim, cohesive classes . Each decorator represents exactly one function and nothing else.
This increases class cohesion and makes the corresponding code easier to maintain and
extend.

```
    - Avoidance of long and confusing inheritance hierarchies.

    - By combining a few Decorator objects, the functionality of the object can be extended
considerably.


    - Decorated objects are wrapped by the actual decorator. This makes it difficult to determine
the identity of the objects.

High number of objects. Each additional feature requires a new Decorator object. The number of many small, similar objects and their initialization code can quickly become confusing. Can be fixed by a factory.

```
    - Decorator-Pattern is a **dynamic process**, so in general the runtime behavior is worse than
with classical inheritance.


#### **6.2.3 Adapter**


_Convert the interface of a class into another interface clients expect. Adapter lets classes work_
_together that couldn‘t otherwise because of incompatible interfaces._


#### The adapter pattern defines four roles:


  Adaptee which is to be adapted to the target interface.

  Adapter who performs the adaptation.

  Target interface to be used by the client.

```
  - **Client** who uses the adaptee through the adapter.


#### Class Adapter uses multiple inheritance:
In the case of the class adapter, the adapter inherits our adaptee class and implements the target interface. When a client object calls a method of the class adapter, the adapter internally calls a method of the adapter it inherits.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-228-0.png)


```
#### Object Adapter relies on object composition:
In the case of an object adapter, the adapter delegates to an instance of the adaptee. The adapter
implements the target interface but does not inherit the classes that need to be adapted. When a
client calls a method of the object adapter, the object adapter calls a corresponding method on
the instance of the adaptee it references.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-228-1.png)


2.3.1 Example: [Sorter](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/adapter/sorter)

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-229-0.png)


```
#### Sorter - Target interface


public interface Sorter {


int [] sort( int [] numbers);


}


```
#### SortListAdapter - Adapter


public class SortListAdapter implements Sorter {


2


public int [] sort( int [] numbers) {


List<Integer> numberList = convertArrayToList(numbers);


5


NumberSorter numberSorter = new NumberSorter();


numberList = numberSorter.sort(numberList);


8


```
return convertListToArray(numberList);


}


}


```
#### NumberSorter - Adaptee


_// A third party implementation of a number sorter that deals with lists, not arrays._


public class NumberSorter {


public List<Integer> sort(List<Integer> numbers) {


return sort(numbers);


}


}


```


Sorter sorter = new SortListAdapter();


sorter.sort( new int [] { 34, 2, 4, 12, 1 });


}


}


```
**6.2.3.2 Example:** **[TextFormatter](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/adapter/textformatter)**

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-230-0.png)


#### TextFormattable


public interface TextFormattable {


String formatText(String text, String separator);


}


```
#### NewlineFormatter


1 **public class NewlineFormatter implements** TextFormattable {


2


public String formatText(String text, String separator) {


if (text != null && separator != null ) {


return Stream.of(text.split(Pattern.quote(separator)))


.map(String::stripLeading)


.collect(Collectors.joining(System.lineSeparator()));


}


```
10


return null ;


}


}


```
#### CsvFormattable


public interface CsvFormattable {


String formatCsv(String text, String separator);


}


```


#### CsvFormatter


1 **public class CsvFormatter implements** CsvFormattable {


2


public String formatCsv(String text, String separator) {


if (text != null && separator != null ) {


return Stream.of(text.split(Pattern.quote(separator)))


.collect(Collectors.joining(", "));


}


```
9


return null ;


}


}


```
#### CsvAdapter


public class CsvAdapter implements TextFormattable {


2


private final CsvFormatter csvFormatter;


4


```
public CsvAdapter(CsvFormatter csvFormatter) {


this .csvFormatter = csvFormatter;


}


```
8


public String formatText(String text, String separator) {


return this .csvFormatter.formatCsv(text, separator);


}


}


```


2


String text = "Adapter Pattern 1. Adapter Pattern 2. Adapter Pattern 3.";


5


TextFormattable tf = new NewlineFormatter();


System.out.println(tf.formatText(text, "."));


8


```
tf = new CsvAdapter( new CsvFormatter());


System.out.println(tf.formatText(text, "."));


}


}


```

#### **6.2.4 Composite**


_Compose objects into tree structures to represent part-whole hierarchies. Composite lets clients_ _treat individual objects and compositions of objects uniformly._

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-232-0.png)


```


  - Manipulation of a hierarchical collection of simple and complex objects.

  - Modelling of a tree-like structure of objects

– File system . Files and folders can be modeled with the composite pattern. Files
represent the leafs and folders represent the composites, as they may contain other
files and folders.

– Menus . Menus consist of a root entry (composite) and commands (leafs).

```
#### –
Hierarchy of graphic objects e.g., in Java / AWT (Container & Component)


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-232-1.png)


2.4.1 Example: [Graphic](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/composite/graphic)


```
#### Component


public interface Component {


void paint();


}


```
#### Composite


public abstract class Composite implements Component {


2


private List<Component> components = new ArrayList<Component>();


4


```


public void paint() {


for (Component component : this .components) {


component.paint();


}


}


```
11


public void add(Component component) {


this .components.add(component);


}


```
15


public void remove(Component component) {


this .components.remove(component);


}


```
19


public Component get( int index) {


return this .components.get(index);


}


}


```
#### Sheet


public class Sheet extends Composite {


public void paint() {


System.out.println(Sheet.class.getSimpleName());


super .paint();


}


}


```


#### Row


public class Row extends Composite {


public void paint() {


System.out.printf(" %s%n", Row.class.getSimpleName());


super .paint();


}


}


```
#### Column


public class Column extends Composite {


public void paint() {


"
```
System.out.printf( %s%n", Column.class.getSimpleName());


super .paint();


}


}


```


Composite sheet = new Sheet();


```
4


Composite r1 = new Row();


r1.add( new Column());


r1.add( new Column());


sheet.add(r1);


```
9


Composite r2 = new Row();


r2.add( new Column());


r2.add( new Column());


sheet.add(r2);


```
14


sheet.paint();


}


}


```


1 Sheet


2 Row


3 Column


4 Column


5 Row


6 Column


7 Column


**6.2.4.2 Example:** **[Organization Chart](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/composite/organizationchart)**


#### Employee


public interface Employee {


void draw();


}


```
#### DisciplinaryLeadership


public abstract class DisciplinaryLeadership implements Employee {


2


private final List<Employee> employees = new ArrayList<>();


4


```


public void draw() {


for (Employee employee : this .employees) {


employee.draw();


}


}


```
11


public void add(Employee employee) {


this .employees.add(employee);


}


```
15


public Employee get( int index) {


return this .employees.get(index);


}


}


```
#### CTO


1 **public class CTO extends** DisciplinaryLeadership {


2


public void draw() {


System.out.println(CTO.class.getSimpleName());


super .draw();


}


}


```
#### VP


1 **public class VP extends** DisciplinaryLeadership {


2


public void draw() {


System.out.printf(" %s%n", VP.class.getSimpleName());


super .draw();


}


}


```


#### Developer


1 **public class Developer implements** Employee {


2


3 @Override


4 **public void** draw() {


" System.out.printf( %s%n", Developer.class.getSimpleName());


}


}


```


3


```
DisciplinaryLeadership cto = new CTO();


DisciplinaryLeadership vp1 = new VP();


DisciplinaryLeadership vp2 = new VP();


```
7


vp1.add( new Developer());


vp1.add( new Developer());


10


```
vp2.add( new Developer());


vp2.add( new Developer());


vp2.add( new Developer());


```
14


cto.add(vp1);


cto.add(vp2);


17


```
cto.draw();


}


}


```
