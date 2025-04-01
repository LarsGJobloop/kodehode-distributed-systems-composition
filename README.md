# Distributed Systems Compositions

This repo explores various deployment patterns in distributed systems using Docker Compose. This isn't meant to be exhaustive, but it offers a glimpse into how system design evolves—and how Compose can help simulate and test these setups locally.

This repository contains a minimal example of a Compose application consisting of:
- Frontend (HTML, CSS, JS)
- Single service (.NET)
- Database + Web UI (PostgreSQL + pgAdmin)
- Reverse Proxy (Traefik)

## Prerequisites

- Docker Desktop (Windows)
- OrbStack (macOS/Windows)

## Commands

- **Startup services**
    ```sh
    docker compose up
    ```

- **Rebuild services**
    ```sh
    docker compose up --build
    ```

- **Cleanup everything**
    ```sh
    docker compose down --volumes --remove-orphans
    ```

## System Architecture Sketches

![Sketch](/docs/distributed-systems-patterns.png)

## Deployment Patterns

A selection of commonly used patterns in system architecture, each with their own trade-offs and degrees of trendiness.

### Monolith

Everything runs in a single process or machine—including the database. The frontend, if any, is typically static HTML or very minimal JavaScript. Data may be persisted in a file, in-memory, or not at all. This can be a *large ball of yarn* with no architectural boundaries, or a *modular monolith* resembling the more structured patterns below. Services communicate via low-latency IPC, shared memory, or direct function calls.

### Server <-> Database

The first major split—moving the database to its own process or server. This allows better resource control and backup policies. The frontend is still simple; most logic lives on the server.

### Client <-> Server <-> Database

The classic “web” architecture. The client (browser) communicates with a backend server, which persists data in a database. The client now includes more logic and state, to reduce latency or offload compute from the server.

### Service-Oriented Architecture (SoA)

The backend is decomposed into services with well-defined responsibilities. Common boundaries:
- User Service
- Order Service
- Payment Service
- Notification Service

Each can evolve and scale independently.

### Microservices / Nanoservices

A refinement of SoA. Services are smaller—sometimes representing a narrow slice of functionality or shared components used across other services. Popular in environments where requirements are changing rapidly and runtime redeployability is critical. Often used to reduce full-system downtime.

### Serverless / Functions

The smallest unit: individual functions deployed independently. Requires significant infrastructure and orchestration, usually handled by cloud providers. Often driven by a desire to minimize runtime overhead (especially with heavyweight languages like Java or C#), or for capabilities that are used infrequently or on demand.

### Message Bus

A system design where components communicate through a shared messaging layer using agreed-upon protocols (e.g., Pub/Sub). Can run locally (language-specific), over OS channels, or via the network (e.g., TCP/IP). Producers and consumers are decoupled—making it easy to add, remove, or scale services.

Benefits:
- Temporal decoupling
- Loose coupling between components
- Easier extensibility
- Needs careful message schema and routing design

## Related Solutions

### Reverse proxy

When deploying multiple services across different machines it can be difficult for the frontend or downstream service to keep track of all the domains/IP addresses. A revers proxy is simply a separate service put in front of other services to aggregate these under a single domain/IP. Can be API services or UI service.

### Gateway

A gateway extends the reverse proxy concept by handling cross-cutting concerns like authentication and authorization, rate limiting, basic message validation, and TLS termination. The scope varies by implementation.

### Ingress/Egress Gateway

A variation of gateways that handle both inbound and outbound traffic. Often used when centralized access control is needed.

### Caches

Caches are specialized servers that store responses to previously received requests and return them immediately—bypassing backend recomputation. Especially useful when many users request the same resource. Less useful for highly dynamic content.

### Content Delivery Network (CDN)

A set of servers for providing assets quicker to clients. Usually used for content that seldom changes or follows set release schedules, and especially if the content is large like the case is with video files. Typically provided by third parties with regional coverage. You don't set it up yourself—you subscribe to a CDN service.

## Notes

While the decomposition of applications into smaller and smaller pieces have led to increased complexity in their composition along with latency increases. Despite increased complexity and potential latency overhead, these architectures have been widely adopted—largely due to their scalability and modularity benefits. One of the major outcomes of this has been the ability to outsource parts of your application to specialized vendors rather than relying on inhouse expertise in all fields. There is also undoubtedly a trend factor here as many developers enjoy playing with the latest gadgets they can get their hands on.
