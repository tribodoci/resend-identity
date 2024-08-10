# Resend: Utilizando com ASP.NET Core Identity

## Criando o projeto
> verificando a versao do .net
```shell
dotnet --info
```
Nesse exemplo estou com a versão 8.0.1, provavelmente vai funcionar em qualquer versão 8.*.*

> Criando a solução, utilizando o parametro ```-n``` para especificar o nome, caso contrário a solução terá o mesmo nome da pasta, no exemplo estamos dentro da pasta ```src```.
```shell
dotnet new sln -n ResendIdentity
```

> Agora o projeto, vamos utilizar o template blazor com o Identity

```shell
dotnet new blazor -n ResendIdentity.WebApp --auth Individual
```
> Projeto criado é só adiciona-lo a Solução, pode ser especificado a pasta do projeto ou o ```.csproj```, no exemplo que segue especificamos apenas o nome da pasta.
```shell
dotnet sln add ResendIdentity.WebApp
```
## Cuidados ao utilizar o ```Polly```
...
## Blog - Post
- https://tribodoci.net/
