# **Database Lessons**

## **Table of Contents**

The Fullstack Concept: A Detective Story.......................................................................................2

Part 1............................................................................................................................................ 2

The Stack.......................................................................................................................................... 2
Why Does This Matter?....................................................................................................................5
The Fullstack Concept: The Sokoban Case......................................................................................6

Part 2............................................................................................................................................ 6
Cracking the Code: The Assignment................................................................................................7
The Database Lineup: A Rogues' Gallery of Data..........................................................................10
The Old Guard: Relational Databases (SQL).................................................................................10
The New Crew: NoSQL Databases ("Not Only SQL")..................................................................11

1. The Document Gang (e.g., MongoDB)..................................................................................11
2. The Key-Value Couriers (e.g., Redis)....................................................................................12
The Final Debrief............................................................................................................................12
Case File #002: The Deployment Job.............................................................................................14

The Simple Drop: Deploying a Static Site.................................................................................14
The Main Heist: Deploying a .NET Application.......................................................................14
The Undercover Operation: Publishing Our First App...................................................................18
The Debrief.....................................................................................................................................21
The Undercover Operation, Part 2: Assembling the "Go-Bag"......................................................21
The Debrief.....................................................................................................................................23
The Case of the Fleeting Memory: Persisting Data with SQLite...................................................24
The Case of the Vanishing Data: An Introduction to Persistence...................................................27
Our First Portable Vault: SQLite....................................................................................................28
The Middleman: Entity Framework Core......................................................................................28
The Plan of Attack: Setting up SQLite with EF Core.....................................................................29
The Case of the Vanishing Data, Part 2: Forging the Vault............................................................31
The Payoff: Accessing the Data......................................................................................................33
The Case of the Invisible Connection.............................................................................................35

The Invisible Thread.......................................................................................................................36

The Problem with Phantom Tables.................................................................................................38


### **The Fullstack Concept: A Detective Story**

**Part 1**


Alright, team, let's dive into the full-stack concept. Imagine we're detectives trying to understand how a
clothing store website works. We need to figure out everything from the clothes displayed in the
window to the warehouse where everything is stored.


**The Crime Scene: The Website**


First, we see the storefront—what the customers see. This is the **front end** . In the video, we see

different models wearing various outfits. Each outfit is a combination of items: shoes, trousers or a
skirt, and a top. These choices matter and create a specific "look" or "style." This is just like a website's
front end, where different elements are combined to create the user experience. You've been working
with HTML, JavaScript, and C#—these are the tools used to build what the user sees and interacts

with.


Now, let's break down the "outfit" of our website. Just like a person has a top, bottom, and shoes, a fullstack application has different layers. You can't wear two pairs of trousers at once, right? Similarly, in
each layer of the "stack," you typically choose one technology.


Here’s a look at the different layers, or "stacks," that make up a full-stack application:


   - **Front-End Frameworks** : Tools that help build the user interface.


   - **Web API** : The messenger that takes requests from the front end to the back end.


   - **Back-End Languages** : The brains of the operation, processing logic and data.


   - **Databases** : The warehouse where all the information is stored.


**Teacher's Notes:**


Here's a little something to help you remember the different parts of the stack. Think of it
like ordering at a restaurant. You (the user) talk to the waiter (the front end), who takes your
order to the kitchen (the back end). The chef (the back-end logic) gets the ingredients from
the pantry (the database) to cook your meal. The waiter then brings the food back to you.
The Web API is like the waiter's notepad, making sure the kitchen understands your order
correctly.

### **The Stack**


So, what exactly is a "stack"? It’s a collection of technologies used to build a web application. Each
layer of the stack builds on the one below it, which is why we call it a "stack."


Here's a simple example of a full stack:


   - **React.js (Front End)** : This is what the user sees and interacts with.


   - **Web API (Application Programming Interface)** : This is how the front end "talks" to the back

end.


   - **C# (Back End)** : This is where the main logic of the application lives.


   - **SQLite (Database)** : This is where all the data is stored.


**Teacher's Notes:**


To give you a better idea of how this works, here are some simple code snippets.


**Front End (React.js):** This code creates a simple button that, when clicked, will fetch data

from the back end.


code JavaScript
I've marked things I don't understand with <-- **((?))**

```
     import React, { useState } <-- ((?)) from 'react';

   function App() {

   const [message, setMessage] = useState('');

   const fetchData = () => {

   fetch('/api/message')

   .then(response => response.text())

   .then(data => setMessage(data));

   };

   return (

   <div>

   <button onClick={fetchData}>Get Message</button>

   <p>{message}</p>

   </div>

   );

   }

   export default App;

```

**Back End (C# with Web API):** This code sets up a simple API endpoint that sends a
message to the front end.


code C#

downloadcontent_copy


expand_less
```
  using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers

{

 [ApiController]

 [Route("api")]

 public class MessageController : ControllerBase

 {

 [HttpGet("message")]

 public string GetMessage()

 {

 return "Hello from the back end!";

 }

 }

}

```

**Database Interaction (C# with SQLite):** Here’s how you might get data from a database
in your back end.


code C#

downloadcontent_copy
expand_less
```
  using Microsoft.Data.Sqlite;

// ... inside a method ...

var connectionStringBuilder = new SqliteConnectionStringBuilder

{

 DataSource = "./mydatabase.db"

};

using ( var connection = new
SqliteConnection(connectionStringBuilder.ConnectionString))

{

 connection.Open();

```

```
   var selectCmd = connection.CreateCommand();

   selectCmd.CommandText = "SELECT name FROM users WHERE id = 1";

   using ( var reader = selectCmd.ExecuteReader())

   {

   while (reader.Read())

   {

   var message = reader.GetString(0);

   }

   }

   }

### **Why Does This Matter?**

```

Understanding the full stack is crucial because it gives you the complete picture of how a web
application works. As a full-stack developer, you are the detective who knows how to investigate every
part of the crime scene, from the display window to the back office. This makes you incredibly
versatile and valuable in the job market.


The lesson emphasizes that while you can specialize in one area (like being a front-end or back-end
developer), knowing the whole stack makes you a more effective team member. You'll be able to
understand the entire workflow and communicate better with your colleagues.


In the next lesson, we'll dive deeper into databases and how they connect to the rest of the stack. For
now, keep this "outfit" analogy in mind. It will help you remember how all the pieces fit together.


### **The Fullstack Concept: The Sokoban Case**

**Part 2**


Alright, team, we’ve learned the layout of the criminal underworld—the "full stack." We know about
the front-end hideouts, the back-end masterminds, and the database vaults where they keep their loot.
Now, it's time to put our skills to the test with our first real case file. They call it "The Sokoban Game."


**Case File #001: The Warehouse Keeper**


Our target is a simple transport puzzle, a classic cat-and-mouse game. The objective is to push boxes
around a maze and place them in designated locations. It sounds simple, but as with any good mystery,
there are rules—and breaking them will get you stuck.


Here's the intel we have on the operation:


   - **The Agent:** You control the player, a warehouse keeper.


   - **The Assets:** These are the boxes that need to be moved.


   - **The Obstacles:** The warehouse is full of walls that you can't move through.


   - **The Drop Zone:** These are the goal locations where the boxes must be delivered.


**The Modus Operandi (The Rules of the Game)**


Every good detective knows you have to understand the criminal's methods to catch them. Here are the
rules of engagement for the Sokoban case:


1. **Movement is Key:** The player can move up, down, left, or right using the arrow keys.


2. **Push, Don't Pull:** You can only _push_ boxes. You can't pull them. Once you push a box against a

wall, it might be stuck for good. Plan your moves carefully.


3. **One at a Time:** You can only push one box at a time. Trying to push a line of two or more


boxes won't work.


4. **Mission Accomplished:** The game is won when all the boxes have been pushed onto the goal


locations.


**Teacher's Notes:**


Think about the logic here. Before your player moves, you need to check the space they

want to move into.


      - Is it an empty space? Great, move there.


      - Is it a wall? Sorry, you can't move.


      - Is it a box? Now it gets interesting. You have to check the space _behind_ the box. Is
_that_ space empty? If so, you can push the box and move into its old spot. If it's
another box or a wall, you're stuck.


### **Cracking the Code: The Assignment**

This isn't just about playing the game; we have to build it from the ground up. Management has given
us a starter file, SokobanBase.js. This file contains the blueprint for the crime scene: a JavaScript object
called tileMap01 that holds a mapGrid. This grid is a two-dimensional array of characters that lays out

the entire level.


Here’s the legend for the map:


   - **W:** Wall


   - **B:** Block (the boxes)


   - **P:** Player's starting position


   - **G:** Goal area for the blocks


Our job is to take this blueprint and build a visual representation using JavaScript to create HTML
elements. No hardcoding the map in HTML—that's cheating, and good detectives don't cheat. We build
our case from the evidence provided.


**Teacher's Notes:**


This is where your skills come into play. You'll need to use JavaScript to dynamically
generate the game board. Here are a couple of clues to get you started.


**Clue #1: Building the Crime Scene (The Game Board)**


You'll need to loop through the mapGrid array. For each character, you create a div element,
give it the correct CSS class (e.g., 'wall', 'box', 'player'), and add it to your game board

container in the HTML.


code JavaScript
downloadcontent_copy
expand_less
```
     // Get the container for your game board from the HTML

   const gameBoard = document.getElementById('game-board');

   // Loop through each row of the map grid

   tileMap01.mapGrid.forEach(row => {

   // Loop through each character in the row

   row.forEach(tile => {

   // Create a new div element for the tile

   const tileDiv = document.createElement('div');

   // Add a base class for all tiles

```

```
 tileDiv.classList.add('tile');

 // Add a specific class based on the character

 if (tile === 'W') {

 tileDiv.classList.add('wall');

 } else if (tile === 'B') {

 tileDiv.classList.add('box');

 } // ... and so on for player, goal, and empty space

 // Add the new tile to the game board

 gameBoard.appendChild(tileDiv);

 });

});

```

**Clue #2: Tailing the Suspect (Handling Player Movement)**


To move the player, you need to listen for keyboard events. Specifically, you're interested in
when the arrow keys are pressed.


code JavaScript
downloadcontent_copy
expand_less
```
  // Add an event listener to the whole document to listen for key

presses

document.addEventListener('keydown', (event) => {

 switch (event.key) {

 case 'ArrowUp':

 console.log('Player wants to move up!');

 // Call your function to handle moving up

 break ;

 case 'ArrowDown':

 console.log('Player wants to move down!');

 // Call your function to handle moving down

 break ;

 case 'ArrowLeft':

 console.log('Player wants to move left!');

 // Call your function to handle moving left

```

```
   break ;

   case 'ArrowRight':

   console.log('Player wants to move right!');

   // Call your function to handle moving right

   break ;

   }

   });

```

With these clues, you're well on your way to solving the Sokoban case. Remember to check your work
against the example provided in the case file. The deadline is approaching, so get to it, detectives


### **The Database Lineup: A Rogues' Gallery of Data**

Alright, team, we've established the concept of the full stack—our criminal underworld. Now we're
zooming in on the most critical part of any operation: the vault. This is where the money, the secrets,
and all the valuable information are kept. In our world, these vaults are called **databases** .


But here's the twist: there isn't just one type of vault. Depending on what you're storing and how fast
you need to get it, you'll use a different kind of safe. Our first stop is a place our informants call **DB-**
**Engines.com**, a website that keeps a "most wanted" list of all the major database players, ranking them
by popularity.


Looking at this list, it's clear we're dealing with a few big families, but they all fall into two main
categories: the old guard and the new crew.

### **The Old Guard: Relational Databases (SQL)**


These are the traditionalists. They've been around since before the internet was a household name, born
from the need for big companies to keep perfect, organized records of things like employees, sales, and

inventory.


Think of a relational database as a massive, old-school bank vault. Everything is stored in neatly
organized filing cabinets, which they call **tables** . Each drawer in the cabinet is a **row**, and each piece of
information in that drawer has a specific, labeled spot, which they call a **column** .


You can't just shove a new piece of paper in there; it has to fit the pre-defined structure, or **schema** . It's
rigid, reliable, and incredibly organized.


Visually, it's like a spreadsheet:


code Code

downloadcontent_copy
expand_less

```
+----+--------------+----------------+

| ID | FirstName  |  LastName  | <- Columns

+----+--------------+----------------+

| 1 | 'Sherlock'  | 'Holmes'    | <- Row 1

+----+--------------+----------------+

| 2 | 'John'    | 'Watson'    | <- Row 2

+----+--------------+----------------+

```

The top dogs in this family, the ones you'll see at the top of the DB-Engines list, are names like **Oracle**,
**MySQL**, **Microsoft SQL Server**, and **PostgreSQL** . They all speak a common language for managing
their data called **SQL (Structured Query Language)** .


**Teacher's Notes:**


The "schema" is the blueprint for a table. You define it once, and every piece of data you
add has to follow its rules. Here’s a simple SQL command to create the "Detectives" table
we drew above. Notice how we have to specify the _type_ of data for each column (e.g., INT
for an integer, VARCHAR for text).


code SQL

downloadcontent_copy
expand_less
```
     -- Creating the blueprint (the schema)

   CREATE TABLE Detectives (

   PersonID INT NOT NULL AUTO_INCREMENT,

   LastName VARCHAR(255) NOT NULL,

   FirstName VARCHAR(255),

   PRIMARY KEY (PersonID)

   );

   -- Inserting data that fits the blueprint

   INSERT INTO Detectives (FirstName, LastName)

   VALUES ('Sherlock', 'Holmes');

### **The New Crew: NoSQL Databases ("Not Only SQL")**

```

The internet changed the game. Suddenly, the old, rigid vaults weren't always the best fit. We needed
something faster, more flexible, and able to handle massive amounts of messy, unpredictable data—
everything from blog posts to user profiles and real-time game data.


Enter the NoSQL family. Their motto is flexibility. They don't always use tables and strict schemas,
which makes them perfect for modern applications where things are constantly changing. Let's look at
a few of the key players in this new crew.


**1. The Document Gang (e.g., MongoDB)**


Instead of storing data in rows and columns, a document database stores everything related to one
record in a single **document** . Think of it less like a spreadsheet and more like a detailed case file,
written in a format called JSON (which should look familiar from your JavaScript work).


code Code

downloadcontent_copy
expand_less
```
  {

 "name": "Midtown Theater",

```

```
 "address": {

  "street": "1234 Main St",

  "city": "Metropolis",

  "state": "NY"

 },

 "movies": [

  { "title": "The Data Heist", "runtime": 120 },

  { "title": "Case of the Missing Semicolon", "runtime": 95 }

 ]

}

```

See how it’s all nested together? This structure is super flexible. If you wanted to add a "critic_rating"
to the movies later, you could just add it without changing the whole database blueprint. **MongoDB** is
the kingpin of this category.


**2. The Key-Value Couriers (e.g., Redis)**


This is the simplest and fastest type of database. Think of it like a coat check at a club. You hand over
your coat (the **value** ) and get a ticket (the **key** ). When you want your coat back, you just show the

ticket.


Key -> Value
"user:101" -> "{ name: 'Jimmie C.', status: 'active' }"

"latest_post" -> "The Case of the Sokoban Game..."


It’s incredibly fast because there's no complex structure to search through. Because of this speed, it's
not always used for permanent storage but is perfect for **caching** —keeping frequently used information
in a temporary, high-speed location so you don't have to retrieve it from a slower main database every
time. **Redis** is the big name here.

### **The Final Debrief**


So, which vault is best? It all depends on the job.


   - If you're building a banking application where every transaction has to be perfectly structured
and reliable, you'll probably go with a **relational (SQL)** database.


   - If you're building a social media app or a blog where the data is unstructured and needs to be
flexible, a **document database (NoSQL)** like MongoDB is a great choice.


   - If you just need to make your website lightning-fast by storing some temporary data, you'll use
a **key-value store (NoSQL)** like Redis for caching.


The big takeaway for us detectives is this: the world of data storage is vast. Knowing the right tool for
the job is half the battle. Relational databases give us structure and consistency, while NoSQL


databases give us speed and flexibility. Most big operations use a mix of both to get the job done. Now,
let's get ready to crack open our first vault.


### **Case File #002: The Deployment Job**

Alright, team, listen up. We've got our operation planned out—our .NET application is coded and
ready. But right now, it's just a blueprint sitting in our secure facility (Visual Studio). To the outside
world, it doesn't exist. Our next job is to get it out there, to deploy it, so it can start doing its work.


This is the deployment phase, and how we handle it depends on the nature of the operation.


**The Simple Drop: Deploying a Static Site**


First, let's consider a low-level informant, a simple static website like the chess game we looked at.
This kind of site is just a collection of files that don't change: HTML, CSS, and JavaScript.


Getting this kind of asset into the field is easy. It's like a dead drop. You just take the folder containing
all the files and copy it over to a public location (a web server). Most web hosting services will give
you access to their server via FTP (File Transfer Protocol), which is just a fancy way of saying "a tool
to copy files."


You take your folder structure...


code Code

downloadcontent_copy
expand_less
```
/chess-game-folder

├── index.html

├── style.css

├── script.js

└── /images

├── b_pawn.svg

└── w_pawn.svg

```

...and you just drag and drop the whole thing onto the server. That's it. Job done. The server knows
what to do with .html files, and the user's browser knows how to handle the rest.


**The Main Heist: Deploying a .NET Application**


Now for our main operation—the .NET application. This is a whole different ballgame. We can't just
copy our C# source code files (.cs files) onto a server. A web server has no idea what to do with C#
code. It would be like handing a secret agent's handler a bunch of notes written in a language they don't

understand.


Our C# code needs to be processed into a language the machine can execute. This is a two-step
process: **Building** and **Publishing** .


Before we do anything, we need to **compile** or **build** our code. This is where Visual Studio takes all our
carefully written C# files and translates them into an "intermediate language" (IL). This process creates


.dll files (Dynamic Link Libraries) and an .exe (executable) file. These are the actual instructions that
can be run by a machine.


You've been doing this all along every time you run your program to test it. But when we're getting
ready for a live mission, we need to be more deliberate. We switch from a **Debug** build to a **Release**

build.


   - **Debug Build:** This is our training mode. It includes a lot of extra information that helps us find
and fix bugs. It's bigger and slower.


   - **Release Build:** This is for live operations. The code is optimized to be faster and smaller, with
all the debugging information stripped out.


Just building the application isn't enough. A build scatters the necessary files across different folders
(/bin, /obj). It's a mess. We need a clean, complete, and portable "mission kit." This is what **Publishing**

is for.


When we publish our project, Visual Studio does something brilliant: it looks at our application,
identifies every single thing it needs to run, and gathers it all into one clean folder.


Here’s how we do it:


1. In Visual Studio, right-click the project you want to deploy (for us, it's the API project).


2. Select **Publish** .


You'll be presented with a list of targets—the destination for your deployment.


code Code

downloadcontent_copy
expand_less
```
Where are you publishing today?

[ Azure ] <-- Microsoft's cloud platform.

[ Docker ] <-- A container for ultimate portability.

[ Folder ] <-- A local folder on our machine.

[  FTP  ] <-- Directly to a web host.

... and more

```

For this mission, we'll choose the **Folder** option. This lets us see exactly what goes into the deployment
package. After we run the publish process, we get a new folder, usually located at
bin/Release/net9.0/publish.


Inside, we find everything needed for the mission. Notice, there's no C# source code here. It's all
compiled and ready to go.


code Code

downloadcontent_copy


expand_less
```
/publish

├── API.exe            <-- The main executable. This is our agent.

├── API.dll            <-- The compiled code for our own project.

├── Humanizer.dll         <-- A dependency from a NuGet package.

├── Microsoft.EntityFrameworkCore.dll <-- Another dependency.

├── appsettings.json       <-- Our configuration file.

└── ... (and dozens of other essential files)

```

This publish folder is our go-bag. _This_ is what we deploy to the server, not our source code.


**Teacher's Notes:**


You don't have to use the Visual Studio interface. You can do all of this from the command

line, which is great for automation. The command is simple. First, you navigate to your
project's directory, then run:


code Bash

downloadcontent_copy
expand_less
```
   # This command builds the project in Release mode and puts the output

   # in the 'publish' folder.

   dotnet publish -c Release

```

This does the exact same thing as the UI wizard.


Another important concept is the **runtime** . For our published application to work, the
server it's running on needs to have the .NET Runtime installed. Think of it as a special
decoder ring that knows how to execute our compiled .dll files.


What if the server doesn't have it? You can create a **self-contained** publish. This packages
the _entire .NET runtime_ inside your publish folder.


code Bash

downloadcontent_copy
expand_less
```
   # The '--self-contained' flag bundles the runtime with your app.

   # The '-r' flag specifies the target runtime, e.g., Linux 64-bit.

   dotnet publish -c Release --self-contained -r linux-x64

```

The downside? Your publish folder gets a lot bigger. The upside? Your app can run on any
machine of that type (e.g., any Linux x64 machine) without needing anything pre-installed.
It's totally self-sufficient.


Now you know the procedure. Building translates the plan, and publishing packs the bag. Once that's
done, we


### **The Undercover Operation: Publishing Our First App**

Alright team, listen up. We've been operating in the shadows, building our gadgets and running our
intel in the safety of our own workshops (our local machines). But now, it's time for the real mission:
we need to deploy our application out in the wild. This operation is called **Publishing** .


**The Challenge: Smuggling Our Gadget**


First, let's consider our previous jobs. Remember the Sokoban case? That was a simple "static" website.
It was just a collection of files—blueprints (HTML), paint jobs (CSS), and basic wiring (JavaScript).
Getting that into the field was easy. You just put the files in a folder, handed it to a courier (FTP), and
they dropped it on any server. Every browser in the world knows how to read those blueprints.


code Code

downloadcontent_copy
expand_less
```
+-- Our Simple "Chess" Project --+

|                |

|  index.html (The blueprints) |

|  style.css  (The paint job) |

|  script.js  (The wiring)   |

|  /images/  (The parts)   |

|                |

+--------------------------------+

|

V

[ Any Web Server ] --> Easy Deployment!

```

But our new gadget, the .NET application, is a different beast entirely. It’s a sophisticated piece of
machinery. We can't just hand the raw C# files to the server. The server won't know what to do with
them. It’s like trying to give a guard the raw chemical formula for an explosive instead of the finished,

armed device. It's useless to them.


Our C# code needs to be processed, packaged, and delivered in a very specific way.


**Phase 1: The Disguise (The Build/Compile Process)**


Before we can even think about delivery, we need to assemble our gadget. We take our C# source code
and run it through a special machine called a **compiler** . This process, known as "building," transforms

our human-readable code into a format that a machine can execute.


Think of it as putting our gadget's components into a black box. The outside world doesn't need to see
the messy wiring inside; they just need the button to press. This "black box" is usually a set of .dll
(Dynamic Link Library) files.


code Code

downloadcontent_copy
expand_less

```
+----------------+   +-----------+   +----------------+

| MyCode.cs   |   |      |   | MyApp.dll   |

| (Raw Intel)  | ===> | COMPILER | ===> | (Secret Code) |

+----------------+   | (Build) |   +----------------+

+-----------+

```

In Visual Studio, we do this by going to the Build menu and clicking Build Solution. This is the first
critical step.


**Phase 2: The Delivery (The Publish Process)**


Once our gadget is built and disguised, we need to choose a delivery method. This is where the
"Publish" feature comes in. When we right-click our project and select "Publish," we're opening up our
mission planning board. It asks us one crucial question: "Where are you publishing today?"


This opens up a list of targets, our possible drop-off points:


1. **FTP/Folder (The Classic Dead Drop):** This is like the old days. We package all our compiled

files into a folder and use a courier (FTP) to drop them on a server. **But there's a huge catch:**
The agent on the other side _must_ have the right decoder ring. In our case, that decoder ring is the
**.NET Runtime** . If the web server doesn't have it installed, our package is just a useless brick.


2. **Azure (The Inside Job):** This is Microsoft's cloud platform. Think of this as delivering our

package to a friendly agency. Azure is built for .NET. When you publish to Azure, you're
handing the package to an agent who is fully equipped to handle it. It's the smoothest and most
integrated option for our line of work.


3. **Docker (The Diplomatic Pouch):** This is the modern, high-tech solution. Instead of just

sending our gadget, we seal it inside a special, standardized container. This "diplomatic pouch"
contains _everything_ : our application (.dll files), the decoder ring (.NET Runtime), and any other

tools it needs to function.


code Code

downloadcontent_copy
expand_less

```
    +---------------------------------+

    |   DOCKER CONTAINER      |

    | (The Diplomatic Pouch)     |

    |                 |

    | +---------------------------+ |

    | | Our App   (MyApp.dll) | |

```

```
    | | .NET Runtime (Decoder) | |

    | | Dependencies (Tools)  | |

    | +---------------------------+ |

    |                 |

    +---------------------------------+

```

The beauty of this is that the pouch can be delivered _anywhere_ —to Azure, to another cloud
provider, to our own servers—and because it's completely self-contained, it's guaranteed to

work.


**Teacher's Notes:**


This is a lot to take in, but the key is understanding that our C# code needs two steps to go

live: **Build** and **Publish** .


You can even do this from the command line, which is how automated systems do it.


code Bash

downloadcontent_copy
expand_less
```
   # This command builds the project in "Release" mode,

   # which is optimized for performance.

   dotnet publish --configuration Release

```

When you get to the Docker step, you'll use a special instruction file called a Dockerfile to
create your "diplomatic pouch." It looks something like this:


code Dockerfile

downloadcontent_copy
expand_less
```
   # Use an official .NET runtime as a parent image

   FROM mcr.microsoft.com/dotnet/aspnet:8.0

   # Set the working directory in the container

   WORKDIR /app

   # Copy the compiled files from our "publish" folder into the container

   COPY ./bin/Release/net8.0/publish/ .

   # Tell the container to run our application when it starts

```

```
   ENTRYPOINT ["dotnet", "MyWebApi.dll"]

```

This file is the instruction manual for building a perfect, portable package every time. We'll
be diving deeper into this soon.

### **The Debrief**


So, what have we learned? Deploying a simple HTML site is like dropping off a letter. Deploying
a .NET application is a covert operation. It requires a **build** step to disguise the code and a **publish** step
to deliver it to a target that has the correct environment to run it.


Our mission, should we choose to accept it, is to select the right target for our API and get it deployed.
For now, we'll start with the "inside job": **Azure** .

### **The Undercover Operation, Part 2: Assembling the "Go-Bag"**


Alright, team. We’ve reviewed the mission parameters for Operation: Publish. We know our targets—
Azure, Docker, and the classic Folder. We're starting with the most fundamental approach: the "Folder"
dead drop. It's time to assemble our go-bag and see exactly what we're delivering.


**Phase 3: The Dead Drop (Publishing to a Folder)**


Back in the safe house (Visual Studio), we've selected "Folder" as our target. We're not using any hightech couriers for this first run. We're simply packaging the device and all its components into a
briefcase that can be carried anywhere.


The system asks for a location. By default, it wants to create a publish folder inside bin/Release. This is
perfect. This keeps our field-ready gear separate from our workshop clutter. We hit "Finish" to create
the plan, and then the big red button: **"Publish."**


The workshop springs to life. The console shows logs of the **build process** starting. It's compiling our
code, gathering all the necessary parts, optimizing them, and stripping out anything we don't need in
the field. After a few moments, we get the confirmation: **Publish Succeeded.**


Our go-bag is packed. Now, let's inspect the contents.


**Inspecting the Contents**


If we navigate to our project folder on our local machine, we find the bin folder. This is where all the
compiled "guts" of our project live. Inside, it's split into two distinct operations:


code Code

downloadcontent_copy
expand_less
```
/bin/

|

```

```
+-- Debug/  <-- The Workshop. Messy, full of extra tools for testing.

|

+-- Release/ <-- The Go-Bag. Sleek, optimized, and ready for the field.

```

The **Debug** folder is what we use when we're running the application locally. It's full of extra files and
metadata that let us set breakpoints and figure out what's going wrong. It's our workshop, and it's not
meant for the public.


The **Release** folder is different. It contains the optimized, stripped-down version of our app. And inside
_that_ folder is our target: the publish directory. This is it. This folder is the entire package. Everything
inside here is what we'd copy to a server.


Inside the publish folder, we see the real tools of the trade:


   - **API.exe** : This is the trigger. This is the executable file that starts our entire operation.


   - **API.dll** : This is the core of our gadget. It contains all the secret logic we wrote in our C# files,
compiled into machine code.


   - **Lots of other .dll files** : These are the specialized tools our gadget needs to function—the
lockpicks (Entity Framework Core), the transmitters (ASP.NET Core), etc.


   - **appsettings.json** : These are the mission parameters, telling our application where to find the
database and other critical settings.


   - **API.pdb** : This is a debug symbols file. It's like a schematic for our gadget. It helps us figure out
what went wrong if the application crashes in the field by giving us detailed error reports.


This publish folder is our self-contained unit. As long as the target server has the right decoder ring (the
.NET Runtime) installed, we can just copy this folder over, run the .exe, and our API will be live.


**Teacher's Notes:**


The difference between a **Debug** and **Release** build is critical. You _never_ deploy a Debug
build to a live server. It's slower, larger, and can be a security risk. The Release build is
optimized by the compiler to run as fast as possible.


When we published, Visual Studio effectively ran this command behind the scenes:


code Bash

downloadcontent_copy
expand_less
```
   # -c tells it to use the "Release" configuration

   dotnet publish -c Release

```

**Phase 4: Cross-Border Operations (Targeting Other Systems)**


This go-bag is perfect for any Windows environment. But what if our drop point is in another country,
one that speaks a different language, like **Linux** ? Our Windows-native .exe file won't work there.


For this, we need to consult the Field Operations Manual—the dotnet command-line interface (CLI).
The CLI gives us the power to create packages for _any_ environment.


If we wanted to create a go-bag specifically for a 64-bit Linux machine, our command would look like

this:


code Bash

downloadcontent_copy
expand_less
```
# The -r flag specifies the "runtime" we are targeting.

dotnet publish -c Release -r linux-x64

```

This tells the compiler to package our gadget with instructions that a Linux machine can understand.
But what if our Linux agent doesn't have the .NET decoder ring installed? We can create a **self-**
**contained** package. This is our "diplomatic pouch" again—it includes both our app and the entire .NET
runtime it needs to work. The bag is much heavier, but it's guaranteed to work anywhere.


code Bash

downloadcontent_copy
expand_less
```
# This creates a completely independent package for Linux.

dotnet publish -c Release -r linux-x64 --self-contained true

### **The Debrief**

```

We’ve successfully assembled our first deployment package. We understand the difference between a
messy workshop (Debug) and a clean go-bag (Release). We've seen the exact files that make our
application run, and we've learned how to prepare our package for missions in foreign territories using

the command line.


The dead drop is prepped and ready. In our next session, we'll take the final step and actually deploy
this thing to a live server.


Of course! Let's get our hands dirty and crack this next case.

### **The Case of the Fleeting Memory: Persisting Data with SQLite**


Alright, team. We've got a problem. Our application has a serious case of amnesia. Every time we
refresh the page, it forgets everything. Any data we've entered, any state we've created—poof, gone. It's
like a witness who can't remember what they saw five seconds ago. This kind of fleeting, in-memory
storage is called **transient** state. It exists only for the current session and is wiped clean the moment the
lights go out (i.e., the page is reloaded).


We've briefly used some small-time tricks to get around this, like hiding notes in the browser's glove
compartment using sessionStorage or localStorage. But that's just not secure or scalable enough for a
real operation. We need a proper vault. We need to **persist** our data on the back end, somewhere

permanent.


This is where our new informant, a shady but brilliant character named **SQLite**, comes in.


**The Informant: SQLite & The Translator**


SQLite is a lightweight, file-based database. It's not a massive, standalone server like MySQL or SQL
Server. Instead, it packs the entire vault into a single file right inside our project. It's perfect for
development, prototyping, and smaller applications because it requires zero setup.


But how do we, speaking C#, talk to a database that speaks SQL? We need a translator. This translator
is a powerful organization known as **Entity Framework Core (EF Core)** . It's an **Object-Relational**
**Mapper (ORM)**, which is a fancy way of saying it maps our C# **Objects** (like a Person class) to
database **Tables** (like a Persons table).


This lets us work with our data using the language we already know—C#—without ever having to
write raw SQL queries by hand.


code Code

downloadcontent_copy
expand_less

```
+-------------------+   +-------------------------+

+-------------------+

|          |   |              |   |  Persons Table |

| public class Person | <==> | Entity Framework Core  | <==> | +----+----------+ |

| {         |   |  (The Translator)   |   | | ID |  Name  | |

|  public int Id; |   |              |   | +----+----------+ |

|  public string Name; |   +-------------------------+   | | 1 | "Jimmie" |
|

| }         |                   | +----+----------+ |

|          |                   |          |

+-------------------+   (C# Code)           +-------------------+

```

```
(Our C# Object)                      (Database Table)

```

**Phase 1: Gearing Up (Installing the Tools)**


To work with EF Core and SQLite, we need to bring in some specialized gear. We'll use the **NuGet**
**Package Manager** —our quartermaster—to get the necessary tools for our API project.


Here's the list of equipment we need to install:


1. Microsoft.EntityFrameworkCore.Relational: The basic toolkit for any relational database work.


2. Microsoft.EntityFrameworkCore.Design: Gives us the command-line tools to build and manage


the database schema.


3. Microsoft.EntityFrameworkCore.Sqlite: The specific driver that allows EF Core to talk to our

informant, SQLite.


**Teacher's Notes:**


Remember to make sure you're installing these packages into the correct project—in our
case, the **API project** . Also, it's crucial that the versions of all the Entity Framework
packages match. Mismatched versions are a common source of mysterious bugs.


**Phase 2: The Blueprint (The DbContext)**


Every operation needs a headquarters, a central place that knows about all our assets. In EF Core, this
is the **DbContext** . It's a C# class that represents our entire database. Inside this class, we'll list every

table we want to create.


We'll create a new class, BackendContext.cs, which will be our database's command center.


code C#

downloadcontent_copy
expand_less
```
using Microsoft.EntityFrameworkCore;

// This class is the blueprint for our entire database.

public class BackendContext : DbContext

{

// The base constructor is needed to pass configuration options.

public BackendContext(DbContextOptions<BackendContext> options) : base(options)
{ }

// Each DbSet<T> represents a table in our database.

// The name "Persons" will become the table name.

public DbSet<Person> Persons { get; set; }

```

```
public DbSet<Address> Addresses { get; set; }

}

```

**Phase 3: The Secure Line (Wiring it Up)**


Now we need to tell our application how to connect to this new database. We do this in Program.cs
using the builder pattern, a technique called **Dependency Injection** . This lets our application provide
services (like a database connection) to any part of our code that needs it.


code C#

downloadcontent_copy
expand_less
```
// In Program.cs, before "var app = builder.Build();"

// Add the DbContext to the services collection.

builder.Services.AddDbContext<BackendContext>(options =>

{

// Tell it to use SQLite and provide the connection string.

// This will create a file named "backend.db" in our project folder.

options.UseSqlite("DataSource=backend.db");

});

```

**Phase 4: The Construction Crew (Migrations)**


We’ve designed our classes (Person, Address) and set up the connection. But the actual database file
and its tables don't exist yet. To build them, we use **Migrations** . A migration is a "recipe" that tells EF
Core how to create or update the database to match our C# models.


We run this from the command line, in the same directory as our API project file:


code Bash

downloadcontent_copy
expand_less
```
# This command creates a new migration file based on our DbContext.

dotnet ef migrations add InitialCreate

```

**CRITICAL CLUE:** The first time we run this, it fails! The error message is a classic: "The entity type

'


Of course! Let's get our detective gear on and dive into the latest case file.

### **The Case of the Vanishing Data: An Introduction to Persistence**


Alright, team, we've got a problem. Every time we close our application, all our evidence, all our leads,
all our data—it vanishes. It's like our entire operation has amnesia. In the field, we call this a lack of
**persistence** .


On the front lines (the front-end), we have a few tricks up our sleeve for this. We can use
sessionStorage to keep notes for as long as a browser tab is open, or localStorage to stash things away a
bit longer. But these are like sticky notes; they're temporary and tied to a single user's browser. They
aren't a real solution for storing important, long-term case files that everyone on the force needs to

access.


If we want our data to truly _persist_ —to survive restarts, to be shared between users, and to be the single

source of truth—we need a real vault. We need a **database** on our back-end.


**The Inefficiency of Hardcoding**


Now, we could try to just write all our data directly into our C# code. It seems easy at first.


code C#

downloadcontent_copy
expand_less
```
// DON'T DO THIS FOR DYNAMIC DATA!

public class NewsService

{

public List<string> GetLatestHeadlines()

{

return new List<string>

{

"Case of the Missing Semicolon Solved!",

"Mysterious Bug Appears in Downtown Server",

"Detective Praised for Flawless Code"

};

}

}

```

But what happens when the news changes? We, the programmers, have to go in, change the code,
rebuild everything, and redeploy the entire application. That's slow, inefficient, and requires an expert
for a simple text change. A good detective agency needs a system where anyone—even the reporters at
the front desk—can file a new story without having to know how to rewire the building.


That's what a database gives us: a central, manageable place for our data that is separate from our
application's logic.

### **Our First Portable Vault: SQLite**


For this case, we're not starting with a massive, city-wide bank vault like MySQL or SQL Server. We're
starting with something smaller, faster, and more portable: **SQLite** .


Think of SQLite as a high-tech, secure briefcase. It's not a separate building we have to connect to over
the network; it's a single file (.db) that lives right inside our project folder.


code Code

downloadcontent_copy
expand_less
```
/MyProject/

|

+-- MyApi.csproj

+-- Program.cs

+-- /Controllers/

+-- my-database.db <-- The entire database is just one file!

```

This makes it incredibly easy to get started with. It's perfect for development, testing, and smaller
applications. When we need to scale up later, we can move our data to a bigger vault, but the principles

we learn here will be the same.

### **The Middleman: Entity Framework Core**


Here's the next piece of the puzzle. We are C# detectives. We think in terms of objects, classes, and
properties. Databases, on the other hand, think in tables, rows, and columns. They speak a different
language called SQL.


We could learn to speak SQL fluently, and we will learn the basics, but it's often clunky to write by
hand. Wouldn't it be great if we had a translator who could take our C# objects and automatically

convert them into database commands?


That translator is called an **Object-Relational Mapper (ORM)**, and our tool of choice is Microsoft's
**Entity Framework Core (EF Core)** .


EF Core is our trusted informant. We give it a C# class, and it figures out how to create a database table
for it. We give it a Person object, and it figures out how to write the SQL INSERT command to save it.
We ask it for a list of all Person objects, and it writes the SELECT query for us. It handles the dirty
work so we can keep thinking in C#.


code Code

downloadcontent_copy
expand_less


```
+---------------+   +----------------+   +----------------+

|        |   |        |   |        |

| C# Code   | <--> | Entity    | <--> | SQLite     |

| (Person p)  |   | Framework Core|   | Database    |

|        |   | (The ORM)   |   | (Tables, Rows) |

+---------------+   +----------------+   +----------------+

### **The Plan of Attack: Setting up SQLite with EF Core**

```

It's time to get our hands dirty. Here's the procedure for setting up our first SQLite vault.


**1. Gear Up: Install the NuGet Packages**


First, we need to requisition the right tools for the job. We'll use the NuGet package manager to install
three key components into our API project:


   - Microsoft.EntityFrameworkCore.Sqlite: The main driver that allows EF Core to talk to SQLite.


   - Microsoft.EntityFrameworkCore.Design: Gives us the special command-line tools for
managing the database schema (we'll get to that).


   - Microsoft.EntityFrameworkCore.Relational: Provides the core relational database functionality.


**2. Identify the Subjects: Create an "Entity"**


An "entity" is just a fancy name for a C# class that we intend to save to the database. Let's take our
simple Address class and turn it into an entity. The most important rule is that every entity needs a
**primary key** —a unique identifier. By convention, a property named Id will automatically be chosen as
the primary key.


**Teacher's Notes:**


Here's how a simple class becomes a database entity. It's all about adding that Id.


code C#

downloadcontent_copy
expand_less
```
   // This is our plain old C# object (POCO)

   public class Address

   {

   public string City { get; set; }

   // ... other properties

   }

   // This is now an ENTITY ready for the database

```

```
   public class Address

   {

   public int Id { get; set; } // The Primary Key

   public string City { get; set; }

   // ... other properties

   }

```

**3. Set Up the Command Center: The DbContext**


The DbContext is the most important class in EF Core. It's our command center. It manages the
connection to the database and, most importantly, it tells EF Core which of our classes should be turned

into tables.


We create a new class that inherits from DbContext and add a DbSet<T> property for each entity we

want to save.


**Teacher's Notes:**


Think of the DbContext as your main case file index. Each DbSet is a filing cabinet for a
specific type of record (like "Addresses" or "Persons").


code C#

downloadcontent_copy
expand_less
```
   using Microsoft.EntityFrameworkCore;

   public class MyDbContext : DbContext

   {

   // This tells EF Core we want a table called "Addresses"

   // that will store our Address objects.

   public DbSet<Address> Addresses { get; set; }

   public MyDbContext(DbContextOptions<MyDbContext> options)

   : base(options)

   {

   }

   }

```

With these steps, we've laid the groundwork. We have our tools, we've identified our subjects, and
we've set up our command center. In the next part of the lesson, we'll connect everything and create our
first real, persistent database.

### **The Case of the Vanishing Data, Part 2: Forging the Vault**


Team, we've laid out the blueprints for Operation: Persistence. We have our tools (Entity Framework
Core), we've identified our subjects (Person, Address), and we've designed the command center
(DbContext). Now, it's time to connect the wires and forge our first real, persistent data vault.


**4. The Final Wiring: The Program.cs Connection**


Our application needs to be officially told about its new command center. We do this in the Program.cs
file, the central power station for our entire API. Here, we add a new service, telling the application's
"builder" to get the DbContext online.


This is a critical step called **Dependency Injection** . We're not creating the DbContext ourselves; we're
just telling the system, "Hey, when someone needs access to the database, I've registered this
BackendContext class. It's your job to create it and provide it."


But just telling it _what_ to create isn't enough. We have to tell it _how_ . What kind of vault is this? Where

is it?


We do this by providing options. We're telling the builder: "When you create this BackendContext, use
SQLite, and you can find the vault in a file named backend.db." This file name is our **connection**
**string** . It's the secret address to our data briefcase.


**Teacher's Notes:**


This piece of code in Program.cs is the magic that connects everything. It's a bit of a
mouthful, but let's break it down.


code C#

downloadcontent_copy
expand_less
```
   // In Program.cs...

   builder.Services.AddDbContext<BackendContext>(options =>

   {

   // We're telling it to use the SQLite provider

   options.UseSqlite("DataSource=backend.db");

```

```
   });

```

1. AddDbContext<BackendContext>: Registers our command center class with the

dependency injection system.


2. options => { ... }: This is a lambda expression, a quick way to write a function. It

lets us configure the options for this specific DbContext.


3. options.UseSqlite(...): This tells EF Core which database driver to use.


4. "DataSource=backend.db": This is the simplest possible connection string for

SQLite. It just means "look for (or create) a file with this name in the same directory
where the app is running."


**5. The First Heist: Creating the Database with Migrations**


Here's where EF Core shows its true power. We have C# classes, and we want a database that matches
them. We could write all the SQL CREATE TABLE commands by hand, but that's tedious and error
prone.


Instead, we use a powerful tool called **Migrations** .


Think of it like this: EF Core is our master forger. We show it our C# models, and it creates a "forgery
plan"—a set of instructions for building a perfect database replica. This plan is called a **migration** . It's
a C# file that contains all the steps needed to create our tables, columns, keys, and relationships.


We execute our first command from the terminal, inside our API project folder:


dotnet ef migrations add InitialCreate


This tells the Entity Framework (ef) tools to create a new migration named InitialCreate. EF Core
inspects our BackendContext, sees our Persons and Addresses DbSet properties, and generates the
forgery plan.


**Clue #1: The Missing Key**


But wait! Our first attempt fails. The system throws an error: The entity type 'Person' requires a primary
key to be defined.


Of course! Every record in a database table needs a unique identifier, a primary key. We forgot to add
one to our Person class. A good detective always double-checks the evidence. We quickly fix our
Person model, adding the crucial Id property.


code C#

downloadcontent_copy
expand_less
```
public class Person

{

public int Id { get; set; } // The missing Primary Key

```

```
public string Name { get; set; }

public int Age { get; set; }

// ...

}

```

With the fix in place, we run the command again. Success! A new Migrations folder appears in our
project. Inside is the recipe for building our vault.


**6. The Final Command: "Build It!"**


We have the plan. Now we execute it. This one last command tells EF Core to take our latest migration
and apply it to the database.


dotnet ef database update


The terminal whirs to life, showing us the exact SQL commands it's running behind the scenes. And
just like that, a new file appears in our project: backend.db. Our vault is built. It's real, and it's on our
file system. We can even open it with a database tool and see our empty Persons and Addresses tables,
ready for data.

### **The Payoff: Accessing the Data**


The vault is built. Now, let's prove we can get data in and out.


We'll create a new **endpoint** in Program.cs—a simple GET request that returns all the people in our

database.


Because we registered our BackendContext with dependency injection, getting access to it is incredibly
easy. We just add it as a parameter to our endpoint's handler function. The system handles the rest.


code C#

downloadcontent_copy
expand_less
```
// Add this to Program.cs

app.MapGet("/api/persons", (BackendContext context) =>

{

// 'context' is our live connection to the database.

// 'Persons' is the filing cabinet (the table).

// 'ToArray()' executes the query and gets all the records.

return context.Persons.ToArray();

});

```

We run the application and use Swagger to test our new /api/persons endpoint. It returns an empty array

[]. This is perfect! It means our API successfully connected to the database and correctly reported that
there are no records yet.


For the final test, we use our IDE's database tool to manually add a row to the Persons table. We run the
endpoint again, and this time, the JSON data for our new person appears.


The case is solved. Our data is no longer vanishing. It has found a home. It has **persisted** .


### **The Case of the Invisible Connection**

Alright, detectives, gather around. We've successfully set up our first data vault, the backend.db file,
using our trusty informant, Entity Framework Core. We've defined our subjects (Person, Address) and
our command center (DbContext).


But now, a new mystery has emerged. We need to understand how our informant _thinks_ . How does it
decide what to store and, more importantly, how does it connect the dots between different pieces of

evidence?


**Recap of the Crime Scene (DbContext)**


Let's quickly review the evidence board. Our BackendContext.cs file looks like this:


code C#

downloadcontent_copy
expand_less
```
public class BackendContext : DbContext

{

// The constructor for dependency injection

public BackendContext(DbContextOptions<BackendContext> options) : base(options)
{}

// Our filing cabinets for records we want to save

public DbSet<Person> Persons { get; set; }

public DbSet<Address> Addresses { get; set; }

}

```

Those DbSet properties are critical. They are our explicit instructions to EF Core, telling it, "I want a

table for Persons and a table for Addresses."


But what are we storing in these tables? Is it the _data_ itself, or is it the _tools_ we use to work with the

data?


**The Carpenter Analogy: Data vs. Tools**


Imagine you're a carpenter. In your workshop, you have two things: your **tools** (hammers, saws,
workbenches) and the **furniture** you build (chairs, tables, shelves).


If you were to create a database for your business, what would you store? You'd store an inventory of
the _furniture_ . You need to know how many chairs you have, what they're made of, and their price. You
wouldn't create a database entry for every single hammer you own. The hammer is a tool you use to
create the product; it isn't the product itself.


In our code, it's the same. Some classes are **tools** (SmsMessenger, EmailMessenger), while others
represent the **data** or "furniture" (Person, Address, CarInsurance). We only create DbSet properties for
the data we want to persist and track.

### **The Invisible Thread**


Here’s where the real detective work begins. Let's look at our Person class again.


code C#

downloadcontent_copy
expand_less
```
public class Person

{

public int Id { get; set; }

public string Name { get; set; }

// ... other properties ...

public Address Address { get; set; } // <-- This is a major clue!

}

```

We've told EF Core to create a table for Persons. But what does it do when it sees that a Person

_contains_ another complex object, an Address? Our informant is smart. It sees this relationship and
automatically deduces that these two pieces of evidence are connected.


To prove this, let's run an experiment. What if we played dumb and _only_ told EF Core about the

Persons table?


**The Experiment: Hiding the Evidence**


Let's go back to our DbContext and hide the Addresses filing cabinet.


code C#

downloadcontent_copy
expand_less
```
public class BackendContext : DbContext

{

public BackendContext(DbContextOptions<BackendContext> options) : base(options)
{}

// We ONLY have a DbSet for Persons now.

public DbSet<Person> Persons { get; set; }

// public DbSet<Address> Addresses { get; set; } // This line is gone!

}

```

Now, let's wipe the crime scene clean. We'll delete our old Migrations folder and the backend.db
database file to start completely fresh.


With a clean slate, we run the investigation again from the terminal:


dotnet ef migrations add InitialCreate


The command succeeds. Now for the big reveal: let's inspect the new migration file that was generated.

What did EF Core find?


Even though we only told it about Persons, the migration file contains instructions to create _two_ tables:

Addresses and Persons!


code Code

downloadcontent_copy
expand_less
```
// Inside the new migration file...

migrationBuilder.CreateTable(

name: "Address",

// ... columns for Id, City, etc.

);

migrationBuilder.CreateTable(

name: "Persons",

columns: table => new

{

Id = table.Column<int>...

Name = table.Column<string>...

AddressId = table.Column<int>... // <-- Where did this come from?!

},

constraints: table =>

{

table.PrimaryKey("PK_Persons", x => x.Id);

// This is the invisible thread!

table.ForeignKey(

name: "FK_Persons_Address_AddressId",

column: x => x.AddressId,

```

```
principalTable: "Address",

principalColumn: "Id");

});

```

EF Core automatically created the Addresses table because it was reachable from the Person class. It
also added an AddressId column to the Persons table. This AddressId is a **Foreign Key** . It's a special
pointer that connects a row in the Persons table to a specific row in the Addresses table. This is what

makes a database "relational."


**Teacher's Notes:**


This is a core concept called **Convention over Configuration** . By default, EF Core follows
common-sense rules (conventions) to figure out your database structure.


      - It saw a property named Id and _assumed_ it was the Primary Key.


      - It saw a property of type Address inside Person and _assumed_ a relationship.


      - It created a Foreign Key column and named it AddressId by convention
(<ClassName>Id).


We didn't have to configure any of this. It just worked because we followed the standard

conventions.

### **The Problem with Phantom Tables**


This is amazing, but there's a catch. Because we didn't create a DbSet for Addresses, we have no direct
way to query that table. We can't write context.Addresses.ToList(). We've created a "phantom table"
that exists in the database but is invisible to our command center. We can only access addresses by
going through a person first.


This is impractical. We'll almost always want to query our main data tables directly.


**The Final Verdict:** It's best to be explicit. You should always add a DbSet<T> for every entity that you
want to be a table in your database. This avoids confusion and makes your code's intentions crystal

clear.


