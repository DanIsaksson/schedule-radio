---
id: kb-design-principles-015
title: "5 VP – Overview"
domain: design-principles
parent_heading: "**6. Design Patterns of the Gang of** **Four**"
intent: "5 VP – Overview: 6 Developer 7 Developer 8 Developer - Simple implementation of **nested** structures Elegant working with the tree structure. The client can easily traverse through the hierarchy, call operations, and manage them, i.e., add new elements and delete existing ones."
tags:
  - architecture
  - classes
  - clean-code
  - example
  - formatting
  - guideline
  - naming
  - principles
  - process
  - testing
source_lines:
  start: 16673
  end: 17984
---
## 5 VP


6 Developer


7 Developer


8 Developer


    - Simple implementation of **nested** structures

    Elegant working with the tree structure. The client can easily traverse through the
hierarchy, call operations, and manage them, i.e., add new elements and delete existing

ones.

    Flexibility and expandability . Easy addition of new elements (leafs or composites).


```


    - **Generalisation of the draft** . If a particular compound element is to have only a fixed
number of children or only certain children, this can only be checked at runtime.


#### **6.2.5 Bridge**


_Decouple an abstraction from its implementation so that the two can vary independently._

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-237-0.png)


    - Avoiding a permanent binding between an abstraction and its implementation.

    - Combining different abstractions and implementations and extending them independently.

    - Changes in the implementation of an abstraction should have no impact on clients.

    - Sharing an implementation among multiple objects.

    - Run-time binding of the implementation.


**6.2.5.1 Example:** **[Message](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/bridge/message)**


#### Message


public abstract class Message {


2


private final MessageSender messageSender;


4


```
protected Message(MessageSender messageSender) {


this .messageSender = messageSender;


}


```
8


public MessageSender messageSender() {


return this .messageSender;


}


```
12


abstract void send();


}


```
#### EmailMessage


1 **public class EmailMessage extends** Message {


2


public EmailMessage(MessageSender messageSender) {


super (messageSender);


}


```
6


public void send() {


messageSender().sendMessage();


}


}


```
#### SmsMessage


1 **public class SmsMessage extends** Message {


2


public SmsMessage(MessageSender messageSender) {


super (messageSender);


}


```
6


public void send() {


messageSender().sendMessage();


}


}


```
#### MessageSender


public interface MessageSender {


2


void sendMessage();


}


```
#### EmailMessageSender


1 **public class EmailMessageSender implements** MessageSender {


2


public void sendMessage() {


System.out.println("Sending email message...");


}


}


```


#### SmsMessageSender


1 **public class SmsMessageSender implements** MessageSender {


2


public void sendMessage() {


System.out.println("Sending sms message...");


}


}


```


2


sendSms();


sendEmail();


}


```
7


private static void sendSms() {


MessageSender sender = new SmsMessageSender();


Message message = new SmsMessage(sender);


message.send();


}


```
13


private static void sendEmail() {


MessageSender sender = new EmailMessageSender();


Message message = new EmailMessage(sender);


message.send();


}


}


```


**6.2.5.2 Example:** **[Television](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/bridge/television)**


#### Font


public interface Television {


2


void on();


4


void off();


6


void channel( int channel);


}


```
#### Samsung


1 **public class Samsung implements** Television {


2


public void on() {


System.out.println("On");


}


```
7


public void off() {


System.out.println("Off");


}


```
12


public void channel( int channel) {


System.out.println(channel);


}


}


```
#### Philips


1 **public class Philips implements** Television {


2


public void on() {


System.out.println("On");


}


```
7


public void off() {


System.out.println("Off");


}


```
12


public void channel( int channel) {


System.out.println(channel);


}


}


```


#### RemoteControl


public abstract class RemoteControl {


2


private final Television television;


4


```
protected RemoteControl(Television television) {


this .television = television;


}


```
8


public void on() {


this .television.on();


}


```
12


public void off() {


this .television.off();


}


```
16


public void channel( int channel) {


this .television.channel(channel);


}


}


```
#### SmartRemoteControl


public class SmartRemoteControl extends RemoteControl {


2


private int channel;


4


```
public SmartRemoteControl(Television television) {


super (television);


}


```
8


public void nextChannel() {


this .channel++;


channel( this .channel);


}


```
13


public void previousChannel() {


this .channel--;


channel( this .channel);


}


}


```


2


useSamsung();


usePhilips();


}


```
7


private static void useSamsung() {


Television television = new Samsung();


SmartRemoteControl remoteControl = new SmartRemoteControl(television);


```


11


remoteControl.on();


remoteControl.nextChannel();


remoteControl.nextChannel();


remoteControl.previousChannel();


remoteControl.off();


}


```
18


private static void usePhilips() {


Television television = new Philips();


SmartRemoteControl remoteControl = new SmartRemoteControl(television);


```
22


remoteControl.on();


remoteControl.nextChannel();


remoteControl.nextChannel();


remoteControl.previousChannel();


remoteControl.off();


}


}


```


    - Abstraction and implementation are **decoupled** and **independent** .

   - Improved **extensibility**, you can extend the abstraction and implementation hierarchies
independently.

    - The implementation is also changed **dynamically at run time** .

    **Hiding** of implementation details from the client.


#### **6.2.6 Flyweight**


_Use sharing to support large numbers of fine-grained objects efficiently._

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-243-0.png)


  - Need to create **large** number of objects when memory cost is a constraint.

  - When many of the object attributes can be made **external and shared** .

  - Avoid unnecessary object **initialization** .

  - Where the **identity** of the object is not a matter of concern and the required operations can
be performed on the shared objects.


As defined by GoF, an object can have two states, the intrinsic and the extrinsic state:


#### Intrinsic state


Stored in the flyweight; it consists of information that’s independent of the flyweights
context, thereby making it shareable.

#### Extrinsic state


Depends on and varies with the flyweight’s context and therefore cannot be shared. Client
objects are responsible for passing extrinsic state to the flyweight if necessary.


2.6.1 Example: [Font](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/flyweight/font)


```
#### Font


public record Font(String name, int size) {


2


}


```
#### FontFactory


public final class FontFactory {


2


private final Set<Font> fonts = new HashSet<>();


4


```
public Font of(String name, int size) {


for (Font font : this .fonts) {


if (font.name().equals(name) && font.size() == size) {


return font;


}


}


```
11


Font font = new Font(name, size);


this .fonts.add(font);


14


```
return font;


}


}


```


2


FontFactory factory = new FontFactory();


5


System.out.println(factory.of("Helvetica", 12));


System.out.println(factory.of("Arial ", 10));


8


```
_// Will return same objects_


System.out.println(factory.of("Helvetica", 12));


System.out.println(factory.of("Arial ", 10));


}


}


```


**6.2.6.2 Example:** **[City](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/flyweight/city)**


#### Person


public record Person(String firstName, String lastName, String street, City city) {


2


}


```
#### City


public record City(String country, String countryCode, String name) {


2


}


```
#### CityFactory


public final class CityFactory {


2


private final Set<City> cities = new HashSet<>();


4


```
public City of(String country, String countryCode, String name) {


for (City city : this .cities) {


if (city.country().equals(country)


&& city.countryCode().equals(countryCode)


&& city.name().equals(name)) {


return city;


}


}


```
13


City city = new City(country, countryCode, name);


this .cities.add(city);


16


```
return city;


}


}


```
#### Employee


2


CityFactory factory = new CityFactory();


5


```
_// Will use same city instance_


new Person("Forster", "Gamlen", "7654 Ruskin Center",


factory.of("United States", "US", "Carson City"));


new Person("Nevin", "Roddell", "5 Del Sol Alley",


factory.of("United States", "US", "Carson City"));


new Person("Hunter", "Lewins", "33733 Heffernan Circle",


factory.of("United States", "US", "Carson City"));


```
13


new Person("Reube", "Gregoretti", "08 Melrose Street",


factory.of("Japan", "JP", "Mutsu"));


}


}


```


  - Improves the performance by **reducing** the number of objects.

**Reduces the memory** footprint as the common properties are shared between objects using intrinsic attributes.


  - If there are **no shareable attributes** in an object, the pattern is of no use.

  - May introduce **run-time costs** associated with transferring, finding, and/or computing
extrinsic state, especially if it was formerly stored as an intrinsic state.


#### **6.2.7 Proxy**


_Provide a surrogate or placeholder for another object to control access to it._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-247-0.png)


    - Proxies are often used in situations where a more complex reference to an object is required
instead of a normal pointer. A distinction is usually made between the following types of proxies:

– Remote proxy provides a local representation of another remote object or resource. – Virtual proxy wrap expensive objects and loads them on-demand. – Protection proxy provides access control to an original object. – Smart reference executes additional operations when an object is accessed.


2.7.1 Example: [Spaceship](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/proxy/spaceship)


```
#### Spaceship


interface Spaceship {


void fly();


}


```
#### Pilot


public record Pilot(String name) {


2


}


```
#### MillenniumFalcon


1 **public final class MillenniumFalcon implements** Spaceship {


2


public void fly() {


System.out.println("Welcome, Han!");


}


}


```
#### MillenniumFalconProxy


public final class MillenniumFalconProxy implements Spaceship {


2


private final Pilot pilot;


private final Spaceship ship;


5


```
public MillenniumFalconProxy(Pilot pilot) {


this .pilot = pilot;


this .ship = new MillenniumFalcon();


}


```
10


public void fly() {


if ("Han Solo".equals( this .pilot.name())) {


this .ship.fly();


System.out.printf("Sorry %s, only Han Solo can fly the Falcon!%n", this .pilot.name());


}


}


}


```


2


Spaceship ship = new MillenniumFalconProxy( new Pilot("Han Solo"));


ship.fly();


```
6


ship = new MillenniumFalconProxy( new Pilot("Darth Vader"));


ship.fly();


}


}


```


**6.2.7.2 Example:** **[ImageViewer](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/proxy/imageviewer)**


#### ImageViewer


interface ImageViewer {


void display();


}


```
#### DefaultImageViewer


public final class DefaultImageViewer implements ImageViewer {


2


private final Image image;


4


```
public DefaultImageViewer(String path) {


_// Slow operation_


this .image = Image.load(path);


}


```
9


public void display() {


_// Slow operation_


this .image.display();


}


}


```
#### Image


public interface Image {


2


void display();


4


```
static Image load(String path) {


_// Not implemented_


return null ;


}


}


```
#### ImageViewerProxy


public final class ImageViewerProxy implements ImageViewer {


2


private final String path;


private ImageViewer viewer;


5


```
public ImageViewerProxy(String path) {


this .path = path;


}


```
9


public void display() {


if ( this .viewer == null ) {


this .viewer = new DefaultImageViewer( this .path);


}


```
15


this .viewer.display();


}


}


```


2


ImageViewer beer = new ImageViewerProxy("./img/beer.png");


ImageViewer hammock = new ImageViewerProxy("./img/hammock.png");


```
6


beer.display();


beer.display(); _// DefaultImageViewer will be call and be executed once_


9


```
hammock.display();


}


}


```


**Security** : Certain conditions can be checked when accessing the object.

    - **Performance** : Objects with expensive operations demanding in terms of memory and
execution time, can be wrapped so that they are called only when they are needed, or unnecessary instantiation can be avoided.


    - **Performance** : This can also be a disadvantage of the proxy pattern if a proxy object is used
to wrap an object that exists somewhere on the network. Since it is a proxy, it can hide from the client the fact that it is a remote communication.

    - Add another layer of abstraction, which can lead to detours and increase complexity.
