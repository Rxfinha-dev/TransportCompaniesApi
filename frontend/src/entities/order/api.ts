import { apiClient } from '@/shared/api/api-client';
import { CreateOrderDto } from './types';

export const orderApi = {
  create: async (data: CreateOrderDto) => {
    const response = await apiClient.post('/order', data);
    return response.data;
  },
};
