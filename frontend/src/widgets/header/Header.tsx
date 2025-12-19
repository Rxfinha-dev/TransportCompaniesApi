import React from 'react';
import { Link } from 'react-router-dom';
import './Header.css';

export const Header: React.FC = () => {
  return (
    <header className="header">
      <div className="header-container">
        <Link to="/" className="header-logo">
          <h1>Gerenciador de transportadoras</h1>
        </Link>
        <nav className="header-nav">
          <Link to="/orders" className="nav-link">
            Pedidos
          </Link>
          <Link to="/costumers" className="nav-link">
            Clientes
          </Link>
          <Link to="/transport-companies" className="nav-link">
            Transportadoras
          </Link>
        </nav>
      </div>
    </header>
  );
};

