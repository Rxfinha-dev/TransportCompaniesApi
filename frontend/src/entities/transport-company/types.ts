export interface TransportCompany {
  id: number;
  name: string;
}

export interface CreateTransportCompanyDto {
  name: string;
}

export interface UpdateTransportCompanyDto extends Partial<CreateTransportCompanyDto> {
  id: number;
}

