import React, {useState} from "react";
import {useQuery, useMutation, useQueryClient} from "@tanstack/react-query";
import {transportCompanyApi} from "@/entities/transport-company/api";
import {CreateTransportCompanyForm} from "@/features/transportCompanies/create-transport-company";
import {Table, TableHeader, TableBody, TableRow, TableCell, Loading, Button} from "@/shared/ui";
import { TransportCompany } from "@/entities/transport-company/types";
import "./TransportCompaniesPage.css";

export const TransportCompaniesPage: React.FC = () => {
    const [showCreateForm, setShowCreateForm] = useState(false);
    const [editingCompany, setEditingCompany] = useState<TransportCompany | null>(null);
    const queryClient = useQueryClient();
    const {data: transportCompanies, isLoading, error} = useQuery({
        queryKey: ['transportCompanies'],
        queryFn: transportCompanyApi.getAll,
    });

    const deleteMutation = useMutation({
        mutationFn: (id: number) => transportCompanyApi.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['transportCompanies'] });
        },
    });

    if(isLoading) {
        return (
            <div className="transport-companies-page">
                <Loading message="Carregando transportadoras..." />
            </div>
        );
    }

    if(error) {
        return (
            <div className="transport-companies-page">
                <div className="error-message">Erro ao carregar transportadoras</div>
            </div>
        );
    }

    return (
        <div className="transport-companies-page">
            <div className="page-header">
                <h1>Gerenciamento de Transportadoras</h1>
                <Button onClick={() => setShowCreateForm(true)}>
                    Nova Transportadora
                </Button>
            </div>

            <div className="page-content">
                {showCreateForm || editingCompany ? (
                    <CreateTransportCompanyForm
                        transportCompany={editingCompany || undefined}
                        onSuccess={() => { setShowCreateForm(false); setEditingCompany(null); }}
                        onCancel={() => { setShowCreateForm(false); setEditingCompany(null); }}
                    />
                ) : (
                    <>
                        {transportCompanies && transportCompanies.length > 0 ? (
                            <Table>
                                <TableHeader>
                                    <TableRow>  
                                        <TableCell header>ID</TableCell>
                                        <TableCell header>Nome</TableCell>
                                        <TableCell header>Ações</TableCell>
                                    </TableRow>
                                </TableHeader>
                                <TableBody>
                                    {transportCompanies.map((company: TransportCompany) => (
                                        <TableRow key={company.id}>
                                            <TableCell>{String(company.id)}</TableCell>
                                            <TableCell>{company.name}</TableCell>
                                            <TableCell>
                                                <div className="table-actions">
                                                    <Button size="small" variant="outline" onClick={() => setEditingCompany(company)}>
                                                        Editar
                                                    </Button>
                                                    <Button size="small" variant="danger" onClick={() => {
                                                        if (window.confirm('Tem certeza que deseja excluir esta transportadora?')) {
                                                            deleteMutation.mutate(company.id);
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
                            <div>Nenhuma transportadora encontrada.</div>
                        )}
                    </>
                )}
            </div>
        </div>
    );              
};
