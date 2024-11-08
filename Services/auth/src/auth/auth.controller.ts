import {
  BadRequestException,
  Body,
  ConflictException,
  Controller,
  HttpCode,
  HttpStatus,
  Post,
  UnauthorizedException,
} from '@nestjs/common';
import { AuthService } from './auth.service';
import { UserService } from '../users/users.service';
import { AuthResponse } from 'src/types/authResponse';

@Controller('auth')
export class AuthController {
  constructor(
    private readonly authService: AuthService,
    private usersService: UserService,
  ) {}

  @Post('login')
  @HttpCode(HttpStatus.OK)
  async login(
    @Body() body: { email: string; password: string },
  ): Promise<AuthResponse> {
    try {
      const user = await this.authService.validateUser(
        body.email,
        body.password,
      );
      if (!user) {
        return new AuthResponse(
          401,
          'Nom d’utilisateur ou mot de passe incorrect',
        );
      }

      const res = await this.authService.signIn(
        user.email,
        user._id,
        user.role,
      );
      return new AuthResponse(200, '', res.access_token);
    } catch (error) {
      if (error instanceof ConflictException) {
        return new AuthResponse(404, error.message);
      }
      if (error instanceof UnauthorizedException) {
        return new AuthResponse(401, error.message);
      }
      return new AuthResponse(500, 'Erreur interne du serveur');
    }
  }

  @Post('register')
  @HttpCode(HttpStatus.OK)
  async register(
    @Body()
    body: {
      email: string;
      password: string;
      password_validate: string;
    },
  ): Promise<AuthResponse> {
    try {
      if (!body.email && !body.password && !body.password_validate) {
        throw new BadRequestException('Champs manquants.');
      }

      const userExist = await this.usersService.getUserByEmail(body.email);

      if (userExist) {
        throw new BadRequestException('Adresse email non disponible.');
      }

      if (body.password !== body.password_validate) {
        throw new BadRequestException('Les mots de passe sont différents.');
      }

      await this.authService.signUp(body.email, body.password);

      return new AuthResponse(200, 'Utilisateur créé avec succès');
    } catch (error) {
      return new AuthResponse(error.status, error.message);
    }
  }

  @Post('verifyAuthorization')
  @HttpCode(HttpStatus.OK)
  async verifyAuthorization(@Body() body: { access_token: string }) {
    try {
      if (!body.access_token) {
        throw new BadRequestException('Champs manquants.');
      }

      const res = await this.authService.verifyUserAuthorization(
        body.access_token,
      );

      if (res == true) {
        return new AuthResponse(200, 'Token verifié avec succès.');
      }
    } catch (error) {
      return new AuthResponse(error.status, error.message);
    }
  }
}
