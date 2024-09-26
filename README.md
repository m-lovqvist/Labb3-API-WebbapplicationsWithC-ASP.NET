# Labb3-API-WebbapplicationsWithC-ASP.NET
This assignment is part of the course Web Applications with i C# and ASP.NET at Edugrade.

## API Documentation, examples of endpoint calls:
### Get all persons
GET/persons (no parameters)

### Get a specific person by ID, including all their interests and links:
GET/persons/{id} (no parameter other than ID)

### Add new person:
POST/persons
```json
{ "id": 0,
"firstName": "string", 
"lastName": "string", 
"phoneNumber": 
"833)431499", 
"email": "user@example.com", 
"age": 0 }
```

### Add new interest:
POST/interests
```json
{ "id": 0, 
"title": "string", 
"description": "string" }
```

### Connect a person to a new interest:
POST/personinterest
```json
{ "id": 0, 
"personId": {id}, 
"interestId": {id} }
```

### Add a link to a person - interest connection:
POST/links
```json
{ "linkId": 0, 
"url": "string", 
"personInterestsId": {id} }
```
