import { useMutation, useQueryClient } from '@tanstack/react-query';
import { costumerApi } from '@/entities/costumer/api';
import { UpdateCostumerDto } from '@/entities/costumer/types';

export const useUpdateCostumer = () => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: ({ id, data }: { id: number; data: UpdateCostumerDto }) => costumerApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['costumers'] });
    },
  });

  return {
    updateCostumer: mutation.mutateAsync,
    isLoading: mutation.isPending,
    error: mutation.error,
  };
};