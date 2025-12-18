import React from 'react';
import { useQuery } from '@tanstack/react-query';
import { orderApi } from '@/entities/order/api';
import { Table, TableHeader, TableBody, TableRow, TableCell, Loading, Button } from '@/shared/ui';
import './OrdersList.css';

export const OrdersList: React.FC = () => {
  const { data: orders, isLoading, error } = useQuery({
    queryKey: ['orders'],
    queryFn: orderApi.getAll,
  });

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
        <Button>Novo Pedido</Button>
      </div>

      <Table>
        <TableHeader>
          <TableRow>
            <TableCell header>ID</TableCell>
            <TableCell header>Cliente ID</TableCell>
            <TableCell header>Transportadora ID</TableCell>
            <TableCell header>Status ID</TableCell>
            <TableCell header>Despachado</TableCell>
            <TableCell header>Ações</TableCell>
          </TableRow>
        </TableHeader>
        <TableBody>
          {orders.map((order) => (
            <TableRow key={order.id}>
              <TableCell>{order.id}</TableCell>
              <TableCell>{order.costumerId}</TableCell>
              <TableCell>{order.transportCompanyId}</TableCell>
              <TableCell>{order.statusId}</TableCell>
              <TableCell>{order.isDispatched ? 'Sim' : 'Não'}</TableCell>
              <TableCell>
                <div className="table-actions">
                  <Button size="small" variant="outline">
                    Ver
                  </Button>
                  <Button size="small" variant="secondary">
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

