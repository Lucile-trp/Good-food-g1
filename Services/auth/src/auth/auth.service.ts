import {
  ForbiddenException,
  Injectable,
  UnauthorizedException,
} from '@nestjs/common';
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
    _id: string,
    _role: string,
  ): Promise<{ access_token: string }> {
    const payload = { email: _email, sub: _id, role: _role };
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

  async verifyUserAuthorization(access_token: string) {
    try {
      const decoded = this.jwtService.verify(access_token);

      const user = await this.usersService.getUserWithId(decoded.sub);

      if (!user) {
        throw new UnauthorizedException('Utilisateur non trouvé');
      }

      if (decoded.role !== user.role) {
        throw new ForbiddenException(
          'Le rôle du token ne correspond pas à celui de l’utilisateur',
        );
      }

      return true;
    } catch (error) {
      if (error.name === 'TokenExpiredError') {
        throw new UnauthorizedException('Le token est expiré');
      }
      throw new UnauthorizedException('Token invalide');
    }
  }
}
