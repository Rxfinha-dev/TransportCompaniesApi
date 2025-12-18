import { useMutation, useQueryClient } from '@tanstack/react-query';
import { costumerApi } from '@/entities/costumer/api';
import { CreateCostumerDto } from '@/entities/costumer/types';

export const useCreateCostumer = () => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: (data: CreateCostumerDto) => costumerApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['costumers'] });
    },
  });

  return {
    createCostumer: mutation.mutateAsync,
    isLoading: mutation.isPending,
    error: mutation.error,
  };
};

