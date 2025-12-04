**Fazenda Urbana – Sistema de Gerenciamento**

Sistema web desenvolvido em C# + ASP.NET Core MVC para cadastro e gerenciamento de produtos de uma fazenda urbana.
O objetivo é permitir o controle de itens, categorias, clientes, forneceodores, vendas e estoque, usando boas práticas de programação e arquitetura.

**Tecnologias Utilizadas**

C#
ASP.NET Core MVC
Entity Framework Core
SQL Server
HTML, CSS e Razor Pages

**Funcionalidades**

✔ Cadastro de clientes
✔ Listagem de clientes
✔ Cadastro de fornecedores
✔ Listagem de fornecedores
✔ Cadastro de produtos
✔ Listagem de produtos
✔ Realização de vendas com comprovante
✔ Listagem de vendas
✔ Edição e exclusão
✔ Controle de categorias
✔ Pesquisa e filtros
✔ Estrutura organizada com Repositórios
✔ Separação de camadas (MVC)

**Arquitetura do Projeto**

O sistema utiliza a arquitetura MVC (Model–View–Controller):

/Models      → Classes de domínio
/Views       → Páginas da interface (Razor)
/Controllers → Lógica de controle e rotas
/Repositories → Acesso ao banco via EF Core

**Banco de Dados**

SQL Server

Mapeamento via Entity Framework Core

**Como Rodar o Projeto**

Clone este repositório:

git clone https://github.com/matheusdomepass/FazendaUrbana

Abra o projeto no Visual Studio.

Configure a connection string no arquivo appsettings.json.

Execute as migrações (se aplicável):

update-database

Rode o projeto:

dotnet run

Desenvolvido por: 
**Matheus Passatuto**

**Contato:**
GitHub: https://github.com/matheusdomepass
Contato: matheus.pass@hotmail.com

Sugestões e melhorias são bem-vindas!
Fique à vontade para abrir issues ou enviar pull requests.
