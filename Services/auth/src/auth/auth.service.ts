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

  // Connection user.
  async signIn(
    _email: string,
    _password: string,
  ): Promise<{ access_token: string }> {
    const user = await this.usersService.getUserByEmail(_email);
    const payload = { email: user.email, sub: user._id };
    return {
      access_token: this.jwtService.sign(payload),
    };
  }

  // User creation. Call to userService
  async signUp(_email: string, _password: string) {
    // TODO : Vériication email bine email et password bien password
    // TODO : vérification email non existant
    // TODO : Insertion en BDD du nouvel utilisateur
    // TODO : Envoyer un mail de confirmation de création de compte
    // TODO : retourner un code HTTP avec resutat
    console.log(_email, _password);
  }

  async validateUser(email: string, password: string): Promise<any> {
    const user = await this.usersService.getUserByEmail(email);
    if (user && (await compare(password, user.password))) {
      return user;
    }
    return null;
  }
}
