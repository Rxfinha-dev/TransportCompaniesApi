import React, { useState } from 'react';
import { Button, Input } from '@/shared/ui';
import { useCreateCostumer } from '../model/useCreateCostumer';
import { CreateCostumerDto } from '@/entities/costumer/types';
import { formatCpf, validateCpf } from '@/shared/lib/utils';
import './CreateCostumerForm.css';

interface CreateCostumerFormProps {
  onSuccess?: () => void;
  onCancel?: () => void;
}

export const CreateCostumerForm: React.FC<CreateCostumerFormProps> = ({
  onSuccess,
  onCancel,
}) => {
  const { createCostumer, isLoading } = useCreateCostumer();
  const [formData, setFormData] = useState<CreateCostumerDto>({
    name: '',
    cpf: '',
  });
  const [error, setError] = useState<string>('');

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
      await createCostumer(formData);
      onSuccess?.();
    } catch (err: any) {
      setError(err.message || 'Erro ao criar cliente');
    }
  };

  return (
    <form onSubmit={handleSubmit} className="create-costumer-form">
      <h2>Criar Novo Cliente</h2>

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
        <Button type="submit" isLoading={isLoading}>
          Criar Cliente
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

