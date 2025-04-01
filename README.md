# Distributed Systems Compositions

This repo presents a minimal example of a lightweight Service-Oriented Architecture (SoA) using Docker Compose, designed to demonstrate how real-world distributed systems can be simulated and tested locally.

While the Compose setup itself is simple, the surrounding documentation explores a range of architectural patterns—showing how this kind of setup fits into the broader evolution of distributed system design. The goal is not to be exhaustive, but to build intuition for how systems can be composed, decomposed, and adapted over time.

The Compose application includes:
- A static frontend (HTML, CSS, JS)
- A single backend service (.NET)
- A PostgreSQL database with pgAdmin
- A reverse proxy (Traefik)

## Prerequisites

- Docker Desktop (Windows)
- OrbStack (macOS/Windows)

## Useful Commands

- **Start everything**
    ```sh
    docker compose up
    ```

- **Rebuild and restart**
    ```sh
    docker compose up --build
    ```

- **Shut down and clean up**
    ```sh
    docker compose down --volumes --remove-orphans
    ```

## Architecture Diagrams

![Sketch](/docs/distributed-systems-patterns.png)

## Deployment Patterns

Below is a progression of common architectural patterns, each with different trade-offs in complexity, scalability, and operational overhead.

### Monolith

All logic (including the database) runs in a single process or on a single machine. The frontend is often static or server-rendered. Communication is internal—via shared memory, function calls, or IPC. Easy to develop, but hard to scale or maintain as complexity grows.

### Server ↔ Database

The backend and database are split into separate services. This introduces better operational control over the database layer (backups, scaling, etc.) while keeping most logic server-side.

### Client ↔ Server ↔ Database

The standard web model. The browser handles more logic (UI, state, sometimes even caching), while the backend processes business logic and persists data. Frontend and backend can scale independently.

### Service-Oriented Architecture (SoA)

The backend is broken into services—each responsible for a specific domain (e.g., User, Orders, Payments). These services are typically deployed independently and communicate via API calls or messaging layers.

### Microservices / Nanoservices

An evolution of SoA, emphasizing smaller and more focused services. Each service may represent a single capability and run independently. This allows frequent, isolated deployments and fault containment—but at the cost of more complex infrastructure and monitoring.

### Serverless / Functions

Here, individual functions are deployed independently (e.g., AWS Lambda). Great for workloads that are infrequent, lightweight, or bursty. Ideal when you want to avoid managing servers, but comes with platform constraints and cold start issues.

### Message Bus

In message-driven architectures, components communicate via a shared messaging layer (e.g., Pub/Sub systems like Kafka, RabbitMQ). This enables loose coupling between services, temporal decoupling, and scalability—but requires thoughtful design around message formats and routing.

**Benefits:**
- Loose coupling
- Easier scaling
- Improved fault tolerance
- More complex debugging and monitoring

## Supporting Infrastructure

### Reverse Proxy

Acts as a single point of entry for routing requests to various backend services. This simplifies service discovery and helps consolidate traffic behind one domain.

### Gateway

Goes beyond basic proxying by handling cross-cutting concerns like auth, rate limiting, TLS termination, and validation. Gateways often act as a programmable control layer over multiple services.

### Ingress/Egress Gateway

Specialized gateways managing inbound (ingress) and outbound (egress) traffic. Common in service mesh setups to enforce network policies and access controls.

### Caches

Caching servers (like Redis or Varnish) store responses to common requests. They reduce latency and offload repeated computation but are less effective for highly dynamic or user-specific content.

### Content Delivery Network (CDN)

A globally distributed set of servers optimized for delivering static or large content (e.g., images, videos). Used to serve assets closer to the user. CDNs are typically managed services you subscribe to, not run yourself.

## Final Notes

Decomposing systems into smaller, independent components introduces complexity—but also unlocks a critical advantage: the ability to evolve. 

Most systems today are developed in uncertain, fast-moving environments. Requirements shift, new use cases emerge, and teams need to experiment without committing to rigid structures. A modular architecture—whether based on services, functions, or message-driven components—lets you change direction more easily. You can refactor incrementally, roll out updates gradually, or even swap out whole components without rewriting the entire stack.

This flexibility often outweighs raw performance concerns. While distributed architectures do come with latency overhead and operational complexity, they’re increasingly favored because they support rapid iteration, organizational scaling, and long-term adaptability. 

It’s not just about handling more users—it’s about handling more *change*.

And yes, part of the appeal is undeniably cultural: developers enjoy exploring new tools, experimenting with architectures, and building systems that are not just scalable, but resilient to the future.
