# AzShipping API Documentation

## Base URL
```
http://localhost:5062/api
```

## Authentication
Currently, no authentication is required. All endpoints are publicly accessible.

---

## Client Segments API

### Get All Client Segments
**GET** `/api/clientsegments`  
**GET** `/api/clientsegments/get/all`

Returns a list of all client segments ordered by priority.

**Response:**
```json
[
  {
    "id": "guid",
    "segmentName": "VIP",
    "segmentPriority": 1,
    "isActive": true,
    "isDefault": false,
    "primaryColor": "#FFFFFF",
    "secondaryColor": "#000000",
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null
  }
]
```

**Status Codes:**
- `200 OK` - Success

---

### Get Client Segment by ID
**GET** `/api/clientsegments/{id}`  
**GET** `/api/clientsegments/get/{id}`

Returns a specific client segment by its ID.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the client segment

**Response:**
```json
{
  "id": "guid",
  "segmentName": "VIP",
  "segmentPriority": 1,
  "isActive": true,
  "isDefault": false,
  "primaryColor": "#FFFFFF",
  "secondaryColor": "#000000",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

**Status Codes:**
- `200 OK` - Success
- `404 Not Found` - Client segment not found

---

### Create Client Segment
**POST** `/api/clientsegments`  
**POST** `/api/clientsegments/create`

Creates a new client segment.

**Request Body:**
```json
{
  "segmentName": "Premium Client",
  "segmentPriority": 1,
  "isActive": true,
  "isDefault": false,
  "primaryColor": "#FF5733",
  "secondaryColor": "#000000"
}
```

**Validation Rules:**
- `segmentName` (required, max 100 characters)
- `segmentPriority` (required, must be > 0)
- `primaryColor` (required, hex color format)
- `secondaryColor` (required, hex color format)

**Response:**
```json
{
  "id": "guid",
  "segmentName": "Premium Client",
  "segmentPriority": 1,
  "isActive": true,
  "isDefault": false,
  "primaryColor": "#FF5733",
  "secondaryColor": "#000000",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

**Status Codes:**
- `201 Created` - Successfully created
- `400 Bad Request` - Validation error

---

### Update Client Segment
**PUT** `/api/clientsegments/{id}`  
**PUT** `/api/clientsegments/update/{id}`

Updates an existing client segment.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the client segment

**Request Body:**
```json
{
  "segmentName": "Premium Client Updated",
  "segmentPriority": 2,
  "isActive": true,
  "isDefault": false,
  "primaryColor": "#FF5733",
  "secondaryColor": "#000000"
}
```

**Response:**
```json
{
  "id": "guid",
  "segmentName": "Premium Client Updated",
  "segmentPriority": 2,
  "isActive": true,
  "isDefault": false,
  "primaryColor": "#FF5733",
  "secondaryColor": "#000000",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-02T00:00:00Z"
}
```

**Status Codes:**
- `200 OK` - Successfully updated
- `400 Bad Request` - Validation error
- `404 Not Found` - Client segment not found

---

### Delete Client Segment
**DELETE** `/api/clientsegments/{id}`  
**DELETE** `/api/clientsegments/delete/{id}`

Deletes a client segment.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the client segment

**Status Codes:**
- `204 No Content` - Successfully deleted
- `404 Not Found` - Client segment not found

---

## Request Sources API

### Get All Request Sources
**GET** `/api/requestsources`  
**GET** `/api/requestsources/get/all`

Returns a list of all request sources ordered by name.

**Response:**
```json
[
  {
    "id": "guid",
    "name": "Email",
    "isActive": true,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null
  }
]
```

**Status Codes:**
- `200 OK` - Success

---

### Get Request Source by ID
**GET** `/api/requestsources/{id}`  
**GET** `/api/requestsources/get/{id}`

Returns a specific request source by its ID.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the request source

**Response:**
```json
{
  "id": "guid",
  "name": "Email",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

**Status Codes:**
- `200 OK` - Success
- `404 Not Found` - Request source not found

---

### Create Request Source
**POST** `/api/requestsources`  
**POST** `/api/requestsources/create`

Creates a new request source.

**Request Body:**
```json
{
  "name": "WhatsApp",
  "isActive": true
}
```

**Validation Rules:**
- `name` (required, max 100 characters)

**Response:**
```json
{
  "id": "guid",
  "name": "WhatsApp",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

**Status Codes:**
- `201 Created` - Successfully created
- `400 Bad Request` - Validation error

---

### Update Request Source
**PUT** `/api/requestsources/{id}`  
**PUT** `/api/requestsources/update/{id}`

Updates an existing request source.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the request source

**Request Body:**
```json
{
  "name": "WhatsApp Business",
  "isActive": true
}
```

**Response:**
```json
{
  "id": "guid",
  "name": "WhatsApp Business",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-02T00:00:00Z"
}
```

**Status Codes:**
- `200 OK` - Successfully updated
- `400 Bad Request` - Validation error
- `404 Not Found` - Request source not found

---

### Delete Request Source
**DELETE** `/api/requestsources/{id}`  
**DELETE** `/api/requestsources/delete/{id}`

Deletes a request source.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the request source

**Status Codes:**
- `204 No Content` - Successfully deleted
- `404 Not Found` - Request source not found

---

## Packagings API

### Get All Packagings
**GET** `/api/packagings`  
**GET** `/api/packagings/get/all`

Returns a list of all packagings with their translations, ordered by name (EN translation preferred).

**Response:**
```json
[
  {
    "id": "guid",
    "name": "Box",
    "isActive": true,
    "translations": {
      "EN": "Box",
      "LT": "Dėžė",
      "RU": "Коробка"
    },
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": null
  }
]
```

**Status Codes:**
- `200 OK` - Success

---

### Get Packaging by ID
**GET** `/api/packagings/{id}`  
**GET** `/api/packagings/get/{id}`

Returns a specific packaging by its ID with all translations.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the packaging

**Response:**
```json
{
  "id": "guid",
  "name": "Box",
  "isActive": true,
  "translations": {
    "EN": "Box",
    "LT": "Dėžė",
    "RU": "Коробка",
    "FR": "Boîte",
    "UK": "Коробка",
    "PL": "Pudełko",
    "KA": "ყუთი",
    "AZ": "Qutu"
  },
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

**Status Codes:**
- `200 OK` - Success
- `404 Not Found` - Packaging not found

---

### Create Packaging
**POST** `/api/packagings`  
**POST** `/api/packagings/create`

Creates a new packaging with translations.

**Request Body:**
```json
{
  "name": "Plastic Container",
  "isActive": true,
  "translations": {
    "EN": "Plastic Container",
    "LT": "Plastikinis konteineris",
    "RU": "Пластиковый контейнер",
    "FR": "Conteneur en plastique",
    "UK": "Пластиковий контейнер",
    "PL": "Pojemnik plastikowy",
    "KA": "პლასტმასის კონტეინერი",
    "AZ": "Plastik konteyner"
  }
}
```

**Validation Rules:**
- `name` (required, max 200 characters) - Used as default EN translation if EN is not in translations
- `translations` (optional) - Dictionary of language codes to names
  - Supported language codes: `LT`, `EN`, `RU`, `FR`, `UK`, `PL`, `KA`, `AZ`

**Response:**
```json
{
  "id": "guid",
  "name": "Plastic Container",
  "isActive": true,
  "translations": {
    "EN": "Plastic Container",
    "LT": "Plastikinis konteineris",
    "RU": "Пластиковый контейнер"
  },
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": null
}
```

**Status Codes:**
- `201 Created` - Successfully created
- `400 Bad Request` - Validation error

**Note:** The `name` field is used as the EN translation. If you provide `EN` in the `translations` object, that value will be used instead.

---

### Update Packaging
**PUT** `/api/packagings/{id}`  
**PUT** `/api/packagings/update/{id}`

Updates an existing packaging and its translations.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the packaging

**Request Body:**
```json
{
  "name": "Plastic Container Updated",
  "isActive": true,
  "translations": {
    "EN": "Plastic Container Updated",
    "LT": "Plastikinis konteineris atnaujintas",
    "RU": "Пластиковый контейнер обновлен"
  }
}
```

**Response:**
```json
{
  "id": "guid",
  "name": "Plastic Container Updated",
  "isActive": true,
  "translations": {
    "EN": "Plastic Container Updated",
    "LT": "Plastikinis konteineris atnaujintas",
    "RU": "Пластиковый контейнер обновлен"
  },
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-02T00:00:00Z"
}
```

**Status Codes:**
- `200 OK` - Successfully updated
- `400 Bad Request` - Validation error
- `404 Not Found` - Packaging not found

**Note:** All existing translations will be replaced with the provided translations.

---

### Delete Packaging
**DELETE** `/api/packagings/{id}`  
**DELETE** `/api/packagings/delete/{id}`

Deletes a packaging and all its translations.

**Parameters:**
- `id` (Guid, path parameter) - The unique identifier of the packaging

**Status Codes:**
- `204 No Content` - Successfully deleted
- `404 Not Found` - Packaging not found

---

## Test Endpoint

### Test API
**GET** `/api/test`

Simple test endpoint to verify the API is working.

**Response:**
```json
{
  "message": "AzShipping API is working!",
  "timestamp": "2024-01-01T00:00:00Z"
}
```

**Status Codes:**
- `200 OK` - Success

---

## Error Responses

All endpoints may return the following error responses:

### 400 Bad Request
```json
{
  "message": "Validation error message",
  "errors": {
    "fieldName": ["Error message 1", "Error message 2"]
  }
}
```

### 404 Not Found
```json
{
  "message": "Resource with ID {id} not found"
}
```

### 500 Internal Server Error
```json
{
  "message": "An error occurred while processing your request"
}
```

---

## Supported Language Codes

For Packagings API translations:
- `LT` - Lithuanian
- `EN` - English (default)
- `RU` - Russian
- `FR` - French
- `UK` - Ukrainian
- `PL` - Polish
- `KA` - Georgian
- `AZ` - Azerbaijani

---

## Example Usage

### JavaScript/Fetch Examples

#### Get All Client Segments
```javascript
const response = await fetch('http://localhost:5062/api/clientsegments');
const segments = await response.json();
```

#### Create Client Segment
```javascript
const response = await fetch('http://localhost:5062/api/clientsegments', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    segmentName: "Premium",
    segmentPriority: 1,
    isActive: true,
    isDefault: false,
    primaryColor: "#FF5733",
    secondaryColor: "#000000"
  })
});
const created = await response.json();
```

#### Update Request Source
```javascript
const response = await fetch(`http://localhost:5062/api/requestsources/${id}`, {
  method: 'PUT',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    name: "Updated Name",
    isActive: true
  })
});
const updated = await response.json();
```

#### Create Packaging with Translations
```javascript
const response = await fetch('http://localhost:5062/api/packagings', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    name: "New Packaging",
    isActive: true,
    translations: {
      "EN": "New Packaging",
      "LT": "Nauja pakuotė",
      "RU": "Новая упаковка"
    }
  })
});
const created = await response.json();
```

#### Delete Packaging
```javascript
const response = await fetch(`http://localhost:5062/api/packagings/${id}`, {
  method: 'DELETE'
});
// Returns 204 No Content on success
```

---

## Swagger Documentation

Interactive API documentation is available at:
```
http://localhost:5062/swagger
```

This provides a UI to test all endpoints directly from the browser.

---

## Notes for Frontend Developers

1. **Content-Type**: All POST and PUT requests require `Content-Type: application/json` header.

2. **Guid Format**: All IDs are GUIDs (UUIDs) in the format: `xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx`

3. **Date Format**: All dates are in ISO 8601 format (UTC): `2024-01-01T00:00:00Z`

4. **Error Handling**: Always check the response status before parsing JSON. Handle 400, 404, and 500 errors appropriately.

5. **CORS**: CORS is enabled for all origins in development mode.

6. **Empty Collections**: If no items exist, endpoints return an empty array `[]`, not `null`.

7. **Packaging Translations**: The `name` field in Packaging DTOs represents the default name (preferably EN). All available translations are in the `translations` dictionary.

8. **Color Format**: Colors should be in hex format (e.g., `#FFFFFF`, `#FF5733`).

