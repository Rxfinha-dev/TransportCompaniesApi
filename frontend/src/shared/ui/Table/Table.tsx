import React from 'react';
import './Table.css';

interface TableProps {
  children: React.ReactNode;
  className?: string;
}

interface TableHeaderProps {
  children: React.ReactNode;
}

interface TableRowProps {
  children: React.ReactNode;
  onClick?: () => void;
}

interface TableCellProps {
  children: React.ReactNode;
  header?: boolean;
}

export const Table: React.FC<TableProps> = ({ children, className = '' }) => {
  return (
    <div className="table-wrapper">
      <table className={`table ${className}`}>{children}</table>
    </div>
  );
};

export const TableHeader: React.FC<TableHeaderProps> = ({ children }) => {
  return <thead className="table-header">{children}</thead>;
};

export const TableBody: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return <tbody className="table-body">{children}</tbody>;
};

export const TableRow: React.FC<TableRowProps> = ({
  children,
  onClick,
}) => {
  return (
    <tr
      className={`table-row ${onClick ? 'table-row-clickable' : ''}`}
      onClick={onClick}
    >
      {children}
    </tr>
  );
};

export const TableCell: React.FC<TableCellProps> = ({
  children,
  header = false,
}) => {
  const Component = header ? 'th' : 'td';
  return (
    <Component className={`table-cell ${header ? 'table-cell-header' : ''}`}>
      {children}
    </Component>
  );
};

