import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5208/api/';

export const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para tratamento de erros global
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      // Erro com resposta do servidor
      const message = error.response.data?.message || 'Erro ao processar requisição';
      return Promise.reject(new Error(message));
    } else if (error.request) {
      // Requisição foi feita mas não houve resposta
      return Promise.reject(new Error('Erro de conexão com o servidor'));
    } else {
      // Erro ao configurar a requisição
      return Promise.reject(error);
    }
  }
);

export default apiClient;

