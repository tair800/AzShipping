# AzShipping API - Quick Reference

## Base URL
```
http://localhost:5062/api
```

---

## Client Segments

| Method | Endpoint | Alternative Endpoint | Description |
|--------|----------|---------------------|-------------|
| GET | `/api/clientsegments` | `/api/clientsegments/get/all` | Get all segments |
| GET | `/api/clientsegments/{id}` | `/api/clientsegments/get/{id}` | Get segment by ID |
| POST | `/api/clientsegments` | `/api/clientsegments/create` | Create segment |
| PUT | `/api/clientsegments/{id}` | `/api/clientsegments/update/{id}` | Update segment |
| DELETE | `/api/clientsegments/{id}` | `/api/clientsegments/delete/{id}` | Delete segment |

**Request Body (POST/PUT):**
```json
{
  "segmentName": "string (required, max 100)",
  "segmentPriority": "number (required, > 0)",
  "isActive": "boolean",
  "isDefault": "boolean",
  "primaryColor": "string (required, hex)",
  "secondaryColor": "string (required, hex)"
}
```

---

## Request Sources

| Method | Endpoint | Alternative Endpoint | Description |
|--------|----------|---------------------|-------------|
| GET | `/api/requestsources` | `/api/requestsources/get/all` | Get all sources |
| GET | `/api/requestsources/{id}` | `/api/requestsources/get/{id}` | Get source by ID |
| POST | `/api/requestsources` | `/api/requestsources/create` | Create source |
| PUT | `/api/requestsources/{id}` | `/api/requestsources/update/{id}` | Update source |
| DELETE | `/api/requestsources/{id}` | `/api/requestsources/delete/{id}` | Delete source |

**Request Body (POST/PUT):**
```json
{
  "name": "string (required, max 100)",
  "isActive": "boolean"
}
```

---

## Packagings

| Method | Endpoint | Alternative Endpoint | Description |
|--------|----------|---------------------|-------------|
| GET | `/api/packagings` | `/api/packagings/get/all` | Get all packagings |
| GET | `/api/packagings/{id}` | `/api/packagings/get/{id}` | Get packaging by ID |
| POST | `/api/packagings` | `/api/packagings/create` | Create packaging |
| PUT | `/api/packagings/{id}` | `/api/packagings/update/{id}` | Update packaging |
| DELETE | `/api/packagings/{id}` | `/api/packagings/delete/{id}` | Delete packaging |

**Request Body (POST/PUT):**
```json
{
  "name": "string (required, max 200)",
  "isActive": "boolean",
  "translations": {
    "EN": "English name",
    "LT": "Lithuanian name",
    "RU": "Russian name",
    "FR": "French name",
    "UK": "Ukrainian name",
    "PL": "Polish name",
    "KA": "Georgian name",
    "AZ": "Azerbaijani name"
  }
}
```

**Note:** `name` field is used as default EN translation if EN is not in translations object.

---

## Test Endpoint

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/test` | Test API health |

---

## Common Response Codes

- `200 OK` - Success
- `201 Created` - Resource created
- `204 No Content` - Success (delete operations)
- `400 Bad Request` - Validation error
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

---

## Headers

All POST/PUT requests require:
```
Content-Type: application/json
```

---

## Swagger UI
```
http://localhost:5062/swagger
```

For detailed documentation, see `API_DOCUMENTATION.md`

