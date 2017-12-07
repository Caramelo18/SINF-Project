# SINF Dashboard 360 - Integration with Primavera API

This project consists on the creation of a WebApp which interacts the [Primavera ERP](https://me.primaverabss.com/) API and shows the most crucial information of a company on an interactive Dashboard.

## Getting Started

To run this project you must:

1. Own a Primavera ERP custom virtual machine
2. Clone this repository into that machine
3. Open the solution (.sln) with Visual Studio
4. Right-click "References" under FirstREST project and click "Add Reference..."
5. Browse to folder "C:\Program Files\PRIMAVERA\SG900\Apl\" and add all Interop*.dll files.
6. Build and Run on Visual Studio

## Creating database

To create a db:

1. In "Solution Explorer", right click App_Data > Add > SQL Server Database or right click App_Data > Add > New Item > SQL Server Database (in Installed > Data)
2. Open "Server Explorer" (View > Server Explorer), right click Tables > New Query
3. Copy the script (in folder db > script.sql) and execute
4. Now, if right click Tables > Refresh, you can see the new tables

To add db to models:

1. In Solution Explorer, right click Models > Add > New Item
2. Add ADO.NET Entity Data Model (Installed > Data) and call it "Database"
3. Choose Model Contents : Next
4. Choose Your Data Connection: Check save connection settings in Web.Config as "DatabaseEntities"
5. Choose Your Database Objects and Settings: Check Tables and choose as Model Namespace "DatabaseModel"

To update models when db has changed:

1. Go to Server Explorer > DatabaseEntities (right click > New Query)
2. Copy script.sql from db folder and paste it. Execute (Ctrl + Shift + E)
3. Go to Models > Database.edmx (double click)
4. Right click class to update > "Update Model from Database..."
**OR** 

5. If errors when running, delete all tables (Ctrl + A, Del)
6. Right click class to update > "Update Model from Database..."
7. In Add tab, check Tables and then click on Finish button

**NOTE:** If, when building, there are some mistakes related to .cs files, go to Models > expand Database.edmx > right click .tt files and select "Run Custom Tool"

## Angular Client

1. Install nodejs and npm (can be downloaded on their website).
2. Install angular by running this command: "npm install -g @angular/cli"
3. Browse to Client and run "npm install".
4. Run the client: "npm start"

**Note**: if you run into `EPERM errors` when executing `npm install`, install NPM 5.3 with `npm install npm@5.3 -g`.

## Authors

* **Catarina Correia**
* **Fábio Caramelo** 
* **José Aleixo**
* **Margarida Viterbo**
* **William Fukunaga**
