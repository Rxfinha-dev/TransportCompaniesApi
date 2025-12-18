import React, { useState } from 'react';
import { Button, Input } from '@/shared/ui';
import { useCreateOrder } from '../model/useCreateOrder';
import { CreateOrderDto } from '@/entities/order/types';
import './CreateOrderForm.css';

interface CreateOrderFormProps {
  onSuccess?: () => void;
  onCancel?: () => void;
}

export const CreateOrderForm: React.FC<CreateOrderFormProps> = ({
  onSuccess,
  onCancel,
}) => {
  const { createOrder, isLoading } = useCreateOrder();
  const [formData, setFormData] = useState<Partial<CreateOrderDto>>({
    statusId: 0,
    costumerId: 0,
    transportCompanyId: 0,
    orderedItens: [],
    origin: { cep: '', number: 0 },
    destination: { cep: '', number: 0 },
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await createOrder(formData as CreateOrderDto);
      onSuccess?.();
    } catch (error) {
      console.error('Erro ao criar pedido:', error);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="create-order-form">
      <h2>Criar Novo Pedido</h2>

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
        <Button type="submit" isLoading={isLoading}>
          Criar Pedido
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

