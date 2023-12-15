import {
  Body,
  Controller,
  Delete,
  Get,
  Post,
  Put,
  Query,
} from '@nestjs/common';
import { UserService } from './users.service';
import { User } from './users.model';
import { hashPassword } from 'src/helpers/hashPassword';

@Controller('users')
export class UserController {
  constructor(private readonly userService: UserService) {}

  @Get()
  async get(@Query('id') id: string): Promise<User | User[]> {
    try {
      if (id) {
        const res = await this.userService.getUserWithId(id);
        return res;
      }
      const res = await this.userService.getAllUsers();
      return res;
    } catch (error) {
      throw error;
    }
  }

  @Post()
  async insertUser(
    @Body('email') email: string,
    @Body('password') password: string,
  ): Promise<string> {
    try {
      const res = await this.userService.insertUser(email, password);
      return res;
    } catch (error) {
      throw error;
    }
  }

  @Put()
  async updateUser(
    @Query('id') id: string,
    @Body('email') email?: string,
    @Body('password') password?: string,
  ) {
    try {
      const user = await this.userService.getUserWithId(id);

      // Assignation des changements
      email ? (user.email = email) : user.email;
      password ? (user.password = await hashPassword(password)) : user.password;

      // Modification de la donnée
      const res = await this.userService.updateUser(id, user);
      return res;
    } catch (error) {
      throw error;
    }
  }

  @Delete()
  async deleteUserQuery(@Query('id') id: string) {
    try {
      const res = await this.userService.deleteUser(id);
      return res;
    } catch (error) {
      throw error;
    }
  }
}
