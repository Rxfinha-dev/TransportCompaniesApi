import React, { useState } from 'react';
import { Button, Input } from '@/shared/ui';
import { useAddTrackingEvent } from '../model/useAddTrackingEvent';
import { CreateTrackingEventDto } from '@/entities/tracking/types';
import { useQuery } from '@tanstack/react-query';
import { statusApi } from '@/entities/status/api';
import './AddTrackingEventForm.css';

interface AddTrackingEventFormProps {
  orderId: number;
  onSuccess?: () => void;
  onCancel?: () => void;
}

export const AddTrackingEventForm: React.FC<AddTrackingEventFormProps> = ({
  orderId,
  onSuccess,
  onCancel,
}) => {
  const { addTrackingEvent, isLoading } = useAddTrackingEvent();
  const [formData, setFormData] = useState<CreateTrackingEventDto>({
    statusId: 0,
    message: '',
    location: '',
  });
  const [error, setError] = useState<string>('');

  const { data: statuses } = useQuery({
    queryKey: ['statuses'],
    queryFn: statusApi.getAll,
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (formData.statusId === 0) {
      setError('Status é obrigatório');
      return;
    }

    try {
      await addTrackingEvent({ orderId, data: formData });
      onSuccess?.();
    } catch (err: any) {
      setError(err.message || 'Erro ao adicionar evento de rastreamento');
    }
  };

  return (
    <form onSubmit={handleSubmit} className="add-tracking-event-form">
      <h3>Adicionar Evento de Rastreamento</h3>

      <div>
        <label>Status</label>
        <select
          value={formData.statusId}
          onChange={(e) => setFormData({ ...formData, statusId: parseInt(e.target.value) })}
          required
        >
          <option value={0}>Selecione um status</option>
          {statuses?.map((status) => (
            <option key={status.id} value={status.id}>
              {status.description}
            </option>
          ))}
        </select>
      </div>

      <Input
        label="Mensagem"
        value={formData.message}
        onChange={(e) => setFormData({ ...formData, message: e.target.value })}
      />

      <Input
        label="Localização"
        value={formData.location || ''}
        onChange={(e) => setFormData({ ...formData, location: e.target.value })}
      />

      <div className="form-actions">
        <Button type="submit" isLoading={isLoading}>
          Adicionar Evento
        </Button>
        {onCancel && (
          <Button type="button" variant="outline" onClick={onCancel}>
            Cancelar
          </Button>
        )}
      </div>
      {error && <div className="error-message">{error}</div>}
    </form>
  );
};