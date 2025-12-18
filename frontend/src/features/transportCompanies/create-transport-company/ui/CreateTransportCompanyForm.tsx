import React, { useState, useEffect } from "react";
import { Button, Input } from "@/shared/ui";
import { useCreateTransportCompany } from "../model/useCreateTransportCompany";
import { useUpdateTransportCompany } from "../model/useUpdateTransportCompany";
import { CreateTransportCompanyDto, TransportCompany } from "@/entities/transport-company/types";
import './CreateTransportCompanyForm.css';

interface CreateTransportCompanyFormProps {
    onSuccess?: () => void;
    onCancel?: () => void;
    transportCompany?: TransportCompany;
}

export const CreateTransportCompanyForm: React.FC<CreateTransportCompanyFormProps> = ({
    onSuccess,
    onCancel,
    transportCompany,
}) => {
    const isEditing = !!transportCompany;
    const { createTransportCompany, isLoading: isCreating } = useCreateTransportCompany();
    const { updateTransportCompany, isLoading: isUpdating } = useUpdateTransportCompany();
    const [formData, setFormData] = useState<CreateTransportCompanyDto>({
        name: '',
    });

    useEffect(() => {
        if (transportCompany) {
            setFormData({
                name: transportCompany.name,
            });
        }
    }, [transportCompany]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            if (isEditing && transportCompany) {
                await updateTransportCompany({ id: transportCompany.id, data: { ...formData, id: transportCompany.id } });
            } else {
                await createTransportCompany(formData);
            }
            onSuccess?.();
        } catch (err: any) {
            console.error(err.message || `Erro ao ${isEditing ? 'atualizar' : 'criar'} transportadora`);
        }
    };

    return(
        <form onSubmit={handleSubmit} className="create-transport-company-form">
            <h2>{isEditing ? 'Editar Transportadora' : 'Criar Nova Transportadora'}</h2>
        
        <Input
            label="Nome"
            value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
            required
            minLength={3}
            maxLength={100}
        />

        <div className="form-actions">
            <Button type="submit" isLoading={isCreating || isUpdating}>
                {isEditing ? 'Atualizar Transportadora' : 'Criar Transportadora'}
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