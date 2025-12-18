import { useMutation, useQueryClient } from '@tanstack/react-query';
import { transportCompanyApi } from '@/entities/transport-company/api';
import { UpdateTransportCompanyDto } from '@/entities/transport-company/types';

export const useUpdateTransportCompany = () => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: ({ id, data }: { id: number; data: UpdateTransportCompanyDto }) => transportCompanyApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['transportCompanies'] });
    },
  });

  return {
    updateTransportCompany: mutation.mutateAsync,
    isLoading: mutation.isPending,
    error: mutation.error,
  };
};