import React, { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { orderApi } from '@/entities/order/api';
import { trackingApi } from '@/entities/tracking/api';
import { AddTrackingEventForm } from '@/features/tracking/add-tracking-event';
import { Table, TableHeader, TableBody, TableRow, TableCell, Loading, Button } from '@/shared/ui';
import { Order } from '@/entities/order/types';
import { Tracking } from '@/entities/tracking/types';
import './TrackingPage.css';

export const TrackingPage: React.FC = () => {
  const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);
  const [showTracking, setShowTracking] = useState(false);
  const [showAddForm, setShowAddForm] = useState(false);

  const { data: orders, isLoading, error } = useQuery({
    queryKey: ['orders'],
    queryFn: orderApi.getAll,
  });

  const { data: trackingHistory } = useQuery({
    queryKey: ['tracking', selectedOrder?.id],
    queryFn: () => selectedOrder ? trackingApi.getHistory(selectedOrder.id) : Promise.resolve([]),
    enabled: !!selectedOrder && showTracking,
  });

  if (isLoading) {
    return (
      <div className="tracking-page">
        <Loading message="Carregando pedidos..." />
      </div>
    );
  }

  if (error) {
    return (
      <div className="tracking-page">
        <div className="error-message">Erro ao carregar pedidos</div>
      </div>
    );
  }

  return (
    <div className="tracking-page">
      <div className="page-header">
        <h1>Gerenciamento de Rastreamento</h1>
      </div>

      <div className="page-content">
        {!showTracking ? (
          <>
            {orders && orders.length > 0 ? (
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableCell header>ID</TableCell>
                    <TableCell header>Cliente</TableCell>
                    <TableCell header>Status</TableCell>
                    <TableCell header>Ações</TableCell>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {orders.map((order) => (
                    <TableRow key={order.id}>
                      <TableCell>{order.id}</TableCell>
                      <TableCell>Cliente {order.costumerId}</TableCell>
                      <TableCell>Status {order.statusId}</TableCell>
                      <TableCell>
                        <div className="table-actions">
                          <Button size="small" variant="outline" onClick={() => { setSelectedOrder(order); setShowTracking(true); }}>
                            Ver Rastreamento
                          </Button>
                        </div>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            ) : (
              <div>Nenhum pedido encontrado.</div>
            )}
          </>
        ) : (
          <div className="tracking-details">
            <Button onClick={() => setShowTracking(false)}>Voltar</Button>
        <h2 className="rastreamento">Rastreamento do Pedido {selectedOrder?.id}</h2>            
            <Button onClick={() => setShowAddForm(!showAddForm)}>
              {showAddForm ? 'Cancelar' : 'Adicionar Evento'}
            </Button>
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
          </div>
        )}
      </div>
    </div>
  );
};