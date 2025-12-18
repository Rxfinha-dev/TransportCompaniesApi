export interface Status {
  id: number;
  description: string;
}

export interface CreateStatusDto {
  description: string;
}

export interface UpdateStatusDto extends Partial<CreateStatusDto> {
  id: number;
}

