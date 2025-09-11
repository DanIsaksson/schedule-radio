Week 10 – 11 Lesson Summaries

Table of Contents
Lambda Expressions	2
LINQ (Language Integrated Query)	2
Key Takeaways	2
LINQ Methods for Data Manipulation	3
Key Concepts	3
Additional Notes	3
Switch Expressions	5
Syntax Breakdown	5
Example Usage	5
1. Assigning a Value from a Method	5
2. Assigning a Value to a Variable	5
Pattern Matching with Tuples	6
Debugging Fundamentals	7
Primary Debugging Tools	7
Using Breakpoints	7
Controlling Execution Flow	7


Lambda Expressions
Lambda expressions provide a concise way to create anonymous functions. They are particularly useful when working with LINQ queries.
Syntax Element	Description	Example
()	Input parameters for the expression.	(word)
=>	The lambda operator, separating the parameters from the expression body.	word =>
{}	The body of the expression, containing the code to be executed.	{ return word.Length > 6; }
A complete simple lambda expression looks like this: (word) => word.Length > 6
LINQ (Language Integrated Query)
LINQ provides a powerful and readable way to query data from various sources, including collections, databases, and XML.
Method	Description	Example Usage
.Where()	Filters a sequence of values based on a predicate (a condition).	words.Where(word => word.Length > 6)
.Select()	Projects each element of a sequence into a new form.	people.Select(person => person.Age)
.ToList()	Converts an IEnumerable<T> to a List<T>.	words.Where(...).ToList()
.ToArray()	Converts an IEnumerable<T> to an array.	words.Where(...).ToArray()
Key Takeaways
    • Lambda expressions are used extensively with LINQ to define the logic for methods like Where and Select. 
    • You can chain LINQ methods together to create complex queries in a readable, step-by-step manner. 
    • The IEnumerable<T> interface is fundamental to LINQ, as it represents a sequence of items that can be iterated over. 

LINQ Methods for Data Manipulation
This lesson expands on LINQ by introducing methods for aggregating, ordering, and combining data collections.
Method	Description	Example Usage
.Sum()	Calculates the sum of a sequence of numeric values.	numbersLinq.Sum()
.Average()	Calculates the average of a sequence of numeric values.	numbersLinq.Average()
.Max()	Returns the maximum value in a sequence.	numbersLinq.Max()
.OrderBy()	Sorts the elements of a sequence in ascending order.	someWords.OrderBy(word => word)
.Distinct()	Returns distinct elements from a sequence.	someWords.Distinct()
.Concat()	Concatenates two sequences.	someWordsToo.Concat(someWordsMoreToo)
.Except()	Produces the set difference of two sequences.	someWordsToo.Except(someWordsMoreToo)
.Intersect()	Produces the set intersection of two sequences.	someWordsToo.Intersect(someWordsMoreToo)
.Union()	Produces the set union of two sequences (combining them and removing duplicates).	someWordsToo.Union(someWordsMoreToo)
.Reverse()	Inverts the order of the elements in a sequence.	someWords.Reverse()
Key Concepts
    • Deferred Execution: LINQ queries are often not executed until the results are actually needed (e.g., when you call .ToList() or iterate over the collection). This can improve performance by avoiding unnecessary work. 
    • Method Chaining: You can chain multiple LINQ methods together to build complex queries in a readable and declarative way. 
    • IEnumerable<T> and IQueryable<T>: These are the two primary interfaces that LINQ operates on. IEnumerable<T> is used for in-memory collections, while IQueryable<T> is often used with databases and other external data sources. 
Additional Notes
    • The lesson also briefly touches on the difference between the Enumerable and Queryable classes in the System.Linq namespace, which provide the static methods for LINQ operations. 
    • It is highlighted that when working with collections of complex objects (like a List<Person>), you need to provide a lambda expression to specify which property to use for operations like OrderBy. 


Switch Expressions
This lesson introduces a more concise syntax for switch statements, known as switch expressions. They are particularly useful for scenarios where a value is assigned based on a condition.
Key Differences from Traditional switch Statements:
    • The variable being evaluated comes before the switch keyword. 
    • It uses the => (lambda) operator instead of case and colons. 
    • Each case is an expression that returns a value, not a block of statements. 
    • There are no break statements. 
    • A default case is required and is represented by a discard _ character. 
Syntax Breakdown
Element	Description	Example
variable	The input value to be matched against the patterns.	operatorKey
switch	The keyword that initiates the switch expression.	operatorKey switch
pattern	A constant or pattern to match against the input variable.	"/"
=>	Separates the pattern from the result expression.	=>
result	The value to be returned if the pattern matches.	100
_	The discard pattern, which acts as the default case.	_ => 0
Example Usage
1. Assigning a Value from a Method
A method can use a switch expression to return different values based on its input parameters.
// Method that returns an integer based on a string and a boolean
int RespondToText(string text, bool option) => (text, option) switch
{
    // Pattern matching on a tuple
    ("Hello", true) => 1,
    ("Hi", true)    => 2,
    // A 'when' clause can be used for more complex conditions
    (_, true) when text.Length > 6 => 40,
    _ => 0 // Default case
};
2. Assigning a Value to a Variable
A switch expression can be used to directly initialize a variable.
string operatorKey = "/";
decimal resultValue = operatorKey switch
{
    "/" => Divide(number1, number2), // Can call methods
    "*" => number1 * number2,
    "+" => number1 + number2,
    "-" => number1 - number2,
    _   => 0
};
Pattern Matching with Tuples
Switch expressions can evaluate multiple values at once by using tuples. This allows for more complex and readable conditions. In the example above, (text, option) creates a tuple that is then used for pattern matching.


Debugging Fundamentals
This lesson provides an introduction to the essential tools and concepts for debugging C# applications in an IDE (Integrated Development Environment).
Primary Debugging Tools
There are two main approaches to debugging demonstrated in the lesson:
    1. Console.WriteLine(): A simple method for printing the values of variables to the console at specific points in the code. This is a quick way to check the state of your program as it runs. 
    2. Breakpoints: A more powerful and interactive method. A breakpoint pauses the execution of the program at a specific line of code, allowing you to inspect the current state in detail. 
Using Breakpoints
The core of the lesson focuses on using breakpoints effectively.
Action	Description	How to Use
Setting a Breakpoint	Pauses the program at the selected line before it is executed.	Click in the margin to the left of the line number.
Running in Debug Mode	Starts the application with the debugger attached, which will stop at any breakpoints.	Use the "Debug" option (often a bug icon) instead of the standard "Run" button.
Inspecting Variables	Once paused, you can see the current values of variables in scope.	Hover over variables in the code or use the "Threads & Variables" window in the debugger panel.
Controlling Execution Flow
When the program is paused at a breakpoint, you have several options for controlling the flow of execution:
Control	Description
Step Into	Moves the debugger to the next line of code. If the current line is a method call, the debugger will enter that method.
Step Over	Executes the current line of code and moves to the next line in the current method, without stepping into any called methods.
Step Out	Finishes executing the current method and returns to the line where it was called.
