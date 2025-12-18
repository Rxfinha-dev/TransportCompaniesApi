import React, { useState } from 'react';
import { OrdersList } from '@/features/orders/list-orders';
import { CreateOrderForm } from '@/features/orders/create-order';
import './OrdersPage.css';

export const OrdersPage: React.FC = () => {
  const [showCreateForm, setShowCreateForm] = useState(false);

  return (
    <div className="orders-page">
      <div className="page-header">
        <h1>Gerenciamento de Pedidos</h1>
      </div>

      <div className="page-content">
        {showCreateForm ? (
          <CreateOrderForm
            onSuccess={() => setShowCreateForm(false)}
            onCancel={() => setShowCreateForm(false)}
          />
        ) : (
          <OrdersList />
        )}
      </div>
    </div>
  );
};

