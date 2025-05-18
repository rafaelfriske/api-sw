# api-sw

Conteudo entregue no projeto

Banco de Dados

Criei 2 scrips para criação do banco de dados juntamente com as tabelas necessárias e dados iniciais.
após executas esses scripts atenção na string de conexão do banco no projeto,
pois está com a string de conexão do meu banco local.
Fiz pelo sqlite, tem um arquivo que chama "meu banco" é só abrir pelo Db Browser
Link para download: https://sqlitebrowser.org/

Regras de negócio do Banco:
Ao criar a tarefa, ele adiciona a data prevista e o status inicia como pendente
Qualquer alteração que for feita, será registrada no campo "DataAlteracao" (se ela estiver marcada como pendente e tiver o data alteração registrado, ela já foi alterada para concluida antes).
Campo TarefaRemovida serve para que ao clicar em remover a tarefa seja removida da tela, mas não do banco. (A idéia e não perder nenhum dado)
No status temos os campos Pendente e Concluido apenas.

Para fazer o login os dados são:
login: teste@teste.com.br
senha: 123456

