import { useMutation, useQueryClient } from '@tanstack/react-query';
import { orderApi } from '@/entities/order/api';
import { CreateOrderDto } from '@/entities/order/types';

export const useCreateOrder = () => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: (data: CreateOrderDto) => orderApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['orders'] });
    },
  });

  return {
    createOrder: mutation.mutateAsync,
    isLoading: mutation.isPending,
    error: mutation.error,
  };
};

