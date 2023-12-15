import { Injectable, UnauthorizedException } from '@nestjs/common';
import { UserService } from '../users/users.service';
import { hashPassword } from 'src/helpers/hashPassword';

@Injectable()
export class AuthService {
  constructor(private usersService: UserService) {}

  async signIn(_email: string, _password: string): Promise<string> {
    const user = await this.usersService.getUserByEmail(_email);
    if (user?.password !== (await hashPassword(_password))) {
      throw new UnauthorizedException();
    }
    const { email } = user;
    // TODO: Generate a JWT and return it here
    // instead of the user object
    return email;
  }
}
