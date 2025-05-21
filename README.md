# PacificKodeTest

Setup Instructions
This solution contains two separate projects:

API Project – Manages backend services and database operations

Web Project – Frontend application that interacts with the API

Steps to Set Up
1. 📂 Database Setup
Create the database in your SQL Server instance.

Run the provided SQL script.
This script includes all required tables and stored procedures.

2. ⚙️ API Project Configuration
Open the appsettings.json file in the API project.

Update the connection string to point to your local SQL Server instance.

3. 🌐 Web Project Configuration
Open the appsettings.json file in the Web project.

No need to modify the localhost port number.

The Web project will automatically communicate with the API running on localhost and its assigned port.
