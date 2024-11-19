import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
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
  async getAll(): Promise<User | User[]> {
    const res = await this.userService.getAllUsers();
    return res;
  }

  @Get(':id')
  async getById(@Param() params: string) {
    const res = await this.userService.getUserWithId(params['id']);
    return res;
  }

  @Get('byEmail/:email')
  async getByEmail(@Param() params: string) {
    const res = await this.userService.getUserByEmail(params['email']);
    return res;
  }

  @Post()
  async insertUser(
    @Body('email') email: string,
    @Body('password') password: string,
  ): Promise<string> {
    const res = await this.userService.insertUser(email, password);
    return res;
  }

  @Put()
  async updateUser(
    @Query('id') id: string,
    @Body('email') email?: string,
    @Body('password') password?: string,
  ) {
    const user = await this.userService.getUserWithId(id);

    // Assignation des changements
    email && (user.email = email);
    password && (user.password = await hashPassword(password));

    // Modification de la donnée
    const res = await this.userService.updateUser(id, user);
    return res;
  }

  @Delete()
  async deleteUserQuery(@Query('id') id: string) {
    const res = await this.userService.deleteUser(id);
    return res;
  }
}
