import { Injectable, UnauthorizedException } from '@nestjs/common';
import { UserService } from '../users/users.service';
import { hashPassword } from 'src/helpers/hashPassword';
import { JwtService } from '@nestjs/jwt';

@Injectable()
export class AuthService {
  constructor(
    private usersService: UserService,
    private jwtService: JwtService,
  ) {}

  async signIn(
    _email: string,
    _password: string,
  ): Promise<{ access_token: string }> {
    const user = await this.usersService.getUserByEmail(_email);
    if (user?.password !== (await hashPassword(_password))) {
      throw new UnauthorizedException();
    }
    // JWT generation & return
    const payload = { sub: user._id };
    return {
      access_token: await this.jwtService.signAsync(payload),
    };
  }

  async signUp(_email: string, _password: string) {
    console.log(_email, _password);
  }
}
