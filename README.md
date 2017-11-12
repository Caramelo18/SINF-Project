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

1. In "Solution Explorer", right click App_Data > Add > SQL Server Database
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

1. Go to Models > Database.edmx (double click)
2. Right click class to update > "Update Model from Database..."

## Authors

* **Catarina Correia**
* **Fábio Caramelo** 
* **José Aleixo**
* **Margarida Viterbo**
* **William Fukunaga**
