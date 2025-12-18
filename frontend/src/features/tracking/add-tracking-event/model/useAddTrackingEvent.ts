import { useMutation, useQueryClient } from '@tanstack/react-query';
import { trackingApi } from '@/entities/tracking/api';
import { CreateTrackingEventDto } from '@/entities/tracking/types';

export const useAddTrackingEvent = () => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: ({ orderId, data }: { orderId: number; data: CreateTrackingEventDto }) => trackingApi.addEvent(orderId, data),
    onSuccess: (_, { orderId }) => {
      queryClient.invalidateQueries({ queryKey: ['tracking', orderId] });
    },
  });

  return {
    addTrackingEvent: mutation.mutateAsync,
    isLoading: mutation.isPending,
    error: mutation.error,
  };
};