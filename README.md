# ContactAPI
<h2>Contact API with funcationality to add, list, edit and delete contacts.</h2>

<h3>Technologies:</h3>
Asp.Net Core 3.1, C#, SwaggerUI,AutoMapper,Entity Framework Core, SQL Server, Visual Studio 2019

<h3>Set up</h3>

1) Clone this repository to your local machine.
2) Open the project with Visual Studio 2019
3) Setup the SQL Server connection in 'aspsetting.json' file otherwise by default it will create database locally.
3) Build the solution.
4) Open the 'Package Manager Console' from Tools tab in Visual Studio. Run the command 'UPDATE-DATABASE'. This will create database and seed the test data.
5) Run the solution. Browser will open with 'https://localhost:44399/swagger/index.html'.

<h3>Endpoints: </h3>

<h4>URI: GET /api/Contact/ListAllContacts</h4>
<h5>Description: Return the list of all the contacts in database table</h5>
<h5>Sample URL: </h5> 'https://localhost:44399/api/Contact/ListAllContacts' 
<h4>Input Parameters:</h4> None
<h4>Response:</h4>
A collection of Contact Object

| ColumnName | Type   |
|----------- | ------ |
| Id         | String |
| FirstName  | String |
| LastName   | String |
| Email      | String |
| Phone      | String |
| Status     | String |

<h4>URI POST /api/Contact/AddNewContact</h4>
<h5>Description: Create new entry for Contact in database. FirstName and LastName are required fields and rest are optional</h5>
 <h4>Input Parameters</h4>
 
| ColumnName | Type   | IsRequired |
|----------- | ------ | ---------- |
| Id         | String | False      |
| FirstName  | String | True       |
| LastName   | String | True       |
| Email      | String | False      |
| Phone      | String | False      |
| Status     | String | False      |
 
 
<h4>Response:</h4> 

| ColumnName | Type   |
|----------- | ------ |
| FirstName  | String |
| LastName   | String |
| Email      | String |
| Phone      | String |
| Status     | String |

<h4>URI PUT /api/Contact/UpdateContact/{id}</h4>
<h5>Description: Update the Contact information based on Contact id. FirstName and LastName are required fields and rest are optional</h5>
 <h4>Input Parameters</h4>
 
| ColumnName | Type   | IsRequired |
|----------- | ------ | ---------- |
| Id         | String | True      |
| FirstName  | String | True       |
| LastName   | String | True       |
| Email      | String | False      |
| Phone      | String | False      |
| Status     | String | False      |
 
 
<h4>Response:</h4> 

| ColumnName | Type   |
|----------- | ------ |
| FirstName  | String |
| LastName   | String |
| Email      | String |
| Phone      | String |
| Status     | String |

<h4>URI DELETE /api/Contact/DeleteContact/{id}</h4>
<h5>Description: Delete Contact based on Contact ID</h5>
 <h4>Input Parameters</h4>
 
| ColumnName | Type   | IsRequired |
|----------- | ------ | ---------- |
| Id         | String | True      |

 




 
