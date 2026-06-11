# Labb 3 - API

A REST API built with ASP.NET Core and Entity Framework Core. The API manages people, their interests, and links associated with each person and interest.

## Technologies
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger

## Database Structure
- **Person** — stores name and phone number
- **Interests** — stores interest name and description
- **Link** — stores a URL connected to both a person and an interest

## Endpoints

### Get all users
**GET** `/api/User`

Response:
```json
[
  {
    "name": "Alice",
    "phoneNumber": "1234567890",
    "id": 1,
    "interests": [
      {
        "id": 1,
        "interestName": "Programming",
        "description": null,
        "links": [
          {
            "id": 1,
            "url": "https://www.thebestexamplewebsite.com"
          },
        ]
      },
```

---

### Get all interests
**GET** `/api/interests`

Explaination: `Get all interests created in the database.`

Response : 
```json
[
  {
    "id": 1,
    "interestName": "Interest 1",
    "description": null
  },
  
  {
    "id": 2,
    "interestName": "Interest 2",
    "description": null
  },
  
  {
    "id": 3,
    "interestName": "Interest 3",
    "description": null
  },
  
  {
    "id": 4,
    "interestName": "Interest 4",
    "description": null
  },
]
```

---

### Get all interests for a person
**GET** `/api/User/{id}/interests`

Example: `/api/catalogue/1/interests`

Explaination: `Only loads the interests, names and no comments.`

Response:
```json
{
  "name": "Alice",
  "phoneNumber": null,
  "id": 1,
  "interests": [
    {
      "id": 1,
      "interestName": "Programming",
      "description": null,
      "links": null
    },
    {
      "id": 2,
      "interestName": "Cooking",
      "description": null,
      "links": null
    },
    {
      "id": 3,
      "interestName": "Traveling",
      "description": null,
      "links": null
    }
  ]
}
```

---

### Get all links for a person
**GET** `/api/User/{id}/links`

Example: `/api/User/1/links`

Response:
```json
{
  "name": "Alice",
  "id": 1,
  "links": [
    {
      "id": 1,
      "url": "https://www.youtube.com"
    },
    {
      "id": 2,
      "url": "https://www.google.com"
    },
    {
      "id": 3,
      "url": "https://www.google.com"
    },
    {
      "id": 11,
      "url": "https://www.microsoft.com"
    }
  ]
}
```

---

### Create a new interest
**POST** `/api/Interest`

Request body:
```json
{
  "interestName": "string",
  "description": "string"
}
```

---

### Create a new user
**POST** `/api/User`

Request body:
```json
{
  "name": "string",
  "phoneNumber": "string"
}
```

---

### Add interest to User
**POST** /api/User/{userID}/interest/{interestID}

Example: `/api/User/1/interests/3`


| Parameter | Type | Description |
|-----------|------|-------------|
| userID | int | ID of the user |
| interestID | int | ID of the interest to attach the link to |


---

### Add link to user's interest
**POST** `/api/User/{userID}/interest/{interestID}/link`

Example: `/api/User/1/interests/3/link`


| Parameter | Type | Description |
|-----------|------|-------------|
| userID | int | ID of the user |
| interestID | int | ID of the interest to attach the link to |

Request body:
```json
{
"url": "string"
}
```


