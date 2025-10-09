# 💰 BankTellerAPI

Aplicação API experimental voltada ao gerenciamento de contas bancárias e à realização de transferências entre contas de forma simples e funcional.

## 📎 A documentação da API pode ser acessada localmente em:
https://localhost:7090/docs

## 🧩 Funcionalidades

### 1. Cadastro de Contas Bancárias
Permite o registro de novas contas para clientes.  
**Regras:**
- Nome e documento são obrigatórios;  
- Não é permitido mais de uma conta por documento;  
- Cada conta inicia com **saldo de R$1000** como bonificação;  

### 2. Consulta de Contas
Permite listar contas cadastradas com opção de filtro por **nome** (parcial ou completo) ou **documento**.  
**Retorno:**
- Nome do cliente  
- Documento  
- Saldo atual  
- Data de abertura  
- Status da conta (ativa ou inativa)

### 3. Inativação de Conta
Permite inativar contas com base no documento do titular.  
**Regras:**
- O documento é obrigatório;  
- Só é possível inativar contas **ativas**;  

### 4. Transferência entre Contas
Permite transferir valores entre contas ativas.  
**Regras:**
- Ambas as contas devem estar **ativas**;  
- A conta de origem deve ter **saldo suficiente**;  

## 🔐 Segurança

A segurança do sistema é garantida pela **restrição física de acesso à máquina** onde a aplicação será executada.  
Não há necessidade de autenticação ou autorização adicionais, pois o ambiente é controlado fisicamente.  
Por isso, **não implemente mecanismos de login, tokens ou controle de acesso** — o sistema é seguro por design local.

## 🛠️ Setup automático

Ao executar a aplicação (`dotnet run`), o banco de dados e suas tabelas são **criados automaticamente** via Entity Framework Core.  
Não é necessário rodar comandos manuais como `dotnet ef database update` — as migrações são aplicadas na inicialização.  
Facilitando o processo de clonar o projeto e rodar a API sem esforço adicional.
Sendo assim, ao baixar o projeto:
  1 - Abra a Solution:
  <img width="257" height="459" alt="image" src="https://github.com/user-attachments/assets/0c576e6c-d321-4433-b78c-5fbf65d2e4e6" />

  2 - Selecione como padrão de execução, o projeto BankTeller.Api
  <img width="288" height="46" alt="image" src="https://github.com/user-attachments/assets/bc2517f2-488f-477d-ad4d-737fb611934f" />
  
  3 - Pronto, a aplicação automaticamente criará o banco de dados BankTellerDb e as tabelas Contas, InativaLogs e Transferências em sua instancia local do SQL Server

## 👨‍💻 Autor

Desenvolvido para fins de viabilidade técnica.  
Sinta-se à vontade para explorar, rodar e avaliar a arquitetura, organização e boas práticas aplicadas.
