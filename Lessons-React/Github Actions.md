## **Table of Contents**

Core Deployment Concepts.......................................................................................................................3

The Build Step: From Source Code to Static Files...............................................................................3
The Deployment Step: Making Files Publicly Accessible....................................................................3
Building a React + Vite Project..................................................................................................................4

The package.json File............................................................................................................................4

The build script.................................................................................................................................4
The preview script............................................................................................................................5
The Build Output (dist folder)...............................................................................................................5

Purpose of the dist folder..................................................................................................................5
Minification and Transpilation.........................................................................................................6
Hosting on GitHub Pages...........................................................................................................................7

Deployment Methods............................................................................................................................ 7

Method 1: Deploy from a Branch.....................................................................................................7
Method 2: GitHub Actions...............................................................................................................7
The Problem with "Deploy from a Branch" for Build-Step Projects....................................................8
Core Deployment Concepts.......................................................................................................................8

The Build Step: From Source Code to Static Files...............................................................................8
The Deployment Step: Making Files Publicly Accessible....................................................................8
Building a React + Vite Project..................................................................................................................9

The package.json File............................................................................................................................9

The build script.................................................................................................................................9
The preview script............................................................................................................................9
The Build Output (dist folder)...............................................................................................................9

Purpose of the dist folder..................................................................................................................9
Minification and Transpilation.........................................................................................................9
Hosting on GitHub Pages...........................................................................................................................9

Deployment Methods............................................................................................................................ 9

Method 1: Deploy from a Branch.....................................................................................................9
Method 2: GitHub Actions...............................................................................................................9
The Problem with "Deploy from a Branch" for Build-Step Projects....................................................9
Configuring a Vite Project for GitHub Pages..........................................................................................10

Updating the Vite Configuration (vite.config.js).................................................................................10

The base option...............................................................................................................................10
Creating the GitHub Actions Workflow..............................................................................................10

The .github/workflows directory....................................................................................................10
The deploy.yml file.........................................................................................................................11
The GitHub Actions Workflow (deploy.yml)...........................................................................................12

Anatomy of the Workflow File............................................................................................................12

name: Naming the workflow..........................................................................................................12
on: Triggering the workflow...........................................................................................................12
permissions: Granting permissions to the workflow......................................................................13
jobs: Defining the sequence of tasks..............................................................................................13
runs-on: Specifying the virtual machine.........................................................................................14
steps: The individual commands to run..........................................................................................14
Key Workflow Steps Explained..........................................................................................................15

actions/checkout: Checking out the repository code......................................................................15


actions/setup-node: Setting up the Node.js environment...............................................................15
run: npm install: Installing project dependencies...........................................................................16
run: npm run build: Running the build script.................................................................................16
actions/upload-pages-artifact: Preparing the site for deployment..................................................16
actions/deploy-pages: Deploying the site.......................................................................................17
Triggering and Monitoring the Deployment............................................................................................17

Pushing to GitHub to Trigger the Workflow.......................................................................................17
Viewing Workflow Progress in the "Actions" Tab..............................................................................18
Verifying the Deployed Site................................................................................................................18


# **Core Deployment Concepts**

In modern web development, the code you write is often not the same code that runs in the user's
browser. A "build" process transforms your source code into optimized, static files (HTML, CSS,
JavaScript). "Deployment" is the process of placing these files onto a server where users can access

                                                              [them. You can read more about the basics of JavaScript build systems here: https://javascript.info/build](https://www.google.com/url?sa=E&q=https%3A%2F%2Fjavascript.info%2Fbuild-tools)

[tools](https://www.google.com/url?sa=E&q=https%3A%2F%2Fjavascript.info%2Fbuild-tools)

## **The Build Step: From Source Code to Static Files**


The build step is a process where a tool (like Vite) takes your development code (e.g., React
components written in JSX) and transforms it into plain HTML, CSS, and JavaScript files that any web
browser can understand. This process also optimizes the files to make them smaller and faster to load.


This isn't something you run in a single file, but a command you run in the terminal for your project.
It's defined in your package.json file.


**code JSON**

```
// In package.json

"scripts": {

 "dev": "vite",

 "build": "vite build", // This is the build command

 "preview": "vite preview"

}

```

To run it, you would type npm run build in the Windsurf terminal.


   - _You should never directly edit the files inside the build output folder (usually named dist or_
_build). Any changes you make there will be overwritten the next time you run the build_

_command._

## **The Deployment Step: Making Files Publicly Accessible**


Deployment is the action of taking the static files generated by the build step and placing them on a
web server. A web server is a computer that is always connected to the internet, allowing anyone to
access your website through a URL (like https://example.com). Services like GitHub Pages act as this
web server for you.


Deployment is usually a configuration step or a command, not a block of code you write. With GitHub
Actions, it's defined in a YAML file. A simple conceptual example is using a command-line tool to
upload files.


**code Bash**

```
# This is a conceptual example, not for direct use here.

# It shows the idea of copying your built files to a remote server.

scp -r ./dist/* user@your-server.com:/ var /www/html

```

   - _Forgetting to run the build step before deploying is a common mistake. If you deploy your_
_source code instead of the built files, the website will not work because browsers don't_
_understand JSX or other special syntax out-of-the-box._

# **Building a React + Vite Project**

Vite is a modern frontend build tool that provides an extremely fast development environment and
bundles your code for production. It processes your files, including React's JSX syntax, and turns them
into a highly optimized set of static assets.

## **The package.json File**

### **The build script**


The build script in package.json is a shortcut for the command that starts the build process. When you
run npm run build, your package manager (npm) looks for this script and executes the associated
command, which in this case is vite build. This tells Vite to bundle and optimize your project for
production.


**code JSON**

```
// package.json

{

 "name": "my-react-app",

 "version": "1.0.0",

 "scripts": {

  "build": "vite build"

 }

}

```

In the terminal: npm run build


   - _Running npm run build will typically delete the old dist folder before creating a new one. Be_
_sure you don't have any important files stored in there that aren't part of your source code._

### **The preview script**


The preview script in package.json lets you test the production build of your site on your local machine
before you deploy it. After running npm run build, you can run npm run preview. This starts a simple
local web server that serves the files from your dist folder, so you can see exactly what your users will

see.


**code JSON**

```
// package.json

{

 "scripts": {

  "build": "vite build",

  "preview": "vite preview"

 }

}

```

In the terminal, after building: npm run preview


   - _The preview server is not a full-featured development server. It doesn't automatically reload_
_when you change your source code; you must run npm run build again and restart the preview_
_server to see changes._

## **The Build Output (dist folder)**

### **Purpose of the dist folder**


The dist folder (short for "distribution") is the default output directory for the vite build command. It
contains all the static, optimized files that make up your website. This folder is the final product that
you will deploy to your hosting service. It's self-contained and has everything needed to run your site.


After running npm run build, your project structure will look something like this. The dist folder is
generated automatically.


**code Text**

```
my-react-app/

```

```
├── dist/

│ ├── assets/

│ │ ├── index-C922ZGMY.js

│ │ └── index-D19-DH40.css

│ └── index.html

├── src/

└── package .json

```

   - _By default, the dist folder is listed in your .gitignore file. This is intentional, as you should not_
_commit this generated code to your repository. The build should happen on the deployment_
_server or in a CI/CD pipeline._

### **Minification and Transpilation**


These are two key optimization processes during the build step. **Transpilation** converts modern
JavaScript and JSX into an older version of JavaScript that all browsers can understand. **Minification**
removes all unnecessary characters from the code (like whitespace, comments, and long variable
names) to make the file size as small as possible, which speeds up download times for users.


code JavaScript
```
// Before build (JSX in your source code)

const Greeting = ({ name }) => {

 // A simple component

 return <h1> Hello, {name}! </h1> ;

};

// After build (minified & transpiled JavaScript in dist/assets/)

// This is a simplified representation.

function G(e){return React.createElement("h1",null,"Hello, ",e.name,"!")}

```

   - _Minified code is nearly impossible for humans to read. If you need to debug an issue on your_
_live site, you'll need to use "source maps," which are special files (also generated by the build_
_tool) that map the minified code back to your original source code._


# **Hosting on GitHub Pages**

GitHub Pages is a static site hosting service that takes HTML, CSS, and JavaScript files straight from a
repository on GitHub, optionally runs them through a build process, and publishes a website.

## **Deployment Methods**

### **Method 1: Deploy from a Branch**


This is the simplest way to deploy on GitHub Pages. You configure your repository to treat a specific
branch (like main) and folder (like / or /docs) as the source for your website. GitHub will automatically
take the files from that location and host them. This method is ideal for simple sites that don't require a
build step.


This is a configuration in your GitHub repository settings, not code.


1. Go to your repository on GitHub.


2. Click Settings > Pages.


3. Under "Build and deployment," select Deploy from a branch.


4. Choose the main branch and the /(root) folder, then click Save.


   - _This method serves files exactly as they are in the repository. If your project needs a build step_
_(like a React app), this will not work correctly because you would be deploying the source code,_
_not the final browser-readable HTML, CSS, and JS files._

### **Method 2: GitHub Actions**


GitHub Actions is a powerful automation tool built into GitHub. For deployment, you can create a
"workflow" file that tells GitHub to perform a series of steps whenever you push code. This workflow
can install dependencies, run your build script (npm run build), and then deploy the contents of the dist
folder to GitHub Pages. This is the correct method for projects that require a build step.


This is a configuration in your GitHub repository settings.


1. Go to Settings > Pages.


2. Under "Build and deployment," select GitHub Actions.


3. GitHub will suggest a workflow for you (e.g., "Static HTML"). You can then configure it to run

your specific build commands.


   - _GitHub Actions workflows are configured using YAML files, which are very strict about_
_indentation. A single misplaced space can cause the entire workflow to fail, so it's important to_
_be careful when editing these files._


## **The Problem with "Deploy from a Branch" for Build-Step Projects**

"Deploy from a branch" simply takes the files from your repository and hosts them. A React project's
source code contains JSX and other syntax that browsers don't understand. The necessary npm run
build step is never executed in this mode. As a result, GitHub Pages would be trying to serve your raw,
un-built source files, and the site would fail to load, often showing a blank page or an error.


Imagine your index.html in the source code looks like this. The browser doesn't know what to do with

src/main.jsx.


**code Html**

```
<!-- Your source index.html -->

<body>

 <div id="root" ></div>

 <script type="module" src="/src/main.jsx" ></script>

</body>

```

The build process transforms this into something the browser _can_ use, pointing to a final, bundled JS

file.


   - _A common point of confusion is seeing the repository files on GitHub and assuming they should_
_work as a website. Always remember that for modern frameworks, what's in the repository is_
_the "recipe" (source code), not the "cake" (the built website)._

# **Core Deployment Concepts**


In modern web development, the code you write is often not the same code that runs in the user's
browser. A "build" process transforms your source code into optimized, static files (HTML, CSS,
JavaScript). "Deployment" is the process of placing these files onto a server where users can access

                                                              [them. You can read more about the basics of JavaScript build systems here: https://javascript.info/build](https://www.google.com/url?sa=E&q=https%3A%2F%2Fjavascript.info%2Fbuild-tools)

[tools](https://www.google.com/url?sa=E&q=https%3A%2F%2Fjavascript.info%2Fbuild-tools)

## **The Build Step: From Source Code to Static Files** **The Deployment Step: Making Files Publicly Accessible**


# **Building a React + Vite Project**

Vite is a modern frontend build tool that provides an extremely fast development environment and
bundles your code for production. It processes your files, including React's JSX syntax, and turns them
into a highly optimized set of static assets.

## **The package.json File**

### **The build script** **The preview script**

## **The Build Output (dist folder)**

### **Purpose of the dist folder** **Minification and Transpilation**

# **Hosting on GitHub Pages**


GitHub Pages is a static site hosting service that takes HTML, CSS, and JavaScript files straight from a
repository on GitHub, optionally runs them through a build process, and publishes a website.

## **Deployment Methods**

### **Method 1: Deploy from a Branch** **Method 2: GitHub Actions**

## **The Problem with "Deploy from a Branch" for Build-Step Projects**


# **Configuring a Vite Project for GitHub Pages**

To deploy a Vite project to GitHub Pages, you need to make two key configurations: tell Vite what the
base URL of your site will be, and create a GitHub Actions workflow file to automate the build and
deployment process.

## **Updating the Vite Configuration (vite.config.js)**

### **The base option**


When you deploy your site to a sub-path, like https://<username>.github.io/<repo-name>/, Vite needs
to know that all asset links (for CSS, JS, images, etc.) should be prefixed with /<repo-name>/. The base
option in your vite.config.js file sets this public path. Without it, the browser would try to load assets
from the root (/), which would fail.


**code JavaScript**
```
// vite.config.js

import { defineConfig } from 'vite'

import react from '@vitejs/plugin-react'

export default defineConfig({

plugins: [react()],

base: '/your-repo-name/', // Replace with your repository name

})

```

   - _A common mistake is forgetting the leading and trailing slashes in the base path (e.g., using_
_'your-repo-name' instead of '/your-repo-name/'). This will cause asset loading to fail._

## **Creating the GitHub Actions Workflow**

### **The .github/workflows directory**


GitHub Actions looks for workflow files in a very specific directory within your project's
root: .github/workflows/. You must create this folder structure exactly. The leading dot in .github makes
it a hidden directory on Unix-like systems (like macOS and Linux).


You will create this directory in the root of your project. The Windsurf IDE's file explorer can be used

for this.


**code Text**

```
your-project/

```

```
├── .github/

│  └── workflows/

│    └── deploy.yml

├── src/

└── package.json

```

   - _If you misspell the directory names or place your .yml file anywhere else, GitHub will not detect_
_or run your workflow._

### **The deploy.yml file**


This is the file that contains all the instructions for your deployment workflow. It is written in YAML, a
human-readable data serialization language that is strict about indentation. This file tells GitHub what
events should trigger the workflow (e.g., a push to the main branch) and what steps to perform (install,
build, deploy).


**code Yaml**

```
# .github/workflows/deploy.yml

# A minimal workflow file. The full version is in section 5.

name: Deploy to GitHub Pages

on:

push:

branches: [ "main" ]

jobs:

deploy:

runs-on: ubuntu-latest

steps:

- name: Deploy

run: echo "Deploying..."

```

   - _YAML uses spaces, not tabs, for indentation. Using tabs will cause your workflow to fail with a_
_parsing error. Most code editors, including Windsurf, can be configured to handle this_
_automatically._


# **The GitHub Actions Workflow (deploy.yml)**

A GitHub Actions workflow is an automated process defined in a YAML file. For deployment, this file
specifies a series of jobs and steps that GitHub's servers will execute, such as checking out your code,
building it, and deploying the result.

## **Anatomy of the Workflow File**

### **name: Naming the workflow**


The name key provides a custom name for your workflow. This is the name that will appear in the
"Actions" tab of your GitHub repository, making it easy to identify this specific workflow among
others you might have.


**code Yaml**

```
# .github/workflows/deploy.yml

name: Deploy static content to Pages

```

   - _This is purely for display purposes and is optional. If you omit it, GitHub will use the relative_
_path to the workflow file (e.g., .github/workflows/deploy.yml) as the name._

### **on: Triggering the workflow**


The on key defines the events that will automatically trigger the workflow. A common setup is to
trigger it on a push to a specific branch, like main. This means that every time you push new commits
to your main branch, the deployment process will start automatically.


**code Yaml**

```
# .github/workflows/deploy.yml

on:

# Runs on pushes targeting the default branch

push:

branches: [ "main" ]

```

   - _Be careful which branch you set as the trigger. If you set it to a development branch, you might_
_accidentally deploy unfinished code every time you push._

### **permissions: Granting permissions to the workflow**


For a workflow to deploy to GitHub Pages, it needs specific permissions. This block grants the
GITHUB_TOKEN (a temporary token automatically created for the job) the ability to read repository
contents and write to the pages service to publish the site.


**code Yaml**

```
# .github/workflows/deploy.yml

permissions:

contents: read

pages: write

id-token: write

```

   - _Without these permissions, the final deployment step will fail with an authentication error_
_because the action won't be authorized to publish the site._

### **jobs: Defining the sequence of tasks**


A workflow is made up of one or more jobs. Each job runs in a fresh virtual environment and contains
a sequence of steps. For a simple deployment, you typically only need one job, which you might name
deploy.


**code Yaml**

```
# .github/workflows/deploy.yml

jobs:

 # Single deployment job

 deploy:

  # ... job configuration and steps go here

```

   - _By default, jobs run in parallel. If you have multiple jobs that need to run in a specific order,_
_you must use the needs keyword to define dependencies between them._


### **runs-on: Specifying the virtual machine**

The runs-on key specifies the type of virtual machine, or "runner," that the job will execute on. ubuntulatest is a common and cost-effective choice provided by GitHub that gives you a clean Linux
environment with many pre-installed tools.


**code Yaml**

```
# .github/workflows/deploy.yml

jobs:

deploy:

runs-on: ubuntu-latest

# ...

```

   - _While you can also use windows-latest or macos-latest, they consume more of your free GitHub_
_Actions minutes. Unless your build has a specific OS dependency, ubuntu-latest is usually the_

_best choice._

### **steps: The individual commands to run**


The steps are the heart of a job. They are a sequence of individual tasks that are executed in order. A
step can either run a command-line script or use a pre-packaged action created by the community or

GitHub.


**code Yaml**

```
  # .github/workflows/deploy.yml

  steps:

    - name: Checkout repository

     uses: actions/checkout@v3

    - name: Install dependencies

     run: npm install

```

   - _Each run command executes in its own shell process. If you need to set an environment variable_
_in one step and use it in the next, you must use a special syntax to pass outputs between steps._


## **Key Workflow Steps Explained**

### **actions/checkout: Checking out the repository code**

This is almost always the first step in any workflow. The actions/checkout action downloads a copy of
your repository's code into the virtual environment, so that the subsequent steps (like npm install and
npm run build) have access to your project's files.


**code Yaml**

```
  # .github/workflows/deploy.yml

  steps:

    - name: Checkout

     uses: actions/checkout@v3

```

   - _If you forget this step, all subsequent steps that try to access your project files will fail with a_
_"file not found" error._

### **actions/setup-node: Setting up the Node.js environment**


The actions/setup-node action installs a specific version of Node.js into the virtual environment and
configures the npm command line tool. This is necessary to run commands like npm install and npm
run build. You can specify a version, or use lts/* to always get the latest Long-Term Support version.


**code Yaml**

```
  # .github/workflows/deploy.yml

    - name: Set up Node

     uses: actions/setup-node@v3

     with :

      node-version: 'lts/*'

      cache: 'npm'

```

   - _The cache: 'npm' line is a performance optimization. It caches your downloaded dependencies,_
_so subsequent workflow runs will be much faster if your package-lock.json hasn't changed._


### **run: npm install: Installing project dependencies**

This step uses npm to install all the dependencies listed in your package-lock.json file. This is the
equivalent of what you do on your local machine to get a project ready. The npm ci command is often
preferred in CI/CD environments because it's faster and ensures an exact reproduction of dependencies,
but npm install is a safer alternative if your package-lock.json is not in the root directory.


**code Yaml**

```
# .github/workflows/deploy.yml

- name: Install dependencies

run: npm install

```

   - _If this step fails, it's often because of a mismatch between the Node.js version specified in setup-_
_node and the version required by your dependencies, or a missing package-lock.json file._

### **run: npm run build: Running the build script**


This step executes the build script defined in your package.json. For a Vite project, this runs vite build,
which transpiles and bundles your code into the static HTML, CSS, and JavaScript files that are placed
in the dist directory.


**code Yaml**

```
# .github/workflows/deploy.yml

- name: Build

run: npm run build

```

   - _If this step fails, it's usually due to an error in your application code that the build tool caught._
_You can often find the specific error by looking at the logs for this step in the GitHub Actions_

_UI._

### **actions/upload-pages-artifact: Preparing the site for deployment**


GitHub Pages deployment is a two-step process. First, this action takes the output of your build (the
dist folder) and packages it into a special file called an "artifact." This artifact is then stored by GitHub,
ready for the final deployment step.


**code Yaml**

```
  # .github/workflows/deploy.yml

```

```
    - name: Upload artifact

     uses: actions/upload-pages-artifact@v1

     with:

      path: './dist'

```

   - _The path must point to your build output directory. If your build tool outputs to a folder named_
_build instead of dist, you must change this path accordingly or the action will fail._

### **actions/deploy-pages: Deploying the site**


This is the final step. The actions/deploy-pages action takes the artifact that was uploaded in the
previous step and deploys it to the GitHub Pages infrastructure, making your site live. It automatically
knows which repository and environment to deploy to based on the context of the workflow run.


**code Yaml**

```
  # .github/workflows/deploy.yml

    - name: Deploy to GitHub Pages

     id: deployment

     uses: actions/deploy-pages@v1

```

   - _This step depends on the upload-pages-artifact step. If the artifact wasn't created or was_
_configured incorrectly, this step will have nothing to deploy and will fail._

# **Triggering and Monitoring the Deployment**


Once your workflow is configured and pushed to your repository, the process becomes automatic.

## **Pushing to GitHub to Trigger the Workflow**


After you have created your deploy.yml file and updated your vite.config.js, you commit these files and
push them to the branch that you configured as the trigger in the on section of your workflow (e.g.,
main).


This is a command you run in the Windsurf terminal.


**code Bash**

```
git add .
git commit -m "Add GitHub Actions workflow for deployment"
git push

```

   - _Even if you only change the workflow file itself, you still need to git push for the changes to take_
_effect on GitHub's side and for the new workflow to run._

## **Viewing Workflow Progress in the "Actions" Tab**


Once you push your commit, you can navigate to your repository on GitHub and click on the "Actions"
tab. You will see your workflow appear in the list. You can click on it to see a real-time log of each job
and step as it executes. If something fails, this is the first place to look for error messages.


This is not a code step, but an action you take on the GitHub website.


1. Go to your repository on github.com.


2. Click the Actions tab.


3. Click on the most recent workflow run to see its details.


   - _A yellow spinning icon means the job is in progress, a green checkmark means it succeeded,_
_and a red 'X' means it failed. You can expand each step to see its detailed command-line output._

## **Verifying the Deployed Site**


After the workflow completes successfully, go to your repository's Settings > Pages tab. The URL of
your live site will be displayed at the top. It can sometimes take a minute or two after the workflow

finishes for the site to become available.


The URL will follow this pattern:


code Text

```
https://<your-username>.github.io/<your-repo-name>/

```

   - _If you visit the site and see a 404 error or a blank page even after a successful deployment, the_
_most common cause is an incorrect base path in your vite.config.js. Double-check that it exactly_
_matches your repository name, enclosed in slashes._


