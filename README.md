# EdgeGuard - API Gateway Project.

## Overview
EdgeGuard is a simple API Gateway built using .NET.

It sits between the client and backend API and handles:
- API Key authentication
- Rate limiting
- Request logging
- Secure request forwarding

---

## How It Works (Simple Flow)

Client → EdgeGuard → Backend API → Response

---

## Architecture Diagram
<img width="1536" height="1024" alt="API gateway architecture workflow diagram" src="https://github.com/user-attachments/assets/6a427870-8b60-4385-8352-524071d208e4" />


---

## Request Flow (Step-by-Step)

1. Client sends request with:
   - x-api-key
   - Authorization (Bearer token)

2. EdgeGuard Gateway:
   - Validates API key
   - Applies rate limit
   - Logs request
   - Forwards request

3. Backend API:
   - Validates JWT token
   - Processes request
   - Returns response

4. Response goes back to client

---

## Features

- API Key Authentication
- Rate Limiting (per client)
- JWT Authentication (backend)
- Request Forwarding
- Request Logging

---

## Tech Stack

- .NET Web API
- SQL Server
- Entity Framework Core

---

## How to Run

1. Clone the repository
2. Run SmartAppointmentScheduler API
3. Run EdgeGuard API
4. Use Swagger to test

---

## Sample Request

POST /gateway/appointments/GetTimeSlot?dateString=16-01-2026

Headers:
x-api-key: client_free_001  
Authorization: Bearer <token>

---

## Related Project

This API Gateway works with the backend service:

👉 SmartAppointmentScheduler API  
[https://github.com/your-username/smartappointmentscheduler](https://github.com/gowsi-dev/SmartAppointmentScheduler.API)

This backend API handles business logic and JWT-based authentication.

---

## Future Improvements

- Redis-based rate limiting
- Azure deployment
- API monitoring dashboard
