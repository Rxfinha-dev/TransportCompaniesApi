export interface Tracking {
  id: number;
  orderId: number;
  statusId: number;
  message?: string;
  location?: string;
  createdAt: string;
}

export interface CreateTrackingEventDto {
  statusId: number;
  message?: string;
  location?: string;
}

