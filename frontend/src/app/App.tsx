import React from 'react';
import { QueryProvider } from './providers';
import { AppRouter } from './router/router';
import './App.css';

export const App: React.FC = () => {
  return (
    <QueryProvider>
      <AppRouter />
    </QueryProvider>
  );
};

