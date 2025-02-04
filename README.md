
# Number Classification API

## Description
This API takes a number as input and returns interesting mathematical properties about it, along with a fun fact. It checks whether the number is prime, perfect, or Armstrong, and provides additional details such as whether it's odd or even. It also calculates the sum of the digits and retrieves a fun fact related to the number from the Numbers API.

## Features
- **Prime Check**: Determines if the number is prime.
- **Perfect Number Check**: Determines if the number is perfect.
- **Armstrong Check**: Identifies if the number is an Armstrong number.
- **Odd/Even Check**: Determines if the number is odd or even.
- **Digit Sum**: Returns the sum of the digits of the number.
- **Fun Fact**: Fetches a fun fact related to the number from the Numbers API.

## Endpoint

### GET `/api/classify-number?number=<number>`

- **Query Parameter**:
  - `number`: The number to classify (integer).

### Response Format (200 OK)

```json
{
    "number": 371,
    "is_prime": false,
    "is_perfect": false,
    "properties": ["armstrong", "odd"],
    "digit_sum": 11,
    "fun_fact": "371 is an Armstrong number because 3^3 + 7^3 + 1^3 = 371"
}
```

### Response Format (400 Bad Request)

```json
{
    "number": "alphabet",
    "error": true
}
```

### Possible Properties
- **"armstrong"**: The number is an Armstrong number.
- **"odd"**: The number is odd.
- **"even"**: The number is even.

### Example Request

```bash
GET https://hng12-numberclassifyapi.onrender.com/api/classify-number?number=371
```

### Example Response

```json
{
    "number": 371,
    "is_prime": false,
    "is_perfect": false,
    "properties": ["armstrong", "odd"],
    "digit_sum": 11,
    "fun_fact": "371 is a narcissistic number"
}
```

## Technology Stack
- **Backend**: C# with ASP.NET Core Web API
- **Deployment**: [Render]
- **External API**: Numbers API (http://numbersapi.com/)

## CORS
The API handles Cross-Origin Resource Sharing (CORS), allowing requests from different origins.

## Error Handling
- Returns HTTP `400` for invalid input (non-integer values).
- Returns HTTP `500` for internal server errors.

## Version Control
The source code is hosted on GitHub.

- Repository: [https://github.com/Fabito97/HNG12_NumberClassifyApi]
- Branch: `main` 

## Deployment
The API is deployed and publicly accessible.

- **Endpoint**: [Insert deployed API URL here, e.g., https://hng12-numberclassifyapi.onrender.com/api/classify-number]

## Requirements
- Accepts only valid integers as input.
- Fast response time (< 500ms).
- Returns responses in JSON format.
