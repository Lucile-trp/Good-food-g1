import {
  Body,
  ConflictException,
  Controller,
  HttpCode,
  HttpStatus,
  Post,
  UnauthorizedException,
} from '@nestjs/common';
import { AuthService } from './auth.service';
import { AuthResponse } from 'src/types/authResponse';

@Controller('auth')
export class AuthController {
  constructor(private readonly authService: AuthService) {}

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

      const res = await this.authService.signIn(user.email, user._id);
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
}
