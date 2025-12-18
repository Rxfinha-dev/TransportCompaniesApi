import { apiClient } from '@/shared/api/api-client';
import { Tracking, CreateTrackingEventDto } from './types';

export const trackingApi = {
  getHistory: async (orderId: number): Promise<Tracking[]> => {
    const response = await apiClient.get<Tracking[]>(
      `/orders/${orderId}/tracking`
    );
    return response.data;
  },

  getLatest: async (orderId: number): Promise<Tracking> => {
    const response = await apiClient.get<Tracking>(
      `/orders/${orderId}/tracking/latest`
    );
    return response.data;
  },

  addEvent: async (
    orderId: number,
    data: CreateTrackingEventDto
  ): Promise<void> => {
    await apiClient.post(`/orders/${orderId}/tracking`, data);
  },

  countEvents: async (
    orderId: number,
    filter?: Date
  ): Promise<number> => {
    const params = filter
      ? { Filter: filter.toISOString() }
      : {};
    const response = await apiClient.get<number>(
      `/orders/${orderId}/tracking/quantity`,
      { params }
    );
    return response.data;
  },
};

