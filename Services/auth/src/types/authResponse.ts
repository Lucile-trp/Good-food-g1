export class AuthResponse {
  status: number;
  message?: string;
  access_token?: string;

  constructor(status: number, message: string, access_token?: string) {
    this.status = status;
    this.message = message;
    this.access_token = access_token;
  }
}
