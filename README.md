# API de Gestão de Estacionamento

## Descrição

Esta aplicação é uma API REST desenvolvida em .NET 6/8 para gerenciamento de um estacionamento. Ela permite o cadastro e controle de clientes, veículos, mensalistas, faturamento, além de suporte para importação de dados via upload de arquivos CSV.

## Tecnologias Utilizadas

- .NET 6/8
- C#
- Entity Framework Core (ORM)
- PostgreSQL
- Swagger (OpenAPI) para documentação
- Docker (opcional, se aplicável)
- AutoMapper (se usado)
- Outros pacotes relevantes

## Funcionalidades

- Cadastro, edição e exclusão de clientes
- Cadastro, edição e exclusão de veículos vinculados a clientes
- Gerenciamento de mensalistas e seus pagamentos
- Controle de faturamento (entrada e saída de veículos)
- Upload e importação de dados via CSV para facilitar cadastros em massa
- Documentação automática via Swagger

## Como Rodar a Aplicação

### Pré-requisitos

- .NET SDK 6 ou 8 instalado
- PostgreSQL instalado e rodando
- (Opcional) Docker para rodar banco ou aplicação em container

### Passos

1. Clone este repositório:
   ```bash
   git clone https://github.com/seuusuario/seurepositorio.git
   cd seurepositorio
