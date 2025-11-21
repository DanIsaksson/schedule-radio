---
id: kb-design-principles-012
title: "2 INSTANCE – **6.2 Structural**"
domain: design-principles
parent_heading: "**6. Design Patterns of the Gang of** **Four**"
intent: "2 INSTANCE – **6.2 Structural**: _Provide a unified interface to a set of interfaces in a subsystem. Facade defines a higher-level_ _interface that makes the subsystem easier to use._ ![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-219-0.png) ``` - Division of a complex system into several simple subsystems."
tags:
  - architecture
  - classes
  - clean-code
  - example
  - formatting
  - functions
  - principles
  - process
source_lines:
  start: 12118
  end: 15641
---
## 2 INSTANCE
### **6.2 Structural**

#### **6.2.1 Facade**


_Provide a unified interface to a set of interfaces in a subsystem. Facade defines a higher-level_
_interface that makes the subsystem easier to use._

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-219-0.png)


```


  - Division of a complex system into several simple subsystems.

  - Facade promotes loose coupling.

  - Subdivision of a system into layers and the communication of these via facades.

  - Reduction of dependencies between the client and the subsystem used.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-219-1.png)


2.1.1 Example: [Travel](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/facade/travel)


```
#### HotelBooker


public class HotelBooker {


public List<Hotel> hotels(LocalDate from, LocalDate to) {


_// returns hotels available in the particular date range_


}


}


```
#### FlightBooker


public class FlightBooker {


public List<Flight> flights(LocalDate from, LocalDate to) {


_// returns flights available in the particular date range_


}


}


```
#### TravelFacade


public class TravelFacade {


private HotelBooker hotelBooker;


private FlightBooker flightBooker;


```
4


public Travels availableTravels(LocalDate from, LocalData to) {


List<Flight> flights = flightBooker.flights(from, to);


List<Hotel> hotels = hotelBooker.hotels(from, to);


```
8


_// process_


}


}


```


TravelFacade facade = new TravelFacade();


facade.availableTravels(from, to);


}


}


```
**6.2.1.2 Example:** **[SmartHome](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/facade/smarthome)**


#### LightController


1 **class LightController** {


2


public void turnOnLights() {


System.out.println("Turning on lights");


}


```
6


public void turnOffLights() {


System.out.println("Turning off lights");


}


}


```
#### SecuritySystem


1 **class SecuritySystem** {


2


public void armSystem() {


System.out.println("Arming security system");


}


```
6


public void disarmSystem() {


System.out.println("Disarming security system");


}


}


```
#### ThermostatController


1 **class ThermostatController** {


2


public void setTemperature( int temperature) {


System.out.println("Setting temperature to " + temperature + " degrees Celsius");


}


}


```
#### SmartHomeFacade


1 **class SmartHomeFacade** {


2


private final LightController lightController;


private final ThermostatController thermostatController;


private final SecuritySystem securitySystem;


```
6


SmartHomeFacade() {


this .lightController = new LightController();


this .thermostatController = new ThermostatController();


this .securitySystem = new SecuritySystem();


}


```
12


public void leaveHome() {


this .lightController.turnOffLights();


this .thermostatController.setTemperature(18);


this .securitySystem.armSystem();


}


```
18


public void returnHome() {


this .securitySystem.disarmSystem();


this .lightController.turnOnLights();


this .thermostatController.setTemperature(22);


}


}


```


2


SmartHomeFacade smartHomeFacade = new SmartHomeFacade();


5


```
smartHomeFacade.leaveHome();


smartHomeFacade.returnHome();


}


}


```


Simplified interface . The client can use a complex system more easily by reducing the number of objects that the client needs to know.

Decoupling the client from the subsystem. Since the client only works against the facade, it is independent of changes in the subsystem.

```
    - **Maintenance** and modifications of the subsystem only mean changes within the system
and at most the facade implementation. The interface of the facade to the outside remains
unchanged.


```
#### **6.2.2 Decorator**


_Attach additional responsibilities to an object dynamically. Decorators provide a flexible_ _alternative to subclassing for extending functionality._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-223-0.png)


  - Alternative to static subclassing (inheritance) of objects

  - Dynamic adding or removing functionalities

  - Extension of final classes, which are therefore not inheritable

  - If functionality extension using static inheritance is impracticable


#### –
z. e.g., with an unmanageable number of classes, which would result if every possible combination of extensions were considered

  - Practical examples:

**–** Java IO API, BufferedInputStream extends the interface around a buffer

#### –
Different types of cars with different features (lowering, spoiler)

**–** Dish with various side dishes

#### –
Different types of a telephone (mobile phone, smartphone, classic telephone) with variable behavior (vibration, ringing, silent, lights)


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-223-1.png)


2.2.1 Example: [Message](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/structural/decorator/message)


```
#### Message


public interface Message {


void print(String text);


}


```
#### DefaultMessage


1 **public class DefaultMessage implements** Message {


2


public void print(String text) {


System.out.println(text);


}


}


```
#### TimeStampMessage


public class TimeStampMessage implements Message {


2


private final Message message;


4


```
public TimeStampMessage(Message message) {


this .message = message;


}


```
8


public void print(String text) {


this .message.print(String.format("%s %s", LocalDateTime.now(), text));


}


}


```
#### UpperCaseMessage


public class UpperCaseMessage implements Message {


2


private final Message message;


4


```
public UpperCaseMessage(Message message) {


this .message = message;


}


```
8


public void print(String text) {


this .message.print(text.toUpperCase());


}


}


```


2


new DefaultMessage().print("DefaultMessage");


new UpperCaseMessage( new DefaultMessage()).print("DefaultMessage");


new TimeStampMessage( new DefaultMessage()).print("DefaultMessage");


new UpperCaseMessage( new TimeStampMessage( new DefaultMessage())).print("DefaultMessage");


}


}


```


1 DefaultMessage
