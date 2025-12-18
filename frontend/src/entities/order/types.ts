import { Costumer } from '../costumer/types';
import { TransportCompany } from '../transport-company/types';
import { Status } from '../status/types';
import { Item } from '../item/types';
import { Address } from '../address/types';

export interface Order {
  id: number;
  orderedItens: Item[];
  statusId: number;
  costumerId: number;
  transportCompanyId: number;
  origin: Address;
  destination: Address;
  isDispatched: boolean;
}

export interface OrderWithRelations extends Order {
  status?: Status;
  costumer?: Costumer;
  transportCompany?: TransportCompany;
}

export interface CreateOrderDto {
  orderedItens: Item[];
  statusId: number;
  costumerId: number;
  transportCompanyId: number;
  origin: Address;
  destination: Address;
}

export interface UpdateOrderDto extends Partial<CreateOrderDto> {
  id: number;
}

