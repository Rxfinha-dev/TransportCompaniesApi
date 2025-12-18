import React from 'react';
import { useQuery } from '@tanstack/react-query';
import { orderApi } from '@/entities/order/api';
import { costumerApi } from '@/entities/costumer/api';
import { transportCompanyApi } from '@/entities/transport-company/api';
import { statusApi } from '@/entities/status/api';
import { Table, TableHeader, TableBody, TableRow, TableCell, Loading, Button } from '@/shared/ui';
import { Order } from '@/entities/order/types';
import './OrdersList.css';

interface OrdersListProps {
  onViewTracking?: (order: Order) => void;
  onEdit?: (order: Order) => void;
  onCreate?: () => void;
}

export const OrdersList: React.FC<OrdersListProps> = ({ onViewTracking, onEdit, onCreate }) => {
  const { data: orders, isLoading, error } = useQuery({
    queryKey: ['orders'],
    queryFn: orderApi.getAll,
  });

  const { data: costumers } = useQuery({
    queryKey: ['costumers'],
    queryFn: costumerApi.getAll,
  });

  const { data: transportCompanies } = useQuery({
    queryKey: ['transportCompanies'],
    queryFn: transportCompanyApi.getAll,
  });

  const { data: statuses } = useQuery({
    queryKey: ['statuses'],
    queryFn: statusApi.getAll,
  });

  const getCostumerName = (id: number) => {
    return costumers?.find(c => c.id === id)?.name || `Cliente ${id}`;
  };

  const getTransportCompanyName = (id: number) => {
    return transportCompanies?.find(tc => tc.id === id)?.name || `Transportadora ${id}`;
  };

  const getStatusDescription = (id: number) => {
    return statuses?.find(s => s.id === id)?.description || `Status ${id}`;
  };

  if (isLoading) {
    return <Loading message="Carregando pedidos..." />;
  }

  if (error) {
    return <div className="error-message">Erro ao carregar pedidos</div>;
  }

  if (!orders || orders.length === 0) {
    return <div className="empty-state">Nenhum pedido encontrado</div>;
  }

  return (
    <div className="orders-list">
      <div className="orders-list-header">
        <h2>Pedidos</h2>
        <Button onClick={onCreate}>Novo Pedido</Button>
      </div>

      <Table>
        <TableHeader>
          <TableRow>
            <TableCell header>ID</TableCell>
            <TableCell header>Cliente</TableCell>
            <TableCell header>Transportadora</TableCell>
            <TableCell header>Status</TableCell>
            <TableCell header>Despachado</TableCell>
            <TableCell header>Ações</TableCell>
          </TableRow>
        </TableHeader>
        <TableBody>
          {orders.map((order) => (
            <TableRow key={order.id}>
              <TableCell>{order.id}</TableCell>
              <TableCell>{getCostumerName(order.costumerId)}</TableCell>
              <TableCell>{getTransportCompanyName(order.transportCompanyId)}</TableCell>
              <TableCell>{getStatusDescription(order.statusId)}</TableCell>
              <TableCell>{order.isDispatched ? 'Sim' : 'Não'}</TableCell>
              <TableCell>
                <div className="table-actions">
                  <Button size="small" variant="outline" onClick={() => onViewTracking?.(order)}>
                    Ver
                  </Button>
                  <Button size="small" variant="secondary" onClick={() => onEdit?.(order)}>
                    Editar
                  </Button>
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
};

