# Clean Architecture

| Projeto                 | tipo                 | Responsabilidade Principal                                                                                            |
|                    :--- |                 :--- |                                                                                                                  :--- |
| **app.Domain**          |  **classlib**        | Modelo de domínios, interfaces, regras e negócio. **O núcleo da aplicação.**                                          |
| **app.Application**     |  **classlib**        | Regras da aplicação, serviços de aplicação, mapeamentos, DTOs (Data Transfer Objects).                                |
| **app.Infrastructure**  |  **classlib**        | Lógica de Acesso a Dados, Contexto de Banco de Dados, Configurações de persistência, ORM (Object-Relational Mapping). |
| **app.CrossCutting**    |  **classlib**        | Inversão de Controle (IoC), Registro dos serviços e recursos, Injeção de Dependência (DI).                            |
| **app.API**             |  **webapi**          | Controladores (Controllers), Endpoints, Filtros. **A camada de apresentação externa.**                                |
