# 🎯 RentIT

**RentIT** is a modern equipment rental platform built using microservices architecture. The system enables users to browse available equipment, create rentals, and manage rental history, while services communicate through an event-driven architecture (RabbitMQ). 

The platform focuses on <b> scalability, reliability, and maintainability </b> using Clean Architecture principles.

## ✨ Features
### 🌐 Platform (User-Facing API)
- User registration & authentication (JWT)
- Browse available equipment
- Create and manage rentals
- Rental history tracking
- Availability validation before booking
- Review and rating system
- Secure access to protected endpoints
- Distributed caching for faster rental queries
- RESTful API structure

## 🧩 Microservices Overview

### 👤 User Service
- User registration & login
- JWT token generation
- Identity management with ASP.NET Core Identity
- Role-based authorization

### 🏗️ Equipment Service
- Equipment catalog management
- CRUD operations for equipment
- Availability tracking
- Integration with Rental Service
- Event publishing to message broker

### 📦 Rental Service
- Rental lifecycle management
- Reservation validation
- Overdue tracking with penalty calculation
- Redis caching for performance
- Integration with Equipment and User services

### 📝 Review Service
- Equipment reviews and ratings
- Review aggregation
- Feedback management
- Event-based communication with other services

### 🌐 API Gateway
- Single entry point for all requests
- Request routing to microservices
- JWT validation
- Centralized authentication
- Aggregation of responses from multiple services

## 🧰 Technologies
### Backend
- ASP.NET Core 9
- Entity Framework Core 9
- PostgreSQL
- Ocelot API Gateway
- RabbitMQ
- Redis
- Polly (Retry & Circuit Breaker policies)

###  DevOps & Infrastructure
- Docker
- Docker Compose
- Visual Studio Container Tools

## 🏗️ Architecture
RentIT is designed as a distributed microservices system where each service is responsible for a specific business domain.

Services communicate synchronously via HTTP and asynchronously using RabbitMQ events, ensuring loose coupling and high scalability.

Key architectural characteristics:

- Independent databases per service
- API Gateway as single entry point
- Event-driven communication between services
- Fault tolerance with retry and circuit breaker policies
- Scalable container-based deployment

Main services:

- User Service
- Equipment Service
- Rental Service
- Review Service
- API Gateway
- RabbitMQ (message broker)
- Redis (cache)

## 🚀 Quick Start
1. Clone repository `git clone https://github.com/TheCrudClapper/RentIT.git` then `cd RentIT`
2. Make sure that docker is up and running
3. Run containers via `docker-compose up -d`
4. Verify services
   - API Gateway: `http://localhost:5050`
   - User Service: `http://localhost:5000/swagger`
   - Equipment Service: `http://localhost:5002/swagger`
   - Rental Service: `http://localhost:5004/swagger`
   - Review Service: `http://localhost:5006/swagger`

## 🔗 Useful Links

- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core)
- [Entity Framework Core Documentation](https://learn.microsoft.com/ef/core)
- [PostgreSQL](https://www.postgresql.org/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Redis](https://redis.io/)
- [Ocelot API Gateway](https://ocelot.readthedocs.io/)
- [Polly (Resilience & Fault Handling)](https://github.com/App-vNext/Polly)
- [Docker](https://www.docker.com/)
