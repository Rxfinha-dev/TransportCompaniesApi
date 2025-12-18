import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from '@/shared/ui';
import './HomePage.css';

export const HomePage: React.FC = () => {
  return (
    <div className="home-page">
      <div className="home-hero">
        <h1>Bem-vindo ao Sistema de Gestão</h1>
        <p className="home-subtitle">
          Gerencie pedidos, clientes, transportadoras e rastreamentos de forma
          eficiente
        </p>
        <div className="home-actions">
          <Link to="/orders">
            <Button size="large">Gerenciar Pedidos</Button>
          </Link>
          <Link to="/costumers">
            <Button size="large" variant="outline">
              Gerenciar Clientes
            </Button>
          </Link>
        </div>
      </div>

      <div className="home-features">
        <div className="feature-card">
          <h3>📦 Pedidos</h3>
          <p>Gerencie todos os pedidos do sistema</p>
          <Link to="/orders">
            <Button variant="outline">Ver Pedidos</Button>
          </Link>
        </div>
        <div className="feature-card">
          <h3>👥 Clientes</h3>
          <p>Cadastre e gerencie seus clientes</p>
          <Link to="/costumers">
            <Button variant="outline">Ver Clientes</Button>
          </Link>
        </div>
        <div className="feature-card">
          <h3>🚚 Transportadoras</h3>
          <p>Configure as transportadoras disponíveis</p>
          <Link to="/transport-companies">
            <Button variant="outline">Ver Transportadoras</Button>
          </Link>
        </div>
      </div>
    </div>
  );
};
