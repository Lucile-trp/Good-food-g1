import { Injectable, UnauthorizedException } from '@nestjs/common';
import { UserService } from '../users/users.service';
import { JwtService } from '@nestjs/jwt';
import { compare } from 'bcrypt';

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
    console.log(user);
    const res = await compare(_password, user.password);
    if (res === false) {
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
