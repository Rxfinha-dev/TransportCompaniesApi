# Arquitetura Feature-Sliced Design (FSD)

Este projeto segue a arquitetura **Feature-Sliced Design**, uma metodologia para organizar código frontend de forma escalável e manutenível.

## 📐 Princípios da Arquitetura

### Hierarquia de Camadas (de baixo para cima)

```
app/          ← Configuração global (mais baixo nível)
  ↓
pages/        ← Páginas completas
  ↓
widgets/      ← Componentes complexos
  ↓
features/     ← Funcionalidades de negócio
  ↓
entities/     ← Modelos de domínio
  ↓
shared/       ← Código reutilizável (mais alto nível)
```

### Regras de Importação

**IMPORTANTE**: As camadas só podem importar de camadas **inferiores** na hierarquia:

- ✅ `features` pode importar de `entities` e `shared`
- ✅ `pages` pode importar de `widgets`, `features`, `entities` e `shared`
- ❌ `entities` **NÃO pode** importar de `features` ou `pages`
- ❌ `shared` **NÃO pode** importar de nenhuma outra camada

## 🗂️ Estrutura Detalhada

### 1. **shared/** - Código Compartilhado

Código que não está relacionado ao domínio do negócio e pode ser reutilizado em qualquer projeto.

```
shared/
├── api/          # Cliente HTTP, configurações de API
├── ui/           # Componentes UI genéricos (Button, Input, Table)
├── lib/          # Utilitários (formatação, validações)
└── types/        # Tipos TypeScript genéricos
```

**Exemplos**:
- Componentes de UI (Button, Input, Modal)
- Utilitários (formatação de CPF, CEP, datas)
- Configuração do cliente HTTP
- Hooks genéricos

### 2. **entities/** - Entidades de Negócio

Modelos e lógica relacionada às entidades principais do sistema.

```
entities/
├── order/
│   ├── api.ts        # Chamadas à API
│   └── types.ts      # Tipos TypeScript
├── costumer/
├── transport-company/
├── status/
└── tracking/
```

**Cada entidade contém**:
- `types.ts`: Interfaces TypeScript
- `api.ts`: Funções de API relacionadas à entidade
- Componentes específicos da entidade (se necessário)

**Exemplo de uso**:
```typescript
import { orderApi } from '@/entities/order/api';
import { Order } from '@/entities/order/types';
```

### 3. **features/** - Funcionalidades

Funcionalidades específicas do negócio que usam entidades.

```
features/
├── orders/
│   ├── create-order/
│   │   ├── ui/           # Componentes visuais
│   │   ├── model/        # Lógica de negócio (hooks, stores)
│   │   └── index.ts
│   └── list-orders/
├── costumers/
│   └── create-costumer/
└── tracking/
    └── view-tracking/
```

**Estrutura de uma feature**:
- `ui/`: Componentes React visuais
- `model/`: Hooks customizados, lógica de estado (React Query, etc)
- `index.ts`: Exports públicos da feature

**Exemplo**:
```typescript
// features/orders/create-order/index.ts
export { CreateOrderForm } from './ui/CreateOrderForm';
export { useCreateOrder } from './model/useCreateOrder';
```

### 4. **widgets/** - Widgets Complexos

Componentes complexos que combinam múltiplas features.

```
widgets/
├── header/       # Cabeçalho com navegação
├── order-card/   # Card de pedido (usa features de order)
└── sidebar/      # Menu lateral
```

**Exemplo**: Um widget `OrderCard` pode usar:
- Feature `view-order-details`
- Feature `track-order`
- Entities `order` e `tracking`

### 5. **pages/** - Páginas

Páginas completas da aplicação que combinam widgets e features.

```
pages/
├── home/
├── orders/
│   └── OrdersPage.tsx    # Usa widgets e features
└── costumers/
```

**Exemplo**:
```typescript
// pages/orders/OrdersPage.tsx
import { OrdersList } from '@/features/orders/list-orders';
import { CreateOrderForm } from '@/features/orders/create-order';
import { Header } from '@/widgets/header';
```

### 6. **app/** - Configuração da Aplicação

Configuração global, providers, routing.

```
app/
├── providers/    # React Query, Redux, etc
├── router/       # Configuração de rotas
├── App.tsx       # Componente raiz
└── index.tsx     # Entry point
```

## 🎯 Boas Práticas

### 1. Nomes de Pastas e Arquivos

- Use **kebab-case** para pastas: `create-order`, `list-orders`
- Use **PascalCase** para componentes: `CreateOrderForm.tsx`
- Use **camelCase** para utilitários: `formatCpf.ts`

### 2. Public API (index.ts)

Cada segmento deve ter um `index.ts` que exporta apenas o que é necessário:

```typescript
// features/orders/create-order/index.ts
export { CreateOrderForm } from './ui/CreateOrderForm';
export { useCreateOrder } from './model/useCreateOrder';
// Não exporte implementações internas!
```

### 3. Estrutura de Imports

Use path aliases configurados:

```typescript
// ✅ Bom
import { Button } from '@/shared/ui';
import { orderApi } from '@/entities/order/api';
import { CreateOrderForm } from '@/features/orders/create-order';

// ❌ Evite
import { Button } from '../../../shared/ui/Button';
```

### 4. Separação de Responsabilidades

- **shared**: Genérico, sem lógica de negócio
- **entities**: Modelos e dados
- **features**: Lógica de negócio isolada
- **widgets**: Composição de features
- **pages**: Composição de widgets e features

## 📝 Exemplo Prático: Criando uma Nova Feature

Vamos criar a feature "editar pedido":

### Passo 1: Criar estrutura

```
features/orders/update-order/
├── ui/
│   ├── UpdateOrderForm.tsx
│   └── UpdateOrderForm.css
├── model/
│   └── useUpdateOrder.ts
└── index.ts
```

### Passo 2: Implementar types (se necessário)

```typescript
// entities/order/types.ts (já existe)
export interface UpdateOrderDto { ... }
```

### Passo 3: Implementar API (se necessário)

```typescript
// entities/order/api.ts (já existe)
export const orderApi = {
  update: async (id: number, data: UpdateOrderDto) => { ... }
}
```

### Passo 4: Criar hook customizado

```typescript
// features/orders/update-order/model/useUpdateOrder.ts
import { useMutation } from '@tanstack/react-query';
import { orderApi } from '@/entities/order/api';

export const useUpdateOrder = () => {
  const mutation = useMutation({
    mutationFn: ({ id, data }) => orderApi.update(id, data),
  });
  
  return {
    updateOrder: mutation.mutateAsync,
    isLoading: mutation.isPending,
  };
};
```

### Passo 5: Criar componente UI

```typescript
// features/orders/update-order/ui/UpdateOrderForm.tsx
import { useUpdateOrder } from '../model/useUpdateOrder';
import { Button, Input } from '@/shared/ui';

export const UpdateOrderForm = ({ orderId }) => {
  const { updateOrder, isLoading } = useUpdateOrder();
  // ...
};
```

### Passo 6: Exportar public API

```typescript
// features/orders/update-order/index.ts
export { UpdateOrderForm } from './ui/UpdateOrderForm';
export { useUpdateOrder } from './model/useUpdateOrder';
```

### Passo 7: Usar na página

```typescript
// pages/orders/OrdersPage.tsx
import { UpdateOrderForm } from '@/features/orders/update-order';

// ...
```

## 🚫 Erros Comuns

1. **Importar de camada superior**:
   ```typescript
   // ❌ ERRADO - entities não pode importar de features
   // entities/order/api.ts
   import { useCreateOrder } from '@/features/orders/create-order';
   ```

2. **Exportar implementações internas**:
   ```typescript
   // ❌ ERRADO - expõe detalhes internos
   export { internalHelper } from './model/helpers';
   
   // ✅ CORRETO - exporta apenas o necessário
   export { CreateOrderForm } from './ui/CreateOrderForm';
   ```

3. **Colocar lógica de negócio em shared**:
   ```typescript
   // ❌ ERRADO - lógica específica do domínio
   // shared/lib/formatOrderStatus.ts
   
   // ✅ CORRETO - colocar em entities/order/lib/
   ```

## 📚 Recursos Adicionais

- [Documentação Oficial FSD](https://feature-sliced.design/)
- [Guia de Boas Práticas](https://feature-sliced.design/docs/guides/best-practices)
- [Exemplos de Projetos](https://github.com/feature-sliced/documentation)

