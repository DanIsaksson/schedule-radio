### **Table of Contents**

Lesson: Your First React Components.......................................................................................................2

What Are Components?.........................................................................................................................2
Refactoring Our First Component.........................................................................................................2

App.jsx (Initial Code)..................................................................................................................3
1. Create a New Component: Birdy.jsx.......................................................................................3
Birdy.jsx Creation.............................................................................................................................4

2. Update App.jsx to Use the New Component...........................................................................5
Updating App.jsx..............................................................................................................................6
Creating a Component to Display Data: Props.....................................................................................7

1. Create the Menu.jsx Component..............................................................................................7
Creating the Menu.jsx Component...................................................................................................7

2. Passing Data via Props.............................................................................................................8
Passing Data via Props.....................................................................................................................9

3. Receiving and Rendering Props in the Child.........................................................................10
Receiving and Rendering Props..................................................................................................... 10
Lesson Part 2: Advanced Component Composition............................................................................12
Separating Data from Components.....................................................................................................12

1. Create a Configuration File....................................................................................................12
navigation.js...............................................................................................................................12
Separating Data (navigation.js)......................................................................................................13

2. Update App.jsx to Import the Data........................................................................................13
Updating App.jsx to Import Data................................................................................................... 14
Creating Wrapper Components with props.children...........................................................................15

1. Create a Table.jsx Wrapper Component................................................................................15
Creating a Wrapper Component (Table.jsx)...................................................................................16

2. Using the Wrapper in App.jsx................................................................................................17
Using the Wrapper in App.jsx.........................................................................................................18


Of course! I'm ready to help you create a clear, structured lesson from this video. Breaking down
complex topics is my specialty.


Here is the first part of the lesson, with the code and structure laid out. Review it, and when you're
ready for the detailed explanations, just let me know.

# **Lesson: Your First React Components**


Welcome! Today, we're going to dive into the single most important concept in React: **Components** .
Think of them as custom, reusable HTML tags that you create. Almost everything you build in React
will be a component.


We'll start by taking a single piece of code and breaking it down into smaller, manageable components.
This process is called **refactoring**, and it's a key skill for any developer.

## **What Are Components?**


_(Relevant Video Timestamp: 01:00)_


In React, we build our user interfaces by breaking them into independent, reusable pieces called
components. Imagine you're building a house with LEGO bricks. You don't build the whole house from
a single, giant block. Instead, you use smaller, standard bricks (windows, doors, wall sections) to

assemble the final structure.


Components are just like those LEGO bricks. A button, a user profile, a search bar, or even an entire
page can be a component.


Why do we do this?


   - **Organization:** It's much easier to manage a project broken into small, self-contained files than
one massive, thousand-line file.


   - **Reusability:** Need another button? Just reuse the Button component you already built, instead
of writing the same code all over again.


   - **Collaboration:** When working on a team, different developers can work on different
components simultaneously without causing conflicts.

## **Refactoring Our First Component**


Let's start with our initial App.jsx file. It has some logic to decide whether to show a "Parrot" or a
"Feather" inside a "Cage".


_(Relevant Video Timestamp: 04:08)_


**App.jsx (Initial Code)**


code Jsx

```
import './App.css';
import Parrot from "./components/Parrot.jsx";
import Cage from "./components/Cage.jsx";
import Feather from "./components/Feather.jsx";

// This is a helper component we'll remove later.
export const Helper = () => {
  return **
}

function App() {
const caught = true;

  return (

     <>

       <Parrot />
{caught ? <Cage /> : <Feather />}
       <Cage>
          <Feather />
       </Cage>
     </>
)
}

export default App;

```

Our goal is to take the logic and JSX related to the bird and move it into its own component.


**1. Create a New Component: Birdy.jsx**


First, in your components folder, create a new file named Birdy.jsx.


_(Relevant Video Timestamp: 04:23)_


code Jsx

```
// src/components/Birdy.jsx

import React from 'react';
import Parrot from "./Parrot.jsx";
import Cage from "./Cage.jsx";
import Feather from "./Feather.jsx";

const Birdy = () => {
const caught = true;

  return (

     <>

       <Parrot />
{caught ? <Cage /> : <Feather />}
       <Cage>
          <Feather />

```

```
       </Cage>
     </>
);
};

export default Birdy;

#### **Birdy.jsx Creation**

```

Hey there! Let's break this down. What we've done here is our first **refactor** . We took a chunk of code
that was getting a bit cluttered in App.jsx and gave it its own home.


**Code/Logic/Syntax Explanation:**


1. **Component as a Function:** At its core, a React component is just a JavaScript function. The

special rules are:


       - Its name **must** start with a capital letter (e.g., Birdy, not birdy). This is how React
distinguishes your components from regular HTML tags like <div>.


       - It **must** return something that React can display, which is usually JSX (the HTML-like
syntax).


2. **import statements:** We need to bring in the other components (Parrot, Cage, Feather) that

Birdy uses. The import statement at the top tells our file, "Hey, I need to use code from these

other files."


code Code


downloadcontent_copy


expand_less

```
    App.jsx
    └── imports Birdy.jsx
    ├── imports Parrot.jsx
    ├── imports Cage.jsx
    └── imports Feather.jsx

```

3. **export default Birdy;** : This line is crucial. It says, "The main thing this file provides is the

Birdy component." This allows other files, like App.jsx, to import it and use it.


**Breaking Down the Goal:**


Our main goal was **organization** . Think of App.jsx as the main blueprint for our application. If we
write every single detail on that one blueprint, it becomes impossible to read. By creating Birdy.jsx,
we're essentially saying, "The details for the bird part of the app are on a separate blueprint." Now, our
main App.jsx just needs to know where to place the Birdy component, not how it's built internally.


**Learning Links from react.dev:**


   - **Your First Component:** This is the perfect place to start. It covers the fundamental rules of
[creating components. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fyour-first-component)


   - **Importing and Exporting Components:** This explains exactly how import and export work to
[connect your files. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fimporting-and-exporting-components)


**2. Update App.jsx to Use the New Component**


Now, we can clean up App.jsx and import our new Birdy component. This makes our main App
component much cleaner and easier to read.


_(Relevant Video Timestamp: 05:01)_


code Jsx

```
// src/App.jsx

import './App.css'
import Birdy from "./components/Birdy.jsx";

function App() {
 return (

  <>

    <Birdy />
  </>
)
}

export default App

#### **Updating App.jsx**

```

Great! Look how much cleaner App.jsx is now. This is a perfect example of how components help us
think about our app's structure.


**Code/Logic/Syntax Explanation:**


1. **Using a Custom Component:** We use our Birdy component just like a regular HTML tag:

<Birdy />. Because we used export default in Birdy.jsx, we can import Birdy here and React
knows exactly what to render.


2. **React Fragments <>:** You might wonder what the empty <></> tags are. This is a **React**

**Fragment** . A component must return a single root element. If you wanted to return two
components side-by-side, this would be an error:


code Jsx

```
    // WRONG: Can't return two elements
    return (
      <Birdy />
      <AnotherComponent />
    )

```

Fragments solve this by letting you group elements together without adding an extra <div> to
the final HTML. It's a clean way to satisfy the "single root element" rule.


**Breaking Down the Goal:**


The goal here was to make App.jsx a **compositional component** . Its main job is no longer to hold
complex logic, but to assemble and display other, more specialized components. As your app grows,
the App component will act like a table of contents, showing which major parts make up the page,
while the smaller components handle the specific details.


**Learning Links from react.dev:**


   - **JSX with Curly Braces:** Fragments are a type of JSX syntax. This page explains more about
[the rules of JSX, including the single root element rule. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fjavascript-in-jsx-with-curly-braces%23rule-jsx-must-have-one-root-element)

## **Creating a Component to Display Data: Props**


Components are most powerful when they can be reused with different data. Let's create a Menu
component. We want to be able to tell the menu which items to display from its parent component
(App).


We pass data from a parent to a child using **props** (short for "properties").


_(Relevant Video Timestamp: 07:01)_


**1. Create the Menu.jsx Component**


Create a new file src/components/Menu.jsx. For now, it will have a hardcoded list of items.


code Jsx

downloadcontent_copy
expand_less
```
// src/components/Menu.jsx

```

```
import React from 'react';

const Menu = () => {
  return (
     <ul>

       <li>File</li>

       <li>Edit</li>

       <li>View</li>

       <li>Format</li>

       <li>Window</li>
       <li>Help</li>
     </ul>
);
};

export default Menu;

#### **Creating the Menu.jsx Component**

```

Okay, we've created another component. This one, Menu, is very simple for now. It's "static," meaning
it always renders the exact same thing. This is a great way to start building a piece of your UI before
you add any complex logic or data.


**Code/Logic/Syntax Explanation:**


1. **Standard JSX:** The code inside the return statement is plain JSX. We're using standard HTML

tags like <ul> (unordered list) and <li> (list item) to structure our content. React is powerful
because it lets you write this familiar syntax directly in your JavaScript.


2. **Component Structure:** Notice the pattern is the same as Birdy.jsx:


       - Import React.


       - Define a function with a capitalized name (Menu).


       - Return JSX.


       - Export the component so it can be used elsewhere.


**Breaking Down the Goal:**


The first step to creating a dynamic, reusable component is often to build its static version first. We
asked ourselves, "What does a menu look like?" The answer is a list of items. So, we built that static

structure. Our next goal will be to figure out how to replace this hardcoded list with data that we can
change.


**Learning Links from react.dev:**


   - **Writing Markup with JSX:** This is a great overview of how to write HTML-like syntax in

[React and the small differences to be aware of. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fwriting-markup-with-jsx)


**2. Passing Data via Props**


Now, let's modify App.jsx to hold the menu data and _pass it down_ to the Menu component.


_(Relevant Video Timestamp: 17:32)_


code Jsx

```
// src/App.jsx

import './App.css'
import Menu from "./components/Menu.jsx";

function App() {
const menuItems = [
{ content: "File", index: 1 },
{ content: "Edit", index: 2 },
{ content: "View", index: 3 },
{ content: "Format", index: 4 },
{ content: "Window", index: 5 },
{ content: "Help", index: 6 },
];

  return (

     <>

       <Menu styling={"menu"} entries={menuItems} />
     </>
)
}

export default App

#### **Passing Data via Props**

```

This is a huge step! We're now making our components talk to each other. The App component (the
parent) is now passing data down to the Menu component (the child). This is called **"passing props."**


**Code/Logic/Syntax Explanation:**


1. **Defining Data:** We created a menuItems array in App.jsx. It's generally good practice for the

"parent" component to own the data that its children will display.


2. **Props as Attributes:** When we use our <Menu /> component, we're adding attributes to it, just

like you would with an HTML tag (<img src="..." />). In React, these attributes are called

**props** .


       - styling={"menu"}: Here, we're passing a simple string.


       - entries={menuItems}: **This is the key part.** The curly braces {} tell JSX, "Don't treat
this as a string. Instead, treat it as a JavaScript expression." In this case, we're passing
the entire menuItems array object.


**Breaking Down the Goal:**


The goal is to make the Menu component **reusable and configurable** . Before, the menu items were
stuck inside Menu.jsx. Now, the App component is in control. It decides what the menu's styling will be
and what entries it will contain. We could easily have another <Menu /> right below it with a
completely different set of items, all without changing the Menu.jsx file itself.


**Learning Links from react.dev:**


   - **Passing Props to a Component:** This is the essential guide to understanding how data flows
[from parent to child. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)


   - **JavaScript in JSX with Curly Braces:** This explains the magic of the {} syntax and how it lets
[you use variables, arrays, and other JavaScript logic directly in your markup. Read more on](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fjavascript-in-jsx-with-curly-braces)

[react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fjavascript-in-jsx-with-curly-braces)


**3. Receiving and Rendering Props in the Child**


Finally, let's update Menu.jsx to dynamically render the data it receives. This is where React's power
really shines. We'll use the JavaScript .map() function to transform our array of data into an array of

JSX elements.


_(Relevant Video Timestamp: 29:32)_


code Jsx

```
// src/components/Menu.jsx

import React from 'react';

const Menu = (props) => {
  return (
     <ul className={props.styling}>
{props.entries.map((entry) => (
          <li key={entry.index}>
{entry.content}
          </li>
))}
     </ul>
);
};

export default Menu;

```

Look at that! We now have a completely reusable Menu component. We could render another menu
with different items just by passing a different array. This is the core idea of component-based design.


#### **Receiving and Rendering Props**

This is the final piece of the puzzle. We've passed the data down to Menu; now we need to receive and

use it.


**Code/Logic/Syntax Explanation:**


1. **The props Object:** React collects all the attributes you passed to a component into a single

object, which is conventionally named props. This object is the first (and only) argument to your
component function.


       - props.styling contains the string "menu".


       - props.entries contains the menuItems array.


2. **Rendering a List with .map():** We can't just put an array directly into JSX. We need to convert

each item in the array into a JSX element. The standard way to do this in JavaScript (and React)
is with the .map() method.


       - props.entries.map(...) iterates over every item in our entries array.


       - For each entry in the array, it executes the function (entry) => ....


       - That function returns a <li> element. The result is a new array of <li> elements, which

React knows how to render.


3. **The key Prop:** This is very important. When you render a list, React needs a way to identify

each item so it can efficiently update, add, or remove items later. The key prop must be a unique
identifier for each item within that list. We're using entry.index here, which we defined in our
data. It's a stable and unique ID for each menu item.


**Breaking Down the Goal:**


We've now made our Menu component **dynamic** . It no longer contains any hardcoded data. It's a
template that says, "Give me an array of entries, and I will render them as a list." This separation of
data and presentation is a fundamental concept in React that makes your code incredibly flexible and
powerful.


**Learning Links from react.dev:**


   - **Rendering Lists:** This is the definitive guide on using .map() to display lists of data in React.

[Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Frendering-lists)


   - **Keeping list items in order with key:** This explains why the key prop is so important and the
[rules for choosing a good key. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Frendering-lists%23keeping-list-items-in-order-with-key)


Of course! Here is the next part of the lesson, covering the concepts from the second video clip.


We'll continue our journey of refactoring and learn how to make our components even more flexible
and powerful.

## **Lesson Part 2: Advanced Component Composition**


In the first part, we learned how to create components and pass data to them using props. Now, we'll
take that a step further by:


1. Completely separating our data into its own "configuration" file.


2. Creating powerful "wrapper" components that can display any content you pass to them.

## **Separating Data from Components**


_(Relevant Video Timestamp: 00:07)_


Right now, our menuItems array lives inside the App component. This is fine for a small app, but as
your project grows, it's a good practice to move static data like this into its own file. This keeps your
components focused on logic and presentation, not on storing data.


**1. Create a Configuration File**


Let's create a new folder and file to store our menu data.


1. In the src folder, create a new directory called siteconfigurations.


2. Inside siteconfigurations, create a new file named navigation.js.


_(Relevant Video Timestamp: 00:25)_


**navigation.js**


code JavaScript

```
// src/siteconfigurations/navigation.js

export const menuItems = [

  { content: "File", index: 1 },

  { content: "Edit", index: 2 },

  { content: "View", index: 3 },

  { content: "Format", index: 4 },

  { content: "Window", index: 5 },

  { content: "Help", index: 6 },

];

```

```
// We could export other navigation-related data from here too!

// export const footerLinks = [...]

#### **Separating Data (navigation.js)**

```

This is a fantastic step towards building a well-structured application. We've moved our data out of the
component that uses it.


**Code/Logic/Syntax Explanation:**


1. **Why a .js file?** We named the file navigation.js instead of .jsx because it contains only plain

JavaScript. It has no React component and no JSX syntax in it. This is a common convention to
signal the file's purpose.


2. **export const menuItems** : This is a **named export** . Unlike export default, you can have many

named exports in a single file. This is perfect for configuration files where you might want to
export several different pieces of data.


       - export default ... - "This file has one main thing to offer."


       - export const ... - "This file offers several things; pick the one you need by its name."


**Breaking Down the Goal:**


The goal is **Separation of Concerns** . Think of it like cooking. Your App.jsx component is the chef.
The navigation.js file is the pantry where the ingredients are stored.


The chef shouldn't have to grow their own vegetables inside the kitchen. Their job is to take ingredients
from the pantry and cook them. By moving menuItems out, our "chef" (App.jsx) is now cleaner and
more focused on its main job: assembling the final UI. This also means if we need to change the menu
items later, we know to go straight to the "pantry" (navigation.js) without touching the "chef's" logic.


**Learning Links from react.dev:**


   - **Importing and Exporting Components:** This page covers both default and named exports,
which is exactly what we're discussing here. It's a fundamental JavaScript concept that React
[uses heavily. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fimporting-and-exporting-components)


**2. Update App.jsx to Import the Data**


Now we can remove the menuItems array from App.jsx and import it directly from our new
configuration file.


_(Relevant Video Timestamp: 01:45)_


code Jsx

```
// src/App.jsx

import './App.css'

import Menu from "./components/Menu.jsx";

// Notice the curly braces {} around menuItems!

import { menuItems } from "./siteconfigurations/navigation.js";

function App() {

 return (

  <>

    <Menu styling={"menu"} entries={menuItems} />

  </>

)

}

export default App

```

Our App component is now incredibly clean! Its only job is to assemble other components and provide
them with the data they need. This is a perfect example of the **Single Responsibility Principle** .

#### **Updating App.jsx to Import Data**


Perfect. Now App.jsx is even simpler. Let's look closely at that new import statement.


**Code/Logic/Syntax Explanation:**


1. **import { menuItems } from ...** : The curly braces {} are the key here. This syntax is used to

import a **named export** . It tells JavaScript, "From the navigation.js file, I specifically want the
thing named menuItems."


2. **The Flow of Data:** The data now flows like this:


code Code

```
  1.   navigation.js (defines and exports menuItems)
    │
    └─► App.jsx (imports menuItems)
    │

```

```
    └─► Menu.jsx (receives menuItems as a prop)

```

2. The App component is acting as a go-between, taking data from a configuration file and passing

it to the component that needs to display it.


**Breaking Down the Goal:**


Our goal was to make the App component a pure **composition root** . This is a fancy term for a central
place where you put your app together. It doesn't create data, and it doesn't have complex internal logic.
It just imports components and data from other places and arranges them. This makes your app much
easier to understand at a high level and to maintain in the long run.


**Learning Links from react.dev:**


   - **Keeping Components Pure:** While this article talks about a slightly different concept, the core
idea is the same: components should be predictable and not have hidden side effects. Separating
[static data helps maintain this purity. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fkeeping-components-pure)

## **Creating Wrapper Components with props.children**


_(Relevant Video Timestamp: 04:05)_


What if you want to create a component that acts as a container, like a card, a modal, or a layout

section? You don't want to hardcode what's _inside_ that container.


React has a special, built-in prop for this: **children** .


**1. Create a Table.jsx Wrapper Component**


Let's create a new component in src/components/Table.jsx. This component will act as a simple
wrapper. Notice how it uses {props.children}.


code Jsx

```
// src/components/Table.jsx

import React from 'react';

const Table = (props) => {

  return (

     <div className={"table-content"}>

       <h2>Table</h2>

       <div className={"table-data"}>

{props.children}

```

```
       </div>

     </div>

);

};

export default Table;

#### **Creating a Wrapper Component (Table.jsx)**

```

This is where React's composition model really shines. We've just created a "wrapper" component, and
the magic ingredient is props.children.


**Code/Logic/Syntax Explanation:**


1. **props.children** : This is a special, built-in prop. It doesn't get passed like an attribute. Instead,

props.children automatically contains whatever JSX you nest _inside_ your component's opening
and closing tags.


2. **The "Wrapper" Pattern:** The Table component is a great example of this pattern. It defines a

structure—in this case, a div with a class, an <h2>, and another div. It doesn't know or care

what it will eventually display. It just knows _where_ to put it: right where {props.children} is.


Think of it like a picture frame. The <Table> component is the frame itself. props.children is the
empty space in the middle where any picture can go.


**Breaking Down the Goal:**


The goal is maximum **reusability** . We want to create components that are flexible enough to be used in
many different situations. By creating a <Table> wrapper that can hold _any_ children, we've built a
component that can be used to display user data, product lists, settings, or anything else, all while
maintaining a consistent style and structure defined by the wrapper itself.


**Learning Links from react.dev:**


   - **Passing JSX as children:** This is the core documentation for this concept. It explains exactly
[how props.children works and why it's so useful for creating layouts and wrappers. Read more](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component%23passing-jsx-as-children)

[on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component%23passing-jsx-as-children)


**2. Using the Wrapper in App.jsx**


Now, in App.jsx, we can use <Table> with an opening and closing tag. **Anything we put between the**
**tags** will be automatically passed into the Table component as the props.children prop.


_(Relevant Video Timestamp: 06:15)_


code Jsx

```
// src/App.jsx

import './App.css'

import Menu from "./components/Menu.jsx";

import { menuItems } from "./siteconfigurations/navigation.js";

import Table from "./components/Table.jsx";

import Parrot from "./components/Parrot.jsx";

import Feather from "./components/Feather.jsx";

function App() {

 return (

  <>

    <Menu styling={"menu"} entries={menuItems} />

    <Table>

{/* Everything in here is the "children" */}

     <h3>Tables have headers!</h3>

     <p>And paragraphs...</p>

     <Parrot />

     <Feather />

     <p>The sum is : { 45 + 23 }</p>

    </Table>

  </>

)

}

export default App

```

This pattern is one of the most powerful in React. It lets you create reusable UI "shells" (like cards,
dialog boxes, page layouts) and then fill them with any content you need, right where you use them.


#### **Using the Wrapper in App.jsx**

Let's look at what we did in App.jsx. This is the payoff for creating our Table wrapper.


**Code/Logic/Syntax Explanation:**


1. **<Table>...</Table>** : By using an opening and closing tag for our Table component, we are

signaling to React that we're providing children.


2. **Children Can Be Anything!** : This is the most important takeaway. Look at what we put inside


<Table>:


       - Regular HTML elements: <h3>, <p>


       - Other React components: <Parrot />, <Feather />


       - JavaScript expressions: { 45 + 23 }


React takes all of that, bundles it up, and makes it available inside the Table.jsx file as
props.children.


**Breaking Down the Goal:**


The goal here was to demonstrate **composition in action** . We are literally composing a piece of our UI
by nesting different elements inside a generic container. This is a far more flexible approach than trying
to pass every little piece of content as a separate prop.


You now know the two main ways to get content into a component:


   - **As attributes (props):** Best for simple data like strings, numbers, arrays, or objects that
configure the component (e.g., <Menu entries={menuItems} />).


   - **As children:** Best for passing arbitrary JSX markup to a component that acts as a wrapper or
container (e.g., <Table><h1>My Content</h1></Table>).


Mastering these two techniques is the key to building complex, yet manageable, applications in React.


**Learning Links from react.dev:**


   - **Passing JSX as children:** It's worth reading this one more time to really let the concept sink in.
[It is fundamental to how you'll build UIs in React. Read more on react.dev](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component%23passing-jsx-as-children)


