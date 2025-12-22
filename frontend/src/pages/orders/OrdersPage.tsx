import React, { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { OrdersList } from '@/features/orders/list-orders';
import { CreateOrderForm } from '@/features/orders/create-order';
import { trackingApi } from '@/entities/tracking/api';
import { AddTrackingEventForm } from '@/features/tracking/add-tracking-event';
import { Table, TableHeader, TableBody, TableRow, TableCell, Button } from '@/shared/ui';
import { Order } from '@/entities/order/types';
import { Tracking } from '@/entities/tracking/types';
import './OrdersPage.css';

export const OrdersPage: React.FC = () => {
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);
  const [showTracking, setShowTracking] = useState(false);
  const [showAddForm, setShowAddForm] = useState(false);
  const [editingOrder, setEditingOrder] = useState<Order | null>(null);

  const { data: trackingHistory } = useQuery({
    queryKey: ['tracking', selectedOrder?.id],
    queryFn: () => selectedOrder ? trackingApi.getHistory(selectedOrder.id) : Promise.resolve([]),
    enabled: !!selectedOrder && showTracking,
  });

  const handleViewTracking = (order: Order) => {
    setSelectedOrder(order);
    setShowTracking(true);
  };

  const handleCreate = () => {
    setShowCreateForm(true);
  };
  const handleEdit = (order: Order) => {
    setEditingOrder(order);
    setShowTracking(false); // Fecha a visualização de tracking se estiver aberta
  };

  return (
    <div className="orders-page">
      <div className="page-header">
        <h1>Gerenciamento de Pedidos</h1>
      </div>

      <div className="page-content">
        {showTracking ? (
          <div className="tracking-details">
            <Button onClick={() => { setShowTracking(false); setSelectedOrder(null); }}>Voltar</Button>
            <h2>Rastreamento do Pedido {selectedOrder?.id}</h2>
            {showAddForm && selectedOrder && (
              <AddTrackingEventForm
                orderId={selectedOrder.id}
                onSuccess={() => setShowAddForm(false)}
                onCancel={() => setShowAddForm(false)}
              />
            )}
            {trackingHistory && trackingHistory.length > 0 ? (
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableCell header>Data</TableCell>
                    <TableCell header>Status</TableCell>
                    <TableCell header>Mensagem</TableCell>
                    <TableCell header>Localização</TableCell>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {trackingHistory.map((tracking: Tracking) => (
                    <TableRow key={tracking.id}>
                      <TableCell>{new Date(tracking.createdAt).toLocaleString()}</TableCell>
                      <TableCell>Status {tracking.statusId}</TableCell>
                      <TableCell>{tracking.message || '-'}</TableCell>
                      <TableCell>{tracking.location || '-'}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            ) : (
              <div>Nenhum evento de rastreamento encontrado.</div>
            )}
            <div className="add-event-button">
              <Button onClick={() => setShowAddForm(!showAddForm)}>
                {showAddForm ? 'Cancelar' : 'Adicionar Evento'}
              </Button>
            </div>
          </div>
        ) : showCreateForm || editingOrder ? (
          <CreateOrderForm
            order={editingOrder || undefined}
            onSuccess={() => { setShowCreateForm(false); setEditingOrder(null); }}
            onCancel={() => { setShowCreateForm(false); setEditingOrder(null); }}
          />
        ) : (
          <OrdersList onViewTracking={handleViewTracking} onEdit={handleEdit} onCreate={handleCreate} />
        )}
      </div>
    </div>
  );
};

