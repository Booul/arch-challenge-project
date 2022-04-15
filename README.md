# Arch Challenge Project

[Projeto realizado visando vaga de emprego na empresa Arch (11/04/2022).](https://drive.google.com/file/d/1Y05B9aR6DwUMw7TIUhPMSXonfKyHq-Do/view)

## Instalação

Use o [Docker](https://www.docker.com/) para resolver tudo rapidamente.

```bash
docker-compose up -d --build
```

Mapeamento de portas:
- transaction-api:  5000
- balance-api:      5001
- redis:            [Não exposta]
- redis-commander:   8082
- mongodb:          [Não exposta]
- mongo-express:     8081

É possível fazer a instalação sem o Docker; nesse caso, garanta ter instalado:
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [Redis](https://redis.io/download/)

Com o MongoDB e o Redis iniciados e configurados corretamente (separadamente em cada API):
```bash
dotnet restore
dotnet watch run (desenvolvimento)
```

Para compilar:
```bash
dotnet publish -c Release -o out
```

Por fim, verifique no console em quais portas as APIs foram iniciadas.

## Para o futuro

- Colocar as APIs em uma solução para reduzir repetição de código.
- Tornar métodos assíncronos para melhor adaptabilidade.
- Implementação de testes unitários.
- Implementação de testes de implementação.
- Melhorias no docker-compose visando versão de produção estável.
- Deploy na Azure.

## Licença
[MIT](https://choosealicense.com/licenses/mit/)
