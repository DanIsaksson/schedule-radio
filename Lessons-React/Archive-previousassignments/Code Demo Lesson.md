## **Table of Contents**

Introduction to React Router......................................................................................................................2


createBrowserRouter.............................................................................................................................2
Route Configuration..............................................................................................................................2
Nested Routes & Outlet.........................................................................................................................3
Project Refactoring: From Monolith to Pages...........................................................................................4

File & Component Organization...........................................................................................................4

Creating a pages directory to house our page-level components.....................................................4
Renaming component files to reflect their purpose (e.g., File.jsx to Search.jsx).............................4
Deleting unused component files to clean up the project.................................................................5
Updating Routing in main.jsx...............................................................................................................5

Defining clear and descriptive routes for our new pages (/, /search, /saved)...................................5
Creating a Navigation Menu.................................................................................................................6

Creating a navigation.js configuration file to manage menu items centrally...................................6
Building a Menu.jsx component that dynamically renders navigation links from the configuration
file.....................................................................................................................................................7
State Management Refactoring..................................................................................................................7

Colocating State....................................................................................................................................8

Moving useState hooks from App.jsx to the child components (Search.jsx, Saved.jsx) that own
the state.............................................................................................................................................8
Moving useEffect hooks for data fetching to the pages that require that specific data....................8
Moving event handler functions (actOnSubmit, postRecord) into the components where the
events originate.................................................................................................................................9
Using React Context for Global State.................................................................................................10

Creating a ThemeContext to manage the application's theme (e.g., light/dark).............................10
Providing the context in App.jsx to make it available to all child components..............................11
Consuming the context with the useContext hook in the Menu.jsx component to toggle the theme.
........................................................................................................................................................ 12

UI Enhancements..................................................................................................................................... 12

Using an Icon Library (react-icons)....................................................................................................12

Replacing text-based buttons with icons for a cleaner, more intuitive interface............................13
Improving Table Layout......................................................................................................................13

Using CSS utility classes to truncate long text within table cells, preventing layout issues..........13
References................................................................................................................................................14


# **Introduction to React Router**

React Router is a standard library for routing in React. It enables the navigation among views of
various components in a React Application, allows changing the browser URL, and keeps the UI in
sync with the URL. You can learn more about the createBrowserRouter API at
[https://reactrouter.com/en/main/routers/create-browser-router. While not a part of the allowed domains,](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Frouters%2Fcreate-browser-router)
it is the official documentation for the library used. We will use it to define the navigational structure of
our application.

## **createBrowserRouter**


A function that creates a router instance that can be used to render a React application.


This function is the recommended way to create a router for web projects using React Router. It takes
an array of route objects as an argument and returns a router instance. This approach enables Data
APIs, which allow routes to load data before they render.
**code JavaScript**
```
// main.jsx
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "./App";
import Search from "./pages/Search";

const router = createBrowserRouter([
{
path: "/",
element: <App />,
children: [
{
path: "/search",
element: <Search />,
},
],
},
]);

// In your render function:
// <RouterProvider router={router} />

```

   - _Gotcha: The router configuration is defined outside of your component tree. You then provide_
_the created router instance to the <RouterProvider /> component at the root of your_
_application._

## **Route Configuration**


An array of route objects, each defining a path and the component to render for that path.


Each route is an object with properties that define the URL path and the component that should be
rendered. The path property is a string that matches a URL segment, and the element property is the
JSX to render when the path matches.


**code JavaScript**
```
// main.jsx
const router = createBrowserRouter([
 {
  path: "/",
  element: <Home />,
 },
 {
  path: "/search",
  element: <Search />,
 },
 {
  path: "/saved",
  element: <Saved />,
 },
]);

```

   - _Gotcha: A common mistake is to forget the leading slash / in the path, which can lead to_
_unexpected routing behavior. Absolute paths should always start with /._

## **Nested Routes & Outlet**


A mechanism to render child routes within a parent route's component, allowing for complex layouts.


Nested routes allow you to build complex, hierarchical UI layouts that correspond to URL segments. A
parent route renders a layout and uses the <Outlet /> component to render the appropriate child route's

element based on the current URL.


**code JavaScript**
```
// Parent Component: App.jsx
import { Outlet } from "react-router-dom";
import Menu from "./components/Menu";

function App() {
 return (
  <div>

    <Menu />

    <main>
{/* Child route components will be rendered here */}
     <Outlet />

    </main>

  </div>
);
}

```

   - _Gotcha: If you define child routes in your createBrowserRouter configuration but forget to_
_include an <Outlet /> component in the parent route's element, the child components will never_

_be rendered._

# **Project Refactoring: From Monolith to Pages**


We will restructure our application from a single large component into a more organized structure with
distinct pages. This improves maintainability and scalability.

## **File & Component Organization**

### **Creating a pages directory to house our page-level components.**


Organizing your project by feature or route is a common practice. Creating a src/pages directory helps
separate top-level components that correspond to different pages of your application from smaller,
reusable components (which might live in src/components).


code Text

```
/src

|-- /components

|  |-- Menu.jsx

|  +-- SearchTable.jsx

|-- /pages

|  |-- Home.jsx

|  |-- Search.jsx

|  +-- Saved.jsx

|-- App.jsx

+-- main.jsx

```

   - _Gotcha: Be consistent with your file and folder naming conventions (e.g., PascalCase for_
_components, lowercase for directories). Inconsistency makes projects harder to navigate as they_

_grow._

### **Renaming component files to reflect their purpose (e.g., File.jsx to** **Search.jsx).**


Give your files and components descriptive names that clearly state their purpose. A file named File.jsx
is vague, while Search.jsx or Saved.jsx immediately tells you what part of the application it represents.


**code JavaScript**


```
// Before: A generic name that doesn't describe the page's content.

import File from "./pages/File.jsx";

// After: A descriptive name that clearly indicates the page's function.

import Search from "./pages/Search.jsx";

import Saved from "./pages/Saved.jsx";

```

   - _Gotcha: When renaming files, especially in a larger project, it's easy to miss an import_
_statement. Use your IDE's "Refactor/Rename" feature to automatically find and update all_
_usages of the file across your project._

### **Deleting unused component files to clean up the project.**


As you refactor, you will inevitably create files that are no longer needed. Deleting these files keeps
your codebase clean, reduces clutter, and makes it easier for other developers (and your future self) to
understand the project structure.


**code Text**

```
// In the video, several files like Birdy.jsx, Cage.jsx, and Feather.jsx

// were part of an initial example and are no longer used.

// These can be safely deleted from the /components directory.

```

   - _Gotcha: Before deleting a file, use your IDE's "Find Usages" feature to ensure it isn't being_
_imported anywhere. Accidentally deleting a file that is still in use will crash your application._

## **Updating Routing in main.jsx**

### **Defining clear and descriptive routes for our new pages (/, /search,** **/saved).**


After creating and renaming your page components, you must update your router configuration in
main.jsx to point to the new files and paths. This connects the URL in the browser to the correct

component.


**code JavaScript**
```
// main.jsx

import App from "./App.jsx";

import Home from "./pages/Home.jsx";

```

```
import Search from "./pages/Search.jsx";

import Saved from "./pages/Saved.jsx";

const router = createBrowserRouter([

{

path: "/",

element: <App />,

children: [

{ path: "/home", element: <Home /> },

{ path: "/search", element: <Search /> },

{ path: "/saved", element: <Saved /> },

],

},

]);

```

   - _Gotcha: A typo in the file path of an import statement (e.g., ./page/Search.jsx instead of_
_./pages/Search.jsx) is a common error that will cause the application to fail to compile._

## **Creating a Navigation Menu**

### **Creating a navigation.js configuration file to manage menu items centrally.**


For a dynamic navigation menu, it's good practice to define the menu items in a separate configuration
file. This makes it easy to add, remove, or reorder menu items without changing the component's logic.


code JavaScript
```
// src/siteconfigurations/navigation.js

export const menuItems = [

 { content: "Home", index: 1 },

 { content: "Search", index: 2 },

 { content: "Saved", index: 3 },

];

```

   - _Gotcha: Ensure you export the configuration array from the file so it can be imported and used_
_by other components, such as Menu.jsx._


### **Building a Menu.jsx component that dynamically renders navigation links** **from the configuration file.**

This component imports the menuItems array and uses the .map() method to render a list of navigation
links. Using NavLink from react-router-dom is beneficial as it can automatically apply styling to
indicate the active page.


**code JavaScript**
```
// src/components/Menu.jsx

import { NavLink } from "react-router-dom";

import { menuItems } from "../siteconfigurations/navigation";

function Menu() {

 return (

  <nav>

{menuItems.map((entry) => (

     <NavLink to={`/${entry.content}`} key={entry.index}>

{entry.content}

     </NavLink>

))}

  </nav>

);

}

```

   - _Gotcha: When rendering a list of elements from an array in React, you must provide a unique_
_key prop to each element in the list. Forgetting this will result in a console warning and can_
_lead to inefficient rendering._

# **State Management Refactoring**


We will move state and logic from a central App.jsx component down into the specific pages where
they are used. This concept is often called "lifting state down" or "colocation."


## **Colocating State**

### **Moving useState hooks from App.jsx to the child components (Search.jsx,** **Saved.jsx) that own the state.**

Colocation means placing state as close as possible to where it is used. Instead of keeping all state in a
top-level App component, move it down to the specific component that needs it. This makes
components more self-contained and easier to reason about.


**code JavaScript**
```
// src/pages/Saved.jsx

import { useState, useEffect } from "react";

import RecordsTable from "../components/RecordsTable";

import axios from "axios";

function Saved() {

const [backendData, setBackendData] = useState([]);

useEffect(() => {

axios.get("http://localhost:5287/api/references").then((response) => {

setBackendData(response.data);

});

}, []);

 return <RecordsTable backendData={backendData} />;

}

```

   - _Gotcha: If you move state down and realize that a sibling component also needs access to it,_
_that's a sign the state should be "lifted up" to the nearest common parent component._

### **Moving useEffect hooks for data fetching to the pages that require that** **specific data.**


Data fetching logic should also be colocated. If the Saved.jsx page is responsible for displaying saved
records, the useEffect hook that fetches those records belongs inside Saved.jsx, not in App.jsx.


**code JavaScript**
```
// src/pages/Saved.jsx

```

```
function Saved() {

 const [backendData, setBackendData] = useState([]);

 // This useEffect now lives inside the component that uses its data.

 useEffect(() => {

  axios.get("http://localhost:5287/api/references").then((response) => {

    setBackendData(response.data);

  });

 }, []); // Empty dependency array means this runs once on mount.

 return (

  <div>

    <h2> Saved Search </h2>

    <RecordsTable backendData={backendData} />

  </div>

 );

}

```

   - _Gotcha: When moving a useEffect hook, double-check its dependency array. If it depends on_
_props or other state, ensure those are also available in the new component, or you might_
_introduce bugs like infinite re-renders or stale data._

### **Moving event handler functions (actOnSubmit, postRecord) into the** **components where the events originate.**


Functions that handle events, like form submissions or button clicks, should be defined in the

component where the event occurs. This keeps the logic for user interactions tightly coupled with the
UI elements that trigger them.


**code JavaScript**
```
// src/pages/Search.jsx

function Search() {

 // ... other state and hooks

 const actOnSubmit = (data) => {

  console.log("search", data);

```

```
  const query = `${data.authorFirstName} ${data.authorLastName}`;

  setSearchString(`http://libris.kb.se/xsearch?query=forf:(${query})`);

 };

 return (

  <form onSubmit={handleSubmit(actOnSubmit)} >

    {/* Form inputs */}

  </form>

 );

}

```

   - _Gotcha: When moving a handler function, you may need to pass it down as a prop to a child_
_component. If you find yourself passing a function down through many layers of components_
_("prop drilling"), it might be a candidate for React Context or another state management_

_solution._

## **Using React Context for Global State**


React Context provides a way to pass data through the component tree without having to pass props
[down manually at every level. You can read more about it at https://react.dev/learn/passing-data-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-data-deeply-with-context)
[deeply-with-context.](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-data-deeply-with-context)

### **Creating a ThemeContext to manage the application's theme (e.g.,** **light/dark).**


createContext lets you create a context object. When React renders a component that subscribes to this
object, it will read the current context value from the closest matching Provider up in the tree.


**code JavaScript**
```
// src/contexts/contexts.js

import { createContext } from "react";

// Create a context for the theme.

// The default value is only used when a component does not have a matching

// Provider above it in the tree.

export const ThemeContext = createContext(null);

```

   - _Gotcha: Exporting the context from its own file (contexts.js) is a good practice that allows any_
_component in your application to import and use it without creating import cycles._

### **Providing the context in App.jsx to make it available to all child** **components.**


To make a context value available to all components in a tree, you wrap a parent component with the
context's Provider component and pass the value you want to share via the value prop.


**code JavaScript**
```
// App.jsx

import { useState } from "react";

import { ThemeContext } from "./contexts/contexts";

function App() {

const [theme, setTheme] = useState("light");

 return (

  // The value prop makes the theme and setTheme function available

  // to any descendant component that consumes this context.

  <ThemeContext.Provider value={{ theme, setTheme }}>

    <Container>

     <Menu />

     <Outlet />

    </Container>

  </ThemeContext.Provider>

);

}

```

   - _Gotcha: Every time the value prop on the provider changes, all descendant components that use_
_that context will re-render. Be careful not to pass a new object or array literal_
_(value={{ theme }}) on every render unless necessary, as this can cause performance issues._


### **Consuming the context with the useContext hook in the Menu.jsx** **component to toggle the theme.**

The useContext hook lets a component "subscribe" to a context. It accepts a context object (the value
returned from React.createContext) and returns the current context value for that context.


**code JavaScript**
```
// src/components/Menu.jsx

import { useContext } from "react";

import { ThemeContext } from "../contexts/contexts";

function Menu() {

const { theme, setTheme } = useContext(ThemeContext);

const toggleTheme = () => {

setTheme(theme === "light" ? "dark" : "light");

};

 return (

  <nav>

{/* ... nav links */}

    <button onClick={toggleTheme}>Toggle Theme</button>

  </nav>

);

}

```

   - _Gotcha: The useContext hook must be called inside a component that is a descendant of the_
_corresponding ThemeContext.Provider. If it's not, the value returned will be the default value_
_passed to createContext, which can lead to errors if it's null or undefined._

# **UI Enhancements**


Finally, we will make some small user interface improvements for a better user experience.

## **Using an Icon Library (react-icons)**


### **Replacing text-based buttons with icons for a cleaner, more intuitive** **interface.**

Libraries like react-icons provide a vast collection of popular icons as React components. This is often
easier and more flexible than managing SVG files manually. You can import just the icons you need,
which helps keep your application's bundle size small.


**code JavaScript**
```
// src/components/SearchTable.jsx

import { MdOutlineSaveAlt } from "react-icons/md";

// ... inside the component

<button onClick={() => postRecord(row.original)}>

 <MdOutlineSaveAlt />

</button>

```

   - _Gotcha: When importing from react-icons, be specific about the icon set (e.g., react-icons/md_
_for Material Design, react-icons/fa for Font Awesome). Importing from the main react-icons_
_entry point can pull in all icon sets, unnecessarily bloating your app._

## **Improving Table Layout**

### **Using CSS utility classes to truncate long text within table cells,** **preventing layout issues.**


When displaying data in tables, some fields like titles or descriptions can be very long and break the
layout. Using CSS to truncate the text adds an ellipsis (...) to indicate that the content is overflowing,
keeping your UI neat.


**code JavaScript**
```
// In your CSS file (e.g., index.css)

.text-truncate {

 overflow: hidden ;

 text-overflow: ellipsis ;

 white-space: nowrap ;

}

// In your component's JSX

<tr className="text-truncate">

 <td>A very long title that would otherwise wrap to multiple lines...</td>

```

```
</tr>

```

   - _Gotcha: For text-overflow: ellipsis to work, the element must have overflow: hidden, white-_
_space: nowrap, and a defined width (either explicitly or by being in a container like a table cell_
_that constrains its width)._

# **References**


**1.1. createBrowserRouter** [: React Router: createBrowserRouter](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Frouters%2Fcreate-browser-router)

This is the official documentation for the createBrowserRouter function. It explains its purpose,
parameters, and provides examples of how to set up a router for a web application. While not on the
allowed domains, it is the canonical source for this library.


   - **1.2. Route Configuration** [: React Router: Route Objects](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Froute%2Froute)
This page details the properties of a route object, such as path, element, loader, and action,
which are used to configure the behavior of each route in your application.


   - **1.3. Nested Routes & Outlet** : [React Router: Outlet](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fcomponents%2Foutlet)

Learn how the <Outlet> component works as a placeholder in parent route elements to render
their child route elements. This is fundamental for creating nested layouts.


   - **2.1.1. File & Component Organization** [: React Docs: Keeping Components Pure](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fkeeping-components-pure)
While not strictly about file structure, this article explains the concept of component purity and
self-containment, which is the driving principle behind organizing components into logical files

and directories.


   - **2.3.2. Building a Menu.jsx component** : [React Router: NavLink](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fcomponents%2Fnav-link)
The official documentation for NavLink, which is a special version of <Link> that knows
whether or not it is "active" and can apply styles accordingly.


   - **3.1.1. Colocating State** [: React Docs: Sharing State Between Components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)
This guide explains the principle of "lifting state up" to the nearest common ancestor, which is
the core concept to understand when deciding where state should live. Colocating state is the
process of moving state down when it's no longer needed by multiple sibling components.


   - **3.1.2. useEffect for Data Fetching** : [React Docs: You Might Not Need an Effect](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fyou-might-not-need-an-effect%23fetching-data)
This section of the React documentation provides modern patterns for fetching data in
components, explaining when and why useEffect is appropriate for this task.


   - **3.1.3. Event Handlers** [: React Docs: Responding to Events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)
This page covers the fundamentals of handling events in React, including how to define and
pass event handler functions as props.


- **3.2. React Context** [: React Docs: Passing Data Deeply with Context](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-data-deeply-with-context)
This is a comprehensive guide to the React Context API, explaining how to create, provide, and
consume context to avoid "prop drilling."


- **4.2.1. Improving Table Layout** [: MDN: text-overflow](https://www.google.com/url?sa=E&q=https%3A%2F%2Fdeveloper.mozilla.org%2Fen-US%2Fdocs%2FWeb%2FCSS%2Ftext-overflow)
The Mozilla Developer Network documentation for the text-overflow CSS property, explaining
how it can be used to create an ellipsis (...) for overflowing text.


