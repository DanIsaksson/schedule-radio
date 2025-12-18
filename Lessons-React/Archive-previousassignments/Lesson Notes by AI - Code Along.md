### **Table of Contents**

React Lesson: Refactoring for a Scalable Layout......................................................................................2

Part 1: Structuring the Main App Layout..............................................................................................2

Step 1: Analyzing the Existing DOM Structure...............................................................................2

Mentor's Notes............................................................................................................................. 3

<div> vs. React Fragment (<>):..............................................................................................3
Why use an id?........................................................................................................................4
Step 3: Styling the Main Container..................................................................................................4

src/App.css...................................................................................................................................4
Step 4: Creating a Reusable `CustomTable` Component.................................................................5

Mentor's Notes............................................................................................................................. 5

Step 5: Passing Data via Props.........................................................................................................6

src/App.jsx (Updated)..................................................................................................................6
src/components/CustomTable.jsx (Updated)...............................................................................7
Mentor's Notes............................................................................................................................. 7

Understanding Props:..............................................................................................................7
Conditional Rendering with the Ternary Operator:.................................................................8
Rendering Lists with .map() and the key Prop:.......................................................................8
Part 2: Adding Interactivity to a Reusable Component.........................................................................9

Step 6: The Challenge with Event Handlers in Child Components..................................................9
Step 7: Passing Functions as Props...................................................................................................9

src/App.jsx (Updated)..................................................................................................................9
Mentor's Notes...........................................................................................................................11
Step 8: Implementing the Action Button in CustomTable.jsx........................................................12

src/components/CustomTable.jsx (Updated).............................................................................12
Mentor's Notes........................................................................................................................... 13
Step 9: Making the Button Column Conditional............................................................................14

src/components/CustomTable.jsx (Final)..................................................................................14
Mentor's Notes........................................................................................................................... 16


# **React Lesson: Refactoring for a Scalable Layout**

Welcome! In this lesson, we'll transform a single-file application into a more organized and scalable
structure. We'll focus on creating a consistent layout and breaking down our UI into reusable
components. This is a fundamental skill for building larger, more manageable React applications.

## **Part 1: Structuring the Main App Layout**


Our goal is to create a centralized layout for our application and begin organizing our code into logical,
reusable pieces.

#### **Step 1: Analyzing the Existing DOM Structure**


**(Video Timestamp: 00:20)**


Before we write any code, let's look at what our browser is currently rendering. If you use your
browser's developer tools (Right-Click -> Inspect), you'll see how our React application is injected into

the HTML.


code Html

```
  <!-- index.html -->
<body>
 <div id="root" >
  <!-- Our React App gets rendered here -->
 </div>
</body> ```

You'll notice a `div` with an `id` of `"root"`. This is the entry point where our
entire React application lives. The instructor points out that while we could apply
styles directly to this `root` element, it's better practice to create a dedicated
container *inside* our React app for layout and styling. This gives us more control
and keeps our styling separate from the main HTML document.

### Step 2: Creating a Main Container in `App.jsx`

**(Video Timestamp: 01:35)**

Currently, our main `App.jsx` component uses a React Fragment (`<>...</>`) to
return multiple elements. A fragment is great, but it doesn't render an actual HTML
element in the DOM. To create a container we can style, we'll replace the fragment
with a `<div>`.

Let's modify `App.jsx` to include a main container `div` that will wrap all of our
application's content. We'll give it an `id` of `"main-container"` so we can easily
style it.

#### `src/App.jsx`

```

```
```javascript
// ... imports and component logic ...

function App() {
 // ... state and other logic ...

 return (
  <div id="main-container" >
    <Menu styling={"menu"} entries={menuItems} />
    <div>

     <Outlet />

     <h2> Saved search </h2>
     {/* The first table for "Saved search" */}
     <Table striped hover >
      {/* ... table content ... */}
     </Table>

     <h2> Search </h2>
     {/* The second table for search results */}
     <Table striped hover >
      {/* ... table content ... */}
     </Table>

     {/* ... rest of the form and other elements ... */}
    </div>

  </div>
 );
}

export default App;

```

<br>

##### **Mentor's Notes**


**<div> vs. React Fragment (<>):**
A <div> is a standard HTML element that gets rendered into the DOM. It acts as a container that you
can see in your browser's inspector and, most importantly, apply CSS styles to (like id, className,
etc.).


A React Fragment, written as <>...</> or <React.Fragment>...</React.Fragment>, is a special
React component that lets you group a list of children without adding an extra node to the
DOM. Think of it as an invisible wrapper. You would use a fragment when you need to return
multiple elements from a component, but you don't want or need an actual container element.
Since our goal here is to create a visible, stylable container for our whole app, a <div> is the
right choice.


       - **Learn More:** [Read about Fragments on the official React docs: React.dev - <Fragment>](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Freference%2Freact%2FFragment)


**Why use an id?**

In CSS, an id is meant to be a **unique identifier** for one, and only one, element on a page. Since our
application will only ever have one "main container," using an id like #main-container is a clear and
appropriate way to target it for styling.


The alternative is className, which is used for styles that you might want to apply to _many_ different
elements (e.g., a .button class). We'll use className more for our reusable components, but for a oneof-a-kind layout element, id is a great choice.


   - **Further Learning:** Learn how to pass attributes like id and className to your elements in
[React Docs: Describing the UI.](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fdescribing-the-ui%23specifying-attributes-with-jsx)

#### **Step 3: Styling the Main Container**


**(Video Timestamp: 02:20)**


Now that we have our container, let's add some simple CSS to center it on the page and give it a
maximum width. This prevents our app from stretching uncomfortably wide on large screens.


We will add the following styles to our App.css file.


**src/App.css**


code CSS

```
  # main-container {
 margin : 0 auto ;
 width : 1000px;
}```

```

**margin: 0 auto; Explained:**
This is a classic CSS trick for centering block-level elements horizontally. Let's break it down:


       - margin: 0 auto; is shorthand for setting the top/bottom margin to 0 and the left/right

margin to auto.


       - When you set a block element's width (like we did with width: 1000px;) and set its left
and right margins to auto, the browser automatically calculates and distributes the
remaining horizontal space equally on both sides. The result? A perfectly centered

element.


   - **Fixed vs. Fluid Width:**

We chose a fixed width of 1000px. This means our container will always be 1000 pixels wide,
regardless of the screen size. This is simple and predictable.


An alternative is a "fluid" or responsive width, like width: 80%;. This would make the container
take up 80% of the browser window's width, so it would shrink and grow as the window is
resized. For more advanced layouts, you often combine these ideas, for example: width: 80%;


max-width: 1000px;. This makes the layout fluid, but stops it from becoming too wide on very
large screens.


With these changes, our application now has a consistent, centered layout. When you refresh the
browser, you'll see that the content is neatly contained within a 1000-pixel-wide column in the center of
the page.

#### **Step 4: Creating a Reusable `CustomTable` Component**


****(Video Timestamp: 03:48)****


Looking at `App.jsx`, we can see we have two very similar table structures: one for "Saved search" and
another for "Search" results. This is a perfect opportunity for refactoring! Repeating code makes
maintenance difficult; if you need to change the table structure, you have to do it in multiple places.


Let's create a single, reusable `CustomTable` component that can handle displaying *any* set of
records we give it.


First, create a new file: `src/components/CustomTable.jsx`.


Next, we'll define the basic structure of our new component. For now, it will just be a shell.

```
#### `src/components/CustomTable.jsx`

```javascript
import React from 'react';
import { Table } from 'react-bootstrap';

const CustomTable = () => {
 return (
  // We will move the table logic here
);
};

export default CustomTable;

```

<br>


**Mentor's Notes**


      - **What is Componentization?**


This is the core idea of React! Instead of thinking of our UI as one giant block of HTML,
we break it down into small, independent, and reusable pieces called components. Each
component manages its own state and logic. In our case, instead of having two different
<table...> blocks in App.jsx, we are creating a single CustomTable component that we can
reuse. This makes our code cleaner, easier to debug, and much simpler to update.


       - **Learn More:** [This is a foundational concept. Get a great introduction here: React.dev -](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fyour-first-component)

[Your First Component](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fyour-first-component)


   - **File Naming Conventions:**
In the React ecosystem, it's a strong convention to name component files using **PascalCase**
(e.g., CustomTable.jsx, UserProfile.jsx). This makes it easy to distinguish component files from
other JavaScript files (which are often camelCase) at a glance. It also matches the way we name
the components themselves inside the code (const CustomTable = ...).

#### **Step 5: Passing Data via Props**


**(Video Timestamp: 04:13 & 08:01)**


Our CustomTable needs data to display. We can't just copy and paste the table from App.jsx because the
data it uses (backendData and search) only exists in App.jsx.


The solution is to pass this data from the parent (App) to the child (CustomTable) using **props** . We'll
pass the array of records that each table should display.


Let's update App.jsx to use our new CustomTable component and pass the appropriate data to each one.


**src/App.jsx (Updated)**


code JavaScript

```
// Import the new component at the top
import CustomTable from './components/CustomTable.jsx';
// ... other imports

function App() {
 // ... state and other logic ...

 return (
  <div id="main-container">
{/* ... Menu and other elements ... */}

    <h2>Saved search</h2>
    <CustomTable records={backendData} />

    <h2>Search</h2>
    <CustomTable records={search} />

{/* ... rest of the form ... */}
  </div>
);
}

```

Now, let's update CustomTable.jsx to receive and use these props. We'll also move the table rendering
logic from App.jsx into our new component.


**src/components/CustomTable.jsx (Updated)**


code JavaScript

```
import React from 'react';
import { Table } from 'react-bootstrap';

const CustomTable = ({ records }) => {
 return (
  <Table striped hover>
    <thead>
{/* You can add table headers here if needed */}
    </thead>
    <tbody>
{records && records.length > 0 ? (
records.map((item) => (
       <tr key={item.identifier}>
        <td>{item.creator}</td>
        <td>{item.title}</td>
{/* We'll add the button back in the next part */}
       </tr>
))
) : (
      <tr>
       <td colSpan="2">No records found.</td>
      </tr>
)}
    </tbody>
  </Table>
);
};

export default CustomTable;

##### **Mentor's Notes**

```

**Understanding Props:**
"Props" (short for properties) are how you pass data from a parent component down to a child
component. It's a one-way data flow. The parent component holds the data (the "source of truth"),
and it passes that data to its children, which then use it to render something.


In our App.jsx, we are passing a prop named records to each <CustomTable />. The value of this prop
is the data array that the table needs to display.


code Code

```
Parent (App.jsx) ---> Child (CustomTable.jsx)
re cords={data}

```

   - **Learn More:** [This is essential reading for any React developer: React.dev - Passing Props to a](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)

[Component](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)


**Destructuring Props ({ records }):**
Inside the CustomTable component, you'll see the function signature is const CustomTable =
({ records }) =>. This is modern JavaScript syntax called "destructuring." It's a shortcut to pull
the records property directly out of the props object.


Without destructuring, we would have to write:


code JavaScript

```
const CustomTable = (props) => {
 // then access records with props.records

return (

 <tbod y>

  {props.records.map(item => ...)}

 </tbod y>

 );

};

```

   - Destructuring is cleaner and makes it immediately clear what props the component expects to

receive.


**Conditional Rendering with the Ternary Operator:**
What if the records array is empty or hasn't loaded yet? We don't want to show an empty table.
The line records && records.length > 0 ? ... : ... is a form of conditional rendering.


   - records && records.length > 0 checks if the records array exists AND has items in it.


   - The ? (ternary operator) acts like an if/else statement. If the condition is true, it runs the code
before the colon (:).


   - If the condition is false, it runs the code after the colon—in our case, rendering a single row that
says "No records found."


   - **Learn More:** [Explore different ways to render content conditionally: React.dev - Conditional](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering)
[Rendering](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering)


**Rendering Lists with .map() and the key Prop:**
This is the standard way to render a list of items in React. The JavaScript .map() method is used
to iterate over an array (records) and return a new array of JSX elements (our <tr> table rows).
React then renders this array of elements.


The key prop is crucial. It gives each element in the list a stable identity. React uses this key to track
which items have changed, been added, or been removed. This helps React update the UI efficiently.


The key should be a unique string or number that identifies the item among its siblings, like a database

ID.


   - **Learn More:** [Master how to display lists of data: React.dev - Rendering Lists](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Frendering-lists)

## **Part 2: Adding Interactivity to a Reusable Component**


We have successfully refactored our tables into a reusable CustomTable component. However, in the
process, we removed the "Add record" button. Now, we'll add that functionality back in a way that
keeps our component flexible and reusable.

#### **Step 6: The Challenge with Event Handlers in Child Components**


**(Video Timestamp: 9:14)**


The "Add record" button has an onClick event that needs to update the backendData state. The problem
is that the state and its setter function (setBackendData) "live" in the App.jsx component (the parent).
Our CustomTable component (the child) has no direct access to them.


This is a very common scenario in React: **How does a child component tell a parent component to**
**change something?**


We can't just move the state into CustomTable, because the "Saved search" table is the only one that
should affect that specific piece of state. The other table shouldn't. The parent component needs to
remain in control of the application's state.

#### **Step 7: Passing Functions as Props**


**(Video Timestamp: 11:23)**


The solution is the same as before: **props!** Just as we can pass data (like strings and arrays) down to a
child, we can also pass _functions_ down as props.


We will pass the setBackendData function and the current backendData array from App.jsx down to our
CustomTable components. This allows the child component to call the parent's state-setting function

when an event occurs.


Let's update App.jsx to pass these new props.


**src/App.jsx (Updated)**


code JavaScript
```
  // ... imports and component logic ...

```

```
function App() {

 const [backendData, setBackendData] = useState([]);

 const [search, setSearch] = useState(libraryData.xsearch.list);

 // ... other logic ...

 return (

  <div id="main-container" >

    {/* ... Menu ... */}

    <div>

     {/* ... */}

     <h2> Saved search </h2>

     <CustomTable

      records={backendData}

     />

     <h2> Search </h2>

     <CustomTable

      records={search}

      setBackendData={setBackendData}

      backendData={backendData}

     />

     {/* ... rest of the form ... */}

    </div>

  </div>

 );

}

```

Notice we are _only_ passing these new props to the second table, because it's the only one that should
have the "Add record" functionality. This is key to making our component adaptable.


<br>


#### **Mentor's Notes**

**Child-to-Parent Communication:**

In React, data flows in one direction: from parent to child via props. A child component should
not directly modify its parent's state. So, how do we handle a button click in a child that needs to
update the parent?


The standard pattern is for the parent to pass a _function_ down to the child as a prop. The child doesn't
know or care what the function does; it just knows to call it when a specific event happens (like
onClick). This is the "callback" pattern. The parent defines _what_ should happen, and the child decides
_when_ it happens. This keeps the state management centralized in the parent, making your application

much easier to reason about.


code Code

```
Parent (App.jsx)

|

| props: { records, setBackendData }

V

Child (CustomTable.jsx) --- calls setBackendData() on click ---> Parent's state is
updated

```

   - **Learn More:** [This is a key concept for managing state in React applications: React.dev -](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)
[Sharing State Between Components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)


**Prop Drilling:**
**When you pass props down through multiple layers of components, it's sometimes called**
**"prop drilling." Imagine if CustomTable didn't use the props itself but had another child**
**component that needed them. We would have to pass them through CustomTable just to get**

**them to their destination.**


For our simple case, this is perfectly fine. However, in very large applications with
deeply nested components, prop drilling can become cumbersome. This is where
more advanced state management tools like the **Context API** come in handy.
Context allows you to make data available to any component in the tree below
without passing it down manually at every level.


          - **Learn More:** Understand how to avoid excessive prop drilling with
[Context: React.dev - Passing Data Deeply with Context](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-data-deeply-with-context)


#### **Step 8: Implementing the Action Button in CustomTable.jsx**

**(Video Timestamp: 11:05)**


Now we need to update CustomTable.jsx to receive these new props and use them. We will also add

back the button.


First, let's destructure the new props. Then, we'll add a new table header (<th>) for the column that will
hold our buttons. Finally, inside our .map() function, we'll add a new table data cell (<td>) containing
the button. The button's onClick handler will now call the setBackendData function that was passed
down from the parent.


**src/components/CustomTable.jsx (Updated)**


code JavaScript

```
  import React from 'react';

import { Table, Button } from 'react-bootstrap';

const CustomTable = ({ records, setBackendData, backendData }) => {

 return (

  <Table striped hover>

    <thead>

     <tr>

      <th>Creator</th>

      <th>Title</th>

      <th>Action</th> {/* Header for our button column */}

     </tr>

    </thead>

    <tbody>

{records && records.length > 0 ? (

records.map((item) => (

       <tr key={item.identifier}>

        <td>{item.creator}</td>

        <td>{item.title}</td>

        <td>

          <Button

size="sm"

onClick={() => {

```

```
setBackendData([

             ...backendData,

{

creator: item.creator,

title: item.title,

identifier: item.identifier

}

]);

}}

          >

Add record

          </Button>

        </td>

       </tr>

))

) : (

      <tr>

       <td colSpan="3">No records found.</td>

      </tr>

)}

    </tbody>

  </Table>

);

};

export default CustomTable;

```

<br>

#### **Mentor's Notes**


      - **Handling Events in React:**
In React, we handle events like clicks, form submissions, and mouse movements by
providing a function to a special prop, like onClick. It's important to notice the
syntax we used:


onClick={() => { ... }}


We are passing an **arrow function** . This is critical. If we had just written
onClick={setBackendData(...)}}, the setBackendData function would be called
_immediately_ when the component renders, which is not what we want. By wrapping
it in an arrow function, we are telling React, "Here is the function you should

execute _when_ the button is clicked."


          - **Learn More:** This is a fundamental skill for making your apps interactive:
[React.dev - Responding to Events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)


      - **The Spread Syntax for Arrays (...):**
One of the golden rules in React is to **treat state as immutable** . This means you
should never modify an existing state object or array directly. Instead, you create a
_new_ one with the updated data.


The line setBackendData([...backendData, newItem]) is the standard, modern way
to do this. The spread syntax (...backendData) takes all of the existing items out of
the backendData array and puts them into a new array. Then, we add our newItem to
the end of that new array. This creates a brand new array, which we then use to
update the state. React detects this new array as a change and re-renders the

component.


          - **Learn More: Master the correct way to handle arrays in state:**
**[React.dev - Updating Arrays in State](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fupdating-arrays-in-state)**

#### **Step 9: Making the Button Column Conditional**


**(Video Timestamp: 12:40)**


There's one last problem. We added an "Action" header and a button to _every_ row. But what about our
first table, the "Saved search" one? We didn't pass setBackendData to it, so it shouldn't have an action
column at all. Right now, it would crash because setBackendData is undefined.


We can fix this with **conditional rendering** . We'll only render the "Action" header and the button
column if the setBackendData prop exists.


**src/components/CustomTable.jsx (Final)**


code JavaScript

```
  import React from 'react';

import { Table, Button } from 'react-bootstrap';

const CustomTable = ({ records, setBackendData, backendData }) => {

 return (

```

```
  <Table striped hover>

    <thead>

     <tr>

      <th>Creator</th>

      <th>Title</th>

{/* Only render this header if the function is provided */}

{setBackendData && <th>Action</th>}

     </tr>

    </thead>

    <tbody>

{records && records.length > 0 ? (

records.map((item) => (

       <tr key={item.identifier}>

        <td>{item.creator}</td>

        <td>{item.title}</td>

{/* Only render this table cell if the function is provided */}

{setBackendData && (

          <td>

           <Button

size="sm"

onClick={() => {

setBackendData([

              ...backendData,

{

creator: item.creator,

title: item.title,

identifier: item.identifier

}

]);

}}

           >

Add record

           </Button>

          </td>

```

```
)}

       </tr>

))

) : (

      <tr>

{/* Adjust colSpan based on whether the action column is present */}

       <td colSpan={setBackendData ? "3" : "2"}>No records found.</td>

      </tr>

)}

    </tbody>

  </Table>

);

};

export default CustomTable;

```

Now our CustomTable component is truly reusable. It can display a simple, read-only table, or it can
display a table with an action button, all depending on the props we pass to it. This pattern of making
components configurable through props is fundamental to building powerful and flexible UIs in React.


<br>

#### **Mentor's Notes**


**Conditional Rendering with &&:**
**The {condition && <SomeComponent />} syntax is a concise and common way to do**
**conditional rendering in JSX. It's a JavaScript trick that works beautifully in React. Here's**

**how it works:**


       - In JavaScript, the && (logical AND) operator evaluates the expression on its left.


       - If the left side is "falsy" (e.g., null, undefined, false), the whole expression immediately
returns that falsy value, and React renders nothing.


       - If the left side is "truthy" (e.g., a function, an object, a number), the expression then
evaluates and returns the value on its right. In our case, that's the JSX for our header or

button cell.


So, setBackendData && <th>Action</th> is a shorthand for "If the setBackendData prop exists
(is truthy), render the <th>Action</th> element." It's a clean way to make parts of your
component appear or disappear based on the props it receives.


       - **Learn More:** [Review this and other patterns for conditional rendering: React.dev -](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering)
[Conditional Rendering](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering)


This concludes our lesson on refactoring and creating reusable, interactive components. Let me know if
you're ready for the final set of mentor's notes


