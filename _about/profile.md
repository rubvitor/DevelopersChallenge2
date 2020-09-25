Meu perfil
-------

**Nome completo:**   
Rubens Vitor de Barros Almeida

**Data de nascimento:** 
31/05/1985

**LinkedIn:**    
https://www.linkedin.com/in/rubens-vitor/

**Como nos conheceu:** 
Através do recrutamento no LinkedIn


Observações sobre a aplicação:

- O projeto back-end foi feito em .net core e para rodar é necessário utilizar Visua Studio 2019. É necessário ter a versão 3.1 SDK do .net Core instalado
na máquina. Link abaixo:

https://dotnet.microsoft.com/download/dotnet-core/3.1

O projeto backend está rodando com a versão do EntityCore em memória RAM. Ou seja, as informações de OFX que forem importados somente ficarão "gravadas" 
enquanto a API (src/src/FinantialManager.Services.Api) estiver rodando. Toda vez que for realizado stop da API os dados serão perdidos.


- O projeto front foi feito em Angular 9. Necessário ter o node.js instalado e alguma IDE como Visual Studio Code. 

Link do node.js: https://nodejs.org/en/download/


O front está na pasta src/src/FinantialManager.UI.Web.

As configurações do Front estão no arquivo enviroment caso seja necessário mudar a porta de comunicação do front com a api.
Enviroment path: \src\src\FinantialManager.UI.Web\src\environments

Quando abrir a pasta Finantoal.UI.Web em sua IDE rodar o comando "npm install" e aguardar a instalação dos pacotes. Após a instalação rodar o comando "npm start".

--> Email e senha padrão para acesso ao front e back
--> Email: xayah@esports.com
--> Password: 123456