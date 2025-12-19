export interface Address {
  cep: string;
  number: string;//pode ser string pois é apenas o número da casa, e pode incluir letras
  rua?: string;
  bairro?: string;
  cidade?: string;
  estado?: string;
  complement?: string;
}

