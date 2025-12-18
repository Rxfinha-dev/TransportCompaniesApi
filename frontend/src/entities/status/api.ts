import { apiClient } from '@/shared/api/api-client';
import { Status, CreateStatusDto, UpdateStatusDto } from './types';

export const statusApi = {
  getAll: async (): Promise<Status[]> => {
    const response = await apiClient.get<Status[]>('/Status');
    return response.data;
  },

  getById: async (id: number): Promise<Status> => {
    const response = await apiClient.get<Status>(`/Status/${id}`);
    return response.data;
  },

  create: async (data: CreateStatusDto): Promise<void> => {
    await apiClient.post('/Status', data);
  },

  update: async (id: number, data: UpdateStatusDto): Promise<void> => {
    await apiClient.put(`/Status/${id}`, { ...data, id });
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/Status/${id}`);
  },
};

