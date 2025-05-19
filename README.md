# api-sw

Conteudo entregue no projeto

# 🚀 API C# com SQLite

🛠️ Tecnologias e Linguagens
<div style="display: flex; gap: 10px; align-items: center;">
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="NET" /> <img src="https://img.shields.io/badge/C%23-239120?style=for-for-the-badge&logo=c-sharp&logoColor=white" alt="c" /> <img src="https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white" alt="sqlite" /> 


## 📦 Pré-requisitos

- [.NET 8+](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

## 🛠️ Configuração

1. Clone o repositório:

   git clone https://github.com/rafaelfriske/api-sw.git

2. Restaure as dependências:

   dotnet restore

## 🏃 Executando a API

   dotnet run

## 📚 Endpoints

### Controller TbTarefas

#### GET:
1. /api/tarefas
  lista todas as tarefas que não foram removidas

2. /api/tarefas/id

    Lista uma tarefa especifica

#### PUT:

1. /api/tarefas/alterarStatus/id

    Altera o status da tarefa

2. /api/tarefas/removerTarefa/id

    Para remover uma tarefa (no caso ele altera o campo TarefaRemovida para 1)

#### POST

1. /api/tarefas

    Adicionar uma tarefa


Link para download: https://sqlitebrowser.org/

## ⚠️ Regras de negócio do Projeto:

1. O status inicial da tarefa deve ser pendente
2. O status devem ser concluido ou pendente
3. Ao remover uma tarefa, ela continuara no banco. Porém o campo TarefaRemovida irá ser alterado para 1. 
4. Todas alterações de status alterará o campo DataAlteracao

