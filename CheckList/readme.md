# Checklist API

API para gerenciamento de checklists de veículos.

Esta API permite que você crie, atualize, obtenha e aprove ou desaprove checklists de veículos. Ela foi desenvolvida usando .NET 8 e Entity Framework Core com SQL Server.

## Tecnologias Utilizadas

- **.NET 8:** Plataforma de desenvolvimento para construir aplicativos modernos.
- **Entity Framework Core:** Framework de mapeamento objeto-relacional (ORM) para acesso a dados.
- **SQL Server:** Sistema de gerenciamento de banco de dados relacional.
- **Swagger:** Ferramenta para documentação de APIs.


## Configuração do Projeto

Certifique-se de ter o .NET 8 instalado em sua máquina.

1. Clone este repositório:

git clone https://github.com/seu-usuario/checklist-api.git

2. Navegue até o diretório do projeto:

cd checklist-api

3. Abra o projeto no Visual Studio 2022.

4. Configure a conexão com o banco de dados SQL Server no arquivo `appsettings.json`.

5. Execute o aplicativo.

6. Acesse a documentação da API em [http://localhost:port/swagger](http://localhost:port/swagger) para obter detalhes sobre os endpoints e como usá-los.

## Endpoints

### `POST /api/checklist`

Cria um novo checklist.

### `GET /api/checklist/{id}`

Obtém um checklist pelo ID.

### `PUT /api/checklist/{id}`

Atualiza um checklist existente.

### `POST /api/checklist/{id}/approve`

Aprova ou desaprova um checklist.

### `POST /api/checklist/{id}/start`

Inicia a execução de um checklist.

## Como Contribuir

1. Fork este repositório.
2. Crie uma nova branch para sua modificação: `git checkout -b feature/nova-feature`.
3. Faça suas alterações e faça commit delas: `git commit -am 'Adicione nova feature'`.
4. Empurre para a branch: `git push origin feature/nova-feature`.
5. Envie um pull request.

## Autor

Luis Denis Pliskievski de lara - luisdenis.lara@icloud.com -

## Licença

Este projeto está licenciado sob a Licença MIT - consulte o arquivo [LICENSE](LICENSE) para obter detalhes.
