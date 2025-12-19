import React, { useState, useEffect } from 'react';
import { Button, Input } from '@/shared/ui';
import { useCreateOrder } from '../model/useCreateOrder';
import { useUpdateOrder } from '../model/useUpdateOrder';
import { CreateOrderDto, Order } from '@/entities/order/types';
import './CreateOrderForm.css';

interface CreateOrderFormProps {
  onSuccess?: () => void;
  onCancel?: () => void;
  order?: Order;
}

export const CreateOrderForm: React.FC<CreateOrderFormProps> = ({
  onSuccess,
  onCancel,
  order,
}) => {
  const isEditing = !!order;
  const { createOrder, isLoading: isCreating } = useCreateOrder();
  const { updateOrder, isLoading: isUpdating } = useUpdateOrder();
  const [formData, setFormData] = useState<Partial<CreateOrderDto>>({
    statusId: 0,
    costumerId: 0,
    transportCompanyId: 0,
    orderedItens: [],
    origin: { street: '', number: '', city: '', state: '', zipCode: '' },
    destination: { street: '', number: '', city: '', state: '', zipCode: '' },
  });

  useEffect(() => {
    if (order) {
      setFormData({
        statusId: order.statusId,
        costumerId: order.costumerId,
        transportCompanyId: order.transportCompanyId,
        orderedItens: order.orderedItens,
        origin: order.origin,
        destination: order.destination,
        isDispatched: order.isDispatched,
      });
    }
  }, [order]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (isEditing && order) {
        await updateOrder({ id: order.id, data: formData as CreateOrderDto });
      } else {
        await createOrder(formData as CreateOrderDto);
      }
      onSuccess?.();
    } catch (error) {
      console.error(`Erro ao ${isEditing ? 'atualizar' : 'criar'} pedido:`, error);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="create-order-form">
      <h2>{isEditing ? 'Editar Pedido' : 'Criar Novo Pedido'}</h2>

      <div className="form-row">
        <Input
          label="ID do Cliente"
          type="number"
          value={formData.costumerId || ''}
          onChange={(e) =>
            setFormData({ ...formData, costumerId: Number(e.target.value) })
          }
          required
        />
        <Input
          label="ID da Transportadora"
          type="number"
          value={formData.transportCompanyId || ''}
          onChange={(e) =>
            setFormData({
              ...formData,
              transportCompanyId: Number(e.target.value),
            })
          }
          required
        />
        <Input
          label="ID do Status"
          type="number"
          value={formData.statusId || ''}
          onChange={(e) =>
            setFormData({ ...formData, statusId: Number(e.target.value) })
          }
          required
        />
      </div>

      <div className="form-actions">
        <Button type="submit" isLoading={isCreating || isUpdating}>
          {isEditing ? 'Atualizar Pedido' : 'Criar Pedido'}
        </Button>
        {onCancel && (
          <Button type="button" variant="outline" onClick={onCancel}>
            Cancelar
          </Button>
        )}
      </div>
    </form>
  );
};

