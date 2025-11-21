---
id: kb-process-practices-022
title: "- CI/CD – **3.1 Communicate through code**"
domain: process-practices
parent_heading: "**Preface**"
intent: "- CI/CD – **3.1 Communicate through code**: _There are only two hard things in Computer Science: cache invalidation and naming things._ - Phil Karlton Java code conventions are a set of guidelines for writing Java code that are intended to help improve the readability and maintainability of the code. Some of the key conventions include using descriptive and meaningful names for variables and methods, using consistent indentation and spacing, and using comments to document the code."
tags:
  - classes
  - comments
  - example
  - formatting
  - functions
  - guideline
  - naming
  - principles
  - process
source_lines:
  start: 311
  end: 4046
---
## - CI/CD
### **3.1 Communicate through code**

_There are only two hard things in Computer Science: cache invalidation and naming things._
- Phil Karlton

#### **3.1.1 Use Java code conventions and avoid misinformation**


Java code conventions are a set of guidelines for writing Java code that are intended to help improve the readability and maintainability of the code. Some of the key conventions include using descriptive and meaningful names for variables and methods, using consistent indentation and spacing, and using comments to document the code.


To avoid misinformation, it is important to follow this code conventions. This can help to ensure that other developers who may be working on the code in the future can easily understand how the code works and what it does.


```


public static final double pi = 3.14159265358979323846;


int YEAR;


String first_name;


public class convert_strategy {}


Set carList;


```
In this code snippet, there are several violations of Java code conventions and potential sources of misinformation:


    - **Inconsistent Naming** : The variable pi violates the convention of using uppercase letters
and underscores for constant names. It should be renamed to PI to adhere to the convention.

Similarly, the variable YEAR violates the convention of using lowercase letters for variable names. It should be renamed to year for consistency.

```
    - **Camel Case Naming** : The variable first_name violates the convention of using camel case
for variable names. It should be renamed to firstName to follow the convention.

    - **Class Naming** : The class convert_strategy violates the convention of using PascalCase for
class names. It should be renamed to ConvertStrategy to adhere to the convention.

    - **Inappropriate Naming** : The variable carList is misleading because it implies that it is a list
of cars. However, it is declared as a Set, which may cause confusion for other developers.
It should be renamed to carSet or another appropriate name that reflects the actual data
structure being used.


```


public static final double PI = 3.14159265358979323846;


int year;


String firstName


public class ConvertStrategy {}


Set cars;


```


Customer Customer = new Customer("Darth Vader");


...


Customer.name() _// static access?_


```
In this code, there are a couple of issues related to naming conventions and potential misinfor mation:


    - **Inconsistent Naming** : The variable Customer violates the convention of using lowercase
letters for variable names. It should be renamed to customer to follow the convention and

differentiate it from the class name.

    - **Static Access Misconception** : The line Customer.name() suggests that name() is a static method,
which could lead to misinformation. However, if Customer is an instance of the Customer class, it should be called as customer.name() to access the instance method. Using proper naming conventions and avoiding misleading code can prevent such misunderstandings.


```


Customer customer = new Customer("Darth Vader");


...


customer.name()


```
Java code conventions can be found here: [Oracle Java code conventions¹](https://www.oracle.com/technetwork/java/codeconventions-150003.pdf) [Google Java Style Guide²](https://google.github.io/styleguide/javaguide.html)


¹ [https://www.oracle.com/technetwork/java/codeconventions-150003.pdf](https://www.oracle.com/technetwork/java/codeconventions-150003.pdf) ² [https://google.github.io/styleguide/javaguide.html](https://google.github.io/styleguide/javaguide.html)


```
#### **3.1.2 Choose an expressive name and avoid mental mapping**


Choose an expressive name for a variable, method, class, or other code element that accurately
and clearly describes its purpose or behavior. This can help to make the code more readable and
understandable, and can reduce the need for other developers to mentally map the code elements
to their intended meanings.


```


int d; _// days since birthday_


String n; _// name of host_


In this code, the variables d and n are not very descriptive. It is not immediately clear what these variables represent or what they are used for. As a result, other developers who may be working on this code in the future may need to spend time mentally mapping these variables to their intended meanings, which can be time-consuming and error-prone.


```


int daysSinceBirthday;


String hostname;


In this code, the variables daysSinceBirthday and hostname are more expressive. Their names
accurately describe their intended meanings and purposes, which makes it easier to understand
the code without having to mentally map the variables to their meanings.

```
#### **3.1.3 Make differences clear with meaningful variable names**


public boolean isFileOlder(File s1, File s2) {}


2


public static String replaceFirst(String string, Pattern pattern, String with) {}


```


public boolean isFileOlder(File file, File referenceFile) {}


2


public static String replaceFirst(String text, Pattern regex, String replacement) {}


```
#### **3.1.4 Use pronounceable names**


Use pronounceable names for variables, methods, classes, and other code elements that are easy to pronounce and remember. This can make the code more readable and understandable, and can make it easier for other developers to work with the code.


```


class DRcd {


private int rtd;


private String stna;


private LocalDateTime genymdhms;


private LocalDateTime modymdhms;


}


```
In this code, the variables rtd, stna, genymdhms and modymdhms are not very pronounceable. Their names are not easy to say out loud, and they do not convey any meaning or information about their intended purposes. As a result, other developers who are working with this code may find it difficult to remember or refer to these variables.


```


class Student {


private int registrationId;


private String name;


private LocalDateTime generated;


private LocalDateTime modified;


}


```

#### **3.1.5 Do not hurt the readers**


We will read more code than we write, so do not hurt the readers. Declare class variables with useful constants. In this way, the meaning or intended use of each literal is indicated. If the constant needs to be changed, the change is also limited to the declaration. There is no need to search and change the code for this literal.


```


1 _// What is 86400000?_


2 setTokenTimeout(86400000);


private static final int MILLISECONDS_IN_A_DAY = 24 * 60 * 60 * 1000;


2


setTokenTimeout(MILLISECONDS_IN_A_DAY);


```


public final class Sphere {


2


private Sphere() {


}


5


```
public static double area( double radius) {


return 3.14 * radius * radius;


}


```
9


public static double volume( double radius) {


return 4.19 * radius * radius * radius; _// Why 4.19?_


}


}


```


public final class Sphere {


2


private Sphere() {


}


5


```
public static double area( double radius) {


return Math.PI * radius * radius;


}


```
9


public static double volume( double radius) {


return 4.0 / 3.0 * Math.PI * radius * radius * radius;


}


}


```

#### **3.1.6 Don‘t add redundant context**


Do not add redundant context in variable or class names that include unnecessary information or details that can make the names longer and less readable.


class CarData {


private String carManufacturer;


private String carModel;


}


```
In this code, the carManufacturer and the carModel variable both include the word car as part of their
names. This is redundant, because the names already include the word “CarData” as part of the
class name, so it is clear that they are related to cars. As a result, the names are longer and less
readable than they need to be.


Moreover, the name of the class CarData could be changed to Car as Data adds no additional meaning
to the class name.


```


class Car {


private String manufacturer;


private String model;


}


```


1 **public interface UserRepository** {


2


3 Optional<User> findUserById( **long** userId);


4


Iterable<User> findAllUsers();


6


User saveUser(User user);


8


Iterable<User> saveAllUsers(Iterable<User> users);


10


void deleteUserById( long userId);


12


void deleteAllUsers();


14


long countUsers();


}


```


1 **public interface UserRepository** {


2


3 Optional<User> findById( **long** id);


4


Iterable<User> findAll();


6


User save(User user);


8


Iterable<User> saveAll(Iterable<User> users);


10


void deleteById( long id);


12


void deleteAll();


14


long count();


}


```
#### **3.1.7 Don’t add words without additional meaning**


Some generic words often don’t add any additional meaning to a name. So, they mean anything
when added to a name.


#### These words often indicate this constellation:


    - Data

    - Information

   - Bean

    - Value

   - Manager

    - Object

    - Entity

    - Instance

   - Custom


By adding these kinds of words, double-check if the name still means the same if you remove it?
If yes, remove it. If not, keep it, or better try to find a better name.


public final class StudentData {


private final String studentFirstName;


private final String studentLastName;


private final AddressInformation addressInformation;


private final CoursesBean coursesBean;


```
6


public StudentData(String studentFirstName, String studentLastName,


AddressInformation addressInformation, CoursesBean coursesBean) {


this .studentFirstName = studentFirstName;


this .studentLastName = studentLastName;


this .addressInformation = addressInformation;


this .coursesBean= coursesBean;


}


}


```


public final class Student {


private final String firstName;


private final String lastName;


private final Address address;


private final Courses courses;


```
6


public Student(String firstName, String lastName, Address address, Courses courses) {


this .firstName = firstName;


this .lastName = lastName;


this .address = address;


this .courses = courses;


}


}


```

#### **3.1.8 Don’t use and or or in method names**


A method should be responsible only for one action. Method names using _and_ or _or_ are an
indicator, that the method has too many responsibilities and combines too much functionality.
In this case, you should consider splitting the functionality of this method into other methods.


```
#### How to improve this:


    - Split the method into smaller pieces and extract the method in separate methods.

    - If the things belong together:


#### –
Consider finding a better name for that entire operation.

#### –
Consider creating something else that encapsulates the combination.


public class JavaMailSender {


public void createMimeMessageAndSend() throws MailException {}


...


}


```


public class JavaMailSender {


public MimeMessage createMimeMessage() {}


public void send(MimeMessage mimeMessage) throws MailException {}


...


}


```

#### **3.1.9 Use positive names for boolean variables and functions**


When defining boolean functions, it is often best to use positive names that accurately describe their intended behavior or state. For example, instead of defining a function named isNotOpen(), it would be more intuitive and readable to define a function named isOpen() . This makes the code more self-explanatory and easier to understand.


```


if (!file.isNotOpen())


if (!window.isNotVisible())


3


if (!notRunning)


if (!hasNoLicense)


Here, the functions isNotOpen(), isNotVisible() and the boolean variables notRunning and hasNoLicense
are defined with negative names, which can lead to confusion and make the code harder to
comprehend. Negated names require extra mental processing to understand their actual meaning.
It is preferable to use positive names that convey the intended behavior more directly.


```


if (file.isOpen())


If (window.isVisible())


3


if (running)


if (hasLicense)


By using positive names for boolean variables and functions, you create code that is more intuitive, easier to understand, and less prone to misinterpretation. Positive names improve code readability and reduce cognitive load for developers who read or maintain the code, leading to more efficient and error-free programming.


```
#### **3.1.10 Respect the order within classes**


    - Be consistent with the order of the class attributes, the constructor parameters, and its
methods.

    - Align the order of the constructor parameter and the methods with the order of the class
attributes declaration order.

    - Private methods should be below the calling method. Thus, the code tells a story with the
related methods below it. Furthermore, searching and scrolling are prevented.


public final class WebServer {


2


private final int port;


private final String hostname;


5


```
public WebServer() {


this ("localhost", 8080);


}


```
9


public WebServer(String hostname, int port) {


this .port = port;


this .hostname = hostname;


}


```
14


public String hostname() {


return this .hostname;


}


```
18


public void start() {


if (isPortFree()) {


....


}


}


```
24


public int port() {


return this .port;


}


```
28


public void stop() {


...


destroyThreadPool();


...


}


```
34


private boolean isPortFree() { ... }


36


private void destroyThreadPool() { ... }


}


```


public final class WebServer {


2


private final int port;


private final String hostname;


5


```
public WebServer() {


this (8080, "localhost");


}


```
9


public WebServer( int port, String hostname) {


this .port = port;


this .hostname = hostname;


}


```
14


public int port() {


return this .port;


}


```
18


public String hostname() {


return this .hostname;


}


```
22


public void start() {


if (isPortFree()) {


....


}


}


```
28


29 **private boolean** isPortFree() { ... }


30


public void stop() {


...


destroyThreadPool();


...


}


```
36


private void destroyThreadPool() { ... }


}


```
#### **3.1.11 Group by line break**


Use line breaks to make associations with related things and separate weakly related things.


public final class FileUtils {


public static final long ONE_KB = 1024;


public static final long ONE_MB = ONE_KB * ONE_KB;


public static final long ONE_GB = ONE_KB * ONE_MB;


public static final File[] EMPTY_FILE_ARRAY = new File[0];


private FileUtils(){}


public static void cleanDirectory(File directory) throws IOException {


File[] files = verifyFiles(directory);


List<Exception> causes = new ArrayList<>();


for (File file : files) {


try {


forceDelete(file);


} catch ( final IOException ioe) {


causes.add(ioe);


}


}


if (!causes.isEmpty()) {


throw new IOException(causes);


}


}


}


```


1 **public final class FileUtils** {


2


public static final long ONE_KB = 1024;


public static final long ONE_MB = ONE_KB * ONE_KB;


public static final long ONE_GB = ONE_KB * ONE_MB;


```
6


public static final File[] EMPTY_FILE_ARRAY = new File[0];


8


private FileUtils() {}


10


```
public static void cleanDirectory(File directory) throws IOException {


File[] files = verifyFiles(directory);


List<Exception> causes = new ArrayList<>();


```
14


for (File file : files) {


try {


forceDelete(file);


} catch ( final IOException ioe) {


causes.add(ioe);


}


}


```
22


if (!causes.isEmpty()) {


throw new IOException(causes);


}


}


}


```

#### **3.1.12 Prefer self-explanatory code instead of comments**


_Don’t comment bad code - rewrite it._

- Brian W. Kernighan, The Elements of Programming Style


Prefer self-explanatory code that is clear and easy to understand without the need for additional
comments or explanations.


    - Comments are no substitute for bad code.

    - Explain it through code.

    - Java is a programming language and should express itself.

    - The more self-explanatory the code is, the less you need to comment it.

    - Treat a comment as a danger signal.

    - Comments are like deo for stinky code.

   - Comment **WHY** not **WHAT** !


final class InchToPointConvertor {


2


private InchToPointConvertor{}


4


```
_// convert the quantity in inches to points_


static float parseInch( float inch) {


return inch * 72; _// one inch contains 72 points_


}


}


```


final class InchToPointConvertor {


2


private static final int POINTS_PER_INCH = 72;


4


private InchToPointConvertor{}


6


```
static float toPoints( float inch) {


return inch * POINTS_PER_INCH;


}


}


```


_// Check if employee has entitlement for extra vacation days_


if ((employee.noVacationDaysLeft && employee.age > 50)


|| (employee.noVacationDaysLeft && employee.hasChildren))


```


1 **if** (employee.hasEntitlementForBonusVacation())


class Passwords {


...


_// check if the password is complex enough_


public boolean isComplexPassword(String password) {


boolean symbolOrDigitFound = false ; _// found a digit or symbol?_


boolean letterFound = false ; _// found a letter?_


```
7


for ( int i = 0; i < password.length(); i++) {


char c = password.charAt(i);


10


```
if (Character.isLowerCase(c) || Character.isUpperCase(c)) {


letterFound = true ;


symbolOrDigitFound = true ;


}


}


```
17


return letterFound && symbolOrDigitFound;


}


}


```


class Passwords {


...


public boolean isComplex(String password){


return containsLetter(password) && (containsDigit(password) || containsSymbol(password));


}


```
6


private boolean containsLetter(String password) { ... }


8


private boolean containsDigit(String password) { ... }


10


private boolean containsSymbol(String password) { ... }


}


```
#### **3.1.13 Refactor step by step**


public List< int []> getList() {


List< int []> data = new ArrayList< int []>();


for ( int [] x : items)


if (x[0] == 4) _// 4 represents flagged_


data.add(x);


return data;


}


```
#### Better code


public List< int []> flaggedCells() {


List< int []> flaggedCells = new ArrayList< int []>();


for ( int [] cell : gameBoard) {


if (cell[STATUS] == FLAGGED)


flaggedCells.add(cell);


return flaggedCells;


}


```


public List<Cell> flaggedCells() {


List<Cell> flaggedCells = new ArrayList<Cell>();


for (Cell cell : gameBoard) {


if (cell.isFlagged()) {


flaggedCells.add(cell);


}


}


return flaggedCells;


}


```
#### Craftsman code


public List<Cell> flaggedCells() {


return gameBoard.stream()


.filter(Cell::isFlagged)


.toList();


}


```
#### More generic craftsman code


public List<Cell> cells(Predicate<Cell> filter) {


return gameBoard.stream()


.filter(filter)


.toList();


}


```
