# Week 10-11 Lesson Summaries

## Table of Contents
1. [Lambda Expressions](#lambda-expressions)
2. [LINQ (Language Integrated Query)](#linq-language-integrated-query)
3. [LINQ Methods for Data Manipulation](#linq-methods-for-data-manipulation)
4. [Switch Expressions](#switch-expressions)
5. [Debugging Fundamentals](#debugging-fundamentals)

## Lambda Expressions
Lambda expressions provide a concise way to create anonymous functions. They are particularly useful when working with LINQ queries.

| Syntax Element | Description | Example |
|---------------|-------------|---------|
| `()` | Input parameters for the expression | `(word)` |
| `=>` | The lambda operator, separating parameters from expression body | `word =>` |
| `{}` | The body of the expression containing the code to execute | `{ return word.Length > 6; }` |

A complete simple lambda expression looks like this: `(word) => word.Length > 6`

## LINQ (Language Integrated Query)
LINQ provides a powerful and readable way to query data from various sources, including collections, databases, and XML.

| Method | Description | Example Usage |
|--------|-------------|---------------|
| `.Where()` | Filters a sequence based on a predicate | `words.Where(word => word.Length > 6)` |
| `.Select()` | Projects elements into a new form | `people.Select(person => person.Age)` |
| `.ToList()` | Converts IEnumerable<T> to List<T> | `words.Where(...).ToList()` |
| `.ToArray()` | Converts IEnumerable<T> to array | `words.Where(...).ToArray()` |

### Key Takeaways
- Lambda expressions are used extensively with LINQ for methods like Where and Select
- LINQ methods can be chained for complex queries
- IEnumerable<T> is fundamental to LINQ for sequence iteration

## LINQ Methods for Data Manipulation
Advanced LINQ methods for aggregating, ordering, and combining data collections.

| Method | Description | Example Usage |
|--------|-------------|---------------|
| `.Sum()` | Calculates sum of numeric values | `numbersLinq.Sum()` |
| `.Average()` | Calculates average of numeric values | `numbersLinq.Average()` |
| `.Max()` | Returns maximum value | `numbersLinq.Max()` |
| `.OrderBy()` | Sorts elements ascending | `someWords.OrderBy(word => word)` |
| `.Distinct()` | Returns distinct elements | `someWords.Distinct()` |
| `.Concat()` | Concatenates sequences | `someWordsToo.Concat(someWordsMoreToo)` |
| `.Except()` | Produces set difference | `someWordsToo.Except(someWordsMoreToo)` |
| `.Intersect()` | Produces set intersection | `someWordsToo.Intersect(someWordsMoreToo)` |
| `.Union()` | Produces set union | `someWordsToo.Union(someWordsMoreToo)` |
| `.Reverse()` | Inverts element order | `someWords.Reverse()` |

### Key Concepts
- **Deferred Execution**: LINQ queries execute only when results are needed
- **Method Chaining**: Multiple LINQ methods can be chained for complex queries
- **IEnumerable<T> vs IQueryable<T>**: In-memory vs database operations

### Additional Notes
- Enumerable and Queryable classes provide static LINQ methods
- Complex objects require property specification in lambda expressions

## Switch Expressions
Modern syntax for switch statements, optimized for value assignment based on conditions.

### Key Differences from Traditional Switch
- Variable precedes switch keyword
- Uses => operator instead of case/colons
- Cases return values directly
- No break statements needed
- Required default case using _

### Syntax Breakdown
| Element | Description | Example |
|---------|-------------|---------|
| variable | Input value for matching | `operatorKey` |
| switch | Switch expression keyword | `operatorKey switch` |
| pattern | Match pattern | `"/"` |
| => | Pattern-result separator | `=>` |
| result | Return value | `100` |
| _ | Default case | `_ => 0` |

### Example Usage

#### 1. Method Value Assignment
