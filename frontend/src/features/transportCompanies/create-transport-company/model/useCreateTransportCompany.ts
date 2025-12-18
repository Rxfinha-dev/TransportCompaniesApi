import {useMutation, useQueryClient} from "@tanstack/react-query";
import {transportCompanyApi} from "@/entities/transport-company/api";
import {CreateTransportCompanyDto} from "@/entities/transport-company/types";

export const useCreateTransportCompany = () => {
    const queryClient = useQueryClient();

const mutation = useMutation({
        mutationFn: (data: CreateTransportCompanyDto) => transportCompanyApi.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: ['transportCompanies']});
        },
    });

    return {
        createTransportCompany: mutation.mutateAsync,
        isLoading: mutation.isPending,
        error: mutation.error,
    };
};