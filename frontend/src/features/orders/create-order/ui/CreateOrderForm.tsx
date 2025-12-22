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

const addressInitialState = {
  cep: '',
  number: '',
};

export const CreateOrderForm: React.FC<CreateOrderFormProps> = ({
  onSuccess,
  onCancel,
  order,
}) => {
  const isEditing = !!order;
  const { createOrder, isLoading: isCreating } = useCreateOrder();
  const { updateOrder, isLoading: isUpdating } = useUpdateOrder();
  const [formData, setFormData] = useState<Partial<CreateOrderDto>>({
    id: undefined,
    statusId: undefined,
    costumerId: undefined,
    transportCompanyId: undefined,
    orderedItens: [],
    origin: addressInitialState,
    destination: addressInitialState,
  });

  useEffect(() => {
    if (order) {
      setFormData({
        id: order.id,
        statusId: order.statusId,
        costumerId: order.costumerId,
        transportCompanyId: order.transportCompanyId,
        orderedItens: order.orderedItens,
        origin: order.origin || addressInitialState,
        destination: order.destination || addressInitialState,
        isDispatched: order.isDispatched,
      });
    }
  }, [order]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Validações
    if (!formData.costumerId || !formData.transportCompanyId || !formData.statusId) {
      alert('Por favor, preencha todos os campos obrigatórios');
      return;
    }

    try {
      if (isEditing && order) {
     
        await updateOrder({ 
          id: order.id, 
          data: {
            id: order.id,
            statusId: formData.statusId!,
            costumerId: formData.costumerId!,
            transportCompanyId: formData.transportCompanyId!,
            orderedItens: formData.orderedItens || [],
            origin: formData.origin || addressInitialState,
            destination: formData.destination || addressInitialState,
            isDispatched: formData.isDispatched,
          } 
        });
      } else {
       
        await createOrder({
          id: 0, 
          statusId: formData.statusId!,
          costumerId: formData.costumerId!,
          transportCompanyId: formData.transportCompanyId!,
          orderedItens: formData.orderedItens || [],
          origin: formData.origin || addressInitialState,
          destination: formData.destination || addressInitialState,
          isDispatched: formData.isDispatched || false,
        });
      }
      onSuccess?.();
    } catch (error: any) {
      console.error(`Erro ao ${isEditing ? 'atualizar' : 'criar'} pedido:`, error);
      alert(`Erro ao ${isEditing ? 'atualizar' : 'criar'} pedido: ${error.message || 'Erro desconhecido'}`);
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

      <h3>Endereço de Origem</h3>
      <div className="form-row">
        <Input
          label="CEP de Origem"
          type="text"
          value={formData.origin?.cep || ''}
          onChange={(e) =>
            setFormData({
              ...formData,
              origin: { ...formData.origin, cep: e.target.value },
            })
          }
          required
        />
        <Input
          label='Número de Origem'
          type="text"
          value={formData.origin?.number || ''}
          onChange={(e) =>
            setFormData({
              ...formData,
              origin: { ...formData.origin, number: e.target.value },
            })
          }
          required
        />  
      </div>

      <h3>Endereço de Destino</h3>
      <div className="form-row">
        <Input
          label='CEP de Destino'
          type="text"
          value={formData.destination?.cep || ''}
          onChange={(e) =>
            setFormData({
              ...formData,
              destination: { ...formData.destination, cep: e.target.value },
            })
          }
          required
        />
        <Input
          label='Número de Destino'
          type="text"
          value={formData.destination?.number || ''}
          onChange={(e) =>
            setFormData({
              ...formData,
              destination: { ...formData.destination, number: e.target.value },
            })
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