import React, { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { costumerApi } from '@/entities/costumer/api';
import { CreateCostumerForm } from '@/features/costumers/create-costumer';
import { Table, TableHeader, TableBody, TableRow, TableCell, Loading, Button } from '@/shared/ui';
import { formatCpf } from '@/shared/lib/utils';
import './CostumersPage.css';

export const CostumersPage: React.FC = () => {
  const [showCreateForm, setShowCreateForm] = useState(false);
  const { data: costumers, isLoading, error } = useQuery({
    queryKey: ['costumers'],
    queryFn: costumerApi.getAll,
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
        {showCreateForm ? (
          <CreateCostumerForm
            onSuccess={() => setShowCreateForm(false)}
            onCancel={() => setShowCreateForm(false)}
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
                          <Button size="small" variant="outline">
                            Editar
                          </Button>
                          <Button size="small" variant="danger">
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

