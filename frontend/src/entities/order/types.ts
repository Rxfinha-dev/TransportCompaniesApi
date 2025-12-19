export interface Address {
  cep: string;
  number: string;
  rua: string;
  bairro: string;
  cidade: string;
  estado: string;
}

export interface CreateOrderDto {
  id?: number;
  statusId: number;
  costumerId: number;
  transportCompanyId: number;
  orderedItens: any[];
  origin: Address;
  destination: Address;
}
