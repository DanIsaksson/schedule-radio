---
id: kb-design-principles-017
title: "2 UNKNOWN, WAITING, RUNNING, COMPLETED, FAILED"
domain: design-principles
parent_heading: "**6. Design Patterns of the Gang of** **Four**"
intent: "2 UNKNOWN, WAITING, RUNNING, COMPLETED, FAILED: 3 } 1 **public class DefaultCallbackRunnable extends** AbstractCallbackRunnable { 2 public void beforeRun() { System.out.println(\"beforeRun()\"); } ``` 7 public void afterRun() { System.out.println(\"afterRun()\"); } ``` 12 public void onError(Exception e) { System.out.println(\"onError()\"); } ``` 17 public void onSuccess() {"
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
  start: 18860
  end: 23970
---
## 2 UNKNOWN, WAITING, RUNNING, COMPLETED, FAILED


3 }


#### DefaultCallbackRunnable


1 **public class DefaultCallbackRunnable extends** AbstractCallbackRunnable {


2


public void beforeRun() {


System.out.println("beforeRun()");


}


```
7


public void afterRun() {


System.out.println("afterRun()");


}


```
12


public void onError(Exception e) {


System.out.println("onError()");


}


```
17


public void onSuccess() {


System.out.println("onSuccess()");


}


```
22


public void startRun() throws Exception {


System.out.println("startRun()");


}


}


```


2


ExecutorService executor = Executors.newFixedThreadPool(1);


executor.submit( new DefaultCallbackRunnable());


}


}


```


   - The use of a template method allows inheriting classes to overwrite certain steps of an
algorithm **without changing the structure of the algorithm** .

   - Common behaviour is **encapsulated** in one class.

    - **All methods** must be overwritten when implementing the subclass.

   - Avoiding **duplicate** code.


   - The design can become **unnecessarily complicated** if subclasses have to implement a large
number of methods to make the algorithm more concrete.

   - It can become a disadvantage if the algorithm follows a **fixed structure** that cannot be
changed.

    - It must be **clear** which methods may or must be overwritten.


#### **6.3.3 Strategy**


_Define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy_
_lets the algorithm vary independently from clients that use it._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-262-0.png)


  - For many similar classes that differ only in behavior.

  - For input masks, the logic for validation and plausibility checks of user inputs can be
encapsulated.

  - Behaviour should also be decoupled from the context if different forms of the same function
are required

**–** Sorting of a collection ( Array, List ), whereby the concrete strategies represent different
sorting methods.

#### –
Storage in different file formats.

#### –
Packer with different compression algorithms.

  - Decoupling and hiding complicated algorithm details from the context.

  - Multiple branches ( if ()... else if()... else ... ) can be avoided and this increases the clarity
of the code.


3.3.1 Example: [Compression](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/strategy/compression)


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-262-1.png)


```
#### CompressionStrategy


public interface CompressionStrategy {


void compressFiles(List<File> files);


}


```
#### RarCompressionStrategy


public class RarCompressionStrategy implements CompressionStrategy {


public void compressFiles(List<File> files) {


_// using RAR approach_


}


}


```
#### ZipCompressionStrategy


public class ZipCompressionStrategy implements CompressionStrategy {


public void compressFiles(List<File> files) {


_// using ZIP approach_


}


}


```
#### CompressionContext


public class CompressionContext {


private CompressionStrategy strategy;


3


```
_// this can be set at runtime by the application preferences_


public void compressionStrategy(CompressionStrategy strategy) {


this .strategy = strategy;


}


```
8


_// use the strategy_


public void createArchive(List<File> files) {


this .strategy.compressFiles(files);


}


}


```


2


CompressionContext ctx = new CompressionContext();


5


_// we could assume context is already set by preferences_


ctx.compressionStrategy( new RarCompressionStrategy());


8


_// get a list of files_


10


```
ctx.createArchive(files);


}


}


```


**6.3.3.2 Example:** **[LogFormatter](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/strategy/logformatter)**


#### LogFormatter


interface LogFormatter {


String format(Log log);


}


```
#### FullLogFormatter


1 **public class FullLogFormatter implements** LogFormatter {


2


public String format(Log log) {


return String.format("Hash: %s%nAuthor: %s%nMessage: %s%nDate: %s%n",


log.hash(),


log.author(),


log.message(),


log.date());


}


}


```
#### ShortLogFormatter


1 **public class ShortLogFormatter implements** LogFormatter {


2


public String format(Log log) {


return String.format("Hash: %s%nAuthor: %s%nMessage: %s%n",


log.hash(),


log.author(),


log.message());


}


}


```
#### OnelineLogFormatter


1 **public class OnelineLogFormatter implements** LogFormatter {


2


public String format(Log log) {


return String.format("%s %s",


log.hash(),


log.message());


}


}


```


#### Log


public record Log(String hash, String author, String message, LocalDateTime date) {


2


}


```
#### GitConsole


public class GitConsole {


2


private final LogFormatter formatter;


4


```
public GitConsole(LogFormatter formatter) {


this .formatter = formatter;


}


```
8


public void printLog(List<Log> logs) {


logs.forEach(log -> System.out.println( this .formatter.format(log)));


}


}


```


2


GitConsole console = new GitConsole( new FullLogFormatter());


5


```
List<Log> logs = List.of(


new Log("397c670", "cleancoder", "Clean up", LocalDateTime.now()),


new Log("07aa4d5", "cleancoder", "Fix NPE in Log", LocalDateTime.now()));


```
9


console.printLog(logs);


}


}


```


    - **Reusability** and **decoupling of context and behaviour** . A family of algorithms is created,
which can be used context-independently. On the one hand, new context objects can use
the existing strategies.

    Dynamic behaviour . The behaviour of the context can be changed at runtime using
appropriate setters.

```
    - It is possible to choose from different implementations, thus increasing flexibility and
reusability.

    - **Multiple branches** can be avoided and this increases the overview of the code.

Alternative implementation . Also, the same function (e.g., sorting) can be offered by different implementations, which differ in non-functional aspects (performance, memory requirements).


```


  - Clients need to know the different strategies to choose between them and initialize the

context.


#### –
Problem can be solved with a Factory. This allows the creation logic to be outsourced
from the client to a factory.


```
#### **6.3.4 Observer**


_Define a one-to-many dependency between objects so that when one object changes state, all its_ _dependents are notified and updated automatically._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-267-0.png)


  - When changing an object makes it is necessary to modify one or more objects.


#### –
GUIs (user changes data, new data must be updated in all GUI components).

#### –
MVC patterns (Model-View-Controller) in view-model communication.

#### –
Every second of a timer pulse, both the digital clock and the analog clock must be updated.

  - Objects should notify other objects without knowing more about the object to be notified.

  - Enables loose coupling of objects.


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-267-1.png)


3.4.1 Example: [DataStore](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/observer/datastore)


```
#### Screen

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-268-0.png)


public class Screen implements Observer {


public void update(Observable o, Object arg) {


_// act on the update_


}


}


```
#### DataStore


public class DataStore extends Observable {


private String data;


3


```
public String data() {


return data;


}


```
7


public void data(String data) {


this .data = data;


_// mark the observable as changed_


setChanged();


}


}


```


Screen screen = new Screen();


DataStore dataStore = new DataStore();


```
5


_// register observer_


dataStore.addObserver(screen);


8


_// do something with dataStore_


10


```
_// send a notification_


dataStore.notifyObservers();


}


}


```
The Observable class and the Observer interface have been deprecated in Java 9. The event model supported by Observer and Observable is quite limited, the order of notifications delivered by Observable is unspecified, and state changes are not in one-for-one correspondence with notifications.


3.4.2 Example: [Influencer](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/observer/influencer)


```
#### Subject


public interface Subject {


2


void register(Observer observer);


4


void unregister(Observer observer);


6


void notifyAllObservers(Object message);


8


}


```
#### Observer


interface Observer {


void update(Object message);


}


```
#### Influencer


public class Influencer implements Subject {


2


private final Queue<Observer> followers = new ConcurrentLinkedQueue<>();


4


private final String name;


6


```
public Influencer(String name) {


}


```
10


public void register(Observer follower) {


this .followers.add(follower);


}


```
15


public void unregister(Observer follower) {


this .followers.remove(follower);


}


```
20


public void notifyAllObservers(Object message) {


this .followers.forEach(follower -> follower.update(message));


}


}


```


#### Follower


public class Follower implements Observer {


2


private final String follower;


4


```
public Follower(String follower) {


this .follower = follower;


}


```
8


public void update(Object message) {


System.out.printf("%s received message: %s%n", this .follower, message);


}


}


```


2


Influencer influencer = new Influencer("Java");


5


```
influencer.register( new Follower("PHP"));


influencer.register( new Follower("QBasic"));


influencer.register( new Follower("TypeScript"));


```
9


influencer.notifyAllObservers("Java rocks!");


}


}


```


Consistency of condition. Automatic adjustment of the observer state when the subject changes.

Reusability . The Observer Pattern allows subject and observer to be varied independently.

Easy extension of different observers that observe a single subject.


```


    Update cascades and cycles . In complex systems with many subjects and observers, update
cascades can easily occur because the observers do not know about each other and cannot
estimate the consequences of a single modification to a subject.


```
#### **6.3.5 Chain of Responsibility**


_Avoid coupling the sender of a request to its receiver by giving more than one object a chance to_ _handle the request. Chain the receiving objects and pass the request along the chain until an_ _object handles it._


```

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-271-0.png)


   - Providing a low coupling between an object that should be handled by potential handler
objects.

   - Where a request can be handled by more than one object.

    - Processing a request at runtime in dynamically sequential order.


**6.3.5.1 Example:** **[Purchase](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/chainofresponsibility/purchase)**


#### Purchase


public record Purchase( double amount) {


2


}


```
#### PurchaseAuthorizeFlow


public interface PurchaseAuthorizeFlow {


void next(Employee nextEmployee);


}


```
#### Employee


public interface Employee extends PurchaseAuthorizeFlow {


void authorize(Purchase purchase);


}


```


#### AbstractEmployee


public abstract class AbstractEmployee implements Employee {


2


private Employee nextEmployee;


4


```


public void next(Employee nextEmployee) {


this .nextEmployee = nextEmployee;


}


```
9


public Employee next() {


return this .nextEmployee;


}


}


```
#### CTO


1 **public class CTO extends** AbstractEmployee {


2


public void authorize(Purchase purchase) {


if (purchase.amount() > 100_000d) {


System.out.println("CTO");


next().authorize(purchase);


}


}


}


```
#### VP


1 **public class VP extends** AbstractEmployee {


2


public void authorize(Purchase purchase) {


if (purchase.amount() > 10_000d && purchase.amount() < 100_000d) {


System.out.println("VP");


next().authorize(purchase);


}


}


}


```


#### TeamLead


1 **public class TeamLead extends** AbstractEmployee {


2


public void authorize(Purchase purchase) {


if (purchase.amount() <= 10_000d) {


System.out.println("TeamLead");


next().authorize(purchase);


}


}


}


```


2


Employee cto = new CTO();


Employee vp = new VP();


Employee teamLead = new TeamLead();


```
7


teamLead.next(vp);


vp.next(cto);


10


```
teamLead.authorize( new Purchase(100)); _// TeamLead_


teamLead.authorize( new Purchase(99_999)); _// VP_


teamLead.authorize( new Purchase(500_000)); _// CTO_


}


}


```


**6.3.5.2 Example:** **[Authentication](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/chainofresponsibility/authentication)**


#### Authentication


1 **public interface Authentication** {


2 }


#### AuthenticationFilter


public interface AuthenticationFilter {


boolean isAuthenticated(Authentication authentication);


void setNextFilter(AuthenticationFilter nextFilter);


}


```
#### AbstractAuthenticationFilter


public abstract class AbstractAuthenticationFilter implements AuthenticationFilter {


2


private AuthenticationFilter nextFilter;


4


```


public void setNextFilter(AuthenticationFilter nextFilter) {


this .nextFilter = nextFilter;


}


```
9


public boolean nextFilter(Authentication authentication) {


if ( this .nextFilter != null ) {


return this .nextFilter.isAuthenticated(authentication);


}


```
14


return false ;


}


}


```
#### BasicAuthenticationFilter


1 **public class BasicAuthenticationFilter extends** AbstractAuthenticationFilter {


2


public boolean isAuthenticated(Authentication authentication) {


if (authentication instanceof BasicAuthentication) {


return true ;


}


```
9


return nextFilter(authentication);


}


}


```


#### BasicAuthentication


1 **public class BasicAuthentication implements** Authentication {


2 }


#### BearerAuthenticationFilter


1 **public class BearerAuthenticationFilter extends** AbstractAuthenticationFilter {


2


public boolean isAuthenticated(Authentication authentication) {


if (authentication instanceof BearerAuthentication) {


return true ;


}


```
9


return nextFilter(authentication);


}


}


```
#### BearerAuthentication


1 **public class BearerAuthentication implements** Authentication {


2 }


#### DigestAuthenticationFilter


1 **public class DigestAuthenticationFilter extends** AbstractAuthenticationFilter {


2


public boolean isAuthenticated(Authentication authentication) {


if (authentication instanceof DigestAuthentication) {


return true ;


}


```
9


return nextFilter(authentication);


}


}


```
#### DigestAuthentication


public class DigestAuthentication implements Authentication {


}


```


2


AuthenticationFilter digestFilter = new DigestAuthenticationFilter();


AuthenticationFilter bearerFilter = new BearerAuthenticationFilter();


AuthenticationFilter basicFilter = new BasicAuthenticationFilter();


```
7


digestFilter.setNextFilter(bearerFilter);


bearerFilter.setNextFilter(basicFilter);


10


```
System.out.println(digestFilter.isAuthenticated( new BearerAuthentication())); _// true_


}


}


```


    - The object does not need to know the structure of the chain, which decouples the sender of
the request and its recipients.

    - Simplifies your object as it does not need to know the structure of the chain and does not
need to hold a direct reference to its members.

    - Allows responsibilities to be added and removed dynamically by changing the members of
the order of the chain.


    - It is not guaranteed that objects are handled in the chain’s flow, but this can also be seen
as an advantage.

    - It can be difficult to observe and debug the runtime properties.


#### **6.3.6 Command**


_Encapsulates a request as an object, thereby letting you parameterize clients with different_ _requests, queue, or log requests, and support undoable operations._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-277-0.png)


    - When you want to **queue** operations or **schedule** their execution.

    - Implementing **undo** or **redo** operations.


**6.3.6.1 Example:** **[FileSystem](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/command/filesystem)**


#### Command


public interface Command {


void execute(FileSystem fs);


}


```
#### CreateCommand


public class CreateCommand implements Command {


2


private final File file;


4


```
public CreateCommand(File file) {


this .file = file;


}


```
8


public void execute(FileSystem fs) {


fs.create( this .file);


}


}


```


#### DeleteCommand


public class DeleteCommand implements Command {


2


private final File file;


4


```
public DeleteCommand(File file) {


this .file = file;


}


```
8


public void execute(FileSystem fs) {


fs.delete( this .file);


}


}


```
#### MoveCommand


public class MoveCommand implements Command {


2


private final File source;


private final File target;


5


```
public MoveCommand(File source, File target) {


this .source = source;


this .target = target;


}


```
10


public void execute(FileSystem fs) {


fs.move( this .source, this .target);


}


}


```
#### FileSystem


1 **public class FileSystem** {


2


public void create(File file) {


System.out.printf("Create %s%n",


file.getName());


}


```
7


public void delete(File file) {


System.out.printf("Delete %s%n",


file.getName());


}


```
12


public void move(File source, File target) {


System.out.printf("Move %s to %s%n",


source.getName(),


target.getName());


}


}


```


#### BatchFileSystemExecutor


public class BatchFileSystemExecutor {


2


private final FileSystem fs;


4


```
public BatchFileSystemExecutor(FileSystem fs) {


this .fs = fs;


}


```
8


public void execute(List<Command> commands) {


commands.forEach(c -> c.execute( this .fs));


}


}


```


2


FileSystem fs = new FileSystem();


BatchFileSystemExecutor executor = new BatchFileSystemExecutor(fs);


```
6


List<Command> commands = List.of(


new CreateCommand( new File("file.tmp")),


new DeleteCommand( new File("file.tmp")),


new CreateCommand( new File("secret.txt")),


new MoveCommand( new File("secret.txt"), new File("topsecret.txt")));


```
12


executor.execute(commands);


}


}


```


Create file.tmp


Delete file.tmp


Create secret.txt


Move secret.txt to topsecret.txt


```


**6.3.6.2 Example:** **[Television](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/command/television)**


#### Command


public interface Command {


void execute();


}


```
#### TurnOn


public class TurnOn implements Command {


2


private final Television device;


4


```
public TurnOn(Television device) {


this .device = device;


}


```
8


public void execute() {


this .device.on();


}


}


```
#### TurnOff


public class TurnOff implements Command {


2


private final Television device;


4


```
public TurnOff(Television device) {


this .device = device;


}


```
8


public void execute() {


this .device.off();


}


}


```
#### VolumeUp


public class VolumeUp implements Command {


2


private final Television device;


4


```
public VolumeUp(Television device) {


this .device = device;


}


```
8


public void execute() {


this .device.volumeUp();


}


}


```


#### VolumeDown


public class VolumeDown implements Command {


2


private final Television device;


4


```
public VolumeDown(Television device) {


this .device = device;


}


```
8


public void execute() {


this .device.volumeDown();


}


}


```
#### Television


public class Television {


2


private int volume = 0;


4


```
public void on() {


System.out.println("TV is on");


}


```
8


public void off() {


System.out.println("TV is off");


}


```
12


public void volumeUp() {


this .volume++;


15


System.out.println("Volume: " + this .volume);


}


18


public void volumeDown() {


this .volume--;


21


```
System.out.println("Volume: " + this .volume);


}


}


```
#### DeviceButton


public class DeviceButton {


2


private Command command;


4


```
public void command(Command command) {


this .command = command;


}


```
8


public void press() {


this .command.execute();


}


}


```


2


Television television = new Television();


5


DeviceButton power = new DeviceButton();


DeviceButton volume = new DeviceButton();


8


power.command( new TurnOn(television));


power.press();


11


```
volume.command( new VolumeUp(television));


volume.press();


volume.press();


volume.press();


```
16


volume.command( new VolumeDown(television));


volume.press();


19


```
power.command( new TurnOff(television));


power.press();


}


}


```


1 TV is on


2 Volume: 1


3 Volume: 2


4 Volume: 3


5 Volume: 2


6 TV is off


   - New commands can be **added easily** without changing existing code.

    - Possibility of implementing **undo** and **redo** operation.

    - You can implement a **sequence** of operations.


    **Large number** of classes and objects working together to achieve the desired goal.


#### **6.3.7 Interpreter**


_Given a language, define a representation for its grammar along with an interpreter that uses_
_the representation to interpret sentences in the language._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-283-0.png)


   - When the grammar is **simple** . A class hierarchy can be designed to represent the set of
grammar rules with every class in the hierarchy representing a separate grammar rule.

    - Representing a simple grammar as an **abstract syntax tree** like structure.


**6.3.7.1 Example:** **[HexBinary](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/interpreter/hexbinary)**


#### Expression


public abstract class Expression {


2


private final int value;


4


```
protected Expression( int value) {


this .value = value;


}


```
8


public int value() {


return this .value;


}


```
12


abstract String interpret(Context context);


}


```
#### IntToBinaryExpression


1 **public final class IntToBinaryExpression extends** Expression {


2


public IntToBinaryExpression( int value) {


super (value);


}


```
6


public String interpret(Context context) {


return Integer.toBinaryString(value());


}


}


```
#### IntToHexExpression


1 **public final class IntToHexExpression extends** Expression {


2


public IntToHexExpression( int value) {


super (value);


}


```
6


public String interpret(Context context) {


return Integer.toHexString(value());


}


}


```
#### Context


public final class Context {


2


private final String expression;


4


```
public Context(String expression) {


this .expression = Objects.requireNonNull(expression);


}


```
8


public String expression() {


return this .expression;


}


```
12


public boolean isToHex() {


return this .expression.contains("Hexadecimal");


}


```
16


public boolean isToBinary() {


return this .expression.contains("Binary");


}


```
20


public int number() {


return Integer.parseInt( this .expression.substring(0, this .expression.indexOf(' ')));


}


}


```


2


print("42 in Binary");


print("42 in Hexadecimal");


}


```
7


private static void print(String expression) {


System.out.printf("%s = %s%n", expression, interpret(expression));


}


```
11


private static String interpret(String expression) {


Context context = new Context(expression);


Expression exp;


```
15


if (context.isToHex()) {


exp = new IntToHexExpression(context.number());


} else if (context.isToBinary()) {


exp = new IntToBinaryExpression(context.number());


return context.expression();


}


```
23


return exp.interpret(context);


}


}


```


42 in Binary = 101010


42 in Hexadecimal = 2a


3.7.2 Example: [Calculator](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/interpreter/calculator)


```
#### Expression


interface Expression {


int interpret();


}


```
#### Number


public final class Number implements Expression {


2


private final int number;


4


```
public Number(String number) {


this .number = Integer.parseInt(number);


}


```
8


public int interpret() {


return this .number;


}


}


```
#### Minus


public final class Minus implements Expression {


2


private final Expression left;


private final Expression right;


5


```
public Minus(Expression left, Expression right) {


this .left = left;


this .right = right;


}


```
10


public int interpret() {


return this .left.interpret() - this .right.interpret();


}


}


```
#### Plus


public final class Plus implements Expression {


2


private final Expression left;


private final Expression right;


5


```
public Plus(Expression left, Expression right) {


this .left = left;


this .right = right;


}


```
10


11 @Override


public int interpret() {


return this .left.interpret() + this .right.interpret();


}


}


```
#### Evaluator


public final class Evaluator {


2


private final String expression;


4


```
public Evaluator(String expression) {


this .expression = Objects.requireNonNull(expression);


}


```
8


public String expression() {


return this .expression;


}


```
12


public int evaluate() {


Expression last = null ;


String splitted[] = this .expression.split(" ");


```
16


for ( int i = 0, splitted.length; i < length; i++) {


if ("+".equals(splitted[i])) {


last = new Plus(last, new Number(splitted[i + 1]));


i++;


} else if ("-".equals(splitted[i])) {


last = new Minus(last, new Number(splitted[i + 1]));


i++;


last = new Number(splitted[i]);


}


}


```
28


return last.interpret();


}


}


```


2


Evaluator evaluator = new Evaluator("5 - 3 + 29 - 1");


5


```
System.out.printf("%s = %s%n",


evaluator.expression(),


evaluator.evaluate());


}


}


```


1 5 - 3 + 29 - 1 = 30


    - Easy to change and extend the grammar. Existing expressions can be inherited and modified
as required.


   - Complex grammars are **difficult to maintain** and could become **unmanageable** .


#### **6.3.8 Iterator**


_Provide a way to access the elements of an aggregate object sequentially without exposing its_
_underlying representation._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-289-0.png)


  - Access aggregate objects without exposing their internal representation.

  - Support multiple traversals of aggregate objects.

  - Provide a uniform interface for traversing different aggregate structures.


An iterator can be designed as either an **internal** iterator or as **external** iterator.


#### Internal Iterators:


  - The collection itself provides methods that allow a client to visit various objects within
the collection. For example, the java.util.ResultSet class contains the data and also provides
methods like next() to navigate through the item list.

  - There can only be one iterator in a collection at a time.

  - The collection must maintain or store the state of the iteration.


#### External Iterators:


  - The iteration functionality is separate from the collection and resides in another object
called an iterator. Normally, the collection itself returns a corresponding iterator object to
the client. For example, the java.util.Vector, has its iterator defined in the form of a separate
object of the type Enumeration .

```
  - There can be multiple iterators for a given collection at any point in time.

  - The overhead associated with storing the iteration state is not associated with the collection.
It lies within the exclusive iterator object.


3.8.1 Example: [Cars - intern](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/iterator/intern/cars)


```
#### Car


public record Car(String name) {


2


}


```


2


List<Car> cars = List.of( new Car("Car 1"), new Car("Car 2"), new Car("Car 3"), new Car("Car 4"\


));


```
6


cars.forEach(c -> System.out.println(c.name()));


}


}


```


1 Car 1


2 Car 2


3 Car 3


4 Car 4


**6.3.8.2 Example:** **[Cars - extern](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/iterator/extern/cars)**


#### Car


public record Car(String name) {


2


}


```
#### CarIterator


public class CarIterator implements java.util.Iterator<Car> {


2


private final Car[] cars;


private int index = 0;


5


```
public CarIterator(Car[] cars) {


this .cars = cars;


}


```
9


public Car next() {


return this .cars[ this .index++];


}


```
14


public boolean hasNext() {


if ( this .index >= this .cars.length || this .cars[ this .index] == null ) {


return false ;


}


```
20


return true ;


}


}


```


2


Car[] cars = new Car[4];


5


```
cars[0] = new Car("Car 1");


cars[1] = new Car("Car 2");


cars[2] = new Car("Car 3");


cars[3] = new Car("Car 4");


```
10


11 Iterator<Car> iterator = **new** CarIterator(cars);


12


while (iterator.hasNext()) {


Car car = iterator.next();


System.out.println(car.name());


}


}


}


```


1 Car 1


2 Car 2


3 Car 3


4 Car 4


    - **Parallel iteration** over the same collection because each iterator object contains its own
iteration state (external iterator).

    - Allow iterating through an aggregate object **without exposing** its internal representation.

    - The traversal task is done by the iterator, which **simplifies** the aggregation class.


#### **6.3.9 Mediator**


_Define an object that encapsulates how a set of objects interact. Mediator promotes loose_
_coupling by keeping objects from referring to each other explicitly, and it lets you vary their_
_interaction independently._


```

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-293-0.png)


   - Many objects communicate in well-defined but complex ways. The resulting dependencies
are unstructured and difficult to understand.

   - Reusing an object is difficult because it interacts and communicates with many other
objects.

   - A behavior that is distributed across several classes should be changeable without much
subclassing.


**6.3.9.1 Example:** **[Chat](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/mediator/chat)**


#### ChatMediator


interface ChatMediator {


2


void sendMessage(User user, String message);


4


void addUser(User user);


}


```
#### Chat


public final class Chat implements ChatMediator {


2


private final List<User> users;


4


```
public Chat() {


this .users = new ArrayList<>();


}


```
8


public void addUser(User user) {


this .users.add(user);


}


```
13


public void sendMessage(User fromUser, String message) {


for (User user : this .users) {


if (user != fromUser) {


user.receive(message);


}


}


}


}


```
#### User


public class User {


2


private final ChatMediator mediator;


private final String name;


5


```
public User(ChatMediator mediator, String name) {


this .mediator = mediator;


}


```
10


public ChatMediator mediator() {


return this .mediator;


}


```
14


}


```
18


public void send(String message) {


System.out.println(name() + ": Sending Message=" + message);


mediator().sendMessage( this, message);


}


```
23


public void receive(String message) {


System.out.println(name() + ": Received Message:" + message);


}


}


```


#### LoggedInUser


1 **public class LoggedInUser extends** User {


2


public LoggedInUser(ChatMediator mediator, String name) {


super (mediator, name);


}


```
6


7 }


#### AnonymousUser


1 **public class AnonymousUser extends** User {


2


public AnonymousUser(ChatMediator mediator) {


super (mediator, "Anonymous");


}


```
6


7 }


2


ChatMediator mediator = new Chat();


User user1 = new LoggedInUser(mediator, "Lia");


User user2 = new LoggedInUser(mediator, "Timo");


User user3 = new LoggedInUser(mediator, "Matheo");


User user4 = new LoggedInUser(mediator, "Luisa");


User user5 = new AnonymousUser(mediator);


```
10


mediator.addUser(user1);


mediator.addUser(user2);


mediator.addUser(user3);


mediator.addUser(user4);


mediator.addUser(user5);


```
16


user1.send("Hi to everybody!");


}


}


```


Lia: Sending Message=Hi to everybody!


Timo: Received Message:Hi to everybody!


Matheo: Received Message:Hi to everybody!


Luisa: Received Message:Hi to everybody!


Anonymous: Received Message:Hi to everybody!


```


**6.3.9.2 Example:** **[Aircraft](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/mediator/aircraft)**


#### AircraftMediator


public interface AircraftMediator {


2


void registerRunway(Runway runway);


4


String allotRunway(Aircraft aircraft);


6


void releaseRunway(Aircraft aircraft);


}


```
#### AircraftTrafficControlRoom


public final class AircraftTrafficControlRoom implements AircraftMediator {


2


private final LinkedList<Runway> availableRunways;


private final Map<Aircraft, Runway> aircraftRunways;


5


```
public AircraftTrafficControlRoom() {


this .availableRunways = new LinkedList<>();


this .aircraftRunways = new HashMap<>();


}


```
10


public void registerRunway(Runway runway) {


this .availableRunways.add(runway);


}


```
15


public String allotRunway(Aircraft aircraft) {


Runway nextAvailableRunway = null ;


```
19


if (! this .availableRunways.isEmpty()) {


nextAvailableRunway = this .availableRunways.removeFirst();


this .aircraftRunways.put(aircraft, nextAvailableRunway);


}


```
24


return nextAvailableRunway == null ? null : nextAvailableRunway.name();


}


27


```


public void releaseRunway(Aircraft aircraft) {


if ( this .aircraftRunways.containsKey(aircraft)) {


Runway runway = this .aircraftRunways.remove(aircraft);


this .availableRunways.add(runway);


}


}


}


```


#### AircraftColleague


public interface AircraftColleague {


2


void startLanding();


4


void finishLanding();


}


```
#### Aircraft


public final class Aircraft implements AircraftColleague {


2


private final AircraftMediator mediator;


private final String flightName;


5


```
public Aircraft(AircraftMediator mediator, String flightName) {


this .mediator = mediator;


this .flightName = flightName;


}


```
10


public void startLanding() {


String runway = this .mediator.allotRunway( this );


```
14


if (runway == null ) {


System.out.println("Due to traffic, there's a delay in landing of " + this .flightName);


System.out.println("Currently landing " + this .flightName + " on " + runway);


}


}


```
21


public void finishLanding() {


System.out.println( this .flightName + " landed successfully");


this .mediator.releaseRunway( this );


}


}


```
#### Runway


public record Runway(String name) {


2


}


```


2


AircraftMediator mediator = new AircraftTrafficControlRoom();


mediator.registerRunway( new Runway("Runway A"));


```
6


AircraftColleague wrightFlight = new Aircraft(mediator, "Wright Flight");


AircraftColleague airbusA320 = new Aircraft(mediator, "Airbus A320");


9


```
wrightFlight.startLanding();


airbusA320.startLanding();


wrightFlight.finishLanding();


}


}


```


1 Currently landing Wright Flight on Runway A


2 Due to traffic, there's a delay in landing of Airbus A320


3 Wright Flight landed successfully


    - The mediator is the **only object** that knows about all other objects.

    - It becomes easier to **change the behavior** of object relationships by replacing the mediator
with one of its subclasses with extended or changed functionality.

    - Moving the dependencies between objects out of the individual objects leads to better
**reusability** of the objects.

    - Since the objects do not have to refer to each other directly, it is **easier to test** .

    - The resulting low coupling makes it possible to **change individual classes** without affecting
other classes.


    - You have to be careful not to create a so-called **god object**, which happens when the
mediator class becomes too big.


#### **6.3.10 Memento**


_Without violating encapsulation, capture and externalize an object‘s internal state so that the_
_object can be restored to this state later._


![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-299-0.png)


    - For designing a mechanism to capture and store the state of an object so, when needed, the
object can be put back to this previous state.


**6.3.10.1 Example:** **[Editor](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/memento/editor)**


#### Editor


public interface Editor {


2


void setContent(String content);


4


void setFontSize( int fontSize);


6


void setFontName(String fontName);


8


void save();


10


void restore();


}


```
#### EditorState


1 **public interface EditorState** {


2


String getContent();


4


int getFontSize();


6


String getFontName();


8


}


```
#### CleanCodeEditor


public final class CleanCodeEditor implements Editor {


2


private final History history = new History();


4


```
private String content;


private int fontSize;


private String fontName;


```
8


public CleanCodeEditor(String content, int fontSize, String fontName) {


this .content = content;


this .fontName = fontName;


this .fontSize = fontSize;


}


```
14


public String getContent() {


return this .content;


}


```
18


public int getFontSize() {


return this .fontSize;


}


```
22


public String getFontName() {


return this .fontName;


}


```
26


public void setContent(String content) {


this .content = content;


}


```
31


public void setFontSize( int fontSize) {


this .fontSize = fontSize;


}


```
36


public void setFontName(String fontName) {


this .fontName = fontName;


}


```
41


public void save() {


EditorState state = new CleanCodeEditorState( this .content, this .fontSize, this .fontName);


this .history.push(state);


}


```
47


public void restore() {


EditorState state = this .history.pop();


this .content = state.getContent();


this .fontName = state.getFontName();


this .fontSize = state.getFontSize();


}


}


```


#### CleanCodeEditorState


1 **public final class CleanCodeEditorState implements** EditorState {


2


private final String content;


private final int fontSize;


private final String fontName;


```
6


public CleanCodeEditorState(String content, int fontSize, String fontName) {


this .content = content;


this .fontName = fontName;


this .fontSize = fontSize;


}


```
12


public String getContent() {


return this .content;


}


```
17


public int getFontSize() {


return this .fontSize;


}


```
22


public String getFontName() {


return this .fontName;


}


}


```
#### History


public final class History {


2


private final Stack<EditorState> history;


4


```
public History() {


this .history = new Stack<>();


}


```
8


public void push(EditorState state) {


this .history.push(state);


}


```
12


public EditorState pop() {


return this .history.pop();


}


}


```


2


Editor editor = new CleanCodeEditor("Java Clean Code", 12, "Courier New");


editor.save();


```
6


editor.setContent("Java");


editor.save();


9


editor.restore();


System.out.println(((CleanCodeEditor) editor).getContent());


12


```
editor.restore();


System.out.println(((CleanCodeEditor) editor).getContent());


}


}


```


1 Java


2 Java Clean Code


**6.3.10.2 Example:** **[Balance](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/memento/balance)**


#### Originator


public class Originator {


2


private Double balance;


4


```
public void setBalance(Double balance) {


this .balance = balance;


}


```
8


public Double getBalance() {


return this .balance;


}


```
12


public Memento save() {


return new Memento( this .balance);


}


```
16


public void restore(Memento memento) {


this .balance = memento.balance();


}


}


```
#### Memento


public record Memento(Double balance) {


2


}


```
#### CareTaker


public final class CareTaker {


2


private final Stack<Memento> history;


4


```
public CareTaker() {


this .history = new Stack<>();


}


```
8


public void push(Memento state) {


this .history.push(state);


}


```
12


public Memento pop() {


return this .history.pop();


}


}


```


2


CareTaker careTaker = new CareTaker();


Originator originator = new Originator();


```
6


originator.setBalance(40.11d);


careTaker.push(originator.save());


System.out.println(originator.getBalance());


```
10


originator.setBalance(100.11d);


careTaker.push(originator.save());


System.out.println(originator.getBalance());


```
14


originator.restore(careTaker.pop());


System.out.println(originator.getBalance());


17


```
originator.restore(careTaker.pop());


System.out.println(originator.getBalance());


}


}


```


11


11


11


11


```


    - You always can **discard changes** and **restore** them to a stable state.

**Easy restore techniques** are provided by this pattern.


    - If you allow a huge amount of history entries it can get a **memory problem** .

    - Saving the states will **reduce** the overall **performance** of the application.


#### **6.3.11 Visitor**


_Represent an operation to be performed on the elements of an object structure. Visitor lets you_ _define a new operation without changing the classes of the elements on which it operates._

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-305-0.png)


```


    - Lets you define a new operation without changing the classes of the elements on which it
operates.

    - Similar operations must be performed on objects of different types across a heterogeneous
class hierarchy.


**6.3.11.1 Example:** **[Fridge](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/visitor/fridge)**


#### FridgeElement


public interface FridgeElement {


void accept(Visitor visitor);


}


```
#### Visitor


1 **public interface Visitor** {


2


void visit(Beer beer);


4


void visit(Milk milk);


6


void visit(Butter butter);


8


}


```
#### Beer


1 **public class Beer implements** FridgeElement {


2


public void accept(Visitor visitor) {


visitor.visit( this );


}


```
7


LocalDate bestBeforeDate() {


return LocalDate.of(2022, 1, 28);


}


}


```
#### Butter


1 **public class Butter implements** FridgeElement {


2


public void accept(Visitor visitor) {


visitor.visit( this );


}


```
7


LocalDate expiryDate() {


return LocalDate.of(2021, 10, 2);


}


}


```
#### Milk


1 **public class Milk implements** FridgeElement {


2


public void accept(Visitor visitor) {


visitor.visit( this );


}


```
7


Date bestBeforeDate() {


return new Date(2021 - 1900, Calendar.JANUARY, 1);


}


}


```
#### BestBeforeDateVisitor


public class BestBeforeDateVisitor implements Visitor {


2


private final LocalDate compareDate;


4


```
public BestBeforeDateVisitor(LocalDate compareDate) {


this .compareDate = compareDate;


}


```
8


public void visit(Beer beer) {


checkBestBeforeDate(beer, beer.bestBeforeDate()); _// method differ_


```


12 }


13


public void visit(Milk milk) {


checkBestBeforeDate(milk, milk.bestBeforeDate() _// method differ_


.toInstant()


.atZone(ZoneId.systemDefault())


.toLocalDate());


}


```
21


public void visit(Butter butter) {


checkBestBeforeDate(butter, butter.expiryDate()); _// method differ_


}


```
26


private void checkBestBeforeDate(FridgeElement element, LocalDate bestBeforeDate) {


if (bestBeforeDate.isAfter( this .compareDate)) {


System.out.println(element.getClass().getSimpleName());


}


}


}


```


2


List<FridgeElement> fridge = List.of(


new Beer(),


new Beer(),


new Milk(),


new Butter());


```
9


Visitor visitor = new BestBeforeDateVisitor(LocalDate.of(2021, 1, 1));


11


System.out.println("Over the best before date:");


13


```
for (FridgeElement element : fridge) {


element.accept(visitor);


}


}


}


```


1 Over the best before date:


2 Beer


3 Beer


4 Butter


**6.3.11.2 Example:** **[Figures](https://github.com/mnhock/swcs/tree/master/swcs-gof/src/main/java/swcs/gof/behavioral/visitor/figures)**


#### Figure


interface Figure {


<T> T accept(Visitor<T> visitor);


}


```
#### Visitor


interface Visitor <T> {


2


T visit(Square square);


4


T visit(Circle circle);


6


T visit(Rectangle rectangle);


}


```
#### Circle


public class Circle implements Figure {


2


private final double radius;


4


```
public Circle( double radius) {


this .radius = radius;


}


```
8


public double radius() {


return this .radius;


}


```
12


public <T> T accept(Visitor<T> visitor) {


return visitor.visit( this );


}


}


```
#### Rectangle


public class Rectangle implements Figure {


2


private final double weight;


private final double height;


5


```
public Rectangle( double weight, double height) {


this .weight = weight;


this .height = height;


}


```
10


public double weight() {


return this .weight;


}


```
14


public double height() {


return this .height;


}


```
18


public <T> T accept(Visitor<T> visitor) {


return visitor.visit( this );


}


}


```
#### Square


public class Square implements Figure {


2


private final double side;


4


```
public Square( double side) {


this .side = side;


}


```
8


public double side() {


return this .side;


}


```
12


public <T> T accept(Visitor<T> visitor) {


return visitor.visit( this );


}


}


```
#### AreaVisitor


1 **public class AreaVisitor implements** Visitor<Double> {


2


public Double visit(Square square) {


return square.side() * square.side();


}


```
7


public Double visit(Circle circle) {


return Math.PI * circle.radius() * circle.radius();


}


```
12


public Double visit(Rectangle rectangle) {


return rectangle.height() * rectangle.weight();


}


}


```


#### PerimeterVisitor


1 **public class PerimeterVisitor implements** Visitor<Double> {


2


public Double visit(Square element) {


return 4 * element.side();


}


```
7


public Double visit(Circle element) {


return 2 * Math.PI * element.radius();


}


```
12


public Double visit(Rectangle element) {


return 2 * element.height() + 2 * element.height();


}


}


```


2


List<Figure> figures = List.of(


new Circle(3),


new Square(4),


new Rectangle(2, 8));


```
8


calculateArea(figures);


calculatePerimeter(figures);


}


```
12


private static void calculateArea(List<Figure> figures) {


Visitor<Double> visitor = new AreaVisitor();


15


```
double totalArea = figures.stream()


.mapToDouble(f -> f.accept(visitor))


.sum();


```
19


System.out.printf("Total area: %f %n", totalArea);


}


22


private static void calculatePerimeter(List<Figure> figures) {


Visitor<Double> visitor = new PerimeterVisitor();


25


```
double totalPerimeter = figures.stream()


.mapToDouble(f -> f.accept(visitor))


.sum();


```
29


System.out.printf("Total perimeter: %f%n", totalPerimeter);


}


}


```


1 Total area: 60,274334


2 Total perimeter: 66,849556


   - New operations can be **added easily** .

    - The logic of the operations is **centralized** in the visitor and not dispersed.

    - The visitor pattern allows the operation to be defined **without changing the class** of any
of the objects in the collection.


    - The implementation of the visitor concrete classes could **break the encapsulation** of the
visited objects. Due to the fact, that a new visitor class needs access to the public members of these objects.
