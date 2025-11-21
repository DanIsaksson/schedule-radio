---
id: kb-design-principles-016
title: "5 VP – **6.3 Behavioural**"
domain: design-principles
parent_heading: "**6. Design Patterns of the Gang of** **Four**"
intent: "5 VP – **6.3 Behavioural**: _Allow an object to alter its behavior when its internal state changes. The object will appear to_ _change its class._ ![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-251-0.png) - An object should change its external behavior at runtime depending on its state."
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
  start: 16673
  end: 18859
---
## 5 VP
### **6.3 Behavioural**

#### **6.3.1 State**


_Allow an object to alter its behavior when its internal state changes. The object will appear to_ _change its class._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-251-0.png)


  - An object should change its external behavior at runtime depending on its state.

**– State machines** e.g., parser. The functionality of many text parsers is inevitably statebased. So, a compiler parsing source code must interpret a character depending on the characters read before.

#### –
Representation of states of network connections


3.1.1 Example: [MP3Player](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/state/mp3player)


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-251-1.png)


```
#### MP3Player


public class MP3Player {


private State state;


3


```
private MP3Player(State state) {


this .state = state;


}


public void play() {


state.pressPlay( this );


}


public void state(State state) {


this .state = state;


}


public State state() {


return state;


}


}


```
#### State


public interface State {


void pressPlay(MP3Player player);


}


```
#### PlayingState


public class PlayingState implements State {


public void pressPlay(MP3Player player) {


player.state( new StandByState());


}


}


```
#### StandByState


public class StandByState implements State {


public void pressPlay(MP3Player player) {


player.state( new PlayingState());


}


}


```


MP3Player player = new MP3Player( new PlayingState());


player.play();


player.play();


player.play();


player.play();


player.play();


player.play();


}


}


```


1 Play state


2 Standby state


3 Play state


4 Standby state


5 Play state


6 Standby state


7 Play state


**6.3.1.2 Example:** **[Door](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/state/door)**


#### Door

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-254-0.png)


public class Door {


2


private DoorState state;


4


```
public Door(DoorState state) {


this .state = state;


}


```
8


public void handle() {


this .state.handle( this );


}


```
12


public void state(DoorState state) {


this .state = state;


}


```
16


public DoorState state() {


return this .state;


}


}


```
#### DoorState


public interface DoorState {


void handle(Door door);


}


```
#### DoorClosed


1 **public class DoorClosed implements** DoorState {


2


public DoorClosed() {


System.out.println("Door closed");


}


```
6


public void handle(Door door) {


door.state( new DoorOpening());


}


}


```


#### DoorClosing


1 **public class DoorClosing implements** DoorState {


2


public DoorClosing() {


System.out.println("Door closing");


}


```
6


public void handle(Door door) {


door.state( new DoorClosed());


}


}


```
#### DoorOpen


1 **public class DoorOpen implements** DoorState {


2


public DoorOpen() {


System.out.println("Door open");


}


```
6


public void handle(Door door) {


door.state( new DoorClosing());


}


}


```
#### DoorOpening


1 **public class DoorOpening implements** DoorState {


2


public DoorOpening() {


System.out.println("Door opening");


}


```
6


public void handle(Door door) {


door.state( new DoorOpen());


}


}


```


Door door = new Door( new DoorOpen());


door.handle();


door.handle();


door.handle();


door.handle();


door.handle();


door.handle();


}


}


```


1 Door open


2 Door closing


3 Door closed


4 Door opening


5 Door open


6 Door closing


7 Door closed


Extensibility and change stability . Easy integration of new states without having to change existing code. Changes to state-dependent behavior only affect a state class and not the context.

Comprehensibility . High cohesion and delegation of responsibilities (the behavior of a state is encapsulated/localized in the corresponding state object itself) make the code easy to understand.

Explicit state transitions . By introducing objects for each state, the state transition becomes explicit. In addition, inconsistent states are prevented, because a state change is an atomic command (setting a variable).

```
   - The design is **less error-prone** concerning later changes than broad, repetitive if-else

constructs.


    - **Increased number of classes** . Is less compact than a single class. However, the aim of the
design pattern is precisely to distribute the behavior of a single confusing class to several
state objects.


```
#### **6.3.2 Template Method**


_Define a skeleton of an algorithm in an operation, deferring some steps to subclasses. Template_ _Method lets subclasses redefine certain steps of an algorithm without changing the algorithm‘s_

_structure._


```

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-257-0.png)


    - Define a template that implements similar algorithms and delegates the individual execution steps to subclasses.

    - Common behavior of subclasses should be moved to a superclass to avoid code duplication.

    - Modelling of fixed sequences and the specification of variation points.


**6.3.2.1 Example:** **[Compiler](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/templatemethod/compiler)**


#### CrossCompiler


public abstract class CrossCompiler {


public final void crossCompile() {


collectSource();


compileToTarget();


}


```
6


_// Template methods_


protected abstract void collectSource();


protected abstract void compileToTarget();


}


```


#### IPhoneCompiler


public class IPhoneCompiler extends CrossCompiler {


protected void collectSource() {


_// anything specific to this class_


}


```
5


protected void compileToTarget() {


_// iphone specific compilation_


}


}


```
#### AndroidCompiler


public class AndroidCompiler extends CrossCompiler {


protected void collectSource() {


_// anything specific to this class_


}


```
5


protected void compileToTarget() {


_// android specific compilation_


}


}


```


CrossCompiler iphone = new IPhoneCompiler();


iphone.crossCompile();


```
5


CrossCompiler android = new AndroidCompiler();


android.crossCompile();


}


}


```
**6.3.2.2 Example:** **[Callbackable](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/templatemethod/callbackable)**


#### Callbackable


interface Callbackable extends Runnable {


void beforeRun();


void afterRun();


void onError(Exception e);


void onSuccess();


}


```


#### AbstractCallbackRunnable


abstract class AbstractCallbackRunnable implements Callbackable {


2


private final LocalDateTime submittedTime;


4


```
private Status status;


private LocalDateTime startedTime;


private LocalDateTime finishedTime;


```
8


AbstractCallbackRunnable() {


this .submittedTime = LocalDateTime.now();


this .status = Status.WAITING;


}


```
13


public void run() {


try {


beforeRunIntern();


startRun();


onSuccessIntern();


} catch (Exception e) {


onErrorIntern(e);


} finally {


afterRunIntern();


}


}


```
26


27 **public abstract void** startRun() **throws** Exception;


28


private void beforeRunIntern() {


this .startedTime = LocalDateTime.now();


this .status = Status.RUNNING;


```
32


beforeRun();


}


35


```
private void afterRunIntern() {


this .finishedTime = LocalDateTime.now();


afterRun();


}


```
40


private void onErrorIntern(Exception e) {


this .status = Status.FAILED;


onError(e);


}


```
45


private void onSuccessIntern() {


this .status = Status.COMPLETED;


onSuccess();


}


```
50


public Status status() {


return this .status;


}


```
54


public LocalDateTime submittedTime() {


return this .submittedTime;


}


58


```
public LocalDateTime startedTime() {


return this .startedTime;


}


```
62


public LocalDateTime finishedTime() {


return this .finishedTime;


}


}


```
#### Status


1 **public enum** Status {
