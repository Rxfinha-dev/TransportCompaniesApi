import React, { useState, useEffect } from 'react';
import { Button, Input } from '@/shared/ui';
import { useCreateCostumer } from '../model/useCreateCostumer';
import { useUpdateCostumer } from '../model/useUpdateCostumer';
import { CreateCostumerDto, Costumer } from '@/entities/costumer/types';
import { validateCpf } from '@/shared/lib/utils';
import './CreateCostumerForm.css';

interface CreateCostumerFormProps {
  onSuccess?: () => void;
  onCancel?: () => void;
  costumer?: Costumer;
}

export const CreateCostumerForm: React.FC<CreateCostumerFormProps> = ({
  onSuccess,
  onCancel,
  costumer,
}) => {
  const isEditing = !!costumer;
  const { createCostumer, isLoading: isCreating } = useCreateCostumer();
  const { updateCostumer, isLoading: isUpdating } = useUpdateCostumer();
  const [formData, setFormData] = useState<CreateCostumerDto>({
    name: '',
    cpf: '',
  });
  const [error, setError] = useState<string>('');

  useEffect(() => {
    if (costumer) {
      setFormData({
        name: costumer.name,
        cpf: costumer.cpf,
      });
    }
  }, [costumer]);

  const handleCpfChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value.replace(/\D/g, '');
    if (value.length <= 11) {
      setFormData({ ...formData, cpf: value });
      setError('');
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!validateCpf(formData.cpf)) {
      setError('CPF deve conter 11 dígitos');
      return;
    }

    try {
      if (isEditing && costumer) {
        await updateCostumer({ id: costumer.id, data: { ...formData, id: costumer.id } });
      } else {
        await createCostumer(formData);
      }
      onSuccess?.();
    } catch (err: any) {
      setError(err.message || `Erro ao ${isEditing ? 'atualizar' : 'criar'} cliente`);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="create-costumer-form">
      <h2>{isEditing ? 'Editar Cliente' : 'Criar Novo Cliente'}</h2>

      <Input
        label="Nome"
        value={formData.name}
        onChange={(e) => setFormData({ ...formData, name: e.target.value })}
        required
        minLength={3}
        maxLength={100}
      />

      <Input
        label="CPF"
        value={formData.cpf}
        onChange={handleCpfChange}
        placeholder="00000000000"
        required
        maxLength={11}
        error={error}
      />

      <div className="form-actions">
        <Button type="submit" isLoading={isCreating || isUpdating}>
          {isEditing ? 'Atualizar Cliente' : 'Criar Cliente'}
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

