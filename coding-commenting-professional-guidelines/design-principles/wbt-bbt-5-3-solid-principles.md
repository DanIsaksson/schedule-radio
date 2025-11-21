---
id: kb-design-principles-007
title: "**WBT** **BBT** – **5.3 SOLID Principles**"
domain: design-principles
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **5.3 SOLID Principles**: _A class should have one, and only one, reason to change._ - This is an object-oriented version of the functional programming principle that functions should have _no side effects_ . - A class should only have functions that directly contribute to the fulfillment of this task."
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
  start: 4688
  end: 11244
---
## **WBT** **BBT**
### **5.3 SOLID Principles**

#### **5.3.1 Single Responsibility Principle**


_A class should have one, and only one, reason to change._


    - This is an object-oriented version of the functional programming principle that functions
should have _no side effects_ .

    - A class should only have functions that directly contribute to the fulfillment of this task.

    - One of the easiest ways to write classes that never need to be changed is to write classes
that serve only one purpose.

    - Each class should implement a coherent set of related functions.

    - Having multiple responsibilities in a class leads to tight coupling and results in fragile
designs.

    - Following the principle of single responsibility is to ask yourself again and again whether
each method and process of a class is directly related to the name of that class.

    - For methods that do not match the name of the class, move these methods to other classes.


**5.3.1.1 Example:** **[Modem](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/srp/modem)**


1 **public interface Modem** {

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-129-0.png)


_// Connection management responsibilities_


void dial(String phoneNumber);


void hangup();


```
5


_// Data management responsibilities_


void send( char c);


char receive();


}


```


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-130-0.png)


The Single Responsibility Principle (SRP) states that a class or module should have only one
reason to change. In the given code example, the SRP violation is evident because the Modem
interface is responsible for both connection management and data management.


To address this violation, the code can be refactored into a compliant solution that separates the
responsibilities.


The DataChannel interface is created to handle the responsibilities related to data management. It
declares the methods send and receive for sending and receiving data, respectively.


```


public interface DataChannel {


void send( char c);


char receive();


}


```
The Connection interface is created to handle the responsibilities related to connection management. It declares the methods dial and hangup for establishing and terminating a connection,
respectively.


public interface Connection {


void dial(String phoneNumber);


void hangup();


}


```
The Modem class is modified to implement both the Connection and DataChannel interfaces. By implementing these interfaces, the Modem class takes on the responsibilities defined by each interface. It separates the connection management logic from the data management logic, adhering to the SRP.


By following this compliant solution, each interface and class has a single responsibility, making the code more maintainable and allowing for easier modification or extension of individual responsibilities without affecting the others.


```


1 **public final class Modem implements** Connection, DataChannel {


2


public void dial(String phoneNumber) {


}


```
7


public void hangup() {


}


```
12


public void send( char c) {


}


```
17


public char receive() {


}


}


```


**5.3.1.2 Example:** **[Book](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/srp/book)**


The Book class violates the SRP by having multiple responsibilities, such as storing book data and
exporting the book to different formats.


1 **public** record Book(String title, String author, String content) {


2


public void exportToPdf() {


}


```
6


public void exportToEpub() {


}


}


```


To address this violation, the code can be refactored into a compliant solution that separates the
responsibilities. The Book class is modified to only handle the responsibility of storing book data.
The export functionality is removed from the class.


```


public record Book(String title, String author, String content) {


2


}


```


public interface BookExporter {


void export(Book book);


}


```
The BookExporter interface is introduced to handle the responsibility of exporting a book. It declares a single method export that takes a Book object as a parameter.


1 **public final class PdfExporter implements** BookExporter {


2


public void export(Book book) {


}


}


```
The PdfExporter class is created to handle the specific responsibility of exporting a book to PDF
format. It implements the BookExporter interface and provides the implementation for the export
method.


```


1 **public final class EpubExporter implements** BookExporter {


2


public void export(Book book) {


}


}


```
The EpubExporter class is created to handle the specific responsibility of exporting a book to EPUB
format. It also implements the BookExporter interface and provides the implementation for the

export method.


By following this compliant solution, the Book class has a single responsibility of storing book data,
while the exporting functionality is moved to separate classes that implement the BookExporter
interface.


3.1.3 Example: [Product](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/srp/product)


```


The Product class violates the SRP by having multiple responsibilities, which stores product data and also handles discount calculation.


1 record Product(String name, **double** price) {


2


public double calculateDiscount() {


_// Calculate the discount for the product._


return 0.0;


}


}


```


In the improved code, we separate the responsibilities of storing product data and discount
calculation into two distinct classes. With this separation, the Product class focuses solely on
storing product data, and the DiscountCalculator class is responsible for discount calculation. This
adheres to the SRP, making the code more modular and maintainable.


```


record Product(String name, double price) {


2


}


```


class DiscountCalculator {


public double calculateDiscount(Product product) {


_// Calculate the discount for the product._


return 0.0;


}


}


```

#### **5.3.2 Open Closed Principle**


_You should be able to extend a classes behavior, without modifying it._


  - Classes that follow the OCP have two essential characteristics:


– Open to extensions, the behavior of such class can be extended to meet new requirements of an existing or even new application. – Closed against subsequent modification, no longer need to be modified to serve as a basis for new requirements.

```
  - Functions that perform different actions due to type switches are good examples of OCP
violations. Such functions are never closed against changes because adding a new type
requires changing the source code of the function.

  - It is more elegant and robust to extend a class group by adding a class than to modify the
existing source code.

  - By using e.g. abstract base classes, software components can be created which have a
fixed unchangeable implementation, but whose behavior can be changed indefinitely by
inheritance and polymorphism.


**5.3.2.1 Example:** **[LoanRequestHandler](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/ocp/loanvalidator)**


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-135-0.png)


The noncompliant code violates the OCP because the LoanRequestHandler class is not closed for
modification.


```


public class LoanRequestHandler {


private final int balance;


private final int period;


```
4


public LoanRequestHandler( int balance, int period) {


this .balance = balance;


this .period = period;


}


```
9


public void approveLoan(PersonalLoanValidator validator) {


if (validator.isValid(balance)) {


System.out.println("Loan approved...");


System.out.println("Sorry not enough balance...");


}


}


}


```


public final class PersonalLoanValidator {


public boolean isValid( int balance) {


return balance > 1000;


}


}


```
The LoanRequestHandler class has a method approveLoan that takes a PersonalLoanValidator object as a parameter. This tightly couples the class to a specific type of validator, violating the OCP. If we need to introduce new types of loan validators, we would have to modify the LoanRequestHandler class, which is against the OCP principle.


```


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-136-0.png)


public class LoanRequestHandler {


private final int balance;


private final int period;


```
4


public LoanRequestHandler( int balance, int period) {


this .balance = balance;


this .period = period;


}


```
9


public void approveLoan(Validator validator) {


if (validator.isValid(balance)) {


System.out.println("Loan approved...");


System.out.println("Sorry not enough balance...");


}


}


}


```
The approveLoan method of the LoanRequestHandler class is modified to accept a Validator interface instead of a specific implementation. This change allows the class to work with any object that implements the Validator interface, making it open for extension.


```


public interface Validator {


boolean isValid( int balance);


}


```
The Validator interface is introduced to define the contract for loan validators. It declares a single method isValid that takes the balance as a parameter and returns a boolean value indicating whether the loan is valid.


```


public class PersonalLoanValidator implements Validator {


public boolean isValid( int balance) {


return balance > 1000;


}


}


```
The PersonalLoanValidator class is created as an implementation of the Validator interface. It provides the specific implementation of the isValid method for personal loans.


```


public class BusinessLoanValidator implements Validator {


public boolean isValid( int balance) {


return balance > 5000;


}


}


```
The BusinessLoanValidator class is created as another implementation of the Validator interface. It provides a different implementation of the isValid method for business loans.


By following this compliant solution, the LoanRequestHandler class remains closed for modification as new validators can be introduced by implementing the Validator interface. The class can work with any object that adheres to the Validator interface, making it open for extension


3.2.2 Example: [Shape](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/ocp/shape)


```


The noncompliant code violates the OCP by using a switch statement to handle different types
of shapes, which requires modifying the draw method whenever a new shape is introduced.


public void draw(Shape[] shapes) {


for (Shape shape : shapes) {


switch (shape.type()) {


case Shape.SQUARE -> draw((Square) shape);


case Shape.CIRCLE -> draw((Circle) shape);


}


}


}


```
9


private void draw(Circle circle) {


_// logic for circle_


}


```
13


private void draw(Square square) {


_// logic for square_


}


```
The draw method takes an array of Shape objects and uses a switch statement to determine the shape type and invoke the corresponding draw method. This approach violates the OCP because whenever a new shape type is added, the draw method needs to be modified to include a new case in the switch statement, which results in the method being open for modification.


```


public interface Shape {


void draw();


}

```
![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-139-0.png)


The Shape interface is introduced to define the contract for shapes. It declares a single method draw that is responsible for drawing the shape.


```


public class Square implements Shape {


public void draw() {}


}


```


public class Circle implements Shape {


public void draw() {}


}


```


public void draw(Shape[] shapes) {


for (Shape shape : shapes) {


shape.draw();


}


}


```
The draw method is modified to iterate over the array of Shape objects and invoke the draw method on each shape. By doing so, the method is closed for modification as it doesn’t need to be updated whenever a new shape is introduced. The implementation of the draw method for each shape is determined by the specific shape class, which follows the OCP by allowing for extension without modification.


3.2.3 Example: [HumanResourceDepartment](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/ocp/humanresource)


```


public class HumanResourceDepartment {


2


private final List<Developer> developers;


private final List<Manager> managers;


5


```
public HumanResourceDepartment() {


this .developers = new ArrayList<>();


this .managers = new ArrayList<>();


}


```
10


public void hire(Developer developer) {


developer.signContract();


this .developers.add(developer);


}


```
15


public void hire(Manager manager) {


manager.signContract();


this .managers.add(manager);


}


}


```
The HumanResourceDepartment class has specific methods for hiring developers and managers. This
tightly couples the class to specific employee types, violating the OCP. If we need to introduce
new types of employees (e.g., Secretary ), we would have to modify the HumanResourceDepartment class,
which is against the OCP principle.


```


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-141-0.png)


interface Employee {


void signContract();


}


```
The Employee interface is introduced to define the contract for employees. It declares a single
method signContract that is responsible for signing the contract.


```


class Developer implements Employee {


public void signContract() {}


}


```


class Manager implements Employee {


public void signContract() {}


}


```


class Secretary implements Employee {


public void signContract() {}


}


```


public class HumanResourceDepartment {


private final List<Employee> employees;


3


```
public HumanResourceDepartment() {


this .employees = new ArrayList<>();


}


```
7


public void hire(Employee employee) {


employee.signContract();


this .employees.add(employee);


}


}


```
The HumanResourceDepartment class is modified to work with the Employee interface instead of specific employee types. The hire method now takes an Employee object as a parameter and invokes the

signContract method on the employee. This change allows the class to work with any object that implements the Employee interface, making it open for extension.


3.2.4 Example: [Calculator](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/ocp/calculator)


```


public class Calculator {


public double calculate( double x, double y, String operator) {


double result;


```
4


if ("plus".equals(operator)) {


result = x + y;


} else if ("minus".equals(operator)) {


result = x - y;


} else if ("multiply".equals(operator)) {


result = x * y;


} else if ("divide".equals(operator)) {


result = x / y;


throw new IllegalArgumentException(String.format("Operator %s not supported!", operator));


}


```
16


return result;


}


}


```
The Calculator class has a calculate method that performs different operations based on the value of the operator parameter. However, if we need to introduce new operations (e.g., exponentiation, square root), we would have to modify the Calculator class, which is against the OCP principle.


```


public enum Operation {


PLUS((x, y) -> x + y),


MINUS((x, y) -> x - y),


MULTIPLY((x, y) -> x * y),


DIVIDE((x, y) -> x / y);


```
6


7 **private final** DoubleBinaryOperator operator;


8


Operation(DoubleBinaryOperator operator) {


this .operator = operator;


}


```
12


public double apply( double x, double y) {


return this .operator.applyAsDouble(x, y);


}


}


```
An enum called Operation is introduced to represent different operations. Each operation is defined as a constant with a corresponding lambda expression ( DoubleBinaryOperator ) that performs the operation. The apply method is provided to apply the operation to the given operands.


By using an enum and lambdas, new operations can be easily added by defining additional constants in the Operation enum without modifying the existing code. This adheres to the OCP principle, as the Calculator class remains closed for modification but open for extension.


```


public class Calculator {


public double calculate( double x, double y, Operation operation) {


return operation.apply(x, y);


}


}


```
The calculate method now takes an Operation object as a parameter and directly applies the operation to the given operands. This change allows the class to work with any operation defined in the Operation enum, making it open for extension.


3.2.5 Example: [FileParser](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/ocp/parser)


```


1 **public class FileParser** {


2


public String parse(String filePath) {


if (filePath.indexOf(".xml") > 1) {


return parseXML(filePath);


} else if (filePath.indexOf(".json") > 1) {


return parseJson(filePath);


}


```
9


return null ;


}


12


```
private String parseXML(String filePath) {


return null ;


}


```
17


private String parseJson(String filePath) {


return null ;


}


}


```
The FileParser class has a parse method that determines the file type based on its extension and
then calls the respective private parse methods ( parseXML and parseJson ). This approach violates
the OCP because whenever a new file type needs to be supported, the FileParser class needs to
be modified to add a new condition and corresponding parsing logic.


```


public interface FileParser {


String parse(String file);


boolean supports(String extension);


}


```
An interface FileParser is introduced, which defines two methods: parse for parsing the file content
and supports for determining whether the parser supports a given file extension.


```


1 **public class JsonParser implements** FileParser {


2


public String parse(String file) {


return String.format("Parser %s", JsonParser.class.getSimpleName());


}


public boolean supports(String extension) {


return "json".equalsIgnoreCase(extension);


}


}


```


1 **public class XmlParser implements** FileParser {


2


public String parse(String file) {


return String.format("Parser %s", XmlParser.class.getSimpleName());


}


public boolean supports(String extension) {


return "xml".equalsIgnoreCase(extension);


}


}


```


public final class FileParserFactory {


2


private FileParserFactory() {


}


5


public static FileParser newInstance(String extension) {


ServiceLoader<FileParser> loader = ServiceLoader.load(FileParser.class);


8


```
for (FileParser parser : loader) {


if (parser.supports(extension)) {


return parser;


}


}


return null ;


}


}


```
The FileParserFactory class is responsible for creating instances of FileParser based on the provided file extension. It uses the ServiceLoader mechanism to dynamically load implementations of the

FileParser interface.


```


_// META-INF/services/swcs.dp.ocp.parser.after.FileParser_


swcs.dp.ocp.parser.after.JsonParser


swcs.dp.ocp.parser.after.XmlParser


```
For the Service API mechanism, a file named META-INF/services/swcs.dp.ocp.parser.after.FileParser is created, which lists the fully qualified names of the available FileParser implementations.


2


FileParser parser = FileParserFactory.newInstance("xml");


System.out.println(parser.parse("test.xml"));


}


}


```

#### **5.3.3 Liskov Substitution Principle**


_Derived classes must be substitutable for their base classes._


_Let q(x) be a property provable about objects x of type T. Then q(y) should be true for objects y of_
_type S, where S is a subtype of T._


```
    - In a clean OOD, the derivation relationship between superclass and subclass represents a socalled **is-a** relationship, i.e. an object of the subclass is compatible in type with a superclass
object and can be used wherever a superclass object is required.

    - This ensures that superclass type operations applied to a subclass object are performed
correctly.

    - In this case, it is always safe to replace a superclass-type object with a subclass-type object.

    - When inheriting, do not only pay attention to the data but also consider the behavior of
the methods of a class.


**5.3.3.1 Example:** **[Rectangle](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/lsp/figure)**


public class Rectangle {


2


private double height;


private double width;


5


```
public double height() {


return this .height;


}


```
9


public double width() {


return this .width;


}


```
13


public void height( double height) {


this .height = height;


}


```
17


public void width( double width) {


this .width = width;


}


```
21


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-148-0.png)


public double area() {


return this .width * this .height;


}


}


```
The Rectangle class has separate height and width properties, and its area method calculates the

area based on those properties. The Square class extends Rectangle and overrides the area method to calculate the area based on the height only (as all sides of a square are equal). However, this violates the LSP because a square is not a proper subtype of a rectangle in terms of behavior.


```


1 **public class Square extends** Rectangle {


2


public double area() {


return this .height() * this .height();


}


}


```


public void do(Rectangle rectangle) {


rectangle.width(5);


rectangle.height(4);


```
4


if (rectangle.area() != 20) {


throw new IllegalStateException("Error in area calculation!");


}


}


```
The do method accepts a Rectangle object as a parameter and performs some operation. Since both

Rectangle and Square are subclasses of Figure, the do method can not work with either a rectangle or a square object without affecting the correctness of the program.


```


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-149-0.png)


public interface Figure {


double area();


}


```


public class Circle implements Figure {


private final double radius;


3


```
public Circle( double radius) {


this .radius = radius;


}


public double area() {


return Math.PI * ( this .radius * this .radius);


}


}


```


public class Rectangle implements Figure {


private final double length;


private final double width;


```
4


public Rectangle( double length, double width) {


this .length = length;


this .width = width;


}


public double area() {


return this .length * this .width;


}


}


```


public class Square extends Rectangle {


public Square( double side) {


super (side, side);


}


}


```
The Square class is modified to extend the Rectangle class. Since a square is a special case of a
rectangle where all sides are equal, the Square class uses the Rectangle class by providing the same
value for both length and width in its constructor.


```


public void do(Rectangle rectangle) {


_// Rectangle double and width was set through constructor_


3


```
if (rectangle.area() != 20) {


throw new IllegalStateException("Error in area calculation!");


}


}


```


**5.3.3.2 Example:** **[Coupon](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/lsp/coupon)**


The noncompliant code violates the LSP because the PromotionCoupon class modifies the behavior of the afterDiscount method, strengthening its pre-conditions.


public class Coupon {


2


private final BigDecimal regularPrice;


4


```
public Coupon(BigDecimal regularPrice) {


this .regularPrice = regularPrice;


}


```
8


public BigDecimal regularPrice() {


return this .regularPrice;


}


```
12


public BigDecimal afterDiscount(BigDecimal discount) {


if (discount == null ) {


return this .regularPrice;


}


```
17


if ( this .regularPrice.compareTo(discount) > 0) {


return this .regularPrice.subtract(discount);


}


```
21


return BigDecimal.ZERO;


}


}


```


public class PromotionCoupon extends Coupon {


2


private static final double PERCENTAGE_DISCOUNT = 0.9;


4


```
public PromotionCoupon(BigDecimal regularPrice) {


super (regularPrice);


}


```
8


public BigDecimal afterDiscount(BigDecimal discount) {


_// strengthened pre-conditions and no check for null_


if (regularPrice().compareTo(discount) < 0) {


throw new IllegalArgumentException("Discount can not be greater than the price!");


}


```
15


return super .afterDiscount(discount)


.multiply(BigDecimal.valueOf(PERCENTAGE_DISCOUNT));


}


}


```


The Coupon class represents a regular coupon with a regularPrice and an afterDiscount method that calculates the price after applying a discount. The PromotionCoupon class extends Coupon and overrides the afterDiscount method to include an additional check for the discount, throwing an exception if the discount is greater than the regular price.


The noncompliant code violates the LSP because the behavior of the afterDiscount method is modified in the PromotionCoupon subclass, strengthening its pre-conditions. As a result, when the

PromotionCoupon class is used in place of the Coupon class, it throws an exception ( IllegalArgumentException ) in cases where the base class implementation would return a valid value.


```


2


private static final BigDecimal REGULAR_PRICE = BigDecimal.valueOf(8.99);


4


```


testCoupon( new Coupon(REGULAR_PRICE), null ); _// Pay 8,990000_


testCoupon( new PromotionCoupon(REGULAR_PRICE), null ); _// NPE_


```
8


testCoupon( new Coupon(REGULAR_PRICE), BigDecimal.valueOf(5)); _// Pay 3,990000_


testCoupon( new PromotionCoupon(REGULAR_PRICE), BigDecimal.valueOf(5)); _// Pay 3,591000_


11


```
testCoupon( new Coupon(REGULAR_PRICE), BigDecimal.valueOf(10)); _// You get it for free!_


testCoupon( new PromotionCoupon(REGULAR_PRICE), BigDecimal.valueOf(10)); _// IAE_


}


```
15


private static void testCoupon(Coupon coupon, BigDecimal discount) {


BigDecimal price = coupon.afterDiscount(discount);


18


```
_// Client code relies on Coupon implementation_


if (BigDecimal.ZERO.compareTo(price) == 0) {


System.out.println("You get it for free!");


System.out.printf("Pay %f%n", price);


}


}


}


```


In the compliant solution, the LSP violation is resolved by introducing the Coupon interface and
providing a clear contract for the behavior of the afterDiscount method:


public interface Coupon {


2


BigDecimal regularPrice();


4


```
_/_


_* Returns a {@code BigDecimal} whose value is {@code (+this)},_


_* minus the provided discount._


_*_


_* @param discount the discount to use._


_* @return {@code this}, minus the discount. If the discount is_


```


_*_ _greater than the regular price {@code BigDecimal.ZERO} will be returned._


_*_ _If the discount is {@code null} than this discount will not be subtracted._


_*/_


BigDecimal afterDiscount(BigDecimal discount);


}


```
The RegularCoupon class implements the Coupon interface and provides the basic behavior of a regular

coupon:


public class RegularCoupon implements Coupon {


2


private final BigDecimal regularPrice;


4


```
public RegularCoupon(BigDecimal regularPrice) {


this .regularPrice = regularPrice;


}


```
8


public BigDecimal regularPrice() {


return this .regularPrice;


}


```
13


public BigDecimal afterDiscount(BigDecimal discount) {


if (discount == null ) {


return this .regularPrice;


}


```
19


if ( this .regularPrice.compareTo(discount) > 0) {


return this .regularPrice.subtract(discount);


}


```
23


return BigDecimal.ZERO;


}


}


```
The PromotionCoupon class extends RegularCoupon and inherits its behavior without modifying the pre-conditions. It only overrides the method to apply an additional discount percentage:


public final class PromotionCoupon extends RegularCoupon {


2


private static final double PERCENTAGE_DISCOUNT = 0.9;


4


```
public PromotionCoupon(BigDecimal regularPrice) {


super (regularPrice);


}


```
8


public BigDecimal afterDiscount(BigDecimal discount) {


return super .afterDiscount(discount)


.multiply(BigDecimal.valueOf(PERCENTAGE_DISCOUNT));


}


}


```


In the compliant solution, the client code can work with any object that implements the Coupon
interface, including both RegularCoupon and PromotionCoupon . This ensures that the client code is not
affected by the specific implementation details of the subclasses and allows for substitutability
without violating the LSP.


```


2


private static final BigDecimal REGULAR_PRICE = BigDecimal.valueOf(8.99);


4


```


testCoupon( new RegularCoupon(REGULAR_PRICE), null ); _// Pay 8,990000_


testCoupon( new PromotionCoupon(REGULAR_PRICE), null ); _// Pay 8,091000_


```
8


testCoupon( new RegularCoupon(REGULAR_PRICE), BigDecimal.valueOf(5)); _// Pay 3,990000_


testCoupon( new PromotionCoupon(REGULAR_PRICE), BigDecimal.valueOf(5)); _// Pay 3,591000_


11


```
testCoupon( new RegularCoupon(REGULAR_PRICE), BigDecimal.valueOf(10)); _// You get it for free!_


testCoupon( new PromotionCoupon(REGULAR_PRICE), BigDecimal.valueOf(10)); _// You get it for free!_


}


```
15


private static void testCoupon(Coupon coupon, BigDecimal discount) {


BigDecimal price = coupon.afterDiscount(discount);


18


```
if (BigDecimal.ZERO.compareTo(price) == 0) { _// Client code relies on Coupon implementation_


System.out.println("You get it for free!");


System.out.printf("Pay %f%n", price);


}


}


}


```


**5.3.3.3 Example:** **[Bird](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/lsp/bird)**


The noncompliant code violates the LSP because the Penguin class overrides the fly() method to throw an UnsupportedOperationException because penguins cannot fly. When we call the fly() method on a Penguin object, it throws an exception, indicating that penguins cannot fly.


```


class Bird {


private final String name;


3


```
public Bird(String name) {


}


```
7


}


```
11


public void eat() {


System.out.println(name + " is eating.");


}


```
15


public void fly() {


System.out.println(name + " is flying.");


}


}


```


class Sparrow extends Bird {


public Sparrow(String name) {


super (name);


}


```
5


public void eat() {


System.out.println(name() + " is pecking at seeds.");


}


}


```


class Penguin extends Bird {


public Penguin(String name) {


super (name);


}


```
5


public void fly() {


throw new UnsupportedOperationException(name() + " can't fly.");


}


}


```


In the compliant solution, the LSP violation is resolved, both Sparrow and Penguin classes override the move() method instead of the fly() method. This allows them to represent their specific modes of movement, where the Sparrow flies, and the Penguin swims. The Penguin class no longer throws an exception because it provides a meaningful implementation for the move() method.


By making this change, you align the class hierarchy with the real-world characteristics of the birds, and the Liskov Substitution Principle is maintained because you can use objects of both derived classes interchangeably without causing issues in the program.


```


class Bird {


private final String name;


3


```
public Bird(String name) {


}


```
7


}


```
11


public void eat() {


System.out.println(name + " is eating.");


}


```
15


public void move() {


System.out.println(name + " is moving.");


}


}


```


class Sparrow extends Bird {


public Sparrow(String name) {


super (name);


}


```
5


public void move() {


System.out.println(name() + " is flying.");


}


}


```


class Penguin extends Bird {


public Penguin(String name) {


super (name);


}


```
5


public void move() {


System.out.println(name() + " is swimming gracefully.");


}


}


```

#### **5.3.4 Interface Segregation Principle**


_Make fine-grained interfaces that are client-specific._


   - Avoidance of _interface pollution_ and _fat_ interfaces.

    - Instead of interfaces with all possible methods that clients might need, there should be
separate interfaces that cover the specific needs of each client type.

    - Methods of individual interfaces should have low coupling.

    - Clients should not depend on methods that are not needed at all.

    - Distribution should be made according to the requirements of the clients for the interfaces.

    - Clients must therefore only operate with interfaces that can only do what they need.

    - Enables software to be divided into decoupled and therefore flexible classes.

    - Future professional or technical requirements therefore only require minor changes to the
software itself.


**5.3.4.1 Example:** **[MultiFunctionDevice](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/isp/printer)**


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-159-0.png)


The noncompliant code example violates the Interface Segregation Principle (ISP). It defines a single interface MultiFunctionDevice that includes methods for printing, faxing, and scanning. This design forces classes that implement this interface to implement all the methods, even if they don’t support or need all the functionalities. This leads to unnecessary dependencies and potential issues if a client tries to call unsupported methods.


```


public interface MultiFunctionDevice {


void print();


void fax();


void scan();


}


```
The AllInOnePrinter class implements this interface and provides implementations for all the methods.


```


class AllInOnePrinter implements MultiFunctionDevice {


public void print() {


}


```
5


public void fax() {


}


```
9


public void scan() {


}


}


```
The InkjetPrinter class also implements the MultiFunctionDevice interface but throws an UnsupportedOperationException for the fax() and scan() methods since it doesn’t support those functionalities.


1 **class InkjetPrinter implements** MultiFunctionDevice {


2


public void print() {


}


```
6


public void fax() {


throw new UnsupportedOperationException();


}


```
11


public void scan() {


throw new UnsupportedOperationException();


}


}


```
This design violates the ISP because it forces all classes implementing MultiFunctionDevice to
provide implementations for all three methods, even if some methods are not applicable to them.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-160-0.png)


The compliant solution resolves the violation by defining separate interfaces for each functionality: Printer, Fax, and Scanner .


```


public interface Printer {


void print();


}


```


public interface Fax {


void fax();


}


```


public interface Scanner {


void scan();


}


```
The InkjetPrinter class now only implements the Printer interface, as it doesn’t support faxing or
scanning.


1 **class InkjetPrinter implements** Printer {


2


public void print() {


}


}


```
The AllInOnePrinter class implements all three interfaces, Printer, Fax, and Scanner, as it supports all the functionalities.


1 **class AllInOnePrinter implements** Printer, Fax, Scanner {


2


public void print() {


}


```
6


public void fax() {


}


```
10


public void scan() {


}


}


```
By splitting the functionalities into separate interfaces, each class can implement only the
interfaces relevant to their capabilities. This adheres to the ISP, as clients can depend on the
specific interfaces they require, reducing unnecessary dependencies and potential issues with
unsupported functionalities.


3.4.2 Example: [TechEmployee](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/isp/techemployee)


```


The noncompliant code example violates the Interface Segregation Principle (ISP). It defines a single interface TechEmployee that includes methods for developing, testing, and designing. This design forces classes that implement this interface to implement all the methods, even if they don’t support or need all the functionalities. This leads to unnecessary dependencies and potential issues if a client tries to call unsupported methods.


```

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-162-0.png)


public interface TechEmployee {


void develop();


void test();


void design();


}


```
The JavaDeveloper class implements this interface and provides an implementation for the develop() method but throws an UnsupportedOperationException for the test() and design() methods since it doesn’t support those functionalities.


```


1 **public class JavaDeveloper implements** TechEmployee {


2


public void develop() {


System.out.println("Yes, I love it!");


}


```
7


public void test() {


throw new UnsupportedOperationException();


}


```
12


public void design() {


throw new UnsupportedOperationException();


}


}


```
The Tester class implements the TechEmployee interface and provides an implementation for the

test() method but throws an UnsupportedOperationException for the develop() and design() methods.


```


1 **public class Tester implements** TechEmployee {


2


public void develop() {


throw new UnsupportedOperationException();


}


```
7


public void test() {


System.out.println("Yes, I love it!");


}


```
12


public void design() {


throw new UnsupportedOperationException();


}


}


```
The Designer class implements the TechEmployee interface and provides an implementation for the

design() method but throws an UnsupportedOperationException for the develop() and test() methods.


1 **public class Designer implements** TechEmployee {


2


public void develop() {


throw new UnsupportedOperationException();


}


```
7


public void test() {


throw new UnsupportedOperationException();


}


```
12


public void design() {


System.out.println("Yes, I love it!");


}


}


```
This design violates the ISP because it forces all classes implementing TechEmployee to provide
implementations for all three methods, even if some methods are not applicable to them.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-164-0.png)


The compliant solution resolves the violation by defining separate interfaces for each functionality: Developer, Tester, and Designer‘.


public interface Developer {


void develop();


}


```


public interface Designer {


void design();


}


```


public interface Tester {


void test();


}


```
The JavaDeveloper class now only implements the Developer interface, as it only supports develop ment.


1 **public class JavaDeveloper implements** Developer {


2


public void develop() {


System.out.println("Yes, I love it!");


}


}


```
The SecurityTester class now only implements the Tester interface, as it only supports testing.


1 **public class SecurityTester implements** Tester {


2


public void test() {


System.out.println("Yes, I love it!");


}


}


```
The UIDesigner class now only implements the Designer interface, as it only supports designing.


1 **public class UIDesigner implements** Designer {


2


public void design() {


System.out.println("Yes, I love it!");


}


}


```
By splitting the functionalities into separate interfaces, each class can implement only the
interfaces relevant to their capabilities.


3.4.3 Example: [StockOrder](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/isp/stockorder)


```


The noncompliant code example violates the Interface Segregation Principle (ISP) by defining a single interface StockOrder that includes multiple methods with different combinations of parameters. This design forces the clients to implement and provide unnecessary parameter combinations, leading to a bloated interface and potential confusion.


```


public interface StockOrder {


2


void sell(StockId id, double price, int quantity);


4


void buy(StockId id, int quantity, double price);


6


void buy(StockId id, int quantity, double price, double commission);


8


void buy(StockId id, int quantity, double minPrice, double maxPrice, double commission);


10


}


```


The compliant solution resolves the violation by redefining the StockOrder interface with two methods: sell() and buy(), both using a Price object to represent the price parameter.


public interface StockOrder {


2


void sell(StockId id, int quantity, Price price);


4


void buy(StockId id, int quantity, Price price);


6


}


The Price object encapsulates the price value, ensuring a consistent representation and avoiding the need for multiple method signatures with different parameter combinations.


By using the Price object, both the sell() and buy() methods can accept the same type of parameter for price, simplifying the interface and avoiding the need for overloaded methods.


Clients implementing the StockOrder interface can now provide the necessary parameters without being burdened with unnecessary combinations, improving code clarity and reducing the chances of errors.


```
#### **5.3.5 Dependency Inversion Principle**


_Depend on abstractions, not on concretions._


  - Dependencies should always be directed from more concrete modules of **lower** levels to
abstract modules of **higher** levels.

  - No class should instantiate foreign classes but should receive them as abstractions (e.g.
interfaces) in the form of a parameter.

  - This ensures that the dependency relationships always run in one direction, from the
concrete to the abstract modules, from the derived classes to the base classes.

  - :
**Hollywood principle** _Don’t call us, we’ll call you!_

  - Dependencies between objects are controlled from the outside.


#### –
Reduction of dependencies between the modules

#### –
Avoidance of cyclical dependencies

#### –
Decoupling of the component from their environment

#### –
Reduction of the necessary knowledge

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-167-0.png)


**5.3.5.1 Example:** **[UserService](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/dip/userservice)**


The noncompliant code example violates the Dependency Inversion Principle (DIP) by directly
creating an instance of UserRepository within the UserService class. This creates a tight coupling
between the UserService and the concrete implementation of UserRepository, making it difficult to
change or extend the behavior of UserService in the future.


```


public class UserService {


private final UserRepository userRepository;


3


```
public UserService() {


this .userRepository = new UserRepositoryHibernate();


}


}


```
This design violates the DIP because it directly depends on the concrete implementation

UserRepositoryHibernate . This tight coupling makes it challenging to substitute or change the implementation of UserRepository without modifying the UserService class.


In the compliant solution, the UserService class no longer creates an instance of UserRepository directly. Instead, the UserRepository is passed as a constructor parameter, allowing the dependency to be injected from the outside.


By using constructor injection, the UserService class becomes decoupled from the specific implementation of UserRepository . This enables flexibility and modularity, as different implementations of UserRepository can be easily provided to the UserService class without requiring any modifications to its code.


```


public class UserService {


private final UserRepository userRepository;


3


```
public UserService(UserRepository userRepository) {


this .userRepository = userRepository;


}


}


```


**5.3.5.2 Example:** **[Logger](https://github.com/mnhock/swcs/tree/master/swcs-dp/src/main/java/swcs/dp/dip/logger)**


The noncompliant code example violates the Dependency Inversion Principle (DIP) by directly
creating an instance of FileSystem within the Logger class. This creates a tight coupling between
the Logger and the concrete implementation of FileSystem, making it difficult to change or extend
the logging mechanism in the future.


```


class Logger {


private final FileSystem fileSystem;


3


```
Logger() {


this .fileSystem = new FileSystem();


}


```
7


public void log(String message) {


this .fileSystem.log(message);


}


}


```
This design violates the DIP because it directly depends on the concrete implementation

FileSystem . This tight coupling makes it challenging to substitute or change the logging mechanism
without modifying the Logger class.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-169-0.png)


The Loggable interface defines a contract for classes that can log messages.


public interface Loggable {


void log(String message);


}


```
FileSystem and Database, that implement the Loggable interface encapsulate the specific logging mechanisms, such as writing to a file or a database.


```


class FileSystem implements Loggable {


public void log(String message) {


_// file handling and writing_


}


}


```


class Database implements Loggable {


public void log(String message) {


_// writing in database_


}


}


```
The Logger class is modified to accept an instance of Loggable through constructor injection.


class Logger {


private final Loggable loggable;


3


```
Logger(Loggable loggable) {


this .loggable = loggable;


}


```
7


public void log(String message) {


this .loggable.log(message);


}


}


```
By depending on the Loggable interface instead of a concrete implementation, the Logger class becomes decoupled from the specific logging mechanism. This allows different logging implementations to be provided at runtime without modifying the Logger class.


Logger fsLogger = new Logger( new FileSystem());


Logger dbLogger = new Logger( new Database());


```
5


fsLogger.log("some text");


dbLogger.log("other text");


}


}


```
