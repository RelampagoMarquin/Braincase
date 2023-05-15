# Guia para iniciar o projeto

# Instalando as dependências
```bash
	dotnet restore # Força a instalação das dependecias
```

Após isso você deve conferir a variavel de ambiente do banco, você vai encontrar no appsettings.json
em seguuida procure pela variavel de nome 'ConnectionMysql' nela terá uma string com o as opções do
banco, a string solicita a url, porta(opcional), nome do banco, usuario e senha, segue o exemplo
"Server=url;port=porta;initial catalog=nomebanco;uid=usuaio;pwd=senha"

Os comandos a seguir não é utilizado exatamente no terminal, para quem tiver no visual studio community
procure pela guia ferramenta depois procure por 'gerenciar pacote nuGet' > 'Console do gerenciador de pacote'
# Executando a migration e o banco de dados
```bash
	add-migration namemigration # Cria uma nova migration
	remove-migration # Caso a migração tenha saido errada
	Get-Migration # Lista as migrações
	update-database # Atualiza o banco de dados
```

caso esteja utilizando o vsCode você precisará fazer os seguintes passos
```bash
	dotnet tool install --global dotnet-ef # installa a dotnet-ef para executar globalmente
	dotnet ef migrations add yourMigrationName # Cria uma nova migration
	dotnet ef migrations remove # Caso a migração tenha saido errada
	dotnet ef migrations list # lista as migrações
	dotnet ef database update # Atualiza o banco de dados
```
Aqui já pode utilizar o terminal padrão
# Iniciando o projeto
	
	Caso esteja utilizando o visual studio community optite por executar pela interface

```bash
	dotnet run
```

# Acessar api

Após executar entrar nesse link, https://localhost:7011/swagger/index.html, 
caso não consiga verifique se seu navegador não esta bloqueando

# Gerar Controlers

vs code

```bash
dotnet tool install -g dotnet-aspnet-codegenerator # instala o gerador de codigo
dotnet-aspnet-codegenerator controller -name nomedomodeloController -async -api -m nomemodelo -dc APIDbContext -outDir Controllers
```

# visual studio

Dê 2 cliques na solução 'api.sln' após isso clique com o botão direito controllers adicionar>novo item com scaffold em seguida vai abrir
uma janela, clique em API no menu lateral e em seguida selecione a opção 'Controlador API com ações, usando Entity Framework', Avance
escolha o modelo e em seguida o context e nomei o controlador.