export interface Tracking {
  statusId: number;
  message: string;
  location?: string;
  createdAt: string;
}

export interface CreateTrackingEventDto {
  statusId: number;
  message?: string;
  location?: string;
}

