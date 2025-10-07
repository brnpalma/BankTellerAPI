# BankTellerAPI

# 💰 Sistema Caixa de Banco

Aplicação desenvolvida para testes pessoais, com o objetivo de criar uma API para gerenciamento de contas bancárias e realização de transferências entre clientes.

---

## Acesse o endpoint local (exemplo)
https://localhost:5001/swagger

---

## 🧩 Funcionalidades

### 1. Cadastro de Contas Bancárias
Permite o registro de novas contas para clientes.  
**Regras:**
- Nome e documento são obrigatórios;  
- Não é permitido mais de uma conta por documento;  
- Cada conta inicia com **saldo de R$1000** como bonificação;  

---

### 2. Consulta de Contas
Permite listar contas cadastradas com opção de filtro por **nome** (parcial ou completo) ou **documento**.  
**Retorno:**
- Nome do cliente  
- Documento  
- Saldo atual  
- Data de abertura  
- Status da conta (ativa ou inativa)

---

### 3. Inativação de Conta
Permite inativar contas com base no documento do titular.  
**Regras:**
- O documento é obrigatório;  
- Só é possível inativar contas **ativas**;  

---

### 4. Transferência entre Contas
Permite transferir valores entre contas ativas.  
**Regras:**
- Ambas as contas devem estar **ativas**;  
- A conta de origem deve ter **saldo suficiente**;  

---

## 👨‍💻 Autor
Desenvolvido para fins de viabilidade técnica.  
Sinta-se à vontade para explorar, rodar e avaliar a arquitetura, organização e boas práticas aplicadas.
