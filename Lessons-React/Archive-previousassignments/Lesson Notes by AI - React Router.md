### **Table of Contents**

React Router Lesson: Part 1.......................................................................................................................3


Introduction to React Hook Form.........................................................................................................3

Step 1: Installing React Hook Form.................................................................................................3
_Step 1: Installing React Hook Form_ .................................................................................................3

MENTOR'S NOTES....................................................................................................................3
Step 2: Importing and Using the useForm Hook..............................................................................4
Step 3: Refactoring the Form............................................................................................................6
_Step 3: Refactoring the Form_ ...........................................................................................................6

MENTOR'S NOTES....................................................................................................................6
Step 4: Handling Form Submission..................................................................................................8
Step 4: Handling Form Submission..................................................................................................8

_MENTOR'S NOTES_ ......................................................................................................................8

React Router Lesson: Part 2.....................................................................................................................10

Introduction to React Bootstrap and React Router..............................................................................10

Step 5: Installing React Bootstrap..................................................................................................10
Step 6: Importing Bootstrap CSS....................................................................................................11
_Step 6: Importing Bootstrap CSS_ ....................................................................................................11

MENTOR'S NOTES..................................................................................................................11
Step 7: Refactoring the UI with React Bootstrap Components......................................................12

_MENTOR'S NOTES_ ....................................................................................................................13
Step 8: Introducing React Router...................................................................................................14
Step 9: Installing React Router.......................................................................................................14
Step 10: Setting Up the Router.......................................................................................................15

_MENTOR'S NOTES_ ....................................................................................................................16
Step 11: Creating Nested Routes and Layouts................................................................................16

MENTOR'S NOTES..................................................................................................................18

Step 12: Creating Dynamic Routes................................................................................................18
_Step 12: Creating Dynamic Routes_ .................................................................................................19

MENTOR'S NOTES..................................................................................................................20
Step 13: Navigating Programmatically...........................................................................................20
Step 5: Installing React Bootstrap..................................................................................................21

MENTOR'S NOTES..................................................................................................................21

Step 6: Importing Bootstrap CSS...................................................................................................22

MENTOR'S NOTES..................................................................................................................22
Step 7: Refactoring the UI with React Bootstrap Components......................................................23

MENTOR'S NOTES..................................................................................................................23
Step 9: Installing React Router.......................................................................................................24

MENTOR'S NOTES..................................................................................................................24
Step 10: Setting Up the Router.......................................................................................................24

MENTOR'S NOTES..................................................................................................................25
Step 11: Creating Nested Routes and Layouts................................................................................25

MENTOR'S NOTES..................................................................................................................26

Step 12: Creating Dynamic Routes................................................................................................27

MENTOR'S NOTES..................................................................................................................28
Step 13: Navigating Programmatically...........................................................................................28

MENTOR'S NOTES..................................................................................................................28


Step 14: Linking Between Routes with <NavLink>......................................................................29

MENTOR'S NOTES..................................................................................................................30


Of course! Here is the first part of your documented lesson. Let me know when you're ready for me to

fill out the "Mentor's Notes" sections.

# **React Router Lesson: Part 1**

## **Introduction to React Hook Form**


Welcome! Today, we're going to learn how to manage forms in React more efficiently using a popular
library called **React Hook Form** . We'll start by installing the library and then refactor an existing form
to see how it simplifies our code.

#### **Step 1: Installing React Hook Form**


First things first, we need to add the react-hook-form library to our project. We'll use npm (Node
Package Manager) to do this.


   - **Timestamp:** [0:38](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D38s)


code Bash

```
npm install react-hook-form

#### **_Step 1: Installing React Hook Form_**

```

   - _**Timestamp:**_ _[0:38](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D38s)_


_code Bash_

```
  npm install react-hook-form

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_This command uses npm, which is a package manager for JavaScript. It's like a tool that_
_helps you download and install libraries (pre-written code that you can use in your project)_
_from a central repository._


      - _npm install: This is the command to install a package._


      - _react-hook-form: This is the name of the specific library we want to install. It's a_
_popular library for managing forms in React applications, known for its_
_performance and ease of use._


_**Why are we doing this?**_


_React Hook Form helps us manage form state, validation, and submission more efficiently_
_than doing it all manually. It uses React Hooks, which are special functions that let you_


_"hook into" React features like state and lifecycle methods. By installing this library, we're_
_adding a powerful tool to our React development toolkit._


_**Analogy:**_ _Imagine you're building a house. You could try to make every single nail and_
_screw yourself, or you could go to a hardware store and buy them. npm install react-hook-_
_form is like going to the hardware store to get a pre-made, high-quality tool (the form_
_library) that will make building your house (your React app) much easier and faster._


_**Further Learning:**_


      - _**What is npm?**_ _[https://docs.npmjs.com/about-npm-install](https://www.google.com/url?sa=E&q=https%3A%2F%2Fdocs.npmjs.com%2Fabout-npm-install)_


      - _**What are Node.js and npm?**_ _[https://react.dev/learn/start-a-new-react-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23setting-up-your-development-environment)_
_[project#setting-up-your-development-environment (This section explains the](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23setting-up-your-development-environment)_
_development environment setup, which includes npm)._

#### **Step 2: Importing and Using the useForm Hook**


Now that we have the library installed, we can import the useForm hook into our App.jsx component.
This hook is the core of React Hook Form and provides us with everything we need to manage our

form.


   - **Timestamp:** [1:45](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D105s)


   - **App.jsx**


code Jsx

```
import { useForm } from 'react-hook-form';

function App() {
const { register, handleSubmit } = useForm();
 // ... (rest of the component)
}

```

Step 2: Importing and Using the useForm Hook


**Timestamp:** [1:45](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D105s)

```
code Jsx

import { useForm } from 'react-hook-form';

function App() {

const { register, handleSubmit } = useForm();

```

```
 // ... (rest of the component)

}

```

**MENTOR'S NOTES**


**What's happening here?**


import { useForm } from 'react-hook-form';: This line imports the useForm function (which is a React
Hook) from the react-hook-form library we just installed.


function App() { ... }: This is a standard React functional component.


const { register, handleSubmit } = useForm();: This is the core of using React Hook Form. We're
calling the useForm() hook, which returns an object containing several helpful functions for managing
our form. We're using object destructuring to pull out two of these functions: register and

handleSubmit.


**What do register and handleSubmit do?**


**register** : This function is used to connect your HTML form inputs (like <input>, <select>, <textarea>)
to React Hook Form. When you use the register function on an input, it automatically handles input
changes, validation, and makes the input's value available to the form. You typically pass the register
function the name of the input field, like register("authorFirstName").


**handleSubmit** : This function takes another function (your onSubmit function) as an argument. When
the form is submitted, React Hook Form will first validate the inputs (if you've set up validation) and
then, if the validation passes, it will call your onSubmit function with the form's data.


**Analogy:** Think of useForm() as getting a special "form management kit." Inside this kit, register is
like a special connector that you attach to each input field in your form so the kit knows about it.
handleSubmit is like the "submit button" that, when clicked, packages up all the information from the
connected inputs and gives it to you in a neat package (your onSubmit function).


**Further Learning:**


**React Hooks:** [https://react.dev/learn/reusing-state-logic-with-custom-hooks (To understand what](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Freusing-state-logic-with-custom-hooks)
hooks are in general).


**useForm hook in React Hook Form:** [https://react-hook-form.com/api/useform/](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact-hook-form.com%2Fapi%2Fuseform%2F) (This is the official
documentation for the useForm hook).


**Registering input elements:** [https://react-hook-form.com/get-started-es7/#_register](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact-hook-form.com%2Fget-started-es7%2F%23_register)


#### **Step 3: Refactoring the Form**

Next, we'll update our form to use the register and handleSubmit functions we get from the useForm

hook.


   - **Timestamp:** [2:20](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D140s)


   - **App.jsx**


code Jsx

```
<form onSubmit={handleSubmit(onSubmit)} >
 <label> Author first name </label>
 <input {...register("authorFirstName")} />

 <label> Author last name </label>
 <input {...register("authorLastName")} />

 <input type="submit" />
</form>

#### **_Step 3: Refactoring the Form_**

```

   - _**Timestamp:**_ _[2:20](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D140s)_


_code Jsx_

```
<form onSubmit={handleSubmit(onSubmit)} >

 <label> Author first name </label>

 <input {...register("authorFirstName")} />

 <label> Author last name </label>

 <input {...register("authorLastName")} />

 <input type="submit" value="Submit" />

</form>

```

_**MENTOR'S NOTES**_


**What's happening here?**


We're modifying our existing HTML form structure to integrate with React Hook Form.


      - **<form onSubmit={handleSubmit(onSubmit)}>** :


       - onSubmit: This is a standard HTML attribute for forms that specifies a

function to call when the form is submitted.


       - {handleSubmit(onSubmit)}: Instead of directly calling our onSubmit
function, we're passing it to the handleSubmit function provided by React

Hook Form. This ensures that React Hook Form handles the submission

process, including validation, before our onSubmit function is called.


   - **<input {...register("authorFirstName")} />** :


       - {...register("authorFirstName")}: This is the crucial part where we "register"
our input field with React Hook Form. The spread syntax (...) takes all the
properties and event handlers that register("authorFirstName") returns and
applies them to this input element. This connects the input to React Hook
Form's state management and validation system. The string
"authorFirstName" becomes the name attribute of the input, which is
important for identifying the input's value when the form is submitted.


   - **<input {...register("authorLastName")} />** : This does the same for the "Author
last name" input field.


   - **<input type="submit" value="Submit" />** : This is a standard HTML submit
button. When clicked, it triggers the form's onSubmit event, which we've hooked

into with handleSubmit.


**Analogy:** Remember our "form management kit"?


   - We used register to connect each input field to the kit. Now, the kit knows about

"authorFirstName" and "authorLastName".


   - We're telling the <form> element to use the handleSubmit function from the kit to
manage the actual submission. This is like telling the kit, "When someone clicks the
submit button, use your special process to gather all the information and then give it
to my onSubmit function."


**Key Concept:** The register function is essential for telling React Hook Form which inputs
to track. Without it, React Hook Form wouldn't know about these inputs or be able to
manage their values or validation.


**Further Learning:**


   - **Handling Form Submission:**
[https://react-hook-form.com/get-started-es7/#_handleSubmit](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact-hook-form.com%2Fget-started-es7%2F%23_handleSubmit)


   - **Registering Inputs:** [https://react-hook-form.com/api/useform/register](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact-hook-form.com%2Fapi%2Fuseform%2Fregister)


#### **Step 4: Handling Form Submission**

Finally, let's create the onSubmit function that will be called when the form is submitted. This function
will receive the form data as an argument.


   - **Timestamp:** [3:15](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D195s)


   - **App.jsx**


code Jsx

```
const onSubmit = (data) => {
 setSearchString(`http://libris.kb.se/xsearch?query=forf:(${data.authorFirstName}
${data.authorLastName})&format=json`);
};

#### **Step 4: Handling Form Submission**

```

   - **Timestamp:** [3:15](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D195s)


   - **App.jsx**


code Jsx

```
const onSubmit = (data) => {

 setSearchString(`http://libris.kb.se/xsearch?query=forf:(${data.authorFirstName}
${data.authorLastName})&format=json`);

};

```

_**MENTOR'S NOTES**_


**What's happening here?**


This is the function that gets executed _after_ React Hook Form has successfully processed
the form submission (including any validation).


      - const onSubmit = (data) => { ... };: We define a function called onSubmit. React
Hook Form automatically passes an object containing all the form's data to this
function when the form is submitted successfully.


      - data: This object will have properties corresponding to the name attributes we used
with the register function. So, data will look something like this: { authorFirstName:
"Lars", authorLastName: "Larsson" }.


      - setSearchString(...): This is a state-updating function (likely from a useState hook
defined earlier in the component, though not shown in this snippet). It's used here to
update the searchString state with a URL that includes the author's first and last
names. This URL is likely used to fetch data from an external API (like libris.kb.se
in this case).


   - [\http://libris.kb.se/xsearch?query=forf:(${data.authorFirstName} $](https://www.google.com/url?sa=E&q=http%3A%2F%2Flibris.kb.se%2Fxsearch%3Fquery%3Dforf%3A($%7Bdata.authorFirstName%7D)
{data.authorLastName})&format=json`: This is a template literal (using backticks ``
``) that constructs the URL. It dynamically inserts the authorFirstName and
`authorLastName` from the `data` object into the URL string.


**Why is this structured this way?**


By separating the form submission logic (handled by handleSubmit) from the actual data
processing (in onSubmit), React Hook Form keeps your code organized and makes it easier
to manage complex forms. The data object provides a clean way to access all the submitted

form values at once.


**Analogy:** Imagine you order a pizza online. handleSubmit is like the confirmation step that
checks if you've filled out all the required fields correctly. Once confirmed, your order
details (the data object) are sent to the kitchen (your onSubmit function), which then
prepares the pizza (updates the searchString state).


**Further Learning:**


   - **Handling Form Submission with handleSubmit:**
[https://react-hook-form.com/get-started-es7/#_handleSubmit (See the "Submit](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact-hook-form.com%2Fget-started-es7%2F%23_handleSubmit)
handler" section).


   -   **Template Literals (for string interpolation):** [https://developer.mozilla.org/en](https://www.google.com/url?sa=E&q=https%3A%2F%2Fdeveloper.mozilla.org%2Fen-US%2Fdocs%2FWeb%2FJavaScript%2FReference%2FTemplate_literals)
[US/docs/Web/JavaScript/Reference/Template_literals](https://www.google.com/url?sa=E&q=https%3A%2F%2Fdeveloper.mozilla.org%2Fen-US%2Fdocs%2FWeb%2FJavaScript%2FReference%2FTemplate_literals)


Of course! Here is the second part of the lesson, building upon what we've already done. Let me know
when you're ready to fill in the "Mentor's Notes."

# **React Router Lesson: Part 2**

## **Introduction to React Bootstrap and React Router**


Now that we have a functional form, let's make our application look a bit more professional by adding
a UI library. We'll use **React Bootstrap**, which is a popular library that provides pre-built React
components based on the Bootstrap framework. This will help us create a clean, modern-looking
interface without writing a lot of custom CSS.


After styling our components, we'll introduce **React Router** to handle navigation within our
application, turning it into a true single-page application (SPA) with different "pages" or views.

#### **Step 5: Installing React Bootstrap**


Just like with React Hook Form, we need to install the necessary packages using npm. We'll install both
react-bootstrap (the React components) and bootstrap (the underlying CSS framework).


   - **Timestamp:** [1:40](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D100s)


code Bash

```
npm install react-bootstrap bootstrap```

```

**Step 5: Installing React Bootstrap**


   - [Timestamp: 1:40](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D100s)

```
code Bash

npm install react-bootstrap bootstrap

```

**MENTOR'S NOTES**


**What's happening here?**


This command installs two separate packages:


react-bootstrap: This package contains the React components (like <Button>, <Table>, <Card>, etc.)
that we can use in our JSX code. These components are built to work seamlessly with React's state and

props system.


bootstrap: This package contains the actual CSS and JavaScript files that provide the styling and some
of the functionality for the components. The React components from react-bootstrap are designed to
work with this specific CSS.


**Why are we doing this?**


Building a good-looking, responsive UI from scratch takes a lot of time and CSS knowledge. A UI
library like React Bootstrap gives us a set of professional, pre-styled, and accessible components right
out of the box. This lets us focus more on our application's logic and less on writing custom CSS.


**Analogy:** Think of building with LEGOs. You could try to create your own plastic bricks from scratch
(writing your own CSS), but it's much faster and easier to use a pre-made LEGO set (React Bootstrap)
that gives you all the standard bricks you need to build your creation. The bootstrap package is the
instruction manual and the design philosophy that makes all the bricks fit together perfectly.


**Further Learning:**


                             **React Bootstrap Documentation:** https://react [bootstrap.github.io/](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact-bootstrap.github.io%2F)


**Sharing State Between Components (relevant for understanding component-based UI):**
[https://react.dev/learn/sharing-state-between-components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)

#### **Step 6: Importing Bootstrap CSS**


For the styles to apply, we need to import the main Bootstrap CSS file into our project. The best place to do this is at the top
level of our application, in the `main.jsx` file.


**Timestamp: [12:12]**


**`main.jsx`**


jsx

```
import 'bootstrap/dist/css/bootstrap.min.css';

#### **_Step 6: Importing Bootstrap CSS_**

```

   - _**Timestamp:**_ _[12:12](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D732s)_


   - _**main.jsx**_


**import** 'bootstrap/dist/css/bootstrap.min.css';


**MENTOR'S NOTES**


**What's happening here?**


This line imports the core Bootstrap stylesheet directly into our application's entry point
(main.jsx). When our app is bundled for the browser, this CSS file will be included, making
all of Bootstrap's styles available to our components globally.


      - 'bootstrap/dist/css/bootstrap.min.css': This is the path to the CSS file inside the
bootstrap package we installed.


          - dist: Short for "distribution." This folder contains the final, compiled code
that's ready to be used in a browser.


          - .min.css: The .min stands for "minified." This means all the whitespace and

comments have been removed from the CSS file to make it smaller and

faster to download.


**Why do we put this in main.jsx?**


We want the Bootstrap styles to be available to our entire application. By importing it in
main.jsx, which is the root of our React component tree, we ensure that any component, no
matter how deeply nested, can use the Bootstrap classes and styles.


**Further Learning:**


      - **Adding Interactivity (covers adding event handlers, which often works with**

                                       **styled components):** [https://react.dev/learn/adding](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fadding-interactivity) interactivity


      - **How to add CSS files in React:** [https://react.dev/learn/start-a-new-react-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23adding-css-and-other-assets)
[project#adding-css-and-other-assets](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23adding-css-and-other-assets)

#### **Step 7: Refactoring the UI with React Bootstrap Components**


Now we can start replacing our standard HTML divs and other elements with components from React
Bootstrap. Let's refactor our search results to use the <Table> component for a cleaner, more organized

look.


   - **Timestamp:** [2:17](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D137s) and [12:50](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D770s)


   - **App.jsx**


code Jsx

```
import Table from 'react-bootstrap/Table';

// ... (inside the App component's return statement)

<h2>Saved search</h2>

<Table striped hover>

 <thead>

  <tr>

    <th>Creator</th>

    <th>Title</th>

```

```
    <th>Action</th>

  </tr>

 </thead>

 <tbody>

{backendData && backendData.map((item) => (

    <tr key={item.identifier}>

     <td>{item.creator}</td>

     <td>{item.title}</td>

     <td><button>Add record</button></td>

    </tr>

))}

 </tbody>

</Table>

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_We are replacing a standard HTML <table> element with the <Table> component from the_
_react-bootstrap library._


      - _import Table from 'react-bootstrap/Table';: Instead of importing the entire library,_
_we are only importing the specific component we need (Table). This is a good_
_practice as it can help reduce the final bundle size of our application._


      - _<Table striped hover>: We are using the imported <Table> component in our JSX._
_The words striped and hover are_ _**props**_ _(properties) that we pass to the component._
_These props are like configuration options._


          - _striped: This prop tells the <Table> component to add a zebra-striping style_
_to the table rows (alternating background colors), making it easier to read._


          - _hover: This prop adds a hover effect, highlighting the row that the user's_
_mouse is currently over._


_**Why is this better?**_


_Instead of writing custom CSS rules like tr:nth-child(even) { background-color: #f2f2f2; }_
_or tr:hover { background-color: #ddd; }, we just add a simple, readable prop to our_
_component. This makes our code cleaner and more declarative. We are describing what we_
_want (a striped, hoverable table), not how to achieve it with CSS._


_**Further Learning:**_


      - _**Importing and Exporting Components:**_ _[https://react.dev/learn/importing-and-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fimporting-and-exporting-components)_

_-_
_exporting_ _components_


      - _**Passing Props to a Component:**_ _[https://react.dev/learn/passing-props-to-a-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)_

_[component](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)_

#### **Step 8: Introducing React Router**


Our application currently shows everything on one page. To create a more traditional multi-page
experience (without full page reloads), we need a routing library. **React Router** is the standard for this
in the React ecosystem. It allows us to map specific URLs to different components, so we can show

different content based on the URL in the browser's address bar.

#### **Step 9: Installing React Router**


First, let's install the library.


   - **Timestamp:** [22:55](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D1375s)


code Bash

```
npm install react-router-dom

```

**MENTOR'S NOTES**


**What's happening here?**


We are installing react-router-dom, the standard library for handling routing in web applications built

with React.


**What is routing?**


In a traditional website, clicking a link like /about sends a request to a server, which then sends back a
completely new HTML page. In a **Single-Page Application (SPA)**, like most React apps, the initial
page load is the only full page load. After that, when you click a link, React Router intercepts the click.
Instead of asking the server for a new page, it changes the URL in the browser's address bar and then
tells React to render a different component without a page refresh. This is called **client-side routing**,
and it makes the application feel much faster and more like a desktop app.


**Further Learning:**


**React Router Official Tutorial:** [https://reactrouter.com/en/main/start/tutorial](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial)


**React doesn't have a built-in routing solution, so third-party libraries are used. This section on**
**starting a project mentions routing as a key feature to add:** [https://react.dev/learn/start-a-new-react-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23can-i-use-react-without-a-framework)
[project#can-i-use-react-without-a-framework](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23can-i-use-react-without-a-framework)


#### **Step 10: Setting Up the Router**

The modern way to configure React Router is by creating a "router" object that defines all the possible
paths in our application. We'll do this in our `main.jsx` file.

```
*  **Timestamp:** [25:25](https://www.youtube.com/watch?v=VIDEO_ID&t=1525s)

*  **`main.jsx`**

```jsx

import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import App from './App.jsx';

import Edit from './pages/Edit.jsx';

import File from './pages/File.jsx';

// ... import other page components

const router = createBrowserRouter([

{

path: "/",

element: <App />,

  // We'll add more here later

},

{

path: "/edit",

element: <Edit />,

},

{

path: "/file",

element: <File />,

},

 // ... other routes

]);

```

```
// Replace the old <App /> with the RouterProvider

ReactDOM.createRoot(document.getElementById('root')).render(

 <React.StrictMode>

  <RouterProvider router={router} />

 </React.StrictMode>

);

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_We are installing react-router-dom, the standard library for handling routing in web_
_applications built with React._


_**What is routing?**_


_In a traditional website, clicking a link like /about sends a request to a server, which then_
_sends back a completely new HTML page. In a_ _**Single-Page Application (SPA)**_ _, like most_
_React apps, the initial page load is the only full page load. After that, when you click a link,_
_React Router intercepts the click. Instead of asking the server for a new page, it changes_
_the URL in the browser's address bar and then tells React to render a different component_
_without a page refresh. This is called_ _**client-side routing**_ _, and it makes the application feel_
_much faster and more like a desktop app._


_**Further Learning:**_


      - _**React Router Official Tutorial:**_ _[https://reactrouter.com/en/main/start/tutorial](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial)_


      - _**React doesn't have a built-in routing solution, so third-party libraries are used.**_
_**This section on starting a project mentions routing as a key feature to add:**_
_[https://react.dev/learn/start-a-new-react-project#can-i-use-react-without-a-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23can-i-use-react-without-a-framework)_
_[framework](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23can-i-use-react-without-a-framework)_

#### **Step 11: Creating Nested Routes and Layouts**


A powerful feature of React Router is nesting routes. We want our main App component (with the
menu and search bar) to act as a layout, and for our "page" components (Edit, File, etc.) to be rendered
_inside_ that layout.


To do this, we use the <Outlet /> component in our parent route (App.jsx) and define the other routes as

its children.


   - **Timestamp:** [43:32](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D2612s)


   - **App.jsx (The Layout Component)**


code Jsx

```
import { Outlet } from 'react-router-dom';

function App() {

 // ... (existing code)

 return (

  <div>

    <Menu entries={menuItems} />

    <Outlet /> {/* Child routes will be rendered here */}

{/* ... (rest of the layout) */}

  </div>

);

}```

* ** **
    `main.jsx` (Updated Router Configuration)

```jsx

const router = createBrowserRouter([

{

path: "/",

element: <App />,

children: [

{

path: "edit", // Note: no leading "/"

element: <Edit />,

},

{

path: "file",

element: <File />,

},

    // ... other child routes

],

},

```

```
]);

```

**MENTOR'S NOTES**


**What's happening here?**


This is one of the most powerful features of React Router: creating layouts. We want our Menu to be
visible on multiple pages. Instead of copying and pasting the <Menu /> component into every page, we
can make our <App /> component a "layout route."


_1._ **The <Outlet /> Component:** Think of <Outlet /> as a placeholder. In App.jsx, we're saying,

"Render the menu, and then, right here, render whatever child component matches the current

URL."


_2._ **The children Array:** In our router configuration, we've nested the routes for /edit and /file

inside the route for /. This tells React Router that they are children of the / route.


**How it works together:**


   - When you go to /edit, React Router sees that it's a child of /.


   - It first renders the parent's element, <App />.


   - When it gets to the <Outlet /> inside <App />, it renders the child's element, <Edit />.


This creates a component structure like this:


code Code

```
<App>

 <Menu />

 <Outlet> -> becomes -> <Edit />

<App>

```

**Further Learning:**


   - **Nested Routes and Layouts:** [https://reactrouter.com/en/main/start/tutorial#nested-routes](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial%23nested-routes)


   - **The <Outlet> Component:** [https://reactrouter.com/en/main/components/outlet](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fcomponents%2Foutlet)

#### **Step 12: Creating Dynamic Routes**


Sometimes, we need a route that can handle dynamic values, like an ID for a specific item. For
example, we might want a URL like /edit/123 to edit the item with ID 123.


   - **Timestamp:** [52:45](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D3165s)


   - **main.jsx (Router with Dynamic Segment)**


code Jsx


```
// ... inside the children array

{

 path: "edit/:id", // The ":id" is a dynamic parameter

 element: <Edit />,

}

```

   - **Edit.jsx (Accessing the Dynamic Parameter)**


code Jsx

```
  import { useParams } from 'react-router-dom';

function Edit() {

const { id } = useParams(); // Hook to get URL parameters

 return (

  <div>

    <h1>Edit Page</h1>

    <p>Editing item with ID: {id}</p>

  </div>

);

}

#### **_Step 12: Creating Dynamic Routes_**

```

   - _**Timestamp:**_ _[52:45](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D3165s)_


   - _**main.jsx (Router with Dynamic Segment)**_


_code Jsx_


   - `{ path: "edit/:id", element: <Edit /> }`


   - _**Edit.jsx (Accessing the Dynamic Parameter)**_


_code Jsx_


   - **`import`** `{ useParams }` **`from`** `'react-router-dom';`


```
 function Edit() {

 const { id } = useParams(); // e.g., if URL is /edit/123, id will be "123"

  return <p>Editing item with ID: {id}</p>;

 }

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_We're creating a route that can match multiple different URLs based on a pattern._


   - _path: "edit/:id": The colon (:) in front of id marks it as a_ _**dynamic segment**_ _or a_
_**URL parameter**_ _. This path will match /edit/123, /edit/abc, /edit/anything, and so on._


   - _useParams(): This is a hook from React Router that gives us access to these_
_dynamic parts of the URL. It returns an object where the keys are the names of the_
_parameters we defined in the path (in this case, id) and the values are what's_
_actually in the URL._


   - _const { id } = useParams();: We use object destructuring to pull the id value out of_
_the object returned by useParams(). Now we can use this id in our component, for_
_example, to fetch the specific data for that item from an API._


_**Further Learning:**_


   - _**URL Parameters:**_ _[https://reactrouter.com/en/main/start/tutorial#url-params](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial%23url-params)_


   - _**The useParams Hook:**_ _[https://reactrouter.com/en/main/hooks/use-params](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fhooks%2Fuse-params)_


#### **Step 13: Navigating Programmatically**

What if we want to redirect the user after an action, like submitting a form? We can't use a link for that.
Instead, we use the useNavigate hook, which gives us a function to change the URL programmatically.


   - **Timestamp:** [41:26](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D2486s)


   - **App.jsx**


code Jsx

```
import { useNavigate } from 'react-router-dom';

```

```
function App() {

const navigate = useNavigate();

const handleMenuClick = (path) => {

navigate(path); // Programmatically changes the URL

};

 // ...

}

```

_**Of course. Here are the detailed mentor's notes for the second part of the lesson.**_

#### **_Step 5: Installing React Bootstrap_**


   - _**Timestamp:**_ _[1:40](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D100s)_


_code Bash_

```
npm install react-bootstrap bootstrap

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_This command installs two separate packages:_


_1. react-bootstrap: This package contains the React components (like <Button>,_

_<Table>, <Card>, etc.) that we can use in our JSX code. These components are_
_built to work seamlessly with React's state and props system._


_2. bootstrap: This package contains the actual CSS and JavaScript files that provide_

_the styling and some of the functionality for the components. The React components_
_from react-bootstrap are designed to work with this specific CSS._


_**Why are we doing this?**_


_Building a good-looking, responsive UI from scratch takes a lot of time and CSS_
_knowledge. A UI library like React Bootstrap gives us a set of professional, pre-styled, and_
_accessible components right out of the box. This lets us focus more on our application's_
_logic and less on writing custom CSS._


_**Analogy:**_ _Think of building with LEGOs. You could try to create your own plastic bricks_
_from scratch (writing your own CSS), but it's much faster and easier to use a pre-made_
_LEGO set (React Bootstrap) that gives you all the standard bricks you need to build your_


_creation. The bootstrap package is the instruction manual and the design philosophy that_
_makes all the bricks fit together perfectly._


_**Further Learning:**_


      - _-_
_**React Bootstrap Documentation:**_ _https://react_ _[bootstrap.github.io/](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact-bootstrap.github.io%2F)_


      - _**Sharing State Between Components (relevant for understanding component-**_
_**based UI):**_ _[https://react.dev/learn/sharing-state-between-components](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fsharing-state-between-components)_

#### **_Step 6: Importing Bootstrap CSS_**


   - _**Timestamp:**_ _[12:12](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D732s)_


   - _**main.jsx**_


_code Jsx_

```
  import 'bootstrap/dist/css/bootstrap.min.css';

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_This line imports the core Bootstrap stylesheet directly into our application's entry point_
_(main.jsx). When our app is bundled for the browser, this CSS file will be included, making_
_all of Bootstrap's styles available to our components globally._


      - _'bootstrap/dist/css/bootstrap.min.css': This is the path to the CSS file inside the_
_bootstrap package we installed._


          - _dist: Short for "distribution." This folder contains the final, compiled code_
_that's ready to be used in a browser._


          - _.min.css: The .min stands for "minified." This means all the whitespace and_
_comments have been removed from the CSS file to make it smaller and faster_

_to download._


_**Why do we put this in main.jsx?**_


_We want the Bootstrap styles to be available to our entire application. By importing it in_
_main.jsx, which is the root of our React component tree, we ensure that any component, no_
_matter how deeply nested, can use the Bootstrap classes and styles._


_**Further Learning:**_


      - _**Adding Interactivity (covers adding event handlers, which often works with styled**_

_-_
_**components):**_ _[https://react.dev/learn/adding](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fadding-interactivity)_ _interactivity_


      - _**How to add CSS files in React:**_ _[https://react.dev/learn/start-a-new-react-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23adding-css-and-other-assets)_
_[project#adding-css-and-other-assets](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23adding-css-and-other-assets)_


#### **_Step 7: Refactoring the UI with React Bootstrap Components_**


   - _**Timestamp:**_ _[2:17 and 12:50](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D770s)_


   - _**App.jsx**_


_code Jsx_

```
import Table from 'react-bootstrap/Table';

<Table striped hover>

{/* ... table content ... */}

</Table>

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_We are replacing a standard HTML <table> element with the <Table> component from the_
_react-bootstrap library._


      - _import Table from 'react-bootstrap/Table';: Instead of importing the entire library,_
_we are only importing the specific component we need (Table). This is a good_
_practice as it can help reduce the final bundle size of our application._


      - _<Table striped hover>: We are using the imported <Table> component in our JSX._
_The words striped and hover are_ _**props**_ _(properties) that we pass to the component._
_These props are like configuration options._


          - _striped: This prop tells the <Table> component to add a zebra-striping style_
_to the table rows (alternating background colors), making it easier to read._


          - _hover: This prop adds a hover effect, highlighting the row that the user's_
_mouse is currently over._


_**Why is this better?**_


_Instead of writing custom CSS rules like tr:nth-child(even) { background-color: #f2f2f2; }_
_or tr:hover { background-color: #ddd; }, we just add a simple, readable prop to our_
_component. This makes our code cleaner and more declarative. We are describing what we_
_want (a striped, hoverable table), not how to achieve it with CSS._


_**Further Learning:**_


      - _**Importing and Exporting Components:**_ _[https://react.dev/learn/importing-and-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fimporting-and-exporting-components)_

_-_
_exporting_ _components_


      - _**Passing Props to a Component:**_ _[https://react.dev/learn/passing-props-to-a-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)_

_[component](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fpassing-props-to-a-component)_


#### **_Step 9: Installing React Router_**


   - _**Timestamp:**_ _[22:55](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D1375s)_


_code Bash_

```
npm install react-router-dom

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_We are installing react-router-dom, the standard library for handling routing in web_
_applications built with React._


_**What is routing?**_


_In a traditional website, clicking a link like /about sends a request to a server, which then_
_sends back a completely new HTML page. In a_ _**Single-Page Application (SPA)**_ _, like most_
_React apps, the initial page load is the only full page load. After that, when you click a link,_
_React Router intercepts the click. Instead of asking the server for a new page, it changes_
_the URL in the browser's address bar and then tells React to render a different component_
_without a page refresh. This is called_ _**client-side routing**_ _, and it makes the application feel_
_much faster and more like a desktop app._


_**Further Learning:**_


      - _**React Router Official Tutorial:**_ _[https://reactrouter.com/en/main/start/tutorial](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial)_


      - _**React doesn't have a built-in routing solution, so third-party libraries are used.**_
_**This section on starting a project mentions routing as a key feature to add:**_
_[https://react.dev/learn/start-a-new-react-project#can-i-use-react-without-a-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23can-i-use-react-without-a-framework)_
_[framework](https://www.google.com/url?sa=E&q=https%3A%2F%2Freact.dev%2Flearn%2Fstart-a-new-react-project%23can-i-use-react-without-a-framework)_

#### **_Step 10: Setting Up the Router_**


   - _**Timestamp:**_ _[25:25](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D1525s)_


   - _**main.jsx**_


code Jsx

```
  import { createBrowserRouter, RouterProvider } from 'react-router-dom';

// ...

const router = createBrowserRouter([

{ path: "/", element: <App /> },

{ path: "/edit", element: <Edit /> },

]);

```

```
ReactDOM.createRoot(document.getElementById('root')).render(

 <React.StrictMode>

  <RouterProvider router={router} />

 </React.StrictMode>

);

```

**MENTOR'S NOTES**


**What's happening here?**


We are defining the entire routing structure of our application.


1. createBrowserRouter([...]): This function takes an array of "route objects" and

creates a router instance. Each object defines a mapping between a URL path and a

React component.


          - { path: "/", element: <App /> }: This tells the router, "When the user is at the
root URL (e.g., http://localhost:3000/), render the <App /> component."


          - { path: "/edit", element: <Edit /> }: This means, "When the user navigates
to /edit, render the <Edit /> component."


2. <RouterProvider router={router} />: This component from React Router is what

actually makes the routing work. We wrap our application with it and pass it the
router configuration we just created. It listens for URL changes and renders the
correct component based on the rules we defined.


We replace our original <App /> in ReactDOM.render with <RouterProvider /> because the
router is now in charge of deciding which page component to show, including the <App />
component itself.


**Further Learning:**


      - **createBrowserRouter:**

[https://reactrouter.com/en/main/router-components/browser-router](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Frouter-components%2Fbrowser-router)


      - **RouterProvider:** [https://reactrouter.com/en/main/router-components/router-](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Frouter-components%2Frouter-provider)
[provider](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Frouter-components%2Frouter-provider)

#### **Step 11: Creating Nested Routes and Layouts**


   - **Timestamp:** [43:32](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D2612s)


   - **App.jsx (The Layout Component)**


code Jsx




```
function App() {

 return (

  <div>

    <Menu />

    <Outlet /> {/* Child routes render here */}

  </div>

);

}

```

   - _**main.jsx (Updated Router Configuration)**_


_code Jsx_


   - **`const`** `router` `=` `createBrowserRouter([`

```
     {

      path: "/",

      element: <App />, // App is the layout

      children: [ // These routes render inside App's <Outlet />

       { path: "edit", element: <Edit /> },

       { path: "file", element: <File /> },

      ],

     },

    ]);

```

**MENTOR'S NOTES**


**What's happening here?**


This is one of the most powerful features of React Router: creating layouts. We want our
Menu to be visible on multiple pages. Instead of copying and pasting the <Menu />
component into every page, we can make our <App /> component a "layout route."


_1._ **The <Outlet /> Component:** Think of <Outlet /> as a placeholder. In App.jsx,

we're saying, "Render the menu, and then, right here, render whatever child
component matches the current URL."


_2._ **The children Array:** In our router configuration, we've nested the routes for /edit

and /file inside the route for /. This tells React Router that they are children of the /

route.


**How it works together:**


      - When you go to /edit, React Router sees that it's a child of /.


      - It first renders the parent's element, <App />.


      - When it gets to the <Outlet /> inside <App />, it renders the child's element,

<Edit />.


This creates a component structure like this:


code Code

```
   <App>

    <Menu />

    <Outlet> -> becomes -> <Edit />

   <App>

```

**Further Learning:**


      -       **Nested Routes and Layouts:** [https://reactrouter.com/en/main/start/tutorial#nested](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial%23nested-routes)

[routes](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial%23nested-routes)


      - **The <Outlet> Component:** [https://reactrouter.com/en/main/components/outlet](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fcomponents%2Foutlet)

#### **Step 12: Creating Dynamic Routes**


   - **Timestamp:** [52:45](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D3165s)


   - **main.jsx (Router with Dynamic Segment)**


code Jsx


   - `{ path: "edit/:id", element: <Edit /> }`


   - **Edit.jsx (Accessing the Dynamic Parameter)**


code Jsx

```
    import { useParams } from 'react-router-dom';

    function Edit() {

    const { id } = useParams(); // e.g., if URL is /edit/123, id will be "123"

     return <p>Editing item with ID: {id}</p>;

    }

```

   

**MENTOR'S NOTES**


**What's happening here?**


We're creating a route that can match multiple different URLs based on a pattern.


      - path: "edit/:id": The colon (:) in front of id marks it as a **dynamic segment** or a
**URL parameter** . This path will match /edit/123, /edit/abc, /edit/anything, and so on.


      - useParams(): This is a hook from React Router that gives us access to these dynamic
parts of the URL. It returns an object where the keys are the names of the
parameters we defined in the path (in this case, id) and the values are what's actually

in the URL.


      - const { id } = useParams();: We use object destructuring to pull the id value out of
the object returned by useParams(). Now we can use this id in our component, for
example, to fetch the specific data for that item from an API.


**Further Learning:**


      - **URL Parameters:** [https://reactrouter.com/en/main/start/tutorial#url-params](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial%23url-params)


      - **The useParams Hook:** [https://reactrouter.com/en/main/hooks/use-params](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fhooks%2Fuse-params)

#### **Step 13: Navigating Programmatically**


   - **Timestamp:** [41:26](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D2486s)


   - **App.jsx**


code Jsx

```
    import { useNavigate } from 'react-router-dom';

    function App() {

    const navigate = useNavigate();

    const handleSomeAction = () => {

      // ...do something, like submit a form...

    navigate('/success'); // Then, redirect the user

    };

     // ...

    }

```

_**MENTOR'S NOTES**_


_**What's happening here?**_


_Sometimes, you need to change the page based on an action, not a user clicking a link. For_
_example, after a user successfully logs in, you want to send them to their dashboard. This is_
_called_ _**programmatic navigation**_ _._


      - _useNavigate(): This hook gives you a navigate function._


      - _const navigate = useNavigate();: We call the hook to get the function and store it in_

_a variable._


      - _navigate('/some-path'): To perform the navigation, you simply call this function with_
_the path you want to go to. React Router will handle changing the URL and_
_rendering the correct component._


_This is the correct way to redirect users within a React SPA, as it avoids a full page reload._


_**Further Learning:**_


      - _**Programmatic Navigation:**_

_-_
_[https://reactrouter.com/en/main/start/tutorial#programmatic](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fstart%2Ftutorial%23programmatic-navigation)_ _navigation_


      - _-_
_**The useNavigate Hook:**_ _[https://reactrouter.com/en/main/hooks/use](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fhooks%2Fuse-navigate)_ _navigate_

#### **Step 14: Linking Between Routes with <NavLink>**


Finally, let's replace our menu's button elements with proper navigation links. Instead of standard <a>
tags (which cause a full page reload), React Router provides the <Link> and <NavLink> components.


We'll use <NavLink> because it can automatically detect when it's the "active" link and apply a special
style.


   - **Timestamp:** [37:44](https://www.google.com/url?sa=E&q=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DVIDEO_ID%26t%3D2264s)


   - **Menu.jsx**


code Jsx

```
import { NavLink } from 'react-router-dom';

function Menu({ entries }) {

 return (

  <nav>

    <ul>

{entries.map((entry) => (

      <li key={entry.index}>

       <NavLink

```

```
to={`/${entry.content.toLowerCase()}`}

className={({ isActive }) => (isActive ? 'active' : '')}

       >

{entry.content}

       </NavLink>

      </li>

))}

    </ul>

  </nav>

);

}

```

**MENTOR'S NOTES**


**What's happening here?**


We are creating navigation links the right way in a React Router application.


      - **Why not <a href="/file">?** A standard <a> tag will trigger a full page reload,
which defeats the purpose of a Single-Page Application and clears all of your

component state.


      - **<NavLink>:** This special component from React Router renders an <a> tag but
intercepts the click event. Instead of reloading the page, it just tells the router to
update the URL and render the new component.


      - **className={({ isActive }) => ...}:** This is the key feature that makes <NavLink>
different from a regular <Link>. The className prop can accept a function. React
Router will call this function and pass it an object containing a boolean isActive.
This boolean is true if the link's to prop matches the current URL. We can use this to
conditionally apply an 'active' class for styling, like highlighting the current page in

the menu.


**Further Learning:**


      - **The <Link> Component:** [https://reactrouter.com/en/main/components/link](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fcomponents%2Flink)


      - **The <NavLink> Component (for active styling):**
[https://reactrouter.com/en/main/components/nav-link](https://www.google.com/url?sa=E&q=https%3A%2F%2Freactrouter.com%2Fen%2Fmain%2Fcomponents%2Fnav-link)


This concludes our lesson on styling with React Bootstrap and setting up routing with React Router.
You now have the foundational skills to build a structured, multi-page React application.


