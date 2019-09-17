# SqlLookupProxy
Web Service to proxy on premises SQL Lookups--

To use, set up connection strings and SQL that is to be executed in the app.config file.  Any values that need to be included in the SQL can be surrounded in curly braces and passed in as parameters in the body of Web Service Call as follows:

{
  Name: "MyQueryToExecute",
  ReplaceableValues: {
    "CustomerId": "1234",
    "IsActive": true
  }
  
