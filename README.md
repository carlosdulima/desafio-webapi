## Serviços de Taxa de Juros

Serviços:
 - **TaxaJuros**: Responsável pela Taxa de Juros.
 - **CalculaJuros**: Responsável pelo Cálculo de Juros aplicando a Taxa de Juros consumida pelo serviço de TaxaJuros.
 
Ambos serviços possuem:
- Clean Architecture / Hexagonal
- Princípios SOLID
- REST API com Swagger e Versionamento de API
- Docker Compose
- Testes unitários com xUnit, AutoBogus, Moq, e FluentAssertions

### Como rodar o projeto localmente

Clonar o projeot para sua máquina e executar o projeto principal "docker-compose", ele vai rodar com o CalculaJuros abrindo no navegador, basta inputar os valores e o cálculo será realizado.
