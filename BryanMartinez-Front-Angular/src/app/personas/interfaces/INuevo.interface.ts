export interface INuevo_Request {
  nombre: string;
  edad: number;
  email: string
}


export interface INuevo_Response {
  ok: boolean;
  results: INuevo_Results_Response;

}

export interface INuevo_Results_Response {
  message: string;
}
