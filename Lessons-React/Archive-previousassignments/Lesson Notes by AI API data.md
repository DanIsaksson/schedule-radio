### **Table of Contents**

Lesson: Fetching and Displaying API Data in React.................................................................................2

Part 1: Setting Up the Project with Static Data.....................................................................................2

Step 1.1: Finding and Understanding the API..................................................................................2
Step 1.2: Creating a Static Data File.................................................................................................3

/src/data/libraryData.js.................................................................................................................3
Mentor's Notes............................................................................................................................. 4
Step 1.2: Creating a Static Data File.................................................................................................4

/src/data/libraryData.js.................................................................................................................4
Mentor's Notes............................................................................................................................. 4
Step 1.3: Setting Up the Main Component (App.jsx).......................................................................5

Mentor's Notes............................................................................................................................. 6
Step 1.2: Creating a Static Data File.................................................................................................6

/src/data/libraryData.js.................................................................................................................6
Mentor's Notes............................................................................................................................. 6
Step 1.3: Setting Up the Main Component (App.jsx).......................................................................7

Mentor's Notes............................................................................................................................. 8
Step 1.4: Rendering a List with .map().............................................................................................8

Mentor's Notes............................................................................................................................. 9
Step 1.4: Rendering a List with .map().............................................................................................9

Mentor's Notes........................................................................................................................... 10
Part 2: Fetching Live Data from the API.............................................................................................11

Step 2.1: Installing Axios................................................................................................................11
Step 2.2: Fetching Data with useEffect and axios..........................................................................11

/src/App.jsx (Final Code)...........................................................................................................11
Mentor's Notes........................................................................................................................... 12
Step 2.2: Fetching Data with useEffect and axios..........................................................................12

/src/App.jsx (Final Code)...........................................................................................................12


Of course! Here is the first part of your lesson on fetching and displaying API data in React.


Once you've reviewed it, let me know, and I'll proceed to fill out the "Mentor's Notes" sections for you.

# **Lesson: Fetching and Displaying API Data in** **React**


Hello! Today, we're going to build a fundamental skill for any web developer: getting data from an
external source (an API) and showing it in our React application.


We'll break this down into two main parts:


1. **Working with Static Data:** First, we'll save a sample of the data directly in our project. This

lets us build and style our components without worrying about network requests.


2. **Fetching Live Data:** Once our components are ready, we'll replace the static data with a live

call to the API using the useEffect hook and a popular library called Axios.


Let's get started!

## **Part 1: Setting Up the Project with Static Data**


Before we try to connect to an external server, it's a great practice to work with a local, static copy of
the data you expect to receive. This allows you to focus entirely on building your React components
without dealing with network errors or waiting for API calls to finish.

#### **Step 1.1: Finding and Understanding the API**


_[(Video Timestamp: 00:12)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D12s)_


For this lesson, we'll use the **Libris API**, a public resource for library information. After some
searching, we find its documentation. The key is to understand the structure of the URL we need to

call.


The documentation shows a base URL and a set of **parameters** we can add to customize our request.
For example:


   - query=...: The search term.


   - format=...: The format of the data we want back (like XML or JSON).


   - n=...: The number of records to retrieve.


_[(Video Timestamp: 05:45)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D345s)_


By testing a URL in the browser, we can see the data structure. We'll request the **JSON** format, as it's
the most common format used in modern web development and works seamlessly with JavaScript.


Here's the URL we will be working with:
http://libris.kb.se/xsearch?query=Ludwig+Wittgenstein&format=json


This URL fetches book data related to "Ludwig Wittgenstein" in JSON format.

#### **Step 1.2: Creating a Static Data File**


_[(Video Timestamp: 08:35)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D515s)_


Now, let's save the JSON output from that URL into our project.


1. Create a new folder in your src directory called data.


2. Inside data, create a new file named libraryData.js.


3. Copy the JSON response and paste it into this file, exporting it as a JavaScript object.


**/src/data/libraryData.js**


code JavaScript
downloadcontent_copy
expand_less
```
export const libraryData = {
"xsearch": {
"from": "1",
"to": "10",
"records": "290",
"list": [
{
"identifier": "http://libris.kb.se/bib/xf9zvcdm6p064f",
"title": "Private Notebooks: 1914-1916",
"creator": "Wittgenstein, Ludwig",
"type": "book",
"publisher": "WW NORTON CO",
"date": "2022",
"language": "eng"
},
{
"identifier": "http://libris.kb.se/bib/19241168",
"title": "Tractatus Logico-Philosophicus : German and English",
"creator": "Wittgenstein, Ludwig",
"type": "E-book",
"publisher": "London : Routledge",
"date": "2001",
"language": "eng"
}
// ... more records would be here
]
}
};

```

<br>


**Mentor's Notes**

#### **_Step 1.2: Creating a Static Data File_**


_**/src/data/libraryData.js**_


_code JavaScript_
```
export const libraryData = {

"xsearch": {

"from": "1",

"to": "10",

"records": "290",

"list": [

{

"identifier": "http://libris.kb.se/bib/xf9zvcdm6p064f",

"title": "Private Notebooks: 1914-1916",

"creator": "Wittgenstein, Ludwig",

"type": "book",

"publisher": "WW NORTON CO",

"date": "2022",

"language": "eng"

},

// ... more records

]

}

};

```

_<br>_


_**Mentor's Notes**_


_[(Video Timestamp: 08:35)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D515s)_


_**Why do this?**_ _Think of this as building a movie set. Before you hire actors and start filming_
_(fetching live data), you build the rooms and props (the React components). This static data_
_file is like a stand-in prop. It lets you perfect your component's design and logic without_
_depending on a live internet connection or a working server. It isolates the "look" from the_
_"fetch"._


_**Code Breakdown:**_


      - _export const libraryData = ...: The export keyword is how you make variables,_
_functions, or objects available to other files in your project. We're exporting a_
_constant named libraryData._


      - _{ "xsearch": { ... } }: This is a standard JavaScript object. Notice the structure. To_
_get to the list of books, we'll need to access libraryData, then its xsearch property,_
_and finally the list property inside that. The path will be libraryData.xsearch.list._
_Understanding this path is key for the next step._


_**React Learning Material:**_


      - _While this is a plain JavaScript concept, it's fundamental to how you organize data_
_in React. Read about_ _**"Importing and Exporting Components"**_ _to understand how_
_[files share code with each other: https://react.dev/learn/importing-and-exporting-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fimporting-and-exporting-components)_

_[components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fimporting-and-exporting-components)_

#### **Step 1.3: Setting Up the Main Component (App.jsx)**


Next, let's set up our main component to import and store this data in its state. We'll use the useState

hook for this.


_[(Video Timestamp: 10:05)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D605s)_


code Jsx

downloadcontent_copy
expand_less
```
// src/App.jsx

import React, { useState } from 'react';
import { libraryData } from './data/libraryData'; // Import our static data

function App() {
// Initialize state with the list of books from our imported data
const [backendData, setBackendData] = useState(libraryData.xsearch.list);

return (
<div className="page">
<h1>Library Search Results</h1>
<div className="results-container">
{/* We will map over the data here */}
</div>

</div>
);
}

export default App;

```

<br>


**Mentor's Notes**


_Excellent! Let's fill in those notes. Here is the completed lesson with detailed explanations for_
_each step._

#### **_Step 1.2: Creating a Static Data File_**


_**/src/data/libraryData.js**_


_code JavaScript_
```
export const libraryData = {

"xsearch": {

"from": "1",

"to": "10",

"records": "290",

"list": [

{

"identifier": "http://libris.kb.se/bib/xf9zvcdm6p064f",

"title": "Private Notebooks: 1914-1916",

"creator": "Wittgenstein, Ludwig",

"type": "book",

"publisher": "WW NORTON CO",

"date": "2022",

"language": "eng"

},

// ... more records

]

}

};

```

_<br>_


_**Mentor's Notes**_


_[(Video Timestamp: 08:35)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D515s)_


_**Why do this?**_ _Think of this as building a movie set. Before you hire actors and start filming_
_(fetching live data), you build the rooms and props (the React components). This static data_
_file is like a stand-in prop. It lets you perfect your component's design and logic without_


_depending on a live internet connection or a working server. It isolates the "look" from the_
_"fetch"._


_**Code Breakdown:**_


      - _export const libraryData = ...: The export keyword is how you make variables,_
_functions, or objects available to other files in your project. We're exporting a_
_constant named libraryData._


      - _{ "xsearch": { ... } }: This is a standard JavaScript object. Notice the structure. To_
_get to the list of books, we'll need to access libraryData, then its xsearch property,_
_and finally the list property inside that. The path will be libraryData.xsearch.list._
_Understanding this path is key for the next step._


_**React Learning Material:**_


      - _While this is a plain JavaScript concept, it's fundamental to how you organize data_
_in React. Read about_ _**"Importing and Exporting Components"**_ _to understand how_
_[files share code with each other: https://react.dev/learn/importing-and-exporting-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fimporting-and-exporting-components)_

_[components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fimporting-and-exporting-components)_

#### **_Step 1.3: Setting Up the Main Component (App.jsx)_**


_code Jsx_

```
// src/App.jsx

import React, { useState } from 'react';

import { libraryData } from './data/libraryData'; // Import our static data

function App() {

// Initialize state with the list of books from our imported data

const [backendData, setBackendData] = useState(libraryData.xsearch.list);

return (

// ... JSX to render the page

);

}

export default App;

```

_<br>_


_**Mentor's Notes**_


_[(Video Timestamp: 10:05)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D605s)_


_**The Goal:**_ _We need our component to "hold" or "remember" the list of books. In React,_
_when a component needs to remember something that can change and affect what's on the_

_screen, we use_ _**state**_ _._


_**Code Breakdown:**_


      - _import { useState } from 'react';: We're importing the useState Hook from the React_
_library. Hooks are special functions that let you "hook into" React features._


      - _import { libraryData } from './data/libraryData';: This line pulls in the libraryData_
_object we just exported from our other file._


      - _const [backendData, setBackendData] = useState(...): This is the core of using_

_state. Let's break it down:_


          - _useState(): You call this function to declare a new piece of state._


          - _libraryData.xsearch.list: The value you pass inside useState() is the_ _**initial**_
_**state**_ _. Here, we're setting it to the array of books from our static data file._


          - _backendData: This is our state variable. It holds the current data (initially,_
_the list of books). You use it in your JSX to display the data._


          - _setBackendData: This is the_ _**updater function**_ _. You never change_
_backendData directly. You always call this function to update the state,_
_which tells React to re-render the component._


_**React Learning Material:**_


      - _To understand how components remember information, read_ _**"State: A**_
_**Component's Memory"**_ _[: https://react.dev/learn/state-a-components-memory](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory)_


      - _For a deep dive into the useState Hook itself, check out_ _**"useState"**_ _:_
_[https://react.dev/reference/react/useState](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Freference%2Freact%2FuseState)_

#### **Step 1.4: Rendering a List with .map()**


_[(Video Timestamp: 11:15)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D675s)_


To display each book from our backendData array, we'll use the JavaScript .map() method. This method
iterates over each item in an array and returns a new array of JSX elements—one for each book.


It's crucial to provide a unique key prop for each element in the list. React uses this key to identify
which items have changed, been added, or been removed, which helps optimize rendering.


code Jsx

downloadcontent_copy
expand_less
```
// src/App.jsx (updated return statement)

return (
<div className="page">
<h1>Library Search Results</h1>
<div className="results-container">
{backendData.map((item) => (
<div className="book-item" key={item.identifier}>
<p><strong>Title:</strong> {item.title}</p>
<p><strong>Creator:</strong> {item.creator}</p>
</div>
))}
</div>

</div>
);

```

<br>


**Mentor's Notes**

#### **_Step 1.4: Rendering a List with .map()_**


_code Jsx_

```
// src/App.jsx (updated return statement)

return (

// ...

<div className="results-container">

{backendData.map((item) => (

<div className="book-item" key={item.identifier}>

<p><strong>Title:</strong> {item.title}</p>

<p><strong>Creator:</strong> {item.creator}</p>

</div>

))}

</div>

// ...

);

```

_<br>_



_**Mentor's Notes**_


_[(Video Timestamp: 11:15)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D675s)_


_**The Goal:**_ _We have an array of book objects in our backendData state. We need to create a_
_piece of HTML (or JSX, in this case) for every single book in that array. Manually writing_
_one for each is not an option!_


_**Code Breakdown:**_


   - _{ ... }: In JSX, curly braces are your escape hatch back into JavaScript. Anything_
_inside them is treated as a JavaScript expression._


   - _backendData.map((item) => ( ... )): This is the standard JavaScript way to_
_transform an array. The .map() function loops through every item in the_
_backendData array and runs the code inside the parentheses for it. It then returns a_
_new array containing the results._


       - _**React's Magic:**_ _When you use .map() inside JSX, React takes the resulting_
_array of JSX elements ([<div>...</div>, <div>...</div>, ...]) and renders_
_them one after another._


   - _key={item.identifier}: This is_ _**extremely important**_ _. When you render a list, React_
_needs a way to track each individual element. The key prop is a unique identifier._


       - _**Why?**_ _It helps React be more efficient. If the list changes, React uses the key_
_to know which item was added, removed, or re-ordered, so it doesn't have to_

_re-render the entire list._


       - _**What makes a good key?**_ _A string or number that is unique and stable for_
_each item in the list. The book's identifier from the API data is perfect. Using_
_the array index ((item, index) => ... key={index}) is a last resort and can_
_cause bugs if the list order changes._


_**React Learning Material:**_


   - _This pattern is fundamental. Learn more about it in_ _**"Rendering Lists"**_ _:_
_[https://react.dev/learn/rendering-lists](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Frendering-lists)_


   - _To understand why the key is so important, read_ _**"Keeping Components Pure"**_ _(the_
_[section on keys): https://react.dev/learn/keeping-components-pure#why-react-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fkeeping-components-pure%23why-react-needs-keys)_
_[needs-keys](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fkeeping-components-pure%23why-react-needs-keys)_


## **Part 2: Fetching Live Data from the API**

Our component now works perfectly with our static data. The next step is to make it dynamic by
fetching this data directly from the Libris API when the component first loads.

#### **Step 2.1: Installing Axios**


_[(Video Timestamp: 20:55)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D1255s)_


While we could use the browser's built-in fetch function, many developers prefer using a library called
**Axios** . It simplifies making HTTP requests and handling responses.


First, let's install it. Open your terminal in the project root and run:


code Bash

downloadcontent_copy
expand_less
```
npm install axios

#### **Step 2.2: Fetching Data with useEffect and axios**

```

_[(Video Timestamp: 24:30)](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3Ds_h_2h3f-g8%26t%3D1470s)_


Fetching data is a **side effect** —an action that interacts with the "outside world" beyond React. The
correct place to handle side effects in a function component is the useEffect hook.


We want this data fetch to happen only **once**, when the component is first mounted to the screen. We
can achieve this by providing an **empty dependency array** ([]) as the second argument to useEffect.


Here is the complete code for App.jsx.


**/src/App.jsx (Final Code)**


code Jsx

downloadcontent_copy
expand_less
```
import React, { useState, useEffect } from 'react';
import axios from 'axios'; // Import axios
import './App.css';

function App() {
// Start with an empty array for our data
const [backendData, setBackendData] = useState([]);

// URL for the API endpoint
const API_URL = 'http://libris.kb.se/xsearch?
query=Ludwig+Wittgenstein&format=json';

// useEffect to fetch data when the component mounts
useEffect(() => {
console.log("useEffect is running!"); // For demonstration

```

```
axios.get(API_URL)
.then((response) => {
// Handle success
console.log("Data fetched successfully:", response.data);
setBackendData(response.data.xsearch.list);
})
.catch((error) => {
// Handle error
console.error("Error fetching data:", error);
});

}, []); // The empty array means this effect runs only once

return (
<div className="page">
<h1>Library Search Results</h1>
<div className="results-container">
{backendData.map((item) => (
<div className="book-item" key={item.identifier}>
<p><strong>Title:</strong> {item.title}</p>
<p><strong>Creator:</strong> {item.creator}</p>
</div>
))}
</div>

</div>
);
}

export default App;

```

<br>


**Mentor's Notes**

#### **_Step 2.2: Fetching Data with useEffect and axios_**


_**/src/App.jsx (Final Code)**_


_code Jsx_

```
import React, { useState, useEffect } from 'react';

import axios from 'axios';

// ...

function App() {

const [backendData, setBackendData] = useState([]); // Start with an empty array

useEffect(() => {

axios.get(API_URL)

```

```
.then((response) => {

setBackendData(response.data.xsearch.list);

})

.catch((error) => {

console.error("Error fetching data:", error);

});

}, []); // Empty array means: run this only once!

// ... return statement using .map() is the same

}```

<br>

> #### Mentor's Notes

>

> *(Video Timestamp: [24:30](https://www.youtube.com/watch?v=s_h_2h3fg8&t=1470s))*

>

> **The Goal:** We want to replace our static data with real data from the API.
This is a "side effect" because it involves interacting with something outside of
our React component (a network). The `useEffect` Hook is the tool for managing

side effects.

>

> **Code Breakdown:**

> *  `import { useEffect } from 'react';`: Just like `useState`, we need to
import this Hook from React.

> *  `useState([])`: Notice our initial state is now an **empty array**. This is
because when our component first renders, we haven't fetched the data yet! The
page will initially show nothing.

> *  `useEffect(() => { ... }, []);`: This is the syntax for the Hook.

>   *  **The Function `() => { ... }`:** This is your "effect." It's the code
that will run. In our case, it's the `axios` call to the API.

>   *  **The Dependency Array `[]`:** This is the crucial second argument. It
tells React *when* to run your effect.

>     *  An **empty array `[]`** means: "Only run this effect one time, right
after the component is first rendered to the screen." This is exactly what we want
for fetching initial data.

> *  `axios.get(API_URL)`: This is the command to make a "GET" request to our API
endpoint. This is an asynchronous operation—it takes time!

```

```
> *  `.then((response) => { ... })`: This block of code will run only *after* the
network request succeeds. The `response` object from `axios` contains all the
details, with the actual JSON data living in `response.data`.

> *  `setBackendData(response.data.xsearch.list)`: Once we have the data, we use
our state updater function to put the list of books into our component's state.
This triggers React to re-render the component, and this time, `backendData` will
be filled with data, and our `.map()` function will display it on the screen.

> *  `.catch((error) => { ... })`: This block runs if the network request fails.
It's essential for good user experience to handle errors gracefully.

>

> **React Learning Material:**

> *  To understand side effects and the `useEffect` Hook, start with
**"Synchronizing with Effects"**: [https://react.dev/learn/synchronizing-witheffects](https://react.dev/learn/synchronizing-with-effects)

> *  For a detailed guide on the dependency array and controlling when effects
run, read **"useEffect"**:
[https://react.dev/reference/react/useEffect](https://react.dev/reference/react/
useEffect)

> *  This pattern is so common that React has a dedicated page for it:
**"Fetching data with Effects"**: [https://react.dev/learn/synchronizing-witheffects#fetching-data](https://react.dev/learn/synchronizing-witheffects#fetching-data)

```

