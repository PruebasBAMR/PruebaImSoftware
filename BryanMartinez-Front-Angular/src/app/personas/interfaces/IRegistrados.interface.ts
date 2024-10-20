


export interface IPersona_Response {
  ok: boolean;
  items: number;
  results: IPersona_Results_Response[]
}

export interface IPersona_Results_Response {
  idpersona: number;
  nombre: string;
  edad: number;
  email: string
}

