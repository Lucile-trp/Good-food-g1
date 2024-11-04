import { Injectable } from '@nestjs/common';
import { UserService } from '../users/users.service';
import { JwtService } from '@nestjs/jwt';
import { compare } from 'bcrypt';

@Injectable()
export class AuthService {
  constructor(
    private usersService: UserService,
    private jwtService: JwtService,
  ) {}

  // Connection user.
  async signIn(_email: string, _id: string): Promise<{ access_token: string }> {
    const payload = { email: _email, sub: _id };
    return {
      access_token: this.jwtService.sign(payload),
    };
  }

  // Register User
  async signUp(_email, _password) {
    const res = await this.usersService.insertUser(_email, _password);
    return res;
  }

  // User validation pass
  async validateUser(email: string, password: string): Promise<any> {
    const user = await this.usersService.getUserByEmail(email);
    if (user && (await compare(password, user.password))) {
      return user;
    }
    return null;
  }
}
