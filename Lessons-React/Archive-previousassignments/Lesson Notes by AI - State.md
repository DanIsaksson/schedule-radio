### **Table of Contents**

Lesson: Managing State in React with the useState Hook.........................................................................2

Part 1: Setting Up Our First Interactive Component.............................................................................2

Recap: Our Static Menu (Video Timestamp: 0:18)..........................................................................2
Introducing State with useState (Video Timestamp: 1:55)...............................................................3

[App.jsx] Mentor's Notes: useState and Array Destructuring......................................................3

_[App.jsx] Mentor's Notes: useState and Array Destructuring_ ..........................................................4
Passing the State Setter Function as a Prop (Video Timestamp: 27:40)...........................................5

[App.jsx] Mentor's Notes: Passing Functions as Props...............................................................6

[App.jsx] Mentor's Notes: useState and Array Destructuring..........................................................6

[App.jsx] Mentor's Notes: Passing Functions as Props....................................................................8
Updating State from the Child Component (Video Timestamp: 21:46)...........................................9

[Menu.jsx] Mentor's Notes: Event Handling and Triggering State Updates.............................10

_[Menu.jsx] Mentor's Notes: Event Handling and Triggering State Updates_ .................................10
Conditionally Rendering Components (Video Timestamp: 25:45)................................................11

[App.jsx] Mentor's Notes: Conditional Rendering....................................................................12

[App.jsx] Mentor's Notes: useState and Array Destructuring........................................................13

[App.jsx] Mentor's Notes: Passing Functions as Props..................................................................14

[Menu.jsx] Mentor's Notes: Event Handling and Triggering State Updates..................................15

[App.jsx] Mentor's Notes: Conditional Rendering.........................................................................17
Lesson: Managing State in React with the useState Hook.......................................................................19

Part 2: Providing User Feedback and Refactoring Our Code.............................................................19

Styling the Active Button with CSS (Video Timestamp: 0:42)......................................................19

[App.css] Mentor's Notes: CSS Pseudo-classes and Dynamic Classes.....................................19

[App.css] Mentor's Notes: CSS Pseudo-classes and Dynamic Classes..........................................20
Passing the Current State to the Child (Video Timestamp: 4:55)...................................................21

[App.jsx] Mentor's Notes: Data Flow in React..........................................................................22

_[App.jsx] Mentor's Notes: Data Flow in React_ ..............................................................................22
Conditionally Applying a CSS Class (Video Timestamp: 5:45).....................................................24

[Menu.jsx] Mentor's Notes: The Ternary Operator for Conditional Logic................................24

[Menu.jsx] Mentor's Notes: The Ternary Operator for Conditional Logic.....................................24
Refactoring Conditional Rendering with a switch Statement (Video Timestamp: 9:43)...............25

[App.jsx] Mentor's Notes: Refactoring with switch..................................................................27

[App.css] Mentor's Notes: CSS Pseudo-classes and Dynamic Classes..........................................27

[App.jsx] Mentor's Notes: Data Flow in React...............................................................................29

[Menu.jsx] Mentor's Notes: The Ternary Operator for Conditional Logic.....................................30

[App.jsx] Mentor's Notes: Refactoring with switch.......................................................................31


Of course! Let's break down this lesson on React's useState hook. Here is the first part of the
documented lesson, with placeholders for the "Mentor's Notes" as you requested.


Once you've reviewed it, just let me know, and I'll proceed with filling out all the detailed explanations.

# **Lesson: Managing State in React with the** **useState Hook**


Welcome! In our last session, we talked about building a user interface by combining different
components. Today, we're going to make those components interactive. We'll learn how to make a
component "remember" things, like which button a user has clicked, and how to update the screen in
response. This concept is called **state** .

## **Part 1: Setting Up Our First Interactive Component**

#### **Recap: Our Static Menu (Video Timestamp: 0:18)**


Let's start with the code we've seen before. We have a navigation.js file that holds an array of menu
items, and a Menu.jsx component that displays them.


**siteconfigurations/navigation.js**


code JavaScript

```
export const menuItems = [
{ content: "File", index: 1 },
{ content: "Edit", index: 2 },
{ content: "View", index: 3 },
{ content: "Format", index: 4 },
{ content: "Window", index: 5 },
{ content: "Help", index: 6 },
];```

** **
 `components/Menu.jsx`
```javascript
import React from 'react';

const Menu = ({ styling, entries }) => {
  return (
     <ul className={styling}>
{entries.map((entry) => (
          <li key={entry.index}>{entry.content}</li>
))}
     </ul>
);
};

export default Menu;

```

Right now, this menu is static. It displays the items, but clicking them does nothing. Our goal is to
make these menu items interactive so that clicking one can change what's displayed on the main part of
our page. To do that, our application needs to _remember_ which item is currently selected.

#### **Introducing State with useState (Video Timestamp: 1:55)**


In React, when we want a component to remember information that can change over time and affect
what's rendered, we use **state** . The most fundamental way to add state to a component is by using the

useState Hook.


A "Hook" is a special function that lets you "hook into" React features. useState lets us hook into

React's state management.


First, we need to import useState from React in our main App.jsx file. Then, we'll call it inside our App
component to create our "state variable."


**App.jsx**


code JavaScript

```
// STEP 1: Import useState from React
import React, { useState } from 'react';
import './App.css';
import Menu from "./components/Menu.jsx";
import { menuItems } from "./siteconfigurations/navigation.js";
import Table from "./components/Table.jsx";
import Parrot from "./components/Parrot.jsx";

function App() {
 // STEP 2: Call useState to create a state variable and a setter function
const [navigation, setNavigation] = useState(0); // Default to 0

 return (

  <>

    <Menu styling={'menu'} entries={menuItems} />
    <div>
{/* We will add our pages here later */}
    </div>
    <Table details={'five'}/>
    <Parrot />

  </>
);
}

export default App;

```

**[App.jsx] Mentor's Notes: useState and Array Destructuring**


      - App.jsx


          - **import** { useState } **from** 'react';


          - const [navigation, setNavigation] = useState(0);


#### **_[App.jsx] Mentor's Notes: useState and Array Destructuring_**

_Let's break this down. This one line is doing a lot, and it's one of the most important concepts in React._


_code JavaScript_
```
//   App.jsx

import React, { useState } from 'react';

// ...

const [navigation, setNavigation] = useState(0);

```

_**1. What is useState?**_

_Think of useState as a tool that gives your component a "memory." Normally, when a JavaScript_
_function finishes running, all its variables disappear. But since React components are functions that_
_run over and over again to draw the UI, we need a way to preserve some information between these_
_"renders." useState is the hook (a special function) that does this._


_**2. What does useState(0) do?**_
_When we call useState(0), we're telling React: "I need to keep track of a piece of information. Its very_
_first value, its initial state, should be 0."_


_**3. What does useState give back?**_
_useState always returns an array with exactly two things in it:_


   - _**Item 1 (value):**_ _The current value of your state. The first time the component renders, this will_
_be the initial value you provided (0 in our case)._


   - _**Item 2 (function):**_ _A special function that lets you update that value._


_**4. What is const [navigation, setNavigation]? (Array Destructuring)**_
_This is a modern JavaScript feature called "array destructuring." It's a shortcut to pull items out of an_
_array and put them into variables._


_Without this shortcut, the code would look like this:_


_code JavaScript_
```
// This is the longer way to do the same thing:

const navigationState = useState(0); // navigationState is an array: [0, function]

const navigation = navigationState[0]; // Get the first item (the value)

const setNavigation = navigationState[1]; // Get the second item (the function)

```

_As you can see, the [navigation, setNavigation] syntax is much cleaner! We're just naming the two_
_things that useState gives back to us. By convention, we name them [thing, setThing]._


_Here's a simple diagram to visualize the process:_


_code Code_

```
App Component

|

| calls useState(0)

|

+--> React returns [ value, function ]

|    |

|    +--> setNavigation (a function to update the
value)

+-----------> navigation (the current state value,
initially 0)

```

_By using setNavigation, you tell React to change the value of navigation and, crucially, to_ _**re-render**_
_**the component**_ _and any of its children that use this state. This is how your UI becomes dynamic!_


_**Learn More:**_


   - _**State: A Component's Memory:**_ _[https://react.dev/learn/state-a-components-memory](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory)_


   - _**useState Hook Reference:**_ _[https://react.dev/reference/react/useState](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Freference%2Freact%2FuseState)_


   - _**Responding to Events**_ _(This shows how setter functions are used):_
_[https://react.dev/learn/responding-to-events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)_

#### **Passing the State Setter Function as a Prop (Video Timestamp: 27:40)**


The App component now holds the navigation state, but the Menu component is the one with the
buttons that need to _change_ that state. How does a child component tell its parent to update the state?


We do this by passing the _setter function_ (setNavigation) down to the child component as a prop. This
is a powerful pattern in React called "lifting state up." The state lives in the parent, but the child can
trigger updates.


Let's pass setNavigation to our Menu component.


**App.jsx**


code JavaScript

```
function App() {
 const [navigation, setNavigation] = useState(0);

 return (

  <>

    {/* Pass the setter function as a prop to the Menu component */}
    <Menu
     styling={'menu'}
     entries={menuItems}
     setNavigation={setNavigation}
    />

    <div>
      <h2> Page {navigation} is showing </h2>
    </div>

    <Table details={'five'}/ >
    <Parrot />

  </>
 );
}

```

**[App.jsx] Mentor's Notes: Passing Functions as Props**


      - App.jsx


          - <Menu ... setNavigation={setNavigation} />


**Excellent! Here are the detailed "Mentor's Notes" for the first part of the lesson.**

#### **[App.jsx] Mentor's Notes: useState and Array Destructuring**


Let's break this down. This one line is doing a lot, and it's one of the most important concepts in React.


code JavaScript
```
//   App.jsx

import React, { useState } from 'react';

// ...

const [navigation, setNavigation] = useState(0);

```

**1. What is useState?**

Think of useState as a tool that gives your component a "memory." Normally, when a JavaScript
function finishes running, all its variables disappear. But since React components are functions that run
over and over again to draw the UI, we need a way to preserve some information between these
"renders." useState is the hook (a special function) that does this.


**2. What does useState(0) do?**
When we call useState(0), we're telling React: "I need to keep track of a piece of information. Its very
first value, its initial state, should be 0."


**3. What does useState give back?**
useState always returns an array with exactly two things in it:


   - **Item 1 (value):** The current value of your state. The first time the component renders, this will
be the initial value you provided (0 in our case).


   - **Item 2 (function):** A special function that lets you update that value.


**4. What is const [navigation, setNavigation]? (Array Destructuring)**
This is a modern JavaScript feature called "array destructuring." It's a shortcut to pull items out of an
array and put them into variables.


Without this shortcut, the code would look like this:


_code JavaScript_
```
// This is the longer way to do the same thing:

const navigationState = useState(0); // navigationState is an array: [0, function]

const navigation = navigationState[0]; // Get the first item (the value)

const setNavigation = navigationState[1]; // Get the second item (the function)

```

As you can see, the [navigation, setNavigation] syntax is much cleaner! We're just naming the two
things that useState gives back to us. By convention, we name them [thing, setThing].


Here's a simple diagram to visualize the process:


code Code

```
App Component

|

| calls useState(0)

|

+--> React returns [ value, function ]

|    |

|    +--> setNavigation (a function to update the
value)

```

```
+-----------> navigation (the current state value,
initially 0)

```

By using setNavigation, you tell React to change the value of navigation and, crucially, to **re-render**
**the component** and any of its children that use this state. This is how your UI becomes dynamic!


_**Learn More:**_


   - _**State: A Component's Memory:**_ _[https://react.dev/learn/state-a-components-memory](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory)_


   - _**useState Hook Reference:**_ _[https://react.dev/reference/react/useState](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Freference%2Freact%2FuseState)_


   - _**Responding to Events**_ _(This shows how setter functions are used):_
_[https://react.dev/learn/responding-to-events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)_

#### **_[App.jsx] Mentor's Notes: Passing Functions as Props_**


_You might be wondering why we're defining the state in App.jsx but need to use it in Menu.jsx. This is a_
_very common and important pattern in React called_ _**"lifting state up."**_


_code JavaScript_
```
//   App.jsx

<Menu ... setNavigation={setNavigation} />```

```

**The Problem:**


The `App` component (the parent) owns the `navigation` state. The `Menu` component (the child)
contains the buttons that the user clicks. The child needs to be able to tell the parent, "Hey, the user just
clicked a button, you need to update your state!"


**The Solution: Pass the "Setter" Function as a Prop**


We solve this by passing the state's setter function (`setNavigation`) down to the child component as a

prop.


Think of it like this: The `App` component is the "brain" that holds the memory (`navigation` state). It
gives the `Menu` component a "remote control" (the `setNavigation` function). The `Menu` component
doesn't know or care what the state is; it just knows that when a user clicks a button, it can use the
remote control to send a signal back up to the brain.

```
App (Parent) - Owns the 'navigation' state and the 'setNavigation' function.

/

/

```

```
/ passes down {setNavigation} as a prop

/

V

```

_<Menu /> (Child) - Receives 'setNavigation' and calls it when a button is clicked._

_```_


This pattern is fundamental. It keeps your application's data flow predictable. State flows down from
parent to child, and events (or requests to change state) flow up from child to parent.


_**Learn More:**_


   - _-_
_**Sharing State Between Components**_ _[(This is the core concept): https://react.dev/learn/sharing](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)_
_[state-between-components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)_


   - _**Passing Props to a Component**_ _(This page explains that you can pass anything as a prop,_
_[including functions): https://react.dev/learn/passing-props-to-a-component](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)_

#### **Updating State from the Child Component (Video Timestamp: 21:46)**


Now, the Menu component will receive setNavigation as a prop. We can use this function in an onClick
event handler on our menu items. We'll also change the <li> elements to <button> elements to make
them clearly interactive.


When a button is clicked, it will call setNavigation with its corresponding index, updating the state in
the parent App component.


**components/Menu.jsx**


code JavaScript

```
import React from 'react';

// STEP 1: Destructure the new 'setNavigation' prop
const Menu = ({ styling, entries, setNavigation }) => {
  return (
     <ul className={styling}>
{entries.map((entry) => (
          <li key={entry.index}>
{/* STEP 2: Add an onClick handler to a button */}
            <button onClick={() => setNavigation(entry.index)}>
{entry.content}
            </button>

          </li>
))}
     </ul>

```

```
);
};

export default Menu;

```

**[Menu.jsx] Mentor's Notes: Event Handling and Triggering State Updates**


      - Menu.jsx


          - const Menu = ({ ..., setNavigation }) => { ... }


          - <button onClick={() => setNavigation(entry.index)}> ... </button>

#### **_[Menu.jsx] Mentor's Notes: Event Handling and Triggering State Updates_**


_Now that the Menu component has the setNavigation function (which we passed as a prop), we need to_

_call it when a user clicks a button._


_code JavaScript_
```
// components/Menu.jsx

// 1. Receive the function as a prop

const Menu = ({ styling, entries, setNavigation }) => {

 // ...

 // 2. Use it in an event handler

 <button onClick={() = > setNavigation(entry.index)}>

  {entry.content}

 </button>

 // ...

}

```

**1. The onClick Event Handler**

In React, onClick is a special prop you can add to elements like <button>. You give it a function, and

React will execute that function whenever the user clicks the element.


**2. The Importance of the Arrow Function: () => ...**
This is the most critical part to understand. Look closely at the syntax:
onClick={() => setNavigation(entry.index)}


We are **not** doing this:
onClick={setNavigation(entry.index)} <-- **INCORRECT!**


Here's the difference:


   - **Incorrect:** setNavigation(entry.index) **calls the function immediately** while the Menu is
rendering. This would instantly try to update the state, causing App to re-render, which would
cause Menu to re-render, which would call the function again... creating an infinite loop and
crashing your app.


   - **Correct:** () => setNavigation(entry.index) **creates a new, anonymous function** . This new
function has one job: to call setNavigation(entry.index) when it is executed. We are passing this
new function to onClick. React holds onto it and only executes it when the user actually clicks

the button.


So, the flow is:


1. User clicks the <button>.


2. The onClick event fires.


3. React executes the arrow function we gave it: () => ...


4. The code inside the arrow function runs, which is setNavigation(entry.index).


5. This calls the function that lives up in the App component, updating the state and triggering a


re-render.


**Learn More:**


   - **Responding to Events:** [https://react.dev/learn/responding-to-events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)


   - **Updating State in React:** [https://react.dev/learn/state-a-components-memory#updating-state-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory%23updating-state-triggers-renders)
[triggers-renders](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory%23updating-state-triggers-renders)

#### **Conditionally Rendering Components (Video Timestamp: 25:45)**


We're almost there! Our state is updating, but we're not using it to change the UI yet. This is where
**conditional rendering** comes in. We can show or hide components based on the current value of our
navigation state.


First, let's create a few simple "page" components.


**pages/File.jsx**


code JavaScript

```
import React from 'react';

const File = () => {
  return (
     <div>
       <h1>File Page</h1>
     </div>
);

```

```
};

export default File;

```

_(You can create similar simple components for Edit.jsx, View.jsx, etc.)_


Now, let's import these new page components into App.jsx and use the navigation state to decide which
one to display.


**App.jsx**


code JavaScript

```
// ... other imports
import { useState } from 'react';
import File from './pages/File.jsx';
import Edit from './pages/Edit.jsx';
import View from './pages/View.jsx';
// ... import other pages as well

function App() {
const [navigation, setNavigation] = useState(0);

 return (

  <>

    <Menu
styling={'menu'}
entries={menuItems}
setNavigation={setNavigation}
    />

{/* This div will now conditionally render our pages */}
    <div>
{navigation === 0 && <p>Welcome! Please select a page.</p>}
{navigation === 1 && <File />}
{navigation === 2 && <Edit />}
{navigation === 3 && <View />}
{/* ... add conditions for other pages */}
    </div>

{/* Other components */}
  </>
);
}

```

**[App.jsx] Mentor's Notes: Conditional Rendering**


      - App.jsx


          - {navigation === 1 && <File />}


**Excellent! Here are the detailed "Mentor's Notes" for the first part of the lesson.**


#### **[App.jsx] Mentor's Notes: useState and Array Destructuring**

Let's break this down. This one line is doing a lot, and it's one of the most important concepts in React.


code JavaScript
```
//   App.jsx

import React, { useState } from 'react';

// ...

const [navigation, setNavigation] = useState(0);

```

**1. What is useState?**

Think of useState as a tool that gives your component a "memory." Normally, when a JavaScript
function finishes running, all its variables disappear. But since React components are functions that run
over and over again to draw the UI, we need a way to preserve some information between these
"renders." useState is the hook (a special function) that does this.


**2. What does useState(0) do?**
When we call useState(0), we're telling React: "I need to keep track of a piece of information. Its very
first value, its _initial state_, should be 0."


**3. What does useState give back?**
useState always returns an array with exactly two things in it:


   - **Item 1 (value):** The current value of your state. The first time the component renders, this will
be the initial value you provided (0 in our case).


   - **Item 2 (function):** A special function that lets you _update_ that value.


**4. What is const [navigation, setNavigation]? (Array Destructuring)**
This is a modern JavaScript feature called "array destructuring." It's a shortcut to pull items out of an
array and put them into variables.


Without this shortcut, the code would look like this:


code JavaScript
```
// This is the longer way to do the same thing:

const navigationState = useState(0); // navigationState is an array: [0, function]

const navigation = navigationState[0]; // Get the first item (the value)

const setNavigation = navigationState[1]; // Get the second item (the function)

```

As you can see, the [navigation, setNavigation] syntax is much cleaner! We're just naming the two
things that useState gives back to us. By convention, we name them [thing, setThing].


Here's a simple diagram to visualize the process:


code Code


```
App Component

|

| calls useState(0)

|

+--> React returns [ value, function ]

|    |

|    +--> setNavigation (a function to update the
value)

+-----------> navigation (the current state value,
initially 0)

```

By using setNavigation, you tell React to change the value of navigation and, crucially, to **re-render**
**the component** and any of its children that use this state. This is how your UI becomes dynamic!


**Learn More:**


   - **State: A Component's Memory:** [https://react.dev/learn/state-a-components-memory](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory)


   - **useState Hook Reference:** [https://react.dev/reference/react/useState](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Freference%2Freact%2FuseState)


   - **Responding to Events** (This shows how setter functions are used):
[https://react.dev/learn/responding-to-events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)

#### **[App.jsx] Mentor's Notes: Passing Functions as Props**


You might be wondering why we're defining the state in App.jsx but need to use it in Menu.jsx. This is
a very common and important pattern in React called **"lifting state up."**


code JavaScript
```
//   App.jsx

<Menu ... setNavigation={setNavigation} />```

```

**The Problem**


The `App` component (the parent) owns the `navigation` state. The `Menu` component (the child)
contains the buttons that the user clicks. The child needs to be able to tell the parent, "Hey, the user just
clicked a button, you need to update your state!"


**The Solution: Pass the "Setter" Function as a Prop**


We solve this by passing the state's setter function (`setNavigation`) down to the child component as a

prop.


Think of it like this: The `App` component is the "brain" that holds the memory (`navigation` state). It
gives the `Menu` component a "remote control" (the `setNavigation` function). The `Menu` component
doesn't know or care what the state is; it just knows that when a user clicks a button, it can use the
remote control to send a signal back up to the brain.


code Code

```
App (Parent) - Owns the 'navigation' state and the 'setNavigation' function.

/

/

/ passes down {setNavigation} as a prop

```

/

V


<Menu /> (Child) - Receives 'setNavigation' and calls it when a button is clicked.

```


This pattern is fundamental. It keeps your application's data flow predictable. State flows down from
parent to child, and events (or requests to change state) flow up from child to parent.


**Learn More:**


   -   **Sharing State Between Components** [(This is the core concept): https://react.dev/learn/sharing](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)
[state-between-components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)


   - **Passing Props to a Component** (This page explains that you can pass anything as a prop,
[including functions): https://react.dev/learn/passing-props-to-a-component](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)

#### **[Menu.jsx] Mentor's Notes: Event Handling and Triggering State Updates**


Now that the Menu component has the setNavigation function (which we passed as a prop), we need to

call it when a user clicks a button.


code JavaScript
```
// components/Menu.jsx

// 1. Receive the function as a prop

const Menu = ({ styling, entries, setNavigation }) => {

 // ...

 // 2. Use it in an event handler

 <button onClick={() = > setNavigation(entry.index)}>

  {entry.content}

```

```
 </button>

 // ...

}

```

**1. The onClick Event Handler**

In React, onClick is a special prop you can add to elements like <button>. You give it a function, and

React will execute that function whenever the user clicks the element.


**2. The Importance of the Arrow Function: () => ...**
This is the most critical part to understand. Look closely at the syntax:
onClick={() => setNavigation(entry.index)}


We are **not** doing this:
onClick={setNavigation(entry.index)} <-- **INCORRECT!**


Here's the difference:


   - **Incorrect:** setNavigation(entry.index) **calls the function immediately** while the Menu is
rendering. This would instantly try to update the state, causing App to re-render, which would
cause Menu to re-render, which would call the function again... creating an infinite loop and
crashing your app.


   - **Correct:** () => setNavigation(entry.index) **creates a new, anonymous function** . This new
function has one job: to call setNavigation(entry.index) when it is executed. We are passing _this_
_new function_ to onClick. React holds onto it and only executes it when the user actually clicks

the button.


So, the flow is:


1. User clicks the <button>.


2. The onClick event fires.


3. React executes the arrow function we gave it: () => ...


4. The code inside the arrow function runs, which is setNavigation(entry.index).


5. This calls the function that lives up in the App component, updating the state and triggering a


re-render.


**Learn More:**


   - **Responding to Events:** [https://react.dev/learn/responding-to-events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)


   - **Updating State in React:** [https://react.dev/learn/state-a-components-memory#updating-state-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory%23updating-state-triggers-renders)
[triggers-renders](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory%23updating-state-triggers-renders)


#### **[App.jsx] Mentor's Notes: Conditional Rendering**

We've successfully updated our state, but nothing on the screen changes yet. The final piece of the
puzzle is to use the state value to show different components. This is called **conditional rendering** .


code JavaScript
```
// App.jsx

<div>

 {navigation === 1 && <File /> }

 {navigation === 2 && <Edit /> }

</div>

```

**How It Works: JavaScript's Logical AND (&&) Operator**
This syntax is a clever use of how JavaScript works. In JavaScript, the expression a && b does the
following:


   - If a is false, the expression stops and returns false.


   - If a is true, the expression continues and returns the value of b.


React takes advantage of this. When it sees {true && <Component />} in your JSX, the expression
evaluates to <Component />, so React renders it. When it sees {false && <Component />}, the
expression evaluates to false, and React knows to render nothing at all.


**Breaking Down Our Code:**
{navigation === 1 && <File />}


1. React checks the condition: navigation === 1.


2. Let's say the user just clicked the "File" button, so the navigation state is 1. The condition is


true.


3. Because the first part is true, the && operator returns the second part, which is the <File />


component.


4. React renders the <File /> component.


If the navigation state were 2, the condition navigation === 1 would be false. The && operator would
stop and return false, and React would render nothing for that line. This is how we ensure only the
correct component for the currently selected state is displayed.


**Learn More:**


   -   **Conditional Rendering in React:** [https://react.dev/learn/conditional](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering) rendering


Now, when you click a button in the Menu, the App component's state will change, and React will
automatically re-render the App component, showing the correct page. You've just built your first piece
of interactive, stateful UI.


Of course. Let's dive into the second part of the lesson, where we'll provide visual feedback to the user

and refactor our code to make it cleaner and more scalable.

# **Lesson: Managing State in React with the** **useState Hook**

## **Part 2: Providing User Feedback and Refactoring Our Code**


In Part 1, we made our menu interactive. Clicking a button updates the state in our App component,
which in turn displays the correct page. However, the menu buttons themselves don't change. The user
has no visual cue to know which page is currently active. Let's fix that.

#### **Styling the Active Button with CSS (Video Timestamp: 0:42)**


Our goal is to apply a special CSS class to the button that is currently selected. First, let's define what
that style looks like. We'll add some base styling for all buttons and then a special class, let's call it
clicked, for the active one.


**App.css**


code CSS

```
/* ... existing styles */

. menu button {
  background-color : hsl(207, 100%, 67%); /* A nice blue */
  color : white ;
  border : none ;
  padding : 4px 8px; /* Add some space inside the buttons */
  cursor : pointer ; /* Show a hand cursor on hover */
}

/* Style for when the mouse is over a button */
. menu button :hover {
  background-color : hsl(207, 100%, 57%); /* A slightly darker blue */
}

/* This is the special class for the active button */
. menu . clicked {
  background-color : hsl(261, 100%, 57%); /* A distinct purple */
}

```

**[App.css] Mentor's Notes: CSS Pseudo-classes and Dynamic Classes**


      - App.css


          - .menu button:hover { ... }


          - .menu .clicked { ... }

#### **[App.css] Mentor's Notes: CSS Pseudo-classes and Dynamic Classes**


Let's look at the CSS we added. It introduces two important concepts for making UIs feel alive:
responding to user actions (:hover) and reflecting the application's state (.clicked).


code CSS

```
/*   App.css   */

/* Style for when the mouse is over a button */

.menu button:hover {

  background-color: hsl(207, 100%, 57%) ;

}

/* This is the special class for the active button */

.menu .clicked {

  background-color: hsl(261, 100%, 57%) ;

}

```

**1. The :hover Pseudo-class**

A **pseudo-class** is a keyword added to a selector that specifies a special state of the selected element(s).
The :hover pseudo-class is built into CSS and automatically applies styles whenever the user's mouse
pointer is over the element.


   - **How it works:** The browser handles this entirely. You don't need any JavaScript. It's perfect for
simple, temporary visual feedback.


   - **Our use case:** We make the button slightly darker when the user hovers over it, signaling that

it's a clickable element.


**2. The .clicked Dynamic Class**
This is different. .clicked is a regular CSS class, but we call it "dynamic" because we will use our React
logic to add or remove it from an element. It represents the application's state, not just a momentary
user action like hovering.


   - **How it works:** React will check our navigation state. If a button's index matches the state,
React will render that button with className="clicked". If it doesn't match, it will render it

without that class. This is how we make the UI reflect the "memory" of our application.


   - **Our use case:** We give the selected button a distinct purple color so the user always knows
which page is currently active. This state is "persistent" until the user clicks another button.


**Visualizing the Difference:**


code Code

```
+----------------+          +----------------+

|  Menu Button | --(Mouse Over)--> | :hover style | (Temporary, handled by
browser)

+----------------+          +----------------+

|

|

(User Clicks)

|

V

+----------------+          +----------------+

|  Menu Button | --(State Match)-->| .clicked style| (Persistent, handled by
React)

+----------------+          +----------------+

```

This combination is powerful. You use :hover for immediate feedback and dynamic classes
like .clicked to represent the more permanent state of your application.


_**Learn More:**_


   - _**React: Adding CSS Styles:**_ _[https://react.dev/learn/writing-markup-with-jsx#adding-css-styles](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fwriting-markup-with-jsx%23adding-css-styles)_


   - _**MDN Documentation on :hover:**_ _[https://developer.mozilla.org/en-US/docs/Web/CSS/:hover](https://www.google.com/url?sa=E&q=https%3A%2F%2Fdeveloper.mozilla.org%2Fen-US%2Fdocs%2FWeb%2FCSS%2F%3Ahover)_

#### **Passing the Current State to the Child (Video Timestamp: 4:55)**


The Menu component needs to know which button to apply the .clicked class to. This information—the
currently selected navigation index—is stored in the App component's state.


Just as we passed the _setter function_ (setNavigation) down to the child to let it update the state, we now
need to pass the _state value_ (navigation) down so the child can read it and render accordingly.


**App.jsx**


code JavaScript

```
function App() {
 const [navigation, setNavigation] = useState(0);

```

```
 return (

  <>

    <Menu
     styling={'menu'}
     entries={menuItems}
     setNavigation={setNavigation}
     // Pass the current state value down as a prop
     navigation={navigation}
    />

    {/* ... rest of the component */}
  </>
 );
}

```

**[App.jsx] Mentor's Notes: Data Flow in React**


      - App.jsx


          - <Menu ... navigation={navigation} />

#### **_[App.jsx] Mentor's Notes: Data Flow in React_**


_This is a great moment to pause and review the complete data flow we've built. This pattern is one of_
_the most important concepts to master in React._


_code JavaScript_
```
//   App.jsx

<Menu

 ...

 setNavigation={setNavigation} // Function flows DOWN

 navigation={navigation} // Data flows DOWN

/>

```

_We often say that in React, "data flows down." This means a parent component passes its state (data)_
_down to its children via props._


_However, a child often needs to change that data in the parent. Since it can't directly reach up and_
_modify the parent's state, the parent passes a function down that allows the child to request a change._


_This creates a complete, predictable loop:_


_**The Data Flow Loop:**_


_1._ _**Parent (App) owns the state:**_ _const [navigation, setNavigation] = useState(0);_


_2._ _**Data flows down:**_ _App passes the current navigation value down to Menu as a prop._


_3._ _**Function flows down:**_ _App passes the setNavigation function down to Menu as a prop._


_4._ _**Child (Menu) uses the props:**_


       - _It reads the navigation prop to know which button to style as "active."_


       - _When a user clicks a button, it calls the setNavigation function it received as a prop._


_5._ _**Action flows up:**_ _Calling setNavigation sends a "request" back up to the App component to_

_update the state._


_6._ _**Parent re-renders:**_ _React updates the navigation state in App and re-renders the component._


_7._ _**The loop repeats:**_ _The new navigation value flows back down to Menu, which re-renders with_

_the updated information._


_code Code_

```
App (Parent)

State: `navigation`

|

| Props sent down: { navigation, setNavigation }

V

Menu (Child)

|

| User clicks a button, calls `setNavigation(2)`

|

^ Action sent up

|

App (Parent)

State updates to `2`, triggers re-render

```

_This pattern, often called "lifting state up," keeps your components decoupled and your data flow easy_
_to trace. The parent is in charge of the state, and the children just report events._


_**Learn More:**_


   - _-_
_**Sharing State Between Components**_ _[(This is the core concept): https://react.dev/learn/sharing](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)_
_[state-between-components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)_


#### **Conditionally Applying a CSS Class (Video Timestamp: 5:45)**

Now our Menu component receives the current navigation state as a prop. We can use this inside our
map function. For each button we render, we'll check if its entry.index matches the navigation prop. If it
does, we apply the clicked class.


We can do this elegantly with a **ternary operator** directly inside the className prop.


**components/Menu.jsx**


code JavaScript

```
import React from 'react';

// STEP 1: Destructure the new 'navigation' prop
const Menu = ({ styling, entries, setNavigation, navigation }) => {
  return (
     <ul className={styling}>
{entries.map((entry) => (
          <li key={entry.index}>
            <button
onClick={() => setNavigation(entry.index)}
              // STEP 2: Apply the class conditionally
className={navigation === entry.index ? 'clicked' : ''}

            >

{entry.content}
            </button>

          </li>
))}
     </ul>
);
};

export default Menu;

```

With this change, clicking a button updates the state in App, which causes App to re-render. It passes
the new navigation value down to Menu, which also re-renders, and our ternary operator ensures that
the correct button gets the clicked class, changing its color!


**[Menu.jsx] Mentor's Notes: The Ternary Operator for Conditional Logic**


      - Menu.jsx


          - className={navigation === entry.index ? 'clicked' : ''}

#### **[Menu.jsx] Mentor's Notes: The Ternary Operator for Conditional Logic**


This line of code is a perfect example of how to write concise, readable conditional logic directly inside

your JSX.


_code JavaScript_
```
  //   components/Menu.jsx

className={navigation === entry.index ? 'clicked' : ''}

```

This is a JavaScript feature called the **ternary operator** . It's a compact, inline if/else statement.


**The Structure:**

The ternary operator always follows this pattern:
condition ? expressionIfTrue : expressionIfFalse


   - **condition:** A statement that evaluates to true or false. In our case, navigation === entry.index.


   - **?:** "If the condition is true, then..."


   - **expressionIfTrue:** The value to use if the condition is true. For us, it's the string 'clicked'.


   - **::** "...otherwise..."


   - **expressionIfFalse:** The value to use if the condition is false. For us, it's an empty string ''.


**How React Uses It:**

When React renders each button, it evaluates this expression for the className prop:


   - **If navigation is 3 and entry.index is 3:** The condition is true. The expression returns 'clicked',

so the button is rendered as <button className="clicked">.


   - **If navigation is 3 and entry.index is 4:** The condition is false. The expression returns '', so the

button is rendered as <button className="">.


This is much cleaner than writing a separate if/else block or a helper function just to determine a class
name. It's one of the most common and useful tools for conditional rendering in React.


_**Learn More:**_


   - _-_
_**Conditional Rendering (Ternary Operator Section):**_ _[https://react.dev/learn/conditional](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering%23conditional-ternary-operator--)_

_-_ _-_ _--_
_[rendering#conditional](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering%23conditional-ternary-operator--)_ _ternary_ _operator_

#### **Refactoring Conditional Rendering with a switch Statement (Video Timestamp:** **9:43)**


Our app is working perfectly! But as developers, we should always look for ways to make our code

cleaner and easier to maintain.


In App.jsx, our conditional rendering logic looks like this:


code JavaScript

```
{navigation === 1 && <File />}
{navigation === 2 && <Edit />}
{navigation === 3 && <View />}
...etc.

```

This works, but if we had 20 pages, this list would become very long and hard to read. A JavaScript
switch statement is a much cleaner way to handle logic that depends on a single variable having many
possible values.


Let's refactor this. We can create a function that holds our switch logic and returns the correct

component.


**App.jsx (Refactored)**


code JavaScript

```
// ... imports
import File from './pages/File.jsx';
import Edit from './pages/Edit.jsx';
import View from './pages/View.jsx';
import Format from './pages/Format.jsx';
import Window from './pages/Window.jsx';
import Help from './pages/Help.jsx';

function App() {
const [navigation, setNavigation] = useState(0);

 // Create a function to handle the page switching logic
const page = () => {
switch (navigation) {
    case 1:
     return <File />;
    case 2:
     return <Edit />;
    case 3:
     return <View />;
    case 4:
     return <Format />;
    case 5:
     return <Window />;
    case 6:
     return <Help />;
default:
     return <h2>Welcome!</h2>;
}
};

 return (

  <>

    <Menu
styling={'menu'}
entries={menuItems}
setNavigation={setNavigation}

```

```
navigation={navigation}
    />

    <div>
{/* Call the function to render the correct page */}
{page()}
    </div>

{/* ... other components */}
  </>
);
}

```

This code does the exact same thing, but it's much more organized. The logic for choosing a page is
neatly contained within the page function, and our main return statement is cleaner. This makes our
component much easier to understand and modify in the future.


**[App.jsx] Mentor's Notes: Refactoring with switch**


      - App.jsx


          - const page = () => { switch (navigation) { ... } }


          - {page()}

#### **[App.css] Mentor's Notes: CSS Pseudo-classes and Dynamic Classes**


Let's look at the CSS we added. It introduces two important concepts for making UIs feel alive:
responding to user actions (:hover) and reflecting the application's state (.clicked).


code CSS

```
/*   App.css   */

/* Style for when the mouse is over a button */

.menu button:hover {

  background-color: hsl(207, 100%, 57%) ;

}

/* This is the special class for the active button */

.menu .clicked {

  background-color: hsl(261, 100%, 57%) ;

}

```

**1. The :hover Pseudo-class**

A **pseudo-class** is a keyword added to a selector that specifies a special state of the selected element(s).
The :hover pseudo-class is built into CSS and automatically applies styles whenever the user's mouse
pointer is over the element.


   - **How it works:** The browser handles this entirely. You don't need any JavaScript. It's perfect for
simple, temporary visual feedback.


   - **Our use case:** We make the button slightly darker when the user hovers over it, signaling that

it's a clickable element.


**2. The .clicked Dynamic Class**
This is different. .clicked is a regular CSS class, but we call it "dynamic" because we will use our React
logic to add or remove it from an element. It represents the application's state, not just a momentary
user action like hovering.


   - **How it works:** React will check our navigation state. If a button's index matches the state,
React will render that button with className="clicked". If it doesn't match, it will render it

without that class. This is how we make the UI reflect the "memory" of our application.


   - **Our use case:** We give the selected button a distinct purple color so the user always knows
which page is currently active. This state is "persistent" until the user clicks another button.


**Visualizing the Difference:**


_code Code_

```
+----------------+          +----------------+

|  Menu Button | --(Mouse Over)--> | :hover style | (Temporary, handled by
browser)

+----------------+          +----------------+

|

|

(User Clicks)

|

V

+----------------+          +----------------+

|  Menu Button | --(State Match)-->| .clicked style| (Persistent, handled
by React)

+----------------+          +----------------+

```

This combination is powerful. You use :hover for immediate feedback and dynamic classes
like .clicked to represent the more permanent state of your application.


_**Learn More:**_


   - _**React: Adding CSS Styles:**_ _[https://react.dev/learn/writing-markup-with-jsx#adding-css-styles](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fwriting-markup-with-jsx%23adding-css-styles)_


   - _**MDN Documentation on :hover:**_ _[https://developer.mozilla.org/en-US/docs/Web/CSS/:hover](https://www.google.com/url?sa=E&q=https%3A%2F%2Fdeveloper.mozilla.org%2Fen-US%2Fdocs%2FWeb%2FCSS%2F%3Ahover)_

#### **[App.jsx] Mentor's Notes: Data Flow in React**


This is a great moment to pause and review the complete data flow we've built. This pattern is one of
the most important concepts to master in React.


code JavaScript
```
//   App.jsx

<Menu

...

setNavigation={setNavigation} // Function flows DOWN

navigation={navigation}    // Data flows DOWN

/>

```

We often say that in React, "data flows down." This means a parent component passes its state (data)
down to its children via props.


However, a child often needs to change that data in the parent. Since it can't directly reach up and
modify the parent's state, the parent passes a function down that allows the child to request a change.


This creates a complete, predictable loop:


**The Data Flow Loop:**


_1._ **Parent (App) owns the state:** const [navigation, setNavigation] = useState(0);


_2._ **Data flows down:** App passes the current navigation value down to Menu as a prop.


_3._ **Function flows down:** App passes the setNavigation function down to Menu as a prop.


_4._ **Child (Menu) uses the props:**


       - It reads the navigation prop to know which button to style as "active."


       - When a user clicks a button, it calls the setNavigation function it received as a prop.


_5._ **Action flows up:** Calling setNavigation sends a "request" back up to the App component to

update the state.


_6._ **Parent re-renders:** React updates the navigation state in App and re-renders the component.


_7._ **The loop repeats:** The new navigation value flows back down to Menu, which re-renders with

the updated information.


code Code

```
App (Parent)

State: `navigation`

|

| Props sent down: { navigation, setNavigation }

V

Menu (Child)

|

| User clicks a button, calls `setNavigation(2)`

|

^ Action sent up

|

App (Parent)

State updates to `2`, triggers re-render

```

This pattern, often called "lifting state up," keeps your components decoupled and your data flow easy
to trace. The parent is in charge of the state, and the children just report events.


_**Learn More:**_


   - _-_
_**Sharing State Between Components**_ _[(This is the core concept): https://react.dev/learn/sharing](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)_
_[state-between-components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)_

#### **[Menu.jsx] Mentor's Notes: The Ternary Operator for Conditional Logic**


This line of code is a perfect example of how to write concise, readable conditional logic directly inside

your JSX.


code JavaScript
```
//   components/Menu.jsx

className={navigation === entry.index ? 'clicked' : ''}

```

This is a JavaScript feature called the **ternary operator** . It's a compact, inline if/else statement.


**The Structure:**

The ternary operator always follows this pattern:
condition ? expressionIfTrue : expressionIfFalse


   - **condition:** A statement that evaluates to true or false. In our case, navigation === entry.index.


   - **?:** "If the condition is true, then..."


   - **expressionIfTrue:** The value to use if the condition is true. For us, it's the string 'clicked'.


   - **::** "...otherwise..."


   - **expressionIfFalse:** The value to use if the condition is false. For us, it's an empty string ''.


**How React Uses It:**

When React renders each button, it evaluates this expression for the className prop:


   - **If navigation is 3 and entry.index is 3:** The condition is true. The expression returns 'clicked',

so the button is rendered as <button className="clicked">.


   - **If navigation is 3 and entry.index is 4:** The condition is false. The expression returns '', so the

button is rendered as <button className="">.


This is much cleaner than writing a separate if/else block or a helper function just to determine a class
name. It's one of the most common and useful tools for conditional rendering in React.


_**Learn More:**_


   - _-_
_**Conditional Rendering (Ternary Operator Section):**_ _[https://react.dev/learn/conditional](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering%23conditional-ternary-operator--)_

_-_ _-_ _--_
_[rendering#conditional](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering%23conditional-ternary-operator--)_ _ternary_ _operator_

#### **[App.jsx] Mentor's Notes: Refactoring with switch**


What we've just done is called **refactoring** . We changed the structure of our code without changing its
behavior. This is a crucial skill for a developer, as it makes our code cleaner, more readable, and easier

to maintain.


**The "Before" - A Long Chain of &&**


_code JavaScript_
```
{navigation === 1 && <File />}

{navigation === 2 && <Edit />}

{navigation === 3 && <View />}

// ... and so on

```

This works fine for a few items. But imagine if we had 15 menu items. This list inside our JSX would
become very long and cluttered. It mixes the "what" (the structure of our page) with the "how" (the
logic for deciding which component to show).


**The "After" - A Clean switch Statement**


_code JavaScript_
```
// App.jsx

```

```
const page = () => {

 switch (navigation) {

  case 1: return <File /> ;

  case 2: return <Edit /> ;

  // ...

  default: return <h2> Welcome! </h2> ;

 }

};

// ... inside the return statement:

<div>

 {page()}

</div>

```

**Why is this better?**


_1._ **Separation of Concerns:** The logic for determining which page to show is now neatly

contained within the page function. Our main JSX return statement is simplified. It just says,
"Render whatever the page function gives me." This makes the overall structure of the App
component much easier to grasp at a glance.


_2._ **Readability and Scalability:** A switch statement is designed specifically for checking one

variable against multiple possible values. It's more readable than a long chain of if/else if
statements or our && expressions. If we need to add a new page, we just add a new case to the
switch block. The JSX itself doesn't need to change.


_3._ **The default Case:** The switch statement has a built-in default case, which is a great place to

handle any state that doesn't match a specific case (like our initial state of 0). It makes our logic

more robust.


Refactoring like this is a sign of a thoughtful developer. You're not just making it work; you're making
it work well and thinking about the future of the codebase.


_**Learn More:**_


   - _-_
_**Conditional Rendering:**_ _[https://react.dev/learn/conditional](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering)_ _rendering_ _(This page covers_
_various patterns, including if and &&, so you can see how switch fits in as another powerful_
_option)._


   - _-_ _-_
_**Keeping Components Pure:**_ _[https://react.dev/learn/keeping](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fkeeping-components-pure)_ _components_ _pure_ _(While our page_
_function isn't a separate component, the principle of extracting logic out of the main render_
_body to keep it clean is highly related)._


And that's it! We've now explored how to manage a component's state, share it between parent and
child components, and use that state to drive both conditional rendering and conditional styling. We
also took an important step in learning how to refactor our code for better readability and maintenance.


