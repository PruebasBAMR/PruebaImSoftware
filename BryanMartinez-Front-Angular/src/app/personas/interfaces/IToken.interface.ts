


export interface IToken_Request {
  pin:string;
}


export interface IToken_Response {
  ok:boolean;
  results: IToken_Results_Response;
  token: string
}


export interface IToken_Results_Response {
  message:string;
}
