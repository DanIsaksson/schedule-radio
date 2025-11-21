---
id: kb-design-principles-011
title: "2 INSTANCE – Overview"
domain: design-principles
parent_heading: "**6. Design Patterns of the Gang of** **Four**"
intent: "2 INSTANCE – Overview: 3 } **Easy to use.** A singleton class is written quickly and easily. - Guarantee that only **one** instance exists in a JVM at a time."
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
source_lines:
  start: 12118
  end: 15118
---
## 2 INSTANCE


3 }


    **Easy to use.** A singleton class is written quickly and easily.

   - Guarantee that only **one** instance exists in a JVM at a time.

    - Compared to global variables there are several advantages:

– Access control. The Singleton encapsulates its creation and can therefore control
exactly when and how access to the Singleton is allowed.
– Lazy-loading. Singletons can only be created when they are needed.


```


    - **Problematic destruction.** To destroy objects in languages with garbage collection, an
object must no longer be referenced. This is difficult to ensure with singletons. Due to the global availability, it happens very quickly that code parts still hold a reference to the singleton.

```
    - Especially in multi-user applications, a Singleton can reduce the **performance** because it
represents a bottleneck - especially in the synchronized form.

    Uniqueness beyond physical limits. Ensuring the uniqueness of a Singleton across
physical boundaries (JVM) is difficult.


² Bloch, Joshua: Effective Java – Third Edition, Item 3


```
#### **6.1.2 Builder**


_Separate the construction of a complex object from its representation so that the same_ _construction process can create different representations._


The Builder design pattern allows the creation of complex objects step by step. It separates the construction of an object from its representation, enabling the same construction process to create various representations of the object.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-190-0.png)

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-190-1.png)


1.2.1 Example: [MealBuilder](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/builder/meal)


```
#### MealBuilder

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-191-0.png)


public abstract class MealBuilder {


private final Meal meal;


3


```
protected MealBuilder() {


this .meal = new Meal();


}


```
7


public abstract void buildDrink();


public abstract void buildMain();


public abstract void buildDessert();


```
11


public Meal meal() {


return this .meal;


}


}


```
#### KidsMealBuilder


public class KidsMealBuilder extends MealBuilder {


public void buildDrink() {


_// add drinks to the meal_


}


```
5


public void buildMain() {


_// add main part of the meal_


}


```
9


public void buildDessert() {


_// add dessert part to the meal_


}


}


```


#### AdultMealBuilder


public class AdultMealBuilder extends MealBuilder {


public void buildDrink() {


_// add drinks to the meal_


}


```
5


public void buildMain() {


_// add main part of the meal_


}


```
9


public void buildDessert() {


_// add dessert part to the meal_


}


}


```
#### MealDirector


1 **public class MealDirector** {


2


public Meal createMeal(MealBuilder builder) {


builder.buildDrink();


builder.buildMain();


builder.buildDessert();


```
7


return builder.meal();


}


}


```


2


MealDirector director = new MealDirector();


MealBuilder builder = null ;


boolean isKid = true ;


```
7


if (isKid) {


builder = new KidsMealBuilder();


builder = new AdultMealBuilder();


}


```
13


Meal meal = director.createMeal(builder);


}


}


```


**6.1.2.2 Example:** **[PizzaBuilder](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/builder/pizza)**


#### PizzaBuilder

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-193-0.png)


abstract class PizzaBuilder {


private final Pizza pizza;


3


```
PizzaBuilder() {


this .pizza = new Pizza();


}


```
7


public Pizza pizza() {


return this .pizza;


}


```
11


public abstract void buildDough();


13


public abstract void buildSauce();


15


public abstract void buildTopping();


}


```
#### HawaiianPizzaBuilder


1 **class HawaiianPizzaBuilder extends** PizzaBuilder {


2


public void buildDough() {


this .pizza().dough("cross");


}


```
7


public void buildSauce() {


this .pizza().sauce("mild");


}


```
12


13 @Override


14 **public void** buildTopping() {


this .pizza().topping("ham+pineapple");


}


}


```
#### MargheritaPizzaBuilder


1 **class MargheritaPizzaBuilder extends** PizzaBuilder {


2


public void buildDough() {


this .pizza().dough("cross");


}


```
7


public void buildSauce() {


this .pizza().sauce("mild");


}


```
12


public void buildTopping() {


}


}


```
#### Pizza


final class Pizza {


private String dough;


private String sauce;


private String topping;


```
5


public void dough(String dough) {


this .dough = dough;


}


```
9


public void sauce(String sauce) {


this .sauce = sauce;


}


```
13


public void topping(String topping) {


this .topping = topping;


}


}


```


#### Luigi


class Luigi {


private final PizzaBuilder pizzaBuilder;


3


```
Luigi(PizzaBuilder pizzaBuilder) {


this .pizzaBuilder = pizzaBuilder;


}


```
7


public Pizza bakePizza() {


this .pizzaBuilder.buildDough();


this .pizzaBuilder.buildSauce();


this .pizzaBuilder.buildTopping();


```
12


return this .pizzaBuilder.pizza();


}


}


```


2


Luigi luigi = new Luigi( new HawaiianPizzaBuilder());


Pizza pizza = luigi.bakePizza();


}


}


```
**6.1.2.3 Example:** **[Email](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/builder/email)**


#### Email


1 **public final class Email** {


2


private final String subject;


private final String message;


private final String recipients;


```
6


private Email(String subject, String message, String recipients) {


this .subject = subject;


this .message = message;


this .recipients = recipients;


}


```
12


public String subject() {


return this .subject;


}


```
16


public String message() {


return this .message;


}


```
20


public String recipients() {


return this .recipients;


}


```
24


public void send() {


}


27


```
public static Builder builder() {


return new Builder();


}


```
31


32 **public static final class Builder** {


33


private String subject;


private String message;


private String signature;


private final Set<String> recipients;


```
38


public Builder() {


this .recipients = new HashSet<>();


}


```
42


public Builder withSubject(String subject) {


this .subject = subject;


}


```
47


public Builder withMessage(String message) {


this .message = message;


}


```
52


public Builder withSignature(String signature) {


this .signature = signature;


}


```
57


public Builder addRecipient(String recipient) {


this .recipients.add(recipient);


}


```
62


public Builder removeRecipient(String recipient) {


this .recipients.remove(recipient);


}


```
67


public Email build() {


return new Email( this .subject,


String.format("%s%n%s", this .message, this .signature),


String.join(".", this .recipients));


}


}


}


```


2


Email email = Email.builder()


.addRecipient("bad@foo.com")


.addRecipient("coder@foo.com")


.withMessage("Your first Builder Pattern")


.withSignature("Clean Coder")


.build();


```
10


email.send();


}


}


```
**6.1.2.4 Example:** **[ImmutablePerson](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/builder/person)**


#### ImmutablePerson


1 **public final class ImmutablePerson** {


2


private final String name;


private final String city;


private final List<String> favoriteDishes;


```
6


private ImmutablePerson(Builder builder) {


this .name = builder.name;


this .city = builder.city;


this .favoriteDishes = builder.favoriteDishes;


}


```
12


}


```
16


public String city() {


return this .city;


}


```
20


public List<String> favoriteDishes() {


return this .favoriteDishes != null ? new ArrayList<>( this .favoriteDishes) : null ;


}


```
24


public Builder toBuilder() {


return new Builder( this );


}


```
28


public static Builder builder() {


return new Builder();


}


```
32


33 **public static final class Builder** {


34


private String name;


private String city;


private final List<String> favoriteDishes;


```


38


public Builder() {


this .favoriteDishes = new ArrayList<>();


}


```
42


public Builder(ImmutablePerson person) {


this .name = person.name;


this .city = person.city;


this .favoriteDishes = person.favoriteDishes != null


? new ArrayList<>(person.favoriteDishes) : null ;


}


```
49


public Builder withName(String name) {


}


```
54


public Builder withCity(String city) {


this .city = city;


}


```
59


public Builder addFavoriteDish(String dish) {


this .favoriteDishes.add(dish);


}


```
64


public Builder removeFavoriteDish(String dish) {


this .favoriteDishes.remove(dish);


}


```
69


public ImmutablePerson build() {


return new ImmutablePerson( this );


}


}


}


```


2


ImmutablePerson hugo = ImmutablePerson.builder()


.withName("Hugo")


.withCity("Nuremberg")


.addFavoriteDish("Pasta")


.addFavoriteDish("Pancake")


.build();


```
10


ImmutablePerson mia = ImmutablePerson.builder()


.withName("Mia")


.withCity("Nuremberg")


.addFavoriteDish("Sausage")


.build();


```
16


ImmutablePerson hugoAfterMove = hugo.toBuilder()


withCity("Munich")


build();


20


```
ImmutablePerson miaAfterMove = mia.toBuilder()


.withCity("Munich")


.build();


}


}


```


    **Different expressions** of complex products can be built.

    - Further expressions can be easily added with **new concrete builder classes** .

    - The logic for the construction is **separated** .

    - A client does not need to know anything about the **generation process** .

    Fine-grained control over the generation process is possible. The product is built piece
by piece under the director’s control. Other generation patterns build the products in one
piece.


```
#### **6.1.3 Factory Method**


_Define an interface for creating an object, but let the subclasses decide which class to_ _instantiate. Factory Method lets a class defer instantiation to subclasses._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-200-0.png)


    - Separation of object processing ( **How?** ) from concrete object creation ( **What?** ).

    - Delegation of object instantiation to subclass.

   - Cases in which an increasing number and shape of products can be expected. As well as
scenarios in which all products have to go through a general manufacturing process, no matter what kind of product they are.

    - If the products to be created are not known or should not be defined from the beginning.


**6.1.3.1 Example:** **[Logger](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/factorymethod/logger)**


#### Logger

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-200-1.png)


public interface Logger {


void log(String message);


String name();


}


```


#### ConsoleLogger


1 **public class ConsoleLogger implements** Logger {


2


public void log(String message) {


System.err.println(message);


}


```
7


return getClass().getSimpleName();


}


}


```
#### AbstractLoggerCreator


1 **public abstract class AbstractLoggerCreator** {


2


public Logger logger() {


_// depending on the subclass, we'll get a particular logger._


Logger logger = createLogger();


```
6


_// could do other operations on the logger here_


logger.log(String.format("Logger %s are used.", logger.name()));


9


return logger;


}


12


protected abstract Logger createLogger();


}


```
#### ConsoleLoggerCreator


1 **public class ConsoleLoggerCreator extends** AbstractLoggerCreator {


2


public Logger createLogger() {


return new ConsoleLogger();


}


}


```


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-202-0.png)


2


```


AbstractLoggerCreator creator = new ConsoleLoggerCreator();


Logger logger = creator.logger();


logger.log("message");


}


}


```
**6.1.3.2 Example:** **[Department](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/factorymethod/department)**


#### Employee


public interface Employee {


void paySalary();


void dismiss();


}


```
#### Programmer


public class Programmer implements Employee {


2


private final int id;


4


```
public Programmer( int id) {


this .id = id;


}


```
8


public void paySalary() {


}


```
12


public void dismiss() {


}


```
16


17 }


#### MarketingSpecialist


public class MarketingSpecialist implements Employee {


2


private final int id;


4


```
public MarketingSpecialist( int id) {


this .id = id;


}


```
8


public void paySalary() {


}


```
12


public void dismiss() {


}


}


```
#### Department


public abstract class Department {


2


protected abstract Employee createEmployee( int id);


4


public void fire( int id) {


Employee employee = createEmployee(id);


7


```
employee.paySalary();


employee.dismiss();


}


}


```
#### TechDepartment


1 **public class TechDepartment extends** Department {


2


public Employee createEmployee( int id) {


return new Programmer(id);


}


}


```


#### MarketingDepartment


1 **public class MarketingDepartment extends** Department {


2


public Employee createEmployee( int id) {


return new MarketingSpecialist(id);


}


}


```

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-204-0.png)


2


```


Department department = new TechDepartment();


department.fire(1);


}


}


```


   - Manufacturing process is **separated** from a concrete implementation.

   - Different product implementations can go through the **same** production process.

    - Reusability and consistency:


#### –
Encapsulation of the object creation code in its class. This creates a uniform and central
interface for the client. The product implementation is **decoupled** from its use.


    - If many simple objects are to be created, the effort is **disproportionately high**, because the
creator always has to be derived.


#### **6.1.4 Abstract Factory**


_Provide an interface for creating families of related or dependent objects without specifying_
_their concrete class._

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-205-0.png)


The Abstract Factory Design Pattern serves to define a coherent family of products.

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-205-1.png)


```


  - When different objects are created for a context and therefore always have to be created
coherently.

  - When a system must be configured with different sets of objects or should be able to do so.

  - When a system should be independent of how certain objects are created.

  - If a family of objects is provided but no statements can or should be made about the concrete
implementations. Instead, interfaces are provided.

  - Typical applications of abstract factors are e.g., GUI libraries that support different Look &
Feels.


1.4.1 Example: [Car](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/abstractfactory/car)


Product family Classes Luxury LuxuryCar, LuxurySUV Non luxury NonLuxuryCar, NonLuxurySUV


```
#### Car


public interface Car {


String name();


String features();


}


```
#### NonLuxuryCar

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-206-0.png)


public class NonLuxuryCar implements Car {


private final String name;


3


```
public NonLuxuryCar(String name) {


}


```
7


}


```
12


public String features() {


return "Non-Luxury Car Features ";


}


}


```


#### LuxuryCar


public class LuxuryCar implements Car {


private final String name;


3


```
public LuxuryCar(String name) {


}


```
7


}


```
12


public String features() {


return "Luxury Car Features ";


}


}


```
Product family Classes Luxury LuxuryCar, LuxurySUV Non luxury NonLuxuryCar, NonLuxurySUV


```
#### Car


public interface SUV {


String name();


String features();


}


```
#### NonLuxurySUV

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-207-0.png)


public class NonLuxurySUV implements SUV {


private final String name;


3


```
public NonLuxurySUV(String name) {


}


```
7


}


```


12


public String features() {


return "Non-Luxury SUV Features ";


}


}


```
#### LuxurySUV


public class LuxurySUV implements SUV {


private final String name;


3


```
public LuxurySUV(String name) {


}


```
7


}


```
12


public String features() {


return "Luxury SUV Features ";


}


}


```
#### VehicleFactory

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-208-0.png)


public abstract class VehicleFactory {


public static final String LUXURY_VEHICLE = "Luxury";


public static final String NON_LUXURY_VEHICLE = "Non-Luxury";


```
4


public abstract Car car();


public abstract SUV suv();


7


```
public static VehicleFactory vehicleFactory(String type) {


if (VehicleFactory.LUXURY_VEHICLE.equals(type)) {


return new LuxuryVehicleFactory();


}


if (VehicleFactory.NON_LUXURY_VEHICLE.equals(type)) {


return new NonLuxuryVehicleFactory();


}


```


15


return new LuxuryVehicleFactory();


}


}


```
#### NonLuxuryVehicleFactory


1 **public class NonLuxuryVehicleFactory extends** VehicleFactory {


2


public Car car() {


return new NonLuxuryCar("NL-C");


}


```
7


public SUV suv() {


return new NonLuxurySUV("NL-S");


}


}


```
#### LuxuryVehicleFactory


1 **public class LuxuryVehicleFactory extends** VehicleFactory {


2


public Car car() {


return new LuxuryCar("L-C");


}


```
7


public SUV suv() {


return new LuxurySUV("L-S");


}


}


```
![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-209-0.png)


2


VehicleFactory vf = VehicleFactory.vehicleFactory(VehicleFactory.LUXURY_VEHICLE);


5


Car car = vf.car();


System.out.println("Name: " + car.name() + " Features: " + car.features());


8


```
SUV suv = vf.suv();


System.out.println("Name: " + suv.name() + " Features: " + suv.features());


}


}


```


   - By **shielding the concrete classes**, the client code becomes generally valid. No code is
necessary for special cases.

Consistency . It is ensured that only those objects that fit together reach the client.

Flexibility . Entire object families can be exchanged since the client only relies on abstractions (Abstract Factory, product interfaces).

Simple extension with new product families . New product families can be integrated into the system very easily. All that is required is the reimplementation of the factory interface. Afterward, the new factory only needs to be instantiated at a central point in the client.


```


    Inflexibility regarding new family members . If a new product is to be added to the
product family, the interface of the Abstract Factory must be changed. However, this leads
to the breaking of the code of all concrete factors. The change effort is high.

```
    - A new implementation with great similarity to an existing one still requires a new factory.


#### **6.1.5 Prototype**


_Specify the kinds of objects to create using a prototypical instance and create new objects by_ _copying this prototype._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-211-0.png)


    - Creating a copy of a prototype object and making required modifications.


Java provides some ways to copy an object to create a new one. One is using the java.lang.Cloneable marker interface. You need to implement the java.lang.Cloneable interface and override the clone method which every object inherits from the Object class.


There are two categories regarding cloning, shallow and deep cloning .


A shallow copy copies all reference types or value types, but it does not copy the objects that the references refer to. The references in the new object point to the same objects that the references in the original object point to. The default clone method implementation in Java creates a clone as a shallow copy.


A deep copy copies the elements, and everything directly or indirectly referenced by the elements.


1.5.1 Example: [Person - Shallow copy](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/prototype/shallow/person)


```
#### Person


public class Person implements Cloneable {


2


private String name;


private Address address;


5


```
public Person(String name, Address address) {


this .address = address;


}


```
10


}


14


```
public void name(String name) {


}


```
18


public Address address() {


return this .address;


}


```
22


public void address(Address address) {


this .address = address;


}


```
26


public Person clone() {


try {


return (Person) super .clone();


} catch (CloneNotSupportedException e) {


throw new AssertionError();


}


}


}


```
#### Address


public class Address implements Cloneable {


2


private String city;


private String street;


5


```
public Address(String city, String street) {


this .city = city;


this .street = street;


}


```
10


public String city() {


return this .city;


}


```
14


public void city(String city) {


this .city = city;


}


```
18


public String street() {


return this .street;


}


```
22


public void street(String street) {


this .street = street;


}


```
26


public Address clone() {


try {


return (Address) super .clone();


} catch (CloneNotSupportedException e) {


```


throw new AssertionError();


}


}


}


```


2


4


Person original = new Person("Original name", new Address("Original city", "Original street"));


print("Original", original);


7


```
_// Clone as a shallow copy_


Person clone = original.clone();


print("Clone", clone);


```
11


clone.name("Modified name");


clone.address().city("Modified city");


14


```
print("Clone after modification", clone);


print("Original after clone modification", original);


}


```
18


private static void print(String object, Person person) {


System.out.println(String.format("%s: %s, %s",


object,


person.name(),


person.address().city()));


}


}


```


1 Original: Original name, Original city


2 Clone: Original name, Original city


3 Clone after modification: Modified name, Modified city


4 Original after clone modification: Original name, Modified city


**6.1.5.2 Example:** **[Person - Deep copy](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/prototype/deep/person)**


#### Address


public class Address implements Cloneable {


2


private String city;


private String street;


5


```
public Address(String city, String street) {


this .city = city;


this .street = street;


}


```
10


public String city() {


return this .city;


}


```
14


public void city(String city) {


this .city = city;


}


```
18


public String street() {


return this .street;


}


```
22


public void street(String street) {


this .street = street;


}


```
26


public Address clone() {


try {


return (Address) super .clone();


} catch (CloneNotSupportedException e) {


throw new AssertionError();


}


}


}


```
#### Person


public class Person implements Cloneable {


2


private String name;


private Address address;


5


```
public Person(String name, Address address) {


this .address = address;


}


```
10


}


```
14


public void name(String name) {


}


18


```
public Address address() {


return this .address;


}


```
22


public void address(Address address) {


this .address = address;


}


```
26


public Person clone() {


try {


Person person = (Person) super .clone();


person.address(person.address.clone());


```
32


return person;


} catch (CloneNotSupportedException e) {


throw new AssertionError();


}


}


}


```


2


4


Person original = new Person("Original name", new Address("Original city", "Original street"));


print("Original", original);


7


```
_// Clone as a deep copy_


Person clone = original.clone();


print("Clone", clone);


```
11


clone.name("Modified name");


clone.address().city("Modified city");


14


```
print("Clone after modification", clone);


print("Original after clone modification", original);


}


```
18


private static void print(String object, Person person) {


System.out.printf("%s: %s, %s%n",


object,


person.name(),


person.address().city());


}


}


```


1 Original: Original name, Original city


2 Clone: Original name, Original city


3 Clone after modification: Modified name, Modified city


4 Original after clone modification: Original name, Original city


1.5.3 Example: [Person - Copy constructor / factory](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/creational/prototype/copy/person)


According to Effective Java clone and java.lang.Cloneable is complex and the rules for an overriding
clone are tricky and difficult to get right.


Object’s clone method is very tricky. It’s based on field copies, and it’s _extra-linguistic_ . It creates
an object without calling a constructor. There are no guarantees that it preserves the invariants
established by the constructors. If that state is mutable, you don’t have two independent objects.
If you modify one, the other changes as well. And suddenly, you get random behavior. A better
approach is providing a _copy constructor_ or _copy factory_ . ³


```
#### Person


public class Person {


2


private String name;


private Address address;


5


```
_// Copy constructor_


public Person(Person person) {


this (person.name(), new Address(person.address()));


}


```
10


public Person(String name, Address address) {


this .address = address;


}


```
15


}


```
19


public void name(String name) {


}


```
23


public Address address() {


return this .address;


}


```
27


public void address(Address address) {


this .address = address;


}


```
31


_// Copy factory_


public static Person newInstance(Person person) {


return new Person(person.name(), new Address(person.address()));


```
³ Bloch, Joshua: Effective Java – Third Edition, Item 13


35 }


36 }


#### Address


public class Address {


2


private String city;


private String street;


5


```
_// Copy constructor_


public Address(Address address) {


this (address.city(), address.street());


}


```
10


public Address(String city, String street) {


this .city = city;


this .street = street;


}


```
15


public String city() {


return this .city;


}


```
19


public void city(String city) {


this .city = city;


}


```
23


public String street() {


return this .street;


}


```
27


public void street(String street) {


this .street = street;


}


```
31


_// Copy factory_


public static Address newInstance(Address address) {


return new Address(address.city(), address.street());


}


}


```


2


4


Person original = new Person("Original name", new Address("Original city", "Original street"));


print("Original", original);


7


```
_// Clone as a deep copy with copy constructor_


Person clone = new Person(original);


_// Alternative as a deep copy with copy factory_


_// Person clone = Person.newInstance(original);_


print("Clone", clone);


```


13


clone.name("Modified name");


clone.address().city("Modified city");


16


```
print("Clone after modification", clone);


print("Original after clone modification", original);


}


```
20


private static void print(String object, Person person) {


System.out.printf("%s: %s, %s%n",


object,


person.name(),


person.address().city());


}


}


```


1 Original: Original name, Original city


2 Clone: Original name, Original city


3 Clone after modification: Modified name, Modified city


4 Original after clone modification: Original name, Original city
