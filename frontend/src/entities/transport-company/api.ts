import { apiClient } from '@/shared/api/api-client';
import {
  TransportCompany,
  CreateTransportCompanyDto,
  UpdateTransportCompanyDto,
} from './types';

export const transportCompanyApi = {
  getAll: async (): Promise<TransportCompany[]> => {
    const response = await apiClient.get<TransportCompany[]>('/TransportCompany');
    return response.data;
  },

  getById: async (id: number): Promise<TransportCompany> => {
    const response = await apiClient.get<TransportCompany>(
      `/TransportCompany/${id}`
    );
    return response.data;
  },

  create: async (data: CreateTransportCompanyDto): Promise<void> => {
    await apiClient.post('/TransportCompany', data);
  },

  update: async (
    id: number,
    data: UpdateTransportCompanyDto
  ): Promise<void> => {
    await apiClient.put(`/TransportCompany/${id}`, { ...data, id });
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/TransportCompany/${id}`);
  },
};

