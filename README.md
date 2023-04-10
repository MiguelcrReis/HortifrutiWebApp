<h1 align="center"> Hortifruti Reis Web App </h1>

<p align="center">
<img src="https://user-images.githubusercontent.com/69518446/227387031-99a2e992-ea74-4254-9f53-d9155a3c3d0a.png"/>
</p>

<p align="center">
<img src="http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge"/>
</p>

## Em aberto

<p align="justify">
A aplicação apresenta problemas com relação ao uso de Cookies no carrinho de compras. Mesmo após finalizar o pedido, o Cookie que armazena o id e as informações do carrinho não está sendo deletado com sucesso, resultando em problemas ao abrir o carrinho em futuras compras. 
Provavelmente este problema esteja sendo influenciando por conta da versão não tão recente do .NET Core.
</p>


## Descrição do projeto 

<p align="justify">
Projeto de aplicação web de um sistema para uma Hortifruti em ASP.NET CORE (versão 3.1) no padrão MVC (Model-View-Controller) 
com Entity Framework Core (versão 3.1.32), utilizando a IDE Visual Studio 2019 (versão 16.11.22) conectado a um banco de dados MySQL (versão 8.0).
Utilizando também os pacotes SendGrid (versão 9.28.1), Image Sharp (versão 2.1.3), MailKit (versão 3.4.3), Text Sharp Core (versão 3.1.0).
</p>

## Funcionalidades

<p align="justify">
:heavy_check_mark: `Funcionalidade 1:` CRUD de Clientes;

:heavy_check_mark: `Funcionalidade 2:` CRUD de Produtos;

:heavy_check_mark: `Funcionalidade 3:` Adicionar produtos ao carrinho de compras;

:heavy_check_mark: `Funcionalidade 4:` Revisar os itens no carrinho;

:heavy_check_mark: `Funcionalidade 5:` Finalizar a compra dos itens no carrinho;

:heavy_check_mark: `Funcionalidade 6:` Envio de E-mail para confirmação de compra;

:heavy_check_mark: `Funcionalidade 7:` Envio de E-mail para confirmação de cadastro de cliente;

:heavy_check_mark: `Funcionalidade 8:` Envio de E-mail para recuperação de senha de cliente;

:heavy_check_mark: `Funcionalidade 9:` Gerenciamento de pedidos realizados, com usuário adm;

:heavy_check_mark: `Funcionalidade 10:` Gerenciamento de clientes, com usuário adm.
</p>

![Hortifruti Reis __ Home page - Google Chrome 2023-03-23 21-35-58](https://user-images.githubusercontent.com/69518446/227396117-ef7ea258-caaa-475e-b68d-6c16517e3c87.gif)

## 📁 Acesso ao Projeto
[Source code](https://github.com/MiguelcrReis/HortifrutiWebApp)

[Download ZIP](https://github.com/MiguelcrReis/HortifrutiWebApp/archive/refs/heads/master.zip)


## 🛠️ Abra e execute o projeto

Deve possuir instaldo [ .NET Core 3.1 SDK ](https://dotnet.microsoft.com/en-us/download/dotnet/3.1)

Deve possuir instaldo [ Servidor MySQL 8.0 ](https://dev.mysql.com/downloads/windows/installer/8.0.html)

Configure a string de conexão com o banco de dados no arquivo appsettings.json, alterando `username`, `password`, e `database` por seus próprios parâmetros:

```cs
"ConnectionStrings": {
  "DefaultConnection":"server=localhost;userid=myusername;password=mypassword;database=mydatabase;"
},
```

Também no arquivo appsettings.json, configure a conexão com o seu servidor de E-mail, alterando com seus dados:

```cs
  "EmailConfiguration": {
    "SenderName": "HortiFruti Reis",
    "SenderEmail": "HortiFrutiReis@gmail.com",
    "Password": "hortifrutireis",
    "ServerAddressEmail": "smtp.gmail.com",
    "ServerPortEmail": 587,
    "UserSSL": true
  }
```

Execute a instalação e a migração do provedor Mysql usando o Visual Studio Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console):

```
Install-Package Pomelo.EntityFrameworkCore.MySql -Version 2.1.1
```
```
Update-Database
```

Depure a Solução.

