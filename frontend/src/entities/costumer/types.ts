export interface Costumer {
  id: number;
  name: string;
  cpf: string;
}

export interface CreateCostumerDto {
  name: string;
  cpf: string;
}

export interface UpdateCostumerDto extends Partial<CreateCostumerDto> {
  id: number;
}

