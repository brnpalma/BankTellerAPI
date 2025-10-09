# 💰 BankTellerAPI

Aplicação API experimental voltada ao gerenciamento de contas bancárias e à realização de transferências entre contas de forma simples e funcional.

---

## 🚨 Atenção! 
**Este projeto foi desenvolvido com _`.NET 10`_ e requer o uso do _`Visual Studio 2026 Insiders Preview`_ para funcionar corretamente.**

### 🔧 Como configurar o ambiente:

1. Instale o SDK do [.NET 10 Preview](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).
2. Baixe e instale o [Visual Studio 2026 Insiders](https://visualstudio.microsoft.com/vs/preview/).
3. O uso de SDKs em preview é habilitado por padrão, mas você pode verificar manualmente com os passos abaixo:
   - Acesse **Tools → Options → Environment → Preview Features**
   - Marque **“Use previews of the .NET SDK”**
   - Caso tenha sido necessário habilitar a opção citada acima, reinicie o Visual Studio

---

## 🛠️ Setup automático

Ao executar a aplicação (`dotnet run`), o banco de dados e suas tabelas são **criados automaticamente** via Entity Framework Core. Não é necessário rodar comandos manuais como `dotnet ef database update` — as migrações são aplicadas na inicialização, facilitando o processo de clonagem e execução do projeto sem esforço adicional.

Sendo assim, ao baixar o projeto:

1. Abra a Solution:
<img width="257" height="459" alt="image" src="https://github.com/user-attachments/assets/0c576e6c-d321-4433-b78c-5fbf65d2e4e6" />

2. Selecione como padrão de execução o projeto `BankTeller.Api` e execute `https`:
<img width="288" height="46" alt="image" src="https://github.com/user-attachments/assets/bc2517f2-488f-477d-ad4d-737fb611934f" />

3. Pronto! A aplicação criará automaticamente o banco de dados `BankTellerDb` e as tabelas `Contas`, `LogsInativacao` e `Transacoes` em sua instância local do SQL Server:
<img width="276" height="277" alt="image" src="https://github.com/user-attachments/assets/b306c6b9-485c-4468-9435-57d2b83c11eb" />


---

## 📎 A documentação da API pode ser acessada localmente em:
https://localhost:7090/docs

---

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

---

## 🔐 Segurança

A segurança do sistema é garantida pela **restrição física de acesso à máquina** onde a aplicação é executada.  
Não há necessidade de autenticação ou autorização adicionais, pois o ambiente é controlado fisicamente.  
Por isso, **não implemente mecanismos de login, tokens ou controle de acesso** — o sistema é seguro por design local.

---

## 👨‍💻 Autor

Desenvolvido para fins experimentais e de viabilidade técnica.  
Sinta-se à vontade para explorar, rodar e avaliar a arquitetura, organização e boas práticas aplicadas.
