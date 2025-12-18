### **Table of Contents**

Lesson: Building Forms in React...............................................................................................................2

Part 1: Setting Up and the "Wrong" Way to Build a Form....................................................................2

1. Project Setup.................................................................................................................................2
Mentor's Notes..................................................................................................................................3
Mentor's Notes for Section 1: Project Setup.....................................................................................3

Learn More with Official React Docs:.........................................................................................4
2. Building an Uncontrolled Form (The Naive Approach)...............................................................4
Mentor's Notes..................................................................................................................................9
Mentor's Notes for Section 3: The React Way: Controlled Components.........................................9

Learn More with Official React Docs:.......................................................................................10
Part 2: Supercharging Your Forms with react-hook-form...................................................................11

4. Why Use a Form Library?..........................................................................................................11
Mentor's Notes................................................................................................................................11
Mentor's Notes for Section 4: Why Use a Form Library?..............................................................12

Learn More with Official React Docs:.......................................................................................12
5. Refactoring Our Form with useForm.........................................................................................12
Mentor's Notes................................................................................................................................15
Mentor's Notes for Section 5: Refactoring Our Form with useForm.............................................15

Learn More with Official React Docs:.......................................................................................17


Of course! Here is the first part of your documented lesson on creating forms in React, based on the
video provided.

# **Lesson: Building Forms in React**


Hello everyone! Today, we're going to dive into one of the most common and essential tasks in web
development: **handling forms** . We'll start by building a simple form the "old-fashioned" way to
understand some core concepts, and then we'll refactor it into the proper "React way" using state.


Our ultimate goal is to build a robust search form, and along the way, we'll learn why managing form
data directly in React is so powerful.

## **Part 1: Setting Up and the "Wrong" Way to Build a Form**


First, let's get our project set up and running.


_(Video Timestamp: 00:04)_

#### **1. Project Setup**


I've already set up a basic React project using Vite. After running npm install, all our necessary
dependencies are in the node_modules folder. We'll be working primarily in the App.jsx file.


Here is the initial state of our App.jsx file. It contains some state variables that we'll use later.


code JavaScript

```
// src/App.jsx

import { useState, useEffect } from 'react';
import axios from 'axios';

function App() {
const [navigation, setNavigation] = useState(4);
const [backendData, setBackendData] = useState([]);
const [search, setSearch] = useState(null);
const [searchString, setSearchString] = useState("http://libris.kb.se/xsearch?
query=forf:(Ludwig+Wittgenstein)&format=json");

 // ... (useEffect and other logic will be simplified for our form example)

 return (
  <div>
{/* Our form will go here */}
  </div>
);
}

export default App;

```

#### **Mentor's Notes** **_Mentor's Notes for Section 1: Project Setup_**

_Hey there! Let's take a closer look at the starting code. It might seem like a lot of setup, but breaking it_
_down makes it simple._


_Our main goal is to have our React component "remember" things, like what a user has typed into a_
_search box. In React, we give components memory by using a_ _**Hook**_ _called useState._


_Think of useState as a way to declare a variable whose value will be preserved between re-renders of_
_the component._


code JavaScript
```
  // Examp le:

c onst [myVariable, setMyVariable] = useState(initialValue);

```






















_Each of these is a piece of memory for our App component. For our forms lesson, we will mostly be_
_creating new state variables, but it's helpful to see that a component can have many of them to manage_
_different things._


_This pattern is the foundation of interactivity in React._


_**Learn More with Official React Docs:**_


   - _**Must-Read on State:**_ _To fully grasp this, the most important page for you is_ _**State: A**_

_**'**_
_**[Component](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstate-a-components-memory)**_ _**s Memory**_ _._


   - _**useState Hook Deep Dive:**_ _For syntax and examples, check out_ _**[useState](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Freference%2Freact%2FuseState)**_ _._


   - _**Adding Interactivity:**_ _This page puts it all together:_ _**[Adding Interactivity](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fadding-interactivity)**_ _._

#### **2. Building an Uncontrolled Form (The Naive Approach)**


_(Video Timestamp: 03:45)_


To understand _why_ we should handle forms in a specific way in React, let's first build one using a
method that feels familiar if you've worked with plain HTML and JavaScript. This is what we call an
**uncontrolled component** .


We'll add a basic HTML <form> with a text input for an author's name and a submit button.


Let's modify the return statement in App.jsx:


code Jsx

```
// src/App.jsx

// ... (imports and state variables remain the same)

function App() {
 // ...

 // This function will handle the form's submission
 const handleSubmit = (e) => {
  // Prevents the browser from reloading the page on form submit
  e.preventDefault();

  // This is the "wrong" way: directly accessing the DOM to get the value
  const authorValue = document.getElementById("author-firstname").value;

  console.log("Submitted value:", authorValue);
  // In a real app, you would do something with this value, like send it to an
API
 };

 return (
  <div>
    {/* Existing JSX can go here */}

    <form onSubmit={handleSubmit}>
     <label name="author-firstname">Author First Name:</label>
     <input
      type="text"
      id="author-firstname"

      name="author-firstname"

     />
     <input type="submit" value="Submit" />

```

```
    </form>

  </div>
 );
}

export default App;

```

**Code Breakdown**


1. **`<form onSubmit={handleSubmit}>`**: We've added a standard HTML form. The `onSubmit`

attribute is an event handler that calls our `handleSubmit` function when the form is submitted (e.g., by
clicking the "Submit" button).


2. **`handleSubmit = (e) => { ... }`**: This is our submission handler function.


3. **`e.preventDefault()`**: This is a crucial line. By default, submitting a form causes the browser to
refresh the page. This line stops that default behavior, allowing our React application to handle the
logic without a full-page reload.


4. **`document.getElementById(...)`**: This is plain JavaScript used to grab the input element
directly from the DOM (the rendered HTML) and read its `value`.


**While this works, it's considered an anti-pattern in React. We're "reaching out" of our React**
**code to manipulate the DOM, which breaks the declarative model that makes React so powerful.**


**Mentor's Notes**


Mentor's Notes for Section 2: Building an Uncontrolled Form


(Video Timestamp: 03:45)


Okay, so we've built a form that works, but I called it the "wrong" or "naive" way. Why is that?


This approach is called an **uncontrolled component** . The term "uncontrolled" means that React is **not**
in control of the data inside the form input. The browser's DOM is holding the value, and we are
manually pulling it out when we need it.


**Here’s the flow of an uncontrolled component:**


1. User types into the <input> field.


(The DOM updates and stores the value internally. React knows nothing about this change.)


2. User clicks the "Submit" button.


3. Our `handleSubmit` function fires.


4. We use `document.getElementById("author-firstname").value` to manually ask the DOM, "Hey,
what's the current value of that input field?"


This breaks a core principle of React. In React, we want our UI to be a **direct reflection of our state** .
The state should be the single source of truth. By letting the DOM manage the input's value, we create
two sources of truth, which can lead to bugs and make the application hard to reason about.


Think of it like this:


**Uncontrolled Flow (The "Wrong" Way):**


code Code

```
 React App      DOM

|         |

| <-- Asks for value on submit -- [<input value="user text">]

|                  ^

+------------------------------------| User types directly here

```

We are breaking out of the React ecosystem to talk to the DOM directly.


**e.preventDefault() Explained**


This little function is very important in Single Page Applications (SPAs) like those built with React. In
traditional HTML, a form submission sends the data to a server and the browser expects a new page in
return, causing a full reload. e.preventDefault() stops this browser default, so our React code can take
over and handle the form data without losing the current page state.


Learn More with Official React Docs:


**Responding to Events:** The onSubmit attribute is an event handler. Learn all about them here:
**[Responding to Events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)** .


**Manipulating the DOM with Refs:** While we used getElementById, the "React" way to access the
DOM directly is with useRef. The documentation explains this but also warns that it should be an
"escape hatch" used sparingly: **[Manipulating the DOM with Refs](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fmanipulating-the-dom-with-refs)** .


3. The React Way: Controlled Components


*(Video Timestamp: 13:23 - The instructor begins refactoring to a state-driven approach)*


The correct way to handle forms in React is by making them **controlled components**. This means
React state is the "single source of truth" for the form's data. The input field's value is controlled by a
state variable, and any changes to that input update the state.


Let's refactor our code.


First, we'll create a state object to hold all our form data. This is much cleaner than creating a separate
`useState` for every single input field.


```javascript

```
// src/App.jsx

// ...

function App() {
 // ... (other state variables)

 // A state object to manage all form fields
 const [searchForm, setSearchForm] = useState({
  authorFirstName: "",
  authorLastName: ""
 });

 // ...
}

```

Now, let's update our form to use this state. We'll bind the input's value to our state and use the
onChange event to update the state whenever the user types.


code Jsx

```
// src/App.jsx

// ...

function App() {
 const [searchForm, setSearchForm] = useState({
  authorFirstName: "",
  authorLastName: "",
 });

 const handleChange = (e) => {
  const { name, value } = e.target;
  setSearchForm(prevState => ({
    ...prevState,
    [name]: value
  }));
 };

 const handleSubmit = (e) => {
  e.preventDefault();
  // Now, we get the data directly from our state !
  console.log("Submitted Form Data:", searchForm);
 };

 return (
  <div>
    <form onSubmit={handleSubmit}>
     { /* Author First Name Input */ }
     <label name="authorFirstName">Author First Name:</label>
     < input

```

```
      type="text"
      name="authorFirstName" // The 'name' attribute must match the state

property
      value ={searchForm.authorFirstName} // The input 's value is controlled by
state
      onChange={handleChange} // This function updates the state
     />

     { /* We can easily add more fields now */ }
     <br />

     { /* Author Last Name Input */ }
     <label name="authorLastName">Author Last Name:</label>
     < input
      type="text"
      name="authorLastName"
      value ={searchForm.authorLastName}
      onChange={handleChange}
     />

     <br />
     < input type="submit" value ="Submit" />
    </form>

  </div>
 );
}

export default App;

```

**Code Breakdown:**


1. **searchForm State** : We now have a single state object, searchForm, that holds the values for all

our inputs. This is much more scalable.


2. **value={searchForm.authorFirstName}** : The input's displayed value is now directly tied to a

property in our React state. The input is "controlled" by React.


3. **onChange={handleChange}** : This is the most important part. Every time the user types a

character, the onChange event fires. It calls our handleChange function.


4. **handleChange Function** : This function receives the event e.


       - e.target is the input element that triggered the change.


       - We destructure name and value from e.target. The name attribute of our input is crucial
—it tells us _which_ property in our searchForm state to update.


       - setSearchForm updates the state. We use the spread operator (...prevState) to copy the
existing state and then dynamically update the property that matches the input's name.


5. **Updated handleSubmit** : Our submit function is now much cleaner. It no longer needs to read

from the DOM. All the data it needs is already available in the searchForm state.


This is the fundamental pattern for building forms in React. Your UI is a direct reflection of your state,
and user actions update that state.


#### **Mentor's Notes** **_Mentor's Notes for Section 3: The React Way: Controlled Components_**

_(Video Timestamp: 13:23)_


_This is the most important concept for forms in React. A_ _**controlled component**_ _is an input form_
_element whose value is controlled by React state._


_Let's break down the data flow, which is a clean, predictable loop:_


_**Controlled Flow (The "React" Way):**_


_code Code_

```
1. User types a character in the <input>.

2. The `onChange` event handler is triggered.

3. The `handleChange` function is called.

4. `handleChange` calls the `setSearchForm` function to update the state with the

new value.

5. React re-renders the `App` component because its state has changed.

6. The <input> field's `value` prop is now set to the newly updated value from the

state.

```

_The data flows from the state to the UI, and user actions flow back up to update the state. This creates_
_a_ _**single source of truth**_ _. The searchForm state always knows what's in the input fields._


_**Managing Multiple Inputs with a Single Handler**_


_The handleChange function is a very common and powerful pattern._


_code JavaScript_
```
const handleChange = (e) => {

 const { name, value } = e.target; // e.g., name="authorFirstName", value ="John"

 setSearchForm(prevState => ({

  ...prevState,

  [name]: value // Dynamic key : becomes { authorFirstName: "John" }

 }));

};

```

_1._ _**const { name, value } = e.target;**_ _: This is key. We get the name and value from the input that_

_triggered the event. This is why it's crucial that the name attribute on your <input>_ _**exactly**_
_**matches**_ _the property name in your state object (authorFirstName, authorLastName)._


_2._ _**setSearchForm(...)**_ _: We are updating an object in state._


_3._ _**...prevState**_ _: The spread syntax copies all the properties from the old state object into the new_

_one. This is vital! If you don't do this, you will wipe out the other properties (e.g., typing in the_
_authorLastName field would erase authorFirstName)._


_4._ _**[name]: value**_ _: This is called a_ _**computed property name**_ _. It lets us use the name variable_

_(which could be "authorFirstName" or "authorLastName") as the key in our new object. This is_
_what allows a single function to handle many different inputs._


_With this pattern, your handleSubmit function becomes incredibly simple. You don't need to hunt for_
_values in the DOM; they are already perfectly organized for you in the searchForm state object._


_**Learn More with Official React Docs:**_


   - _**Core Concept - Sharing State:**_ _Controlled components are a form of "lifting state up," which is_
_a fundamental concept. This is a must-read:_ _**[Sharing State Between Components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)**_ _._


   - _**Updating Objects in State:**_ _The ...prevState pattern is the standard way to update objects. Learn_
_why here:_ _**[Updating Objects in State](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fupdating-objects-in-state)**_ _._


   - _**Managing State - Overview:**_ _For a broader look at all these concepts together, this is an_
_excellent guide:_ _**[Managing State](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fmanaging-state)**_ _._


This concludes the first part of our lesson. We've seen the wrong way and the right way to handle form
inputs. When you're ready, let me know, and I'll fill out the "Mentor's Notes" with detailed explanations

and links to the official React documentation.


Of course! Here is the second part of the lesson, which introduces a more powerful and efficient way to
handle forms using a popular library.

## **Part 2: Supercharging Your Forms with react-hook-form**


We've successfully built a controlled form, which is the "React way" to do things. However, you might
have noticed that for every input, we need to wire up value and onChange. If we have a form with 10,
20, or even more fields, our handleChange function and state object can become quite large and

cumbersome.


This is a common problem, and thankfully, the React community has created some amazing libraries to
solve it. Today, we'll look at one of the most popular: **React Hook Form** .


_(Video Timestamp: 15:12 - The instructor mentions React Hook Form and begins the installation.)_


#### **4. Why Use a Form Library?**

React Hook Form helps us by:


   - **Reducing Boilerplate** : It abstracts away the need for a manual handleChange function and
connecting value for every input.


   - **Improving Performance** : It cleverly minimizes the number of re-renders, which can be a
performance bottleneck in large forms.


   - **Built-in Validation** : It provides a simple and powerful way to handle form validation.


Let's install it. Open your terminal in the project directory and run:


code Bash

```
npm install react-hook-form

```

This command downloads the library and adds it to our node_modules folder and package.json file.

#### **Mentor's Notes**


_Of course. Here are the detailed Mentor's Notes for the second part of the lesson._

#### **_Mentor's Notes for Section 4: Why Use a Form Library?_**


_(Video Timestamp: 15:12)_


_So, you might be thinking, "We just learned the 'right' way to do forms with useState. Why are we_
_immediately throwing it away for a library?" That's a great question._


_The controlled component pattern we learned is the_ _**fundamental concept**_ _, and you'll use its principles_
_everywhere in React. However, as your forms get bigger, managing them manually with useState has_

_two main drawbacks:_


_1._ _**Boilerplate Code:**_ _For every single input, you have to add a value prop, an onChange prop, a_

_name prop, and a corresponding property in your state object. If your form has 30 fields, that's_
_a lot of repetitive code. It's easy to make a typo and hard to maintain._


_2._ _**Performance:**_ _Remember how our controlled component works? Every single keystroke in any_

_input field calls setSearchForm. This causes the entire App component to re-render. For a small_
_form, this is fine. But for a large, complex form, re-rendering the whole thing on every keystroke_
_can make your application feel slow and laggy._


_Libraries like_ _**React Hook Form**_ _are designed specifically to solve these problems. They provide a set_
_of pre-built hooks and components that handle the tedious parts for you, while also being highly_
_optimized for performance. They often achieve this by tracking the input state without causing a full_
_component re-render for every change, only re-rendering when necessary (like when an error needs to_
_be displayed)._


_Learning to use popular libraries like this is a key skill for a developer. It saves you from "reinventing_
_the wheel" and lets you build faster, more robust applications by leveraging the work of the open-_

_source community._


_**Learn More with Official React Docs:**_


   - _**Escape Hatches:**_ _While form libraries are more of a "power tool" than an "escape hatch," the_
_React docs have a great page that discusses when it makes sense to step outside the standard_
_React flow. This mindset is useful for understanding why libraries like this exist:_ _**[Escape](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fescape-hatches)**_

_**[Hatches](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fescape-hatches)**_ _._


   - _**React Hook Form Documentation:**_ _The best place to learn more is their official "Get Started"_
_guide:_ _**[React Hook Form - Get Started](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact-hook-form.com%2Fget-started)**_ _._


<br>

#### **5. Refactoring Our Form with useForm**


Now, let's refactor our component to use the useForm hook provided by the library.


_(Video Timestamp: 15:15 - The instructor starts modifying the code to use the new library.)_


code Jsx

```
// src/App.jsx

import { useForm } from 'react-hook-form'; // <-- Import the hook

function App() {
 // The useForm hook manages all our form state internally
const { register, handleSubmit, formState: { errors } } = useForm();

 // This is our custom submit logic. It now receives the form data as an argument!
const onFormSubmit = (data) => {
console.log("Form data from React Hook Form:", data);
  // You can now use this 'data' object to send to an API
};

 // This function will be called if validation fails
const onErrors = (errors) => {
console.error("Validation Errors:", errors);
};

 return (
  <div>
{/* We wrap our own submit handler with the one from the hook */}
    <form onSubmit={handleSubmit(onFormSubmit, onErrors)}>

{/* Author First Name Input */}
     <label name="authorFirstName">Author First Name:</label>
     <input
      type="text"
      // The 'register' function connects this input to the form state
{...register("authorFirstName", {
required: "First name is required",

```

```
minLength: {
value: 2,
message: "First name must be at least 2 characters"
}
})}
     />
{/* Conditionally render an error message if validation fails */}
{errors.authorFirstName && <p style={{color: 'red'}}
>{errors.authorFirstName.message}</p>}

     <br />

{/* Author Last Name Input */}
     <label name="authorLastName">Author Last Name:</label>
     <input
      type="text"
{...register("authorLastName", { required: "Last name cannot be
empty" })}
     />
{errors.authorLastName && <p style={{color: 'red'}}
>{errors.authorLastName.message}</p>}

     <br />
     <input type="submit" value="Submit" />
    </form>

  </div>
);
}

export default App;

```

**Code Breakdown:**


This looks quite different, so let's walk through it.


1. **import { useForm } from 'react-hook-form';** : We import the necessary hook from the library.


2. **const { register, handleSubmit, ... } = useForm();** : This is where the magic happens. We call

the useForm hook, which returns an object with several helpful properties and methods.


       - **register** : This is a function we'll use to "register" our inputs with the form. It takes the
place of writing name, value, and onChange manually.


       - **handleSubmit** : This is a function that will wrap our own submission logic. It handles
preventing the default browser behavior (e.preventDefault()) and gathers all the form

data for us.


       - **formState: { errors }** : This is an object that contains information about the form's state,
including any validation errors.


3. **<form onSubmit={handleSubmit(onFormSubmit, onErrors)}>** : Our onSubmit now calls the


handleSubmit function from the hook.


       - The first argument, onFormSubmit, is the function that will be called with the form data
**only if validation succeeds** .


       - The second (optional) argument, onErrors, is a function that will be called if any

validation rules fail.


4. **{...register("authorFirstName", { ... })}** : This is the most significant change.


       - The spread operator (...) applies all the necessary props (onChange, onBlur, ref, name) to
the input element automatically.


       - The first argument to register, "authorFirstName", is the name of the field. This will be
the key in our final data object.


       - The second argument is an optional object for **validation rules** . We've added a required
rule and a minLength rule. If the input is empty or too short, an error will be generated.


5. **{errors.authorFirstName && ...}** : This is how we display validation errors. We are using a

logical AND (&&) for conditional rendering. If errors.authorFirstName exists (meaning there's
an error for that field), the <p> tag with the error message will be rendered. The message comes
directly from the validation rules we defined in register.


Look at how much cleaner this is! We've removed our manual useState for the form and the

handleChange function entirely. The validation logic is right next to the input it belongs to, making the

code much easier to read and maintain.


<br>

#### **Mentor's Notes** **_Mentor's Notes for Section 5: Refactoring Our Form with useForm_**


_(Video Timestamp: 15:15)_


_Okay, let's break down the new syntax from react-hook-form. It might look like magic at first, but it_
_follows a very logical pattern._


_**The useForm() Hook**_


_code JavaScript_
```
  const { register, handleSubmit, formState: { errors } } = useForm();

```

_useForm is a custom hook that acts as the "brain" of our form. It does all the heavy lifting of tracking_
_input values, handling validation, and managing the form's state. We just need to connect our inputs to_
_it. We are destructuring the object it returns to pull out the specific functions and objects we need._


_**The register Function and Spread Operator (...)**_


_code Jsx_

```
  < input {...register("authorFirstName", { required: "..." })} />

```

_This is the core of React Hook Form. The register function is what connects your input to the useForm_

_hook._


   - _When you call register("authorFirstName"), you're telling useForm, "Please track an input with_

_the name 'authorFirstName'"._


   - _The register function returns an object with all the props an input needs: onChange, onBlur,_
_name, and ref._


   - _The_ _**spread operator (...)**_ _is a clean way to pass all of those props to the <input> element at_
_once. Instead of writing them out one by one, {...register()} does it for you. It's equivalent to_

_writing:_


_code Jsx_

```
    <input

     name="authorFirstName"

     onChange={...} // The handler from the library

     onBlur={...} // The handler from the library

     ref={...} // A reference for the library to access the DOM node
    directly

    />

```

_**The handleSubmit Wrapper**_


_code Jsx_

```
<form onSubmit={handleSubmit(onFormSubmit, onErrors)}>

```

_The handleSubmit function from the library is a wrapper. It's a brilliant piece of design that separates_
_your logic from the form's submission event._


_Here's the flow:_


_1. The user clicks "Submit"._


_2. The onSubmit event fires, which calls the handleSubmit function from the library._


_3. handleSubmit automatically runs all your validation rules._


_4._ _**If validation passes:**_ _It calls your onFormSubmit function and passes it a single argument: a_

_neat object containing all the form data (e.g., { authorFirstName: 'John', authorLastName:_
_'Doe' })._


_5._ _**If validation fails:**_ _It does not call onFormSubmit. Instead, it calls your onErrors function and_

_populates the errors object so you can display messages to the user._


_This pattern is great because your onFormSubmit function can focus purely on what to do with the data_
_(like sending it to a server), knowing that it will only ever be called with valid data._


_**Conditional Rendering with the errors Object**_


_code Jsx_

```
  {errors.authorFirstName && <p> {errors.authorFirstName.message} </p> }

```

_This is a standard React pattern for showing or hiding an element._


   - _errors.authorFirstName will be undefined if there is no error. In JavaScript, undefined is a_
_"falsy" value._


   - _The && (logical AND) operator works like this: if the left side is falsy, it stops and returns the_
_left side. If the left side is "truthy" (i.e., an error object exists), it proceeds to the right side and_

_returns that._


   - _So, this line effectively says: "If an error for authorFirstName exists, then render this <p> tag_
_with the error message." The .message property comes directly from the validation rule you_
_defined in register._


_This declarative way of handling errors is much cleaner than manually setting error states with_

_useState._


_**Learn More with Official React Docs:**_


   - _**Reusing Logic with Custom Hooks:**_ _useForm is a great example of a custom hook._
_Understanding this concept will help you see how libraries can extend React's functionality:_
_**[Reusing Logic with Custom Hooks](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Freusing-logic-with-custom-hooks)**_ _._


   - _**Conditional Rendering:**_ _The && pattern is a core part of rendering UI based on state. This is a_
_must-read:_ _**[Conditional Rendering](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fconditional-rendering)**_ _._


   - _**Responding to Events:**_ _This page provides the foundation for understanding props like_
_onSubmit:_ _**[Responding to Events](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fresponding-to-events)**_ _._


This concludes our lesson on forms! We've gone from a simple (but flawed) uncontrolled form, to a
proper controlled component, and finally to a powerful, scalable solution using react-hook-form. Using
libraries like this is a standard practice in professional React development because it saves time and
helps prevent common bugs.


Let me know when you are ready, and I will fill out the final Mentor's Notes with more in-depth
explanations.


