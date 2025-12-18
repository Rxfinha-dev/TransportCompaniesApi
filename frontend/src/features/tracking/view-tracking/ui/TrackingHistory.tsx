import React from 'react';
import { useQuery } from '@tanstack/react-query';
import { trackingApi } from '@/entities/tracking/api';
import { Loading } from '@/shared/ui';
import { formatDate } from '@/shared/lib/utils';
import './TrackingHistory.css';

interface TrackingHistoryProps {
  orderId: number;
}

export const TrackingHistory: React.FC<TrackingHistoryProps> = ({
  orderId,
}) => {
  const { data: history, isLoading, error } = useQuery({
    queryKey: ['tracking', orderId],
    queryFn: () => trackingApi.getHistory(orderId),
  });

  if (isLoading) {
    return <Loading message="Carregando histórico..." />;
  }

  if (error) {
    return <div className="error-message">Erro ao carregar histórico</div>;
  }

  if (!history || history.length === 0) {
    return <div className="empty-state">Nenhum evento de rastreamento encontrado</div>;
  }

  return (
    <div className="tracking-history">
      <h3>Histórico de Rastreamento</h3>
      <div className="tracking-timeline">
        {history.map((event, index) => (
          <div key={index} className="tracking-event">
            <div className="tracking-event-dot"></div>
            <div className="tracking-event-content">
              <div className="tracking-event-header">
                <span className="tracking-event-date">
                  {formatDate(event.createdAt)}
                </span>
                {event.location && (
                  <span className="tracking-event-location">
                    📍 {event.location}
                  </span>
                )}
              </div>
              <p className="tracking-event-message">{event.message}</p>
              <span className="tracking-event-status">
                Status ID: {event.statusId}
              </span>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

