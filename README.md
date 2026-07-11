# Clean Architecture

| Projeto                 | tipo                 | Responsabilidade Principal                                                                                            |
|                    :--- |                 :--- |                                                                                                                  :--- |
| **app.Domain**          |  **classlib**        | Modelo de domínios, interfaces, regras e negócio. **O núcleo da aplicação.**                                          |
| **app.Application**     |  **classlib**        | Regras da aplicação, serviços de aplicação, mapeamentos, DTOs (Data Transfer Objects).                                |
| **app.Infrastructure**  |  **classlib**        | Lógica de Acesso a Dados, Contexto de Banco de Dados, Configurações de persistência, ORM (Object-Relational Mapping). |
| **app.CrossCutting**    |  **classlib**        | Inversão de Controle (IoC), Registro dos serviços e recursos, Injeção de Dependência (DI).                            |
| **app.API**             |  **webapi**          | Controladores (Controllers), Endpoints, Filtros. **A camada de apresentação externa.**                                |

## Projeto `Shoop.Domain`

- **Entities**: Representam os modelos de domínio.
- **Interfaces**: Definem contratos para as regras de negócio.

## Projeto `Shoop.Application`

- **DTOs (Data Transfer Objects)**: Objetos que representam dados a serem transferidos entre camadas.
- **Interfaces**: Contratos para os serviços de aplicação.
- **Mappings**: Mapeamentos de objetos para DTOs e vice-versa.
- **Services**: Implementações dos contratos definidos nas interfaces.

## Projeto `Shoop.Infrastructure`

- **Context**: Contexto do banco de dados.
- **EntitiesConfiguration**: Configurações das entidades no contexto de banco de dados.
- **Repositories**: Implementações das interfaces de repositório para acessar dados do banco de dados.

## Projeto `Shoop.CrossCutting`

- **IoC (Inversão de Controle)**: Configuração e registro dos serviços para injeção de dependência.
