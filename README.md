# 🧩 Microsserviços com .NET 8, Docker, RabbitMQ e MySQL

Este repositório implementa a comunicação entre microsserviços desenvolvidos em **C# com .NET 8**, utilizando **RabbitMQ** para mensageria, **MySQL** como banco de dados relacional e **Docker Compose** para orquestrar os serviços em contêineres.

## Objetivo

Demonstrar, de forma prática, uma arquitetura baseada em microsserviços com foco em:

- Comunicação assíncrona entre serviços com RabbitMQ
- Isolamento de serviços em contêineres com Docker
- Escalabilidade e organização modular de sistemas distribuídos

---

## 🛠️ Tecnologias Utilizadas

- **.NET 8**
- **Docker**
- **Docker Compose**
- **RabbitMQ**
- **MySQL**
- **REST APIs**
- **C#**
- **Entity Framework Core**

---

## ▶️ Como Executar o Projeto

### Pré-requisitos

- Docker

### Passos

1. Clone o repositório:

```bash
git clone https://github.com/gustavoszo/microservices-.net.git
cd repositorio
```

2. Execute os contêineres:

```bash
docker-compose up --build
```

3. Os serviços estarão disponíveis em:
- http://localhost:8080/api/restaurant - API do microsserviço de restaurante
- http://localhost:8081/api/item - API do microsserviço de itens
- http://localhost:15672 – Painel do RabbitMQ (usuário: user / senha: password)
- localhost:3307 – Banco de dados do microsserviço de restaurante
- localhost:3308 – Banco de dados do microsserviço de itens
