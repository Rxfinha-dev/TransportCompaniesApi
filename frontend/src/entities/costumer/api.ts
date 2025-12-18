import { apiClient } from '@/shared/api/api-client';
import { Costumer, CreateCostumerDto, UpdateCostumerDto } from './types';

export const costumerApi = {
  getAll: async (): Promise<Costumer[]> => {
    const response = await apiClient.get<Costumer[]>('/Costumer');
    return response.data;
  },

  getById: async (id: number): Promise<Costumer> => {
    const response = await apiClient.get<Costumer>(`/Costumer/${id}`);
    return response.data;
  },

  create: async (data: CreateCostumerDto): Promise<void> => {
    await apiClient.post('/Costumer', data);
  },

  update: async (id: number, data: UpdateCostumerDto): Promise<void> => {
    await apiClient.put(`/Costumer/${id}`, data);
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/Costumer/${id}`);
  },
};

