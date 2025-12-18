# Transport Companies Frontend

Frontend React para o sistema de gestão de transportadoras, construído com Feature-Sliced Design (FSD) architecture.

## 🏗️ Arquitetura

Este projeto utiliza a arquitetura **Feature-Sliced Design (FSD)**, que organiza o código em camadas:

### 📁 Estrutura de Pastas

```
src/
├── app/           # Configuração da aplicação, providers, routing
├── pages/         # Páginas completas da aplicação
├── widgets/       # Componentes complexos compostos de features
├── features/      # Funcionalidades específicas do negócio
├── entities/      # Entidades de negócio (modelos, componentes relacionados)
└── shared/        # Código compartilhado (UI kit, utils, etc)
```

### Camadas

- **app**: Configuração inicial, routing, providers globais
- **pages**: Páginas completas da aplicação
- **widgets**: Componentes complexos que combinam features
- **features**: Funcionalidades de negócio isoladas (ex: criar pedido, listar clientes)
- **entities**: Entidades de negócio (Order, Costumer, etc) com seus tipos e APIs
- **shared**: Código reutilizável (componentes UI, utils, configs)

## 🚀 Como executar

### Pré-requisitos

- Node.js 18+ 
- npm ou yarn

### Instalação

```bash
cd frontend
npm install
```

### Desenvolvimento

```bash
npm run dev
```

O projeto estará disponível em `http://localhost:3000`

### Build para produção

```bash
npm run build
```

## 🔧 Configuração

### Variáveis de Ambiente

Crie um arquivo `.env` na raiz do frontend:

```env
VITE_API_URL=http://localhost:5000/api
```

## 📦 Tecnologias Utilizadas

- **React 18** - Biblioteca UI
- **TypeScript** - Tipagem estática
- **Vite** - Build tool
- **React Router** - Roteamento
- **TanStack Query (React Query)** - Gerenciamento de estado servidor
- **Axios** - Cliente HTTP

## 🎨 Componentes UI

Componentes reutilizáveis disponíveis em `shared/ui`:

- `Button` - Botão com variantes e estados
- `Input` - Campo de entrada com validação
- `Table` - Tabela responsiva
- `Loading` - Indicador de carregamento

## 📝 Features Implementadas

### Pedidos (Orders)
- ✅ Listar pedidos
- ✅ Criar pedido
- 🔄 Atualizar pedido (em desenvolvimento)
- 🔄 Visualizar detalhes do pedido (em desenvolvimento)

### Clientes (Costumers)
- ✅ Listar clientes
- ✅ Criar cliente
- 🔄 Atualizar cliente (em desenvolvimento)
- 🔄 Excluir cliente (em desenvolvimento)

### Rastreamento (Tracking)
- ✅ Visualizar histórico de rastreamento
- 🔄 Adicionar evento de rastreamento (em desenvolvimento)

## 🔄 Próximos Passos

- [ ] Completar CRUD de todas as entidades
- [ ] Adicionar validação de formulários mais robusta
- [ ] Implementar tratamento de erros global
- [ ] Adicionar testes unitários
- [ ] Implementar autenticação/autorização
- [ ] Adicionar filtros e busca
- [ ] Implementar paginação

## 📚 Documentação FSD

Para mais informações sobre Feature-Sliced Design:
- [Documentação Oficial](https://feature-sliced.design/)
- [Guia de Boas Práticas](https://feature-sliced.design/docs/guides/best-practices)

