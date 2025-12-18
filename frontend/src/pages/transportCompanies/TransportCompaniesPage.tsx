import React, {useState} from "react";
import {useQuery} from "@tanstack/react-query";
import {transportCompanyApi} from "@/entities/transport-company/api";
import {CreateTransportCompanyForm} from "@/features/transportCompanies/create-transport-company";
import {Table, TableHeader, TableBody, TableRow, TableCell, Loading, Button} from "@/shared/ui";
import "./TransportCompaniesPage.css";

interface TransportCompany {
    id: string | number;
    name: string;
}

export const TransportCompaniesPage: React.FC = () => {
    const [showCreateForm, setShowCreateForm] = useState(false);
    const {data: transportCompanies, isLoading, error} = useQuery({
        queryKey: ['transportCompanies'],
        queryFn: transportCompanyApi.getAll,
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
                {showCreateForm ? (
                    <CreateTransportCompanyForm
                        onSuccess={() => setShowCreateForm(false)}
                        onCancel={() => setShowCreateForm(false)}
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
                                                    <Button size="small" variant="outline">Editar</Button>
                                                    <Button size="small" variant="outline" color="danger">Excluir</Button>
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
