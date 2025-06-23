
# 🚀 .NET Microservices Project – Based on Les Jackson's Full Course

This project is a practical implementation of a microservices architecture using .NET, inspired by [Les Jackson’s full course on YouTube](https://www.youtube.com/watch?v=DgVjEo3OGBI&list=PLpXfHEl2fzl7a7p4ntTmdjmNSD1iEYXrm). It covers service design, inter-service communication, containerization, and orchestration using Kubernetes.

---

## 🧠 Architecture Overview

### 🧩 Solution Architecture

This diagram shows the high-level components, inter-service communication patterns, and message flow using REST, gRPC, and RabbitMQ.

![Solution Architecture](images/solution%20arc.jpg)

### ☸️ Kubernetes Deployment Architecture

This diagram illustrates how the microservices are deployed in a Kubernetes cluster with an API Gateway and RabbitMQ as the message broker.

![Kubernetes Architecture](images/k8s%20arc.jpg)

---

## 📦 Features

- ✅ Two .NET Microservices with REST APIs
- ✅ Dedicated PostgreSQL databases for each service
- ✅ API Gateway (Ocelot) for routing
- ✅ Synchronous service communication via HTTP and gRPC
- ✅ Asynchronous messaging using RabbitMQ
- ✅ Docker-based local development
- ✅ Kubernetes manifests for cloud-native deployment

---

## 🔧 Tech Stack

| Layer         | Tool/Framework     |
|---------------|--------------------|
| Backend       | .NET 7             |
| API Gateway   | Ocelot             |
| Messaging     | RabbitMQ           |
| Sync Comm     | HTTP / gRPC        |
| Persistence   | PostgreSQL         |
| Container     | Docker             |
| Orchestration | Kubernetes (YAML)  |

---

## 🧪 Getting Started

### Clone the repo:
```bash
git clone https://github.com/40gilad/dotnet_microservices_course.git
cd dotnet_microservices_course
```

### Run with Docker:
```bash
docker-compose up --build
```

---

## 🙌 Acknowledgements

Thanks to [Les Jackson](https://www.youtube.com/@binarythistle) for the comprehensive walkthrough on building microservices with .NET. This project is based directly on his instructional video and serves as both a learning tool and a scalable foundation.

---
