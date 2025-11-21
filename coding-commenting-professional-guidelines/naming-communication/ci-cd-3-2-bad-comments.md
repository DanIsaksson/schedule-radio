---
id: kb-naming-communication-001
title: "- CI/CD – **3.2 Bad comments**"
domain: naming-communication
parent_heading: "**Preface**"
intent: "- CI/CD – **3.2 Bad comments**: _// Deletes a directory and checks if it is a directory._ _// Throw IOException if not exists._ public static void deleteDirectory(File directory) throws IOException { if (directory != null && directory.isDirectory() && !directory.exists()) { return ; } ``` 7 if (!directory.delete()) { throw new IOException(\"Unable to delete directory \" + directory + \".\");"
tags:
  - architecture
  - comments
  - formatting
  - functions
  - guideline
  - naming
  - process
source_lines:
  start: 311
  end: 4427
---
## - CI/CD
### **3.2 Bad comments**

#### **3.2.1 Redundant comments**


_// Deletes a directory and checks if it is a directory._


_// Throw IOException if not exists._


public static void deleteDirectory(File directory) throws IOException {


if (directory != null && directory.isDirectory() && !directory.exists()) {


return ;


}


```
7


if (!directory.delete()) {


throw new IOException("Unable to delete directory " + directory + ".");


}


}


```
    - Reading the comment probably takes longer than reading the code itself.

    - The comment is certainly no more informative than the code.

    - Neither does it justify the code, nor does it state its purpose or reason for existence.

    - The commentary is less precise than the code and seduces the reader to accept this lack of
precision.

#### **3.2.2 Misleading comments**


_// Deletes a directory and checks if it is a directory._


_// Throw IOException if not exists._


public static void deleteDirectory(File directory) throws IOException {


if (directory != null && directory.isDirectory() && !directory.exists()) {


return ;


}


```
7


if (!directory.delete()) {


throw new IOException("Unable to delete directory " + directory + ".");


}


}


```
    - This comment is redundant and misleading!

    - This method does not throw IOException if the directory does not exist. It throws IOException
if the directory cannot be deleted.


#### **3.2.3 Mandatory comments**


_/_


_* Returns the day of the year._


_*_


_* @return the day of the year._


_*/_


public int getDayOfYear() {


return this .dayOfYear;


}


```


_/_


_* @param title The title of the CD_


_* @param author The author of the CD_


_* @param tracks The number of tracks on the CD_


_* @param durationInMinutes The duration of the CD in minutes_


_*/_


public void addCD(String title, String author, int tracks, int durationInMinutes)


```
    - This commentary provides no additional information, only obscures the code and brings
the potential for misleading.

    - It’s bad to rule that every function of a Javadoc or every variable should have a comment.

    - Such comments only make the code more confusing and lead to general confusion and
disorder.

#### **3.2.4 Diary comments**


_// Changes history_


_//_


_// 2020-09-30 Implement FactoryAware to inject Factory_


_// 2020-09-30 Remove unnecessary stubbing_


_// 2020-09-30 Adapt to API changes_


_// 2020-09-30 Merge branch '2.3.x'_


_// 2020-09-30 Introduce a dedicated @Compatibility annotation_


_// 2020-09-30 Fix matching of SNAPSHOT artifacts when customizing layers_


_// 2020-09-30 Adapt to API change_


_// 2020-09-30 Merge branch '2.3.x'_


_// 2020-09-30 Start building against version 5.3.0_


_// 2020-09-30 Do not execute datasource initialization in a separate thread_


_// 2020-09-30 Start building against Java 16_


_// 2020-09-29 Reduce configuration resolution when building a layered jar_


_// 2020-09-29 Merge pull request #56701_


_// 2020-09-24 Polish_


_// 2020-09-17 Add support for Oracle_


```
    - A long time ago, there was a reason for the creation and maintenance of such log entries.

    - Nowadays there are version control systems.

    - These comments are disturbance data that spread confusion and should be removed.


#### **3.2.5 Gossip**


_/_


_* Default constructor._


_*/_


public Parser () {


}


```


1 _/** The day of the month. */_


2 **private int** dayOfMonth;


private void startSending() {


try {


send();


} catch (Exception e) {


try {


response.add(ErrorResponder.makeExceptionString(e));


response.closeAll();


} catch (Exception e1) {


_// Who cares!?_


}


}


}


```
    - These comments are so garrulous that we learn to ignore them.

    - When we read the code, our eyes just jump over it.

    - At some point, the comments start lying when the surrounding code changes.

#### **3.2.6 Position identifier**


1 _// Methods //////////////////////////////////////_


#### Bad code, seen in a real project


_//################################################_


_//###_ _getter and setter_


_//################################################_


```

#### **3.2.7 Write-ups and incidental remarks**


1 _// Added by Bad Programmer_


    - This information can be seen in the version management system.

#### **3.2.8 Don’t leave commented out code in your codebase**


public static String extension(String filename) {


_// if (filename == null) { return null; }_


3


```
String name = new File(filename).getName();


int extensionPosition = name.lastIndexOf('.’);


if (extensionPosition < 0) {


return "";


}


return name.substring(extensionPosition + 1);


}


```
    - Others who see this commented code will not have the courage to delete it.

    - They will believe that there is a reason why the code is there and that it is too important to
be deleted.

#### **3.2.9 Rules for commenting**


#### Primary Rule


Comments are for things that can **not** be expressed in code.


#### Redundancy Rule


Comments which **restate** code must be deleted.


#### Single Truth Rule


If the comment says what the code could say, then the code must change to make the comment
redundant.


More information can be found here:

https://agileinaflash.blogspot.com/2009/04/rules-for-commenting.html


```
