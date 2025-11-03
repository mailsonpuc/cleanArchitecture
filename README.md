# Clean Architecture

| Projeto | Responsabilidade Principal |
| :--- | :--- |
| **app.Domain** | Modelo de domínios, interfaces, regras e negócio. **O núcleo da aplicação.** |
| **app.Application** | Regras da aplicação, serviços de aplicação, mapeamentos, DTOs (Data Transfer Objects). |
| **app.Infrastructure** | Lógica de Acesso a Dados, Contexto de Banco de Dados, Configurações de persistência, ORM (Object-Relational Mapping). |
| **app.CrossCutting** | Inversão de Controle (IoC), Registro dos serviços e recursos, Injeção de Dependência (DI). |
| **app.API** | Controladores (Controllers), Endpoints, Filtros. **A camada de apresentação externa.** |
