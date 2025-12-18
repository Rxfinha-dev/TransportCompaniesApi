import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { HomePage } from '@/pages/home';
import { OrdersPage } from '@/pages/orders';
import { CostumersPage } from '@/pages/costumers';
import { Header } from '@/widgets/header';
import { TransportCompaniesPage } from '@/pages/transportCompanies';

export const AppRouter: React.FC = () => {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/orders" element={<OrdersPage />} />
        <Route path="/costumers" element={<CostumersPage />} />
        <Route path='/transport-companies' element={<TransportCompaniesPage />} />
        {/* Adicione mais rotas conforme necessário */}
      </Routes>
    </BrowserRouter>
  );
};

