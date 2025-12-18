import { apiClient } from '@/shared/api/api-client';
import { Status, CreateStatusDto, UpdateStatusDto } from './types';

export const statusApi = {
  getAll: async (): Promise<Status[]> => {
    const response = await apiClient.get<Status[]>('/status');
    return response.data;
  },

  getById: async (id: number): Promise<Status> => {
    const response = await apiClient.get<Status>(`/status/${id}`);
    return response.data;
  },

  create: async (data: CreateStatusDto): Promise<void> => {
    await apiClient.post('/status', data);
  },

  update: async (id: number, data: UpdateStatusDto): Promise<void> => {
    await apiClient.put(`/status/${id}`, { ...data, id });
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/status/${id}`);
  },
};

