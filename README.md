# Cursos Online 

Esta é uma api exemplo criada para consolidar os estudos  e conceitos aprendidos, bem como servir de exemplo e referencias para implementações futras.

### Pré-requisitos

```
Windows, Linux ou Mac
Vscode
Git
```

### Instalando

Execute os comandos abaixo:

```bash
dotnet restore
dotnet ef database update -v
```

## Built With

* [.Net5](https://dotnet.microsoft.com/download/dotnet/5.0)
* [EntityframeworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)
* [SqlServer](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
* [KissLog](https://kisslog.net/)
* [FluentValidation](https://fluentvalidation.net/)
* [Automapper](https://automapper.org/)
* [JWT](https://jwt.io/)


## Autor

* **Leandro Alves** -  [Github](https://github.com/leandrodasilvaalves)


# Demandas
- [x] Incluir JWT
- [x] Refatorar servico JWT
- [x] Padronizar responses
  - [x] OkReponses
  - [x] BadRequestResponses
  - [x] NotFoundResonses
- [x] Incluir validações
- [x] Refatorar construtores Responses
- [x] Traduzir erros de modelstate
- [x] Extrair Identity para servico
- [x] Fechar endpoints protegidos
- [x] Utilizar claims nos endpoints 
- [x] Versionar API
- [x] Melhorar documentação via Swagger
- [x] Incluir logs e monitoramento (KissLog)
- [x] [Bug] KissLog logando senhas
- [x] Validar logs nas demais controllers
- [x] Manter propriedade token apenas para response de autenticação
- [x] Exception Middleware
- [x] Healthcheck
- [x] CORs Police
- [] Upload de imagem
  - [x] Cadastro de Aluno com imagem
  - [x] Extrair upload para servico
  - [x] Atualizacao do aluno com imagem
  - [x] Remover imagem em caso de erro
  - [x] [Bug] As imagems com url estão dando 404
  - [] Upload com base64
- [x] AutoMapper
  - [x] Mapear imagem aluno com url completa
  - [x] Normalizar cadastro de aluno v1
- [x] Endpoint para troca de senha
- [x] Endpoints para gerenciar claims
  - [x] Listar usário com suas claims
  - [x] Incluir claims para um usuário
  - [x] Atualizar claims de um usuário
  - [x] Remover claims de um usuário