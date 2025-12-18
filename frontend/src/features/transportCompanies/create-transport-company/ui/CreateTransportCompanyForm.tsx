import React, {useState} from "react";
import { Button, Input } from "@/shared/ui";
import { useCreateTransportCompany } from "../model/useCreateTransportCompany";
import { CreateTransportCompanyDto } from "@/entities/transport-company/types";
import './CreateTransportCompanyForm.css';

interface CreateTransportCompanyFormProps {
    onSuccess?: () => void;
    onCancel?: () => void;
}

export const CreateTransportCompanyForm: React.FC<CreateTransportCompanyFormProps> = ({
    onSuccess,
    onCancel,
}) => {
    const { createTransportCompany, isLoading } = useCreateTransportCompany();
    const [formData, setFormData] = useState<CreateTransportCompanyDto>({
        name: '',
    });
    const [error, setError] = useState<string>('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');

        try {
            await createTransportCompany(formData);
            onSuccess?.();
        } catch (err: any) {
            setError(err.message || 'Erro ao criar transportadora');
        }
    };

    return(
        <form onSubmit={handleSubmit} className="create-transport-company-form">
            <h2>Criar Nova Transportadora</h2>
        
        <Input
            label="Nome"
            value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
            required
            minLength={3}
            maxLength={100}
        />

        <div className="form-actions">
            <Button type="submit" isLoading={isLoading}>
                Criar Transportadora
            </Button>
        </div>
    </form>
    );
};