import React, { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { costumerApi } from '@/entities/costumer/api';
import { CreateCostumerForm } from '@/features/costumers/create-costumer';
import { Table, TableHeader, TableBody, TableRow, TableCell, Loading, Button } from '@/shared/ui';
import { formatCpf } from '@/shared/lib/utils';
import { Costumer } from '@/entities/costumer/types';
import './CostumersPage.css';

export const CostumersPage: React.FC = () => {
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingCostumer, setEditingCostumer] = useState<Costumer | null>(null);
  const queryClient = useQueryClient();
  const { data: costumers, isLoading, error } = useQuery({
    queryKey: ['costumers'],
    queryFn: costumerApi.getAll,
  });

  const deleteMutation = useMutation({
    mutationFn: (id: number) => costumerApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['costumers'] });
    },
  });

  if (isLoading) {
    return (
      <div className="costumers-page">
        <Loading message="Carregando clientes..." />
      </div>
    );
  }

  if (error) {
    return (
      <div className="costumers-page">
        <div className="error-message">Erro ao carregar clientes</div>
      </div>
    );
  }

  return (
    <div className="costumers-page">
      <div className="page-header">
        <h1>Gerenciamento de Clientes</h1>
        <Button onClick={() => setShowCreateForm(true)}>
          Novo Cliente
        </Button>
      </div>

      <div className="page-content">
        {showCreateForm || editingCostumer ? (
          <CreateCostumerForm
            costumer={editingCostumer || undefined}
            onSuccess={() => { setShowCreateForm(false); setEditingCostumer(null); }}
            onCancel={() => { setShowCreateForm(false); setEditingCostumer(null); }}
          />
        ) : (
          <>
            {costumers && costumers.length > 0 ? (
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableCell header>ID</TableCell>
                    <TableCell header>Nome</TableCell>
                    <TableCell header>CPF</TableCell>
                    <TableCell header>Ações</TableCell>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {costumers.map((costumer) => (
                    <TableRow key={costumer.id}>
                      <TableCell>{costumer.id}</TableCell>
                      <TableCell>{costumer.name}</TableCell>
                      <TableCell>{formatCpf(costumer.cpf)}</TableCell>
                      <TableCell>
                        <div className="table-actions">
                          <Button size="small" variant="outline" onClick={() => setEditingCostumer(costumer)}>
                            Editar
                          </Button>
                          <Button size="small" variant="danger" onClick={() => {
                            if (window.confirm('Tem certeza que deseja excluir este cliente?')) {
                              deleteMutation.mutate(costumer.id);
                            }
                          }}>
                            Excluir
                          </Button>
                        </div>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            ) : (
              <div className="empty-state">Nenhum cliente encontrado</div>
            )}
          </>
        )}
      </div>
    </div>
  );
};

