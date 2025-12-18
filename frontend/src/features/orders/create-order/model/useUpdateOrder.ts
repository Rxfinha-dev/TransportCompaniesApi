import { useMutation, useQueryClient } from '@tanstack/react-query';
import { orderApi } from '@/entities/order/api';
import { UpdateOrderDto } from '@/entities/order/types';

export const useUpdateOrder = () => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: ({ id, data }: { id: number; data: UpdateOrderDto }) => orderApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['orders'] });
    },
  });

  return {
    updateOrder: mutation.mutateAsync,
    isLoading: mutation.isPending,
    error: mutation.error,
  };
};