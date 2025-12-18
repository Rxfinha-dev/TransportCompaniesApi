import { apiClient } from '@/shared/api/api-client';
import { Order, CreateOrderDto, UpdateOrderDto } from './types';
import { Address } from '../address/types';

export const orderApi = {
  getAll: async (): Promise<Order[]> => {
    const response = await apiClient.get<Order[]>('/order');
    return response.data;
  },

  getById: async (id: number): Promise<Order> => {
    const response = await apiClient.get<Order>(`/order/${id}`);
    return response.data;
  },

  create: async (data: CreateOrderDto): Promise<void> => {
    await apiClient.post('/order', data);
  },

  update: async (id: number, data: UpdateOrderDto): Promise<void> => {
    await apiClient.put(`/order/${id}`, { ...data, id });
  },

  updateStatus: async (id: number, data: UpdateOrderDto): Promise<void> => {
    await apiClient.patch(`/order/${id}/status`, { ...data, id });
  },

  updateItems: async (id: number, data: UpdateOrderDto): Promise<void> => {
    await apiClient.put(`/order/${id}/items`, { ...data, id });
  },

  updateAddress: async (
    id: number,
    address: { origin?: Address; destination?: Address }
  ): Promise<void> => {
    await apiClient.patch(`/order/${id}/addresses`, address);
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/order/${id}`);
  },
};

