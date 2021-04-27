---
title: 'README'
---

API - documentation
===

# Description

This is a API and the database schema that is used by server to get the needed data from db using endpoints.

# Table of Contents

[TOC]

# Endpoints
## 1. api/users
**[GET]** `api/users?username=<string:username>`
**Description:** return data about user with given username.
**Params:** `URI: <string:username>`
**Example response:**
```gherkin=
200 OK
{
    "id": 1,
    "password": "123",
    "username": "multiUser"
}
```

**[GET]** `api/users`
**Description:** return data about all users.
**Params:** `None`
**Example response:**
```gherkin=
200 OK
[
    {
        "id": 1,
        "password": "123",
        "username": "multiUser"
    },
    {
        "id": 2,
        "password": "123",
        "username": "userMulti"
    }
]
```

**[POST]** `api/users`
**Description:** add new user with given username and password.
**Params:** `JSON:`
```gherkin=
{
    "username": "multiUser",
    "password": "pass"
}
```
**Example response:** `201 CREATED`

**[PUT]** `api/users?username=<string:username>`
**Description:** change all params in specific user entity for given username.
**Params:** `URI: <string:username>` `JSON:`
```gherkin=
{
    "username": "new_username",
    "password": "new_password"
}
```
**Example response:** `200 ALTERED`

**[PATCH]** `api/users?username=<string:username>`
**Description:** change username or password in specific user entity for given username.
**Params:** `URI: <string:username>` `JSON:`
```gherkin=
{
    "username": "new_username"
}
```
**Example response:** `200 ALTERED`

**[DELETE]** `api/users?username=<string:username>`
**Description:** delete specific user entity for given username.
**Params:** `URI: <string:username>` 
**Example response:** 
```gherkin=
200 OK
{
    "Message": "User multiKorzych2 deleted."
}
```
## 2. api/quizzes

**[GET]** `api/quizzes?category_name=<string:category_name>`
**Description:** return set of questions for given category name.
**Params:** `URI: <string:category_name>`
**Example response:**
```gherkin=
200 OK
[
    {
        "Answers": [
            {
                "answer": "Kashima",
                "id": 149,
                "questionid": 38
            },
            {
                "answer": "Ashima",
                "id": 150,
                "questionid": 38
            },
            {
                "answer": "Silver",
                "id": 152,
                "questionid": 38
            },
            {
                "answer": "Gold",
                "id": 151,
                "questionid": 38
            }
        ],
        "Question": {
            "correct_answer": 149,
            "id": 38,
            "question": "Najlepszy rodzaj powłoki w amortyzatorach firmy FOX?",
            "quizid": 1
        }
    },
    {
        "Answers": [
            {
                "answer": "BMC",
                "id": 147,
                "questionid": 37
            },
            {
                "answer": "Marin",
                "id": 146,
                "questionid": 37
            },
            {
                "answer": "Specialized",
                "id": 148,
                "questionid": 37
            },
            {
                "answer": "Kross",
                "id": 145,
                "questionid": 37
            }
        ],
        "Question": {
            "correct_answer": 145,
            "id": 37,
            "question": "Polska marka rowerów to?",
            "quizid": 1
        }
    }
]
```

## 3. api/stats

**[GET]** `api/stats?userid=<string:userid>`
**Description:** return stats array for user with given userid.
**Params:** `URI: <string:userid>`
**Example response:**
```gherkin=
200 OK
[
    {
        "id": 5,
        "quizid": 1,
        "score": 1,
        "userid": 1
    },
    {
        "id": 6,
        "quizid": 2,
        "score": 1,
        "userid": 1
    }
]
```

**[PATCH]** `api/stats`
**Description:** create if not exist or update stats for user with given userid if current score is higher than current value stored in database.
**Params:** `JSON:`
```gherkin=
{
    "userid" : 1,
    "quizid": 2,
    "score":1
}
```
**Example response:** `200 OK`

